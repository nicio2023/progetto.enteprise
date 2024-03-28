using Microsoft.Extensions.Options;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.Options;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(CreateTokenRequest request);
    }
}
