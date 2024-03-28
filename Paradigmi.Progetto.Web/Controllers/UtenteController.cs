using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Factories;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.Models.Responses;
using Paradigmi.Progetto.Application.Services;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UtenteController : ControllerBase
    {
        private readonly IUtenteService _utenteService;
        public UtenteController(IUtenteService utenteService)
        {
            _utenteService = utenteService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUtente (CreateUtenteRequest request)
        {
            var utente = request.ToEntity();
            await _utenteService.AddUtenteAsync(utente);
            var response = new CreateUtenteResponse();
            response.Utente = new Application.Models.Dtos.UtenteDto(utente);
            return Ok(ResponseFactory
                .WithSuccess(response));
        
        }
    }
}
