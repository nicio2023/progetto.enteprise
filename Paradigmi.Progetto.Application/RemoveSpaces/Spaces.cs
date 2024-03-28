using System.Text.RegularExpressions;

namespace Paradigmi.Progetto.Application.RemoveSpaces
{
    public static class Spaces
    {
        public static string? RemoveExtraSpaces(string? input)
        {
            if (input != null)
            {
                return Regex.Replace(input, @"\s+", " ");
            }
            else
            {
                return null;
            }
        }
    }
}
