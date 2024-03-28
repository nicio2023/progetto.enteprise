using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.Options;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Paradigmi.Progetto.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthenticationOption _jwtAuthOption;
        private readonly IUtenteRepository _utenteRepository;
        public TokenService(IOptions<JwtAuthenticationOption> jwtAuthOption, IUtenteRepository utenteRepository)
        {
            _jwtAuthOption = jwtAuthOption.Value;
            _utenteRepository = utenteRepository;
        }

        public async Task<string> CreateToken(CreateTokenRequest request)
        {
           var utente = await _utenteRepository.GetUtenteByNomeCognomePasswordAsync(Spaces.RemoveExtraSpaces(request.Nome.ToLower()), Spaces.RemoveExtraSpaces(request.Cognome.ToLower()), request.Password);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("id_utente", utente.IdUtente.ToString() ));
            claims.Add(new Claim("Nome", utente.Nome));
            claims.Add(new Claim("Cognome", utente.Cognome));

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtAuthOption.Key)
                );
            var credentials = new SigningCredentials(securityKey
                , SecurityAlgorithms.HmacSha256);
            //il token sarà valido solo per la nostra applicazione quindi l'audience non serve e lo lasciamo a null
            var securityToken = new JwtSecurityToken(_jwtAuthOption.Issuer
                , null
                , claims
                , expires: DateTime.Now.AddMinutes(30)
                //il token deve essere firmato, le credenziali di firma le possiamo fare o a chiave simmetrica e quindi
                //condivisa (lato mio per criptarla e lato client per decodificarla) o asimmetrica. Per facilità 
                //andiamo a generare una chiave asimmetrica (variabile securityKey)
                , signingCredentials: credentials
                );
            //prendiamo il nostro token di sicurezza e tramite le credenziali di firma ci risponderà il nostro
            //token vero e proprio 
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            //STEP 3 : Restituisco il token
            return token;
        }
    }
}
