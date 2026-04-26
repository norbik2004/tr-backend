using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using tr_core.DTO.LinkedIn;
using tr_core.Services;
using tr_service.Exceptions;

namespace tr_service.LinkedIn
{
    public class LinkedInService(HttpClient _client, LinkedInConfig _lconfig, IConfiguration _config) : ILinkedInService
    {

        public async Task<string> ExchangeCodeForAccessToken(string code, string redirectUri)
        {
            var values = new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = redirectUri,
                ["client_id"] = _lconfig.ClientId,
                ["client_secret"] = _lconfig.ClientSecret
            };

            var resp = await _client.PostAsync($"{_config["LinkedIn:BaseUrl"]}accessToken", new FormUrlEncodedContent(values));
            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                throw new BadRequestException($"LinkedIn token exchange failed: {resp.StatusCode} - {json}");
            }

            try
            {
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("access_token").GetString()!;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Failed to parse access token from LinkedIn response.", ex);
            }
        }

        public async Task<string> GetPersonId(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _client.DefaultRequestHeaders.Remove("X-Restli-Protocol-Version");
            _client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");
            var resp = await _client.GetAsync($"{_config["LinkedIn:ApiUrl"]}userinfo");
            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                throw new BadRequestException($"LinkedIn /userinfo failed: {resp.StatusCode} - {json}");
            }

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.String)
                    return idProp.GetString()!;

                if (root.TryGetProperty("sub", out var subProp) && subProp.ValueKind == JsonValueKind.String)
                    return subProp.GetString()!;

                throw new BadRequestException($"Unable to locate person id in LinkedIn response: {json}");
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse person id from LinkedIn response.", ex);
            }
        }

        public async Task PostTextAsync(LinkedInPostDTO linkedInPostDTO)
        {
            var accessToken = linkedInPostDTO.UserPlatform.AccessToken;
            var authorUrn = $"urn:li:person:{linkedInPostDTO.UserPlatform.ExternalAccountId}";
            var text = linkedInPostDTO.Request.Text;

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

            var resp = await _client.PostAsync($"{_config["LinkedIn:ApiUrl"]}ugcPosts", new StringContent(raw, Encoding.UTF8, "application/json"));
            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                throw new BadRequestException($"LinkedIn post failed: {resp.StatusCode} - {json}");
            }
        }
    }
}
