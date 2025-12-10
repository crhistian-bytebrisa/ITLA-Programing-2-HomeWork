using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using Microsoft.AspNetCore.Identity;

namespace MediAgenda.Application.Interfaces
{
    public interface IAuthService
    {
        Task<JWTResponse> Login(LoginDTO dto);
        Task<JWTResponse> Register(RegisterDTO dto);
    }
}