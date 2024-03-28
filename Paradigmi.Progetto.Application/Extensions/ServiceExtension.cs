using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Services;
using Paradigmi.Progetto.Models.Context;

namespace Paradigmi.Progetto.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(
            AppDomain.CurrentDomain.GetAssemblies().
            SingleOrDefault(assembly => assembly.GetName().Name == "Paradigmi.Progetto.Application"));
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ILibroService, LibroService>();
            services.AddScoped<IUtenteService, UtenteService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
