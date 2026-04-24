using System.Threading.Tasks;

namespace tr_core.Services
{
    public interface ILinkedInService
    {
        Task<string> ExchangeCodeForAccessToken(string code, string redirectUri);
        Task<string> GetPersonId(string accessToken);
        Task PostTextAsync(string accessToken, string authorUrn, string text);
    }
}
