using System.Threading.Tasks;
using tr_core.DTO.LinkedIn;
using tr_core.DTO.LinkedIn.Request;

namespace tr_core.Services
{
    public interface ILinkedInService
    {
        Task<string> ExchangeCodeForAccessToken(string code, string redirectUri);
        Task<string> GetPersonId(string accessToken);
        Task PostTextAsync(LinkedInPostRequest request);
    }
}
