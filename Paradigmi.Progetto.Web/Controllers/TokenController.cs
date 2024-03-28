using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Factories;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.Models.Responses;
using Paradigmi.Progetto.Application.Services;

namespace Paradigmi.Progetto.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController (ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateToken(CreateTokenRequest request)
        {
            var token = await _tokenService.CreateToken(request);
            return Ok(
                ResponseFactory.WithSuccess(
                    new CreateTokenResponse(token)
                    )
                );
        }
    }
}
