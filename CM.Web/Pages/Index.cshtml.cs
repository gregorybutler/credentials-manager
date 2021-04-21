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
    public class IndexModel : PageModel
    {
        public string? Message { get; set; }
        
        public IndexModel()
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

        public async Task<IActionResult> OnGet()
        {
            try
            {
                Manager.Initialize();
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            
            return Page();
            
        }
    }
}