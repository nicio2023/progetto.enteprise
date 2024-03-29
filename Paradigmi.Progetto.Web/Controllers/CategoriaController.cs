using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Factories;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Application.Responses;

namespace Paradigmi.Progetto.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService) {
            
            _categoriaService=categoriaService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategoria(CreateCategoriaRequest request)
        {
            request.Nome = Spaces.RemoveExtraSpaces(request.Nome);
            var categoria = request.ToEntity();
            await _categoriaService.AddCategoriaAsync(categoria);
            var response = new CreateCategoriaResponse();
            response.Categoria = new Application.Dtos.CategoriaDto(categoria);
            return Ok(ResponseFactory
                .WithSuccess(response)
                );
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteCategoria(DeleteCategoriaRequest request)
        {
            var categoria = await _categoriaService.GetCategoriaByNomeAsync(Spaces.RemoveExtraSpaces(request.Nome));
            var valid = await _categoriaService.IsCategoriaVuotaAsync(categoria.Nome);
            if (valid)
            {
                await _categoriaService.DeleteCategoriaAsync(categoria);
                var response = new DeleteCategoriaResponse();
                response.Categoria = new Application.Dtos.CategoriaDto(categoria);
                return Ok(ResponseFactory
                    .WithSuccess("la categoria <"+response.Categoria.Nome+"> è stata eliminata con successo."));
            }
            else
            {
                return BadRequest(ResponseFactory.WithError(new Exception("la categoria ha associato dei libri, " +
                "perciò non può essere eliminata")));
              };
        }
    }
}
