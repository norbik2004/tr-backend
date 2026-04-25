using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using tr_core.Services;

namespace tr_service.LinkedIn
{
    public class LinkedInService : ILinkedInService
    {
        private readonly HttpClient _client;
        private readonly LinkedInConfig _config;

        public LinkedInService(HttpClient httpClient, LinkedInConfig config)
        {
            _client = httpClient;
            _config = config;
        }

        public async Task<string> ExchangeCodeForAccessToken(string code, string redirectUri)
        {
            var values = new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = redirectUri,
                ["client_id"] = _config.ClientId,
                ["client_secret"] = _config.ClientSecret
            };

            var resp = await _client.PostAsync("https://www.linkedin.com/oauth/v2/accessToken", new FormUrlEncodedContent(values));
            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                throw new tr_service.Exceptions.BadRequestException($"LinkedIn token exchange failed: {resp.StatusCode} - {json}");
            }

            try
            {
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("access_token").GetString()!;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse access token from LinkedIn response.", ex);
            }
        }

        public async Task<string> GetPersonId(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _client.DefaultRequestHeaders.Remove("X-Restli-Protocol-Version");
            _client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");
            var resp = await _client.GetAsync("https://api.linkedin.com/v2/userinfo");
            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                throw new tr_service.Exceptions.BadRequestException($"LinkedIn /userinfo failed: {resp.StatusCode} - {json}");
            }

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.String)
                    return idProp.GetString()!;

                if (root.TryGetProperty("sub", out var subProp) && subProp.ValueKind == JsonValueKind.String)
                    return subProp.GetString()!;

                throw new tr_service.Exceptions.BadRequestException($"Unable to locate person id in LinkedIn response: {json}");
            }
            catch (tr_service.Exceptions.BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse person id from LinkedIn response.", ex);
            }
        }

        public async Task PostTextAsync(string accessToken, string authorUrn, string text)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _client.DefaultRequestHeaders.Remove("X-Restli-Protocol-Version");
            _client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");

            var body = new
            {
                author = authorUrn,
                lifecycleState = "PUBLISHED",
                specificContent = new
                {
                    com_linkedin_ugc_ShareContent = new
                    {
                        shareCommentary = new { text },
                        shareMediaCategory = "NONE"
                    }
                },
                visibility = new { com_linkedin_ugc_MemberNetworkVisibility = "PUBLIC" }
            };

            var raw = JsonSerializer.Serialize(body);

            raw = raw.Replace("com_linkedin_ugc_ShareContent", "com.linkedin.ugc.ShareContent")
                     .Replace("com_linkedin_ugc_MemberNetworkVisibility", "com.linkedin.ugc.MemberNetworkVisibility");

            var resp = await _client.PostAsync("https://api.linkedin.com/v2/ugcPosts", new StringContent(raw, Encoding.UTF8, "application/json"));
            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                throw new tr_service.Exceptions.BadRequestException($"LinkedIn post failed: {resp.StatusCode} - {json}");
            }
        }
    }
}
