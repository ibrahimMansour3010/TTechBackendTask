using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.Services.Abstraction;
using TTechTask.Services.Settings;

namespace TTechTask.Services.Servives
{
    public class MainServices: IMainServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWT _jwt;

        public MainServices( UserManager<IdentityUser> userManager,
            IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<string> GenerateToken(IdentityUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userid", user.Id)
            }.Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(14),
                    signingCredentials: signInCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public string UploadPhoto(IFormFile file)
        {
            try

            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory()+ "\\wwwroot\\Images\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\");
                    }
                    using FileStream fileStream = File.Create(Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\" + file.FileName);
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return "\\Images\\" + file.FileName;

                }
                else
                {
                    return "Failure";
                }
            }
            catch (Exception ex)

            {
                return ex.Message.ToString();
            }
        }
        public string DeletePhoto(string Path)
        {
            try
            {
                File.Delete(Directory.GetCurrentDirectory() + Path);
                return "Deleted Successfully";

            }
            catch (Exception ex)

            {
                return ex.Message.ToString();
            }
        }
    }
}
