using IdentityService.API.Core.Authentication;
using IdentityService.API.Data.Common;
using IdentityService.API.Data.Entities;
using IdentityService.API.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IdentityDbContext _context;
        private readonly IAuthenticationHandler _authenticationHandler;

        public IdentityService(IdentityDbContext context, IAuthenticationHandler authenticationHandler)
        {
            _context = context;
            _authenticationHandler = authenticationHandler;
        }

        public async Task<(User User, string token)> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) ErrorManagement.HandleError("Please fill in the required fields.");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) ErrorManagement.HandleError("User is not found.");
            if (!CryptographyHandler.VerifyHashed(password, user.PasswordHash, user.PasswordSalt)) ErrorManagement.HandleError("Password is wrong.");

            string token = _authenticationHandler.CreateToken(user);

            return (user, token);
        }

        public async Task<User> RegisterAsync(string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(email)) ErrorManagement.HandleError("Email field is empty.");
            if (string.IsNullOrEmpty(password)) ErrorManagement.HandleError("Password field is empty.");
            if (password != confirmPassword) ErrorManagement.HandleError("Both the password and confirm password fields value must be matched.");
            if (await _context.Users.AnyAsync(x => x.Email == email)) ErrorManagement.HandleError("Email already exists.");

            var (passwordHash, passwordSalt) = CryptographyHandler.HashText(password);

            return await _context.SaveAsync(new User
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
        }
    }
}
