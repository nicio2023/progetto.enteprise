using System.Text.Json.Serialization;

namespace Paradigmi.Progetto.Application.Models.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; } = null;
        //quest attributo ci sta dicendo che quando l'atttrbituo è nullo lo ignoriamo 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //risultato effettivo delle nostre risposte
        public T? Result { get; set; } = default;


    }
}
