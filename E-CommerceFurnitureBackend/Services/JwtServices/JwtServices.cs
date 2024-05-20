using E_CommerceFurnitureBackend.DbCo;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace E_CommerceFurnitureBackend.Services.JwtServices
{
    public class JwtServices: IJwtServices
    {
        private readonly string secreteKey;
        public JwtServices(IConfiguration configuration)
        {
            this.secreteKey = configuration["JwtConfig:Key"];
        }
        public async Task<int> GetUserIdFromToken(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreteKey)),
            };
            try
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var response = tokenhandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                if (validatedToken is not JwtSecurityToken jwtToken)
                    throw new SecurityTokenException("Invalid Jwt Token");
                var SecurityToken = tokenhandler.ReadJwtToken(token);
                var claim = SecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                if (claim is null)
                    throw new SecurityTokenException("Id not Found");
                return Convert.ToInt32(claim);
            }catch (Exception ex)
            {
                throw new Exception("Something Went wrong");
            }
        }
    }
}
