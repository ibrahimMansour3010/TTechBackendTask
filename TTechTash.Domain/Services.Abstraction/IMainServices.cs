using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTechTash.Domain.Services.Abstraction
{
    public interface IMainServices
    {
        Task<string> GenerateToken(IdentityUser user);
        string UploadPhoto(IFormFile file);
        string DeletePhoto(string Path);
    }
}
