using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Factories;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Application.Responses;
using Paradigmi.Progetto.Application.Services;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Web.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;
        private readonly MyDbContext _ctx;
        public LibroController(ILibroService libroService, MyDbContext ctx)
        {
            _libroService = libroService;
            _ctx = ctx;
        }
        [HttpPost]
        public async Task<IActionResult> CreateLibro(CreateLibroRequest request)
        {
            var categorie = await _ctx.Categorie.Where(c => request.Categorie.Contains(c.Nome)).ToListAsync();
            List<CategoriaLibro> categoriaLibri = categorie.Select(c => new CategoriaLibro { Categoria = c }).ToList();
            var libro = request.ToEntity(categoriaLibri);
            await _libroService.AddLibroAsync(libro);
            var response = new CreateLibroResponse();
            response.Libro = new Application.Dtos.LibroDto(libro);
            return Ok(ResponseFactory
            .WithSuccess(response)
            );
        }
        [HttpPut]
        public async Task<IActionResult> ModifyLibro(ModifyLibroRequest request)
        {
            var libro = await _libroService.GetLibroAsync(request.Nome, request.Autore);
            await _libroService.ModifyLibro(libro, request);
            var response = new ModifyLibroResponse();
            response.Libro = new Application.Dtos.LibroDto(libro);
            return Ok(ResponseFactory
                .WithSuccess(response)
                );

        }
        /*[HttpGet]
        public async Task<IActionResult> GetLibroCategorie(string name, string autore)
        {
            var libro = await _libroService.GetLibroAsync(name, autore);
            var response = new GetLibroResponse();
            response.Libro = new Application.Dtos.LibroDto(libro);
            return Ok(ResponseFactory
                .WithSuccess(response)
                );
        }*/

    }        
    }
