using System;
using System.Collections.Generic;
using CM.BL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CM.Web.Pages
{
    public sealed class ListModel : PageModel
    {
        public string Users { get; set; }
        public string Message { get; set; }

        public ListModel()
        {
            try
            {
                Manager.Initialize();
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            
            Users = Manager.GetUserNames();
            Page();
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