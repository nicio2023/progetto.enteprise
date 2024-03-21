using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Paradigmi.Progetto.Web.Results;

namespace Paradigmi.Progetto.Web.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = (context) =>
                    {
                        return new BadRequestResultFactory(context);
                    };
                });
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Unicam Paradigmi Test App",
                    Version = "v1"
                });
                }
            );
                services.AddEndpointsApiExplorer();
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
