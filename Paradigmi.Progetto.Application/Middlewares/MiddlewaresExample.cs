using Microsoft.Extensions.Options;
using Paradigmi.Progetto.Application.Abstractions.Services;

namespace Paradigmi.Progetto.Application.Middlewares
{
    public class MiddlewaresExample 
    {
        private RequestDelegate _next;
        public MiddlewaresExample(RequestDelegate next
            )
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context
            , ICategoriaService categoriaService
            //IConfiguration è trattao come un servizio di default, in qualsiasi classe abbiamo l'opportunità
            //di iniettarci dentro l'ooggett IConfiguration, dal quale poi abbiamo l'opportunità di andare a
            //prendere gli elementi dall'appsettings. In questa maniera abbiamo l'opportunità di andare a leggere
            //le configurazioni che andiamo a mettere all'interno dell'appsettings
            , IConfiguration configuration
            , ILibroService libroService
            , IUtenteService utenteService
            )
        {

            context.RequestServices.GetRequiredService<ICategoriaService>();
            context.RequestServices.GetRequiredService<IUtenteService>();
            context.RequestServices.GetRequiredService<ILibroService>();
            //Implementiamo il codice effettivo del nostro middleware

            //Per andare al middleware successivo dobbiamo chiamare _next.Invoke();
            //context.Response.WriteAsync("Blocco la chiamata");
            await _next.Invoke(context);
        }
    }
}
