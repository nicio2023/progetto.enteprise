
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddModelServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyDbContext>(conf =>
            {
                conf.UseSqlServer(configuration.GetConnectionString("MyDbContext"));
            });

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddScoped<IUtenteRepository, UtenteRepository>();
            return services;
        }
    }
}

