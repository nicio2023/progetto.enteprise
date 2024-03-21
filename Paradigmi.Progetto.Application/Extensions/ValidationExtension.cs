using FluentValidation;
using System.Text.RegularExpressions;

namespace Paradigmi.Progetto.Application.Extensions
{ 
 public static class ValidationExtensions
{
    //il primo argomento genrico dice la classe che stai validando (CreateTokenRequest), la seconda dice il tipo di proprietà
    //che stai valutando (password, che è una stringa)
    public static void RegEx<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string regex, string validationMessage)
    {
        ruleBuilder.Custom((value, context) =>
        {
            var regEx = new Regex(regex);
            if (regEx.IsMatch(value.ToString()) == false)
            {
                context.AddFailure(validationMessage);
            }
        });
    }
}
}
