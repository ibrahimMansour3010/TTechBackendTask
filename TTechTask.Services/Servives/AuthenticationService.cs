using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.DTOs;
using TTechTash.Domain.Services.Abstraction;
using TTechTask.Domain.DTOs;

namespace TTechTask.Services.Servives
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMainServices _mainServices;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<IdentityUser> userManager
            , SignInManager<IdentityUser> signInManager, IMapper mapper, IMainServices mainServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mainServices = mainServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserDTO.Get>> Register(UserDTO.Register model)
        {
            var response = new ApiResponse<UserDTO.Get>();
            var user = _mapper.Map<IdentityUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                response.Status = false;
                response.Message = "";
                foreach (var error in result.Errors)
                {
                    response.Message += error.Description + "\n";
                }
                return response;
            }
            IdentityResult addToRoleResult = null;
            if (model.UserType == 1)
                addToRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
            else if (model.UserType == 2)
                addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
            else if (model.UserType == 3)
                addToRoleResult = await _userManager.AddToRoleAsync(user, "Tester");
            else
            {
                response.Status = false;
                response.Message = "Invalid Role";
                await _userManager.DeleteAsync(user);

                return response;
            }

            if (!addToRoleResult.Succeeded)
            {
                response.Status = false;
                response.Message = "";
                foreach (var error in addToRoleResult.Errors)
                {
                    response.Message += error.Description + "\n";
                }
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<UserDTO.Get>(user);
            return response;
        }
        public async Task<ApiResponse<UserDTO.LoginResult>> Login(UserDTO.Login model)
        {
            var response = new ApiResponse<UserDTO.LoginResult>();
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
                if (user == null)
                {
                    response.Status = false;
                    response.Message = "Invalid Username Or Email";

                    return response;
                }
            }
            var login = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!login.Succeeded)
            {
                response.Status = false;
                response.Message = "Invlaid Password";

                return response;
            }
            Boolean IsInRole = false;
            if (model.UserType == 1)
                IsInRole = await _userManager.IsInRoleAsync(user, "Admin");
            else if (model.UserType == 2)
                IsInRole = await _userManager.IsInRoleAsync(user, "User");
            else if (model.UserType == 3)
                IsInRole = await _userManager.IsInRoleAsync(user, "Tester");
            else
            {
                response.Status = false;
                response.Message = "Invalid Role";

                return response;
            }
            if (!IsInRole)
            {
                response.Status = false;
                response.Message = "Invlaid Role";

                return response;
            }
            var userDTO = _mapper.Map<UserDTO.LoginResult>(user);
            userDTO.Token = await _mainServices.GenerateToken(user);

            response.Status = true;
            response.Message = "success";
            response.Response = userDTO;

            return response;
        }

    }
}
