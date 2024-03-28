namespace Paradigmi.Progetto.Application.Models.Responses
{
    public class CreateTokenResponse
    {
        public string Token { get; set; }
        public CreateTokenResponse(string token)
        {
            Token = token;
        }
    }
}
