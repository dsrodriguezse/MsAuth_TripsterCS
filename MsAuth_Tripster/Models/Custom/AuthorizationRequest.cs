using Microsoft.AspNetCore.Authentication;

namespace MsAuth_Tripster.Models.Custom
{
    public class AuthorizationRequest
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
    }
}
