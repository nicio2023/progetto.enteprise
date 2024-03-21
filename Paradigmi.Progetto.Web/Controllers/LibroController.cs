using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Factories;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Application.Responses;
using Paradigmi.Progetto.Application.Services;

namespace Paradigmi.Progetto.Web.Controllers
{

        [ApiController]
        [Route("api/v1/[controller]")]
        public class LibroController : ControllerBase
        {
            private readonly ILibroService _libroService;
            public LibroController (ILibroService libroService)
            {
                _libroService = libroService;
            }
            [HttpPost]
            public async Task<IActionResult> CreateLibro(CreateLibroRequest request)
            {
                var libro = request.ToEntity();
                await _libroService.AddLibroAsync(libro);
                var response = new CreateLibroResponse();
                response.Libro = new Application.Dtos.LibroDto(libro);
                return Ok(ResponseFactory
                .WithSuccess(response)
                );
            }
        }
    }

