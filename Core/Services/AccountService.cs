using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kaizen.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AccountService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> SignUpAsync(ApplicationUser user, string userPassword)
        {
            return await unitOfWork.AccountRepository.SignUpAsync(user, userPassword);
        }

        public async Task<ResponseAuthenticationDto> BuildToken(ApplicationUser user, bool IsNew = false)
        {
            if (IsNew)
            {
                var claims = new List<Claim>()
                {
                    new Claim("email",user.Email),
                    new Claim("role","agent")
                };

                await unitOfWork.AccountRepository.AddClaimsAsync(user, claims);
            }

            var claimsDB = await unitOfWork.AccountRepository.GetClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claimsDB
            , expires: expiration, signingCredentials: credentials);

            return new ResponseAuthenticationDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await unitOfWork.AccountRepository.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public async Task<bool> AddRoleAsync(ApplicationUser user, string roleName)
        {
            return await unitOfWork.AccountRepository.AddRoleAsync(user, roleName);
        }

        public async Task<bool> RemoveRoleAsync(ApplicationUser user, string roleName)
        {
            return await unitOfWork.AccountRepository.RemoveRoleAsync(user, roleName);
        }
    }
}