using MsAuth_Tripster.Models.Custom;

namespace MsAuth_Tripster.Services
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> DevolverToken(AuthorizationRequest authorization)
    }
}
