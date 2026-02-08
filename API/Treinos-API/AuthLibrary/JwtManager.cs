using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AuthLibrary
{
    public class JwtManager
    {
        private readonly string scretKey;
        private readonly byte[] secretBytes;
        private readonly SymmetricSecurityKey signInKey;

        public JwtManager(string secretKey)
        {
            this.scretKey = secretKey;
            secretBytes = Encoding.UTF8.GetBytes(secretKey);
            this.signInKey = new SymmetricSecurityKey(secretBytes);
        }

        public string GenerateToken(string username, int expireMinutes = 1)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var credentials = new SigningCredentials(this.signInKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "SistemaTreinoAPI",
                audience: "AngularAppFitness",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials
        );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
