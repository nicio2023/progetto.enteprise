using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Dtos;
using Paradigmi.Progetto.Application.Factories;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.Models.Responses;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Application.Responses;
using Paradigmi.Progetto.Application.Services;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Web.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;
        private readonly ICategoriaService _categoriaService;
        public LibroController(ILibroService libroService, ICategoriaService categoriaService)
        {
            _libroService = libroService;
            _categoriaService = categoriaService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateLibro(CreateLibroRequest request)
        {
            request.Nome = Spaces.RemoveExtraSpaces(request.Nome);
            request.Autore = Spaces.RemoveExtraSpaces(request.Autore);
            request.Editore = Spaces.RemoveExtraSpaces(request.Editore);
            request.Categorie = request.Categorie.Select(x => Spaces.RemoveExtraSpaces(x)).ToList();
            var categorie = await _categoriaService.GetCategorieByNomiAsync(request.Categorie);
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
        [Route("modify")]
        public async Task<IActionResult> ModifyLibro(ModifyLibroRequest request)
        {
            request.Nome = Spaces.RemoveExtraSpaces(request.Nome);
            request.Autore = Spaces.RemoveExtraSpaces(request.Autore);
            request.NomeModificato = Spaces.RemoveExtraSpaces(request.NomeModificato);
            request.AutoreModificato = Spaces.RemoveExtraSpaces(request.AutoreModificato);
            request.EditoreModificato = Spaces.RemoveExtraSpaces(request.EditoreModificato);
            request.CategorieModificate = request.CategorieModificate.Select(x => Spaces.RemoveExtraSpaces(x)).ToList();
            var libro = await _libroService.GetLibroAsync(request.Nome, request.Autore);
            var total = await _libroService.GetNumeroLibri(request.NomeModificato?.ToLower(), request.AutoreModificato?.ToLower());
            if (total <=1)
            {
                await _libroService.ModifyLibroAsync(libro, request);
                var response = new ModifyLibroResponse();
                response.Libro = new Application.Dtos.LibroDto(libro);
                return Ok(ResponseFactory
                    .WithSuccess(response)
                    );
            }
            else
            {
                return BadRequest(ResponseFactory.WithError(new Exception("libro con nome <" + Spaces.RemoveExtraSpaces(request.NomeModificato) + "> " +
                    "e autore <" + Spaces.RemoveExtraSpaces(request.AutoreModificato) + "> già esistente")));
            }

        }

  
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteLibro(DeleteLibroRequest request)
        {
            var libro = await _libroService.GetLibroAsync(Spaces.RemoveExtraSpaces(request.Nome), Spaces.RemoveExtraSpaces(request.Autore));
            var response = new DeleteLibroResponse();
            response.Libro = new Application.Dtos.LibroDto(libro);
            await _libroService.DeleteLibroAsync(libro);
            return Ok(ResponseFactory
                .WithSuccess("libro <" + response.Libro.Nome + "> con autore <" + response.Libro.Autore + "> eliminato correttamente"));
        }
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> GetLibri(GetLibriRequest request)
        {
            var nome = Spaces.RemoveExtraSpaces(request.Nome);
            var autore = Spaces.RemoveExtraSpaces(request.Autore);
            var categoria = Spaces.RemoveExtraSpaces(request.Categoria);
            int totalNum = 0;
            var libri = _libroService.GetLibri(request.PageNumber * request.PageSize, request.PageSize, nome, autore, request.DataPubblicazione, categoria, out totalNum);
            if (totalNum == 0)
            {
                return BadRequest(ResponseFactory.WithError(new Exception("non esiste nessun libro con tali caratteristiche")));
            }
            var response = new GetLibriResponse();
            var pageFounded = (totalNum / (decimal)request.PageSize);
            response.NumeroPagine = (int)Math.Ceiling(pageFounded);
            response.Libri = libri.Select(s =>
            new LibroDto(s)).ToList();

            return Ok(ResponseFactory
              .WithSuccess(response)
              );
        }
    }        
 }
