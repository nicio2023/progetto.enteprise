using System.Text.RegularExpressions;

namespace Paradigmi.Progetto.Application.RemoveSpaces
{
    public static class Spaces
    {
        public static string? RemoveExtraSpaces(string? input)
        {
            if (input != null)
            {
                return Regex.Replace(input.Trim(), @"\s{2,}", " " );
            }
            else
            {
                return null;
            }
        }
    }
}
