using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CM.BL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CM.Web.Pages
{
    public sealed class LoginModel : PageModel
    {
        private const string LoginSuccessful = "Welcome";
        private const string FailedLogin = "Invalid credentials!";
        
        [BindProperty]
        public string? UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Message { get; set; }
        
        public LoginModel()
        {
            try
            {
                Manager.Initialize();
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            Page();
        }

        public async Task<IActionResult?> OnPost()
        {
            var credentials = new Credentials(UserName, Password);
            if (!credentials.Validate())
            {
                return null;
            }
            if (Manager.Login(credentials))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                Message = LoginSuccessful;
                return Page();
            }

            Message = FailedLogin;
            return Page();
        }
        
        public void OnGet()
        {
            try
            {
                Manager.Initialize();
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            Page();
        }
    }
}