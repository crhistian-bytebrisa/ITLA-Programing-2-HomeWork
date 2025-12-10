using Azure;
using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationUserRepository _userRepo;
        private readonly IPatientRepository _patientRepo;

        public AuthService(IConfiguration configuration,  IApplicationUserRepository user, IPatientRepository patient)
        {
            _configuration = configuration;
            _userRepo = user;
            _patientRepo = patient;
        }

        public async Task<JWTResponse> Login(LoginDTO dto)
        {
            var user = await _userRepo.GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            var isValidPassword = await _userRepo.CheckPasswordAsync(user, dto.Password);

            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }


            var roles = await _userRepo.GetRolesByUserIdAsync(user.Id);


            var token = await GenerateToken(user, roles);

            var expirationMinutes = Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"]);
            var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

            var response = new JWTResponse
            {
                Token = token,
                User = user.Adapt<ApplicationUserDTO>(),
                ExpirationToken = expiresAt
            };

            return response;
        }

        public async Task<JWTResponse> Register(RegisterDTO dto)
        {
            var hasher = new PasswordHasher<ApplicationUserModel>();
            var UserCreateModel = new ApplicationUserModel
            {
                Email = dto.Email,
                PasswordHash = hasher.HashPassword(new ApplicationUserModel(), dto.Password),
                NameComplete = dto.NameComplete,
                EmailConfirmed = true,
                PhoneNumber = dto.PhoneNumber
            };

            var user = await _userRepo.AddAsync(UserCreateModel);

            var PatientCreateModel = new PatientModel
            {
                UserId = user.Id,
                InsuranceId = dto.InsuranceId,
                Identification = dto.Identification,
                DateOfBirth = dto.DateOfBirth,
                Bloodtype = dto.BloodTypeDTO.Adapt<Bloodtype>(),
                Gender = dto.GenderDTO.Adapt<Gender>()
            };

            await _patientRepo.AddAsync(PatientCreateModel);

            var roles = await _userRepo.GetRolesByUserIdAsync(user.Id);


            var token = await GenerateToken(user, roles);

            var expirationMinutes = Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"]);
            var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

            var response = new JWTResponse
            {
                Token = token,
                User = user.Adapt<ApplicationUserDTO>(),
                ExpirationToken = expiresAt
            };

            return response;
        }

        private async Task<string> GenerateToken(ApplicationUserModel user, List<IdentityRole> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.NameComplete),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (user.Patient != null)
            {
                claims.Add(new Claim("PatientId", user.Patient.Id.ToString()));
            }

            if (user.Doctor != null)
            {
                claims.Add(new Claim("DoctorId", user.Doctor.Id.ToString()));
            }


            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Name)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task SetCookie(string token, DateTime expiration)
        {
           
        }
    }
}
