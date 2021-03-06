using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AngularCore.Data.ViewModels;
using AngularCore.Data.Models;
using System;
using System.Linq;
using AngularCore.Repositories;
using AngularCore.Services;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace AngularCore.Controllers
{

    [Route("api/v1/auth")]
    public class AuthController : Controller
    {

        private IUserRepository _userRepository;
        private IAuthService _authService;
        private IMapper _mapper;

        public AuthController(IUserRepository userRepository, IAuthService authService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        public IActionResult Login([FromBody] LoginForm form)
        {
            User userFound = _userRepository.GetWhere( u => u.Email == form.Email && u.Password == form.Password )
                                            .FirstOrDefault();
            if( userFound == null )
            {
                return BadRequest( new ErrorMessage("Incorrect credentials") );
            }
            return Ok(GenerateLoginResponse(userFound));
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponse), 201)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        public IActionResult Register([FromBody] RegisterForm form)
        {
            User user = _userRepository.GetWhere( u => u.Email.Equals(form.Email)).FirstOrDefault();
            if(user != null || !form.Password.Equals(form.PasswordCheck))
            {
                string message = (user != null ? "This mail is already taken." : "Passwords don't match.");
                return BadRequest(new ErrorMessage(message));
            }

            User newUser = new User {
                Name = form.Name,
                Surname = form.Surname,
                Email = form.Email,
                Password = form.Password
            };
            _userRepository.Add(newUser);

            return CreatedAtAction($"/api/v1/users/{newUser.Id}", GenerateLoginResponse(newUser));
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPost("promote")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult PromoteToAdmin([FromBody] string userId)
        {
            var user = _userRepository.GetById(userId);
            if(user == null)
            {
                return NotFound();
            }
            user.IsAdmin = true;
            _userRepository.Update(user);
            return Ok();
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPost("degrade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DegradeFromAdmin([FromBody] string userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.IsAdmin = false;
            _userRepository.Update(user);
            return Ok();
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("isadmin/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CheckIfAdmin(string userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null || !user.IsAdmin)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Authorize]
        [HttpGet("renew")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public LoginResponse RenewSession()
        {
            User currentUser = _userRepository.GetById(User.Identity.Name);
            LoginResponse renewalResponse = GenerateLoginResponse(currentUser);
            return renewalResponse;
        }

        private LoginResponse GenerateLoginResponse(User user)
        {
            return new LoginResponse(){
                User = _mapper.Map<DetailedUserVM>(user),
                JwtToken = _authService.GenerateJWTToken(user),
                ExpiresIn = TimeSpan.FromDays(_authService.TokenValidityPeriod).TotalSeconds.ToString()
            };
        }

    }
}