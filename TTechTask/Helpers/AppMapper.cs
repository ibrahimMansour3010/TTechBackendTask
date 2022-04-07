using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.DTOs;
using TTechTash.Domain.Models;
using TTechTask.Domain.DTOs;

namespace TTechTask.Helpers
{
    public class AppMapper:Profile
    {
        public AppMapper()
        {
            CreateMap<Product, ProductDTO.Add>().ReverseMap();
            CreateMap<Product, ProductDTO.Edit>().ReverseMap();
            CreateMap<Product, ProductDTO.Get>().ReverseMap();

            CreateMap<IdentityUser, UserDTO.Register>().ReverseMap();
            CreateMap<IdentityUser, UserDTO.LoginResult>().ReverseMap();
            CreateMap<IdentityUser, UserDTO.Get>().ReverseMap();
        }

    }
}
