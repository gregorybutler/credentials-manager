using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CM.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CM.Web.Pages
{
    public sealed class RegisterModel : PageModel
    {
        private const string RegisterSuccessful = "Registration successful!";
        private const string FailedRegister = "User name is taken!";
        
        [BindProperty]
        public string? UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Message { get; set; }
        
        public RegisterModel()
        {
          
        }

        public async Task<IActionResult?> OnPost()
        {
            var credentials = new Credentials(UserName, Password);
            if (!credentials.Validate())
            {
                return null;
            }

            if (Manager.Register(credentials))
            {
                Message = RegisterSuccessful;
                return Page();
            }

            Message = FailedRegister;
            return Page();
        }
        public async Task<IActionResult?> OnGet()
        {
            try
            {
                Manager.Initialize();
            }
            catch (Exception e)
            {
                Message = e.Message;
                return LocalRedirect("/Error");
            }

            return Page();
        }
    }
}