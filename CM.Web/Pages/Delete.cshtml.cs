using System;
using System.Threading.Tasks;
using CM.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CM.Web.Pages
{
    public sealed class DeleteModel : PageModel
    {
        private const string DeleteSuccessful = "User was deleted!";
        private const string FailedDelete = "Unable to delete!";
        
        [BindProperty]
        public string? UserName { get; set; }
        public string? Message { get; set; }

        public DeleteModel()
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
            if (Manager.Delete(UserName))
            {
                Message = DeleteSuccessful;
                return Page();
            }

            Message = FailedDelete;
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