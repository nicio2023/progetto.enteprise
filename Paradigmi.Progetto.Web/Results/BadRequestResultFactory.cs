using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Responses;

namespace Paradigmi.Progetto.Web.Results
{
    public class BadRequestResultFactory : BadRequestObjectResult
    {
        public BadRequestResultFactory(ActionContext context) : base(new BadResponse())
        {
            var retErrors = new List<string>();
            foreach (var key in context.ModelState)
            {
                //se c'è almeno un errore per quella determinata chiave aggiungo il messaggio di errore alla lista 
                var errors = key.Value.Errors;
                for (var i = 0; i < errors.Count(); i++)
                {
                    retErrors.Add(errors[0].ErrorMessage);
                }
            }

            var response = (BadResponse)Value;
            response.Errors = retErrors;
        }
    }
}

