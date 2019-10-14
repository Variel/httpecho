using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpEcho.Models;
using HttpEcho.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HttpEcho.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<HttpEchoUser> _signInManager;

        public AuthController(SignInManager<HttpEchoUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginCallAsync(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
                return Ok(new
                {
                    returnUrl
                });

            return Unauthorized();
        }

        public IActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> JoinCallAsync(JoinViewModel model, string returnUrl = null)
        {
            return Ok();
        }

        public IActionResult AccessDenied()
        {
            return Ok();
        }
    }
}
