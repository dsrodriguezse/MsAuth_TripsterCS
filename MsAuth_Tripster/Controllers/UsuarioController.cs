using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MsAuth_Tripster.Models.Custom;
using MsAuth_Tripster.Services;

namespace MsAuth_Tripster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public UsuarioController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody]AuthorizationRequest authorization)
        {
            var resultado_autorizacion = await _authorizationService.DevolverToken(authorization);
            if(resultado_autorizacion == null)
            {
                return Unauthorized();
            }
            return Ok(resultado_autorizacion);
        }
    }
}
