using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.ViewModels;

namespace WebApplication1.Pages
{
    public class LogoutModel : PageModel
    {

		private readonly SignInManager<Member> signInManager; 
		
		public LogoutModel(SignInManager<Member> signInManager)
		{
			this.signInManager = signInManager;
		}

		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await signInManager.SignOutAsync(); return RedirectToPage("Login");
		}

		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}


		public void OnGet()
        {
        }
    }
}
