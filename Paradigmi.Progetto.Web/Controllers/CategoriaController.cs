using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Factories;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Application.Responses;

namespace Paradigmi.Progetto.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService) {
            
            _categoriaService=categoriaService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoria(CreateCategoriaRequest request)
        {
            var categoria = request.ToEntity();
            await _categoriaService.AddCategoriaAsync(categoria);
            var response = new CreateCategoriaResponse();
            response.Categoria = new Application.Dtos.CategoriaDto(categoria);
            return Ok(ResponseFactory
                .WithSuccess(response)
                );
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoria(DeleteCategoriaRequest request)
        {
            var categoria=request.ToEntity();
            await _categoriaService.DeleteCategoriaAsync(categoria);
            var response = new DeleteCategoriaResponse();
            response.Categoria = new Application.Dtos.CategoriaDto(categoria);
            return Ok(ResponseFactory
                .WithSuccess(response));
        }
    }
}
