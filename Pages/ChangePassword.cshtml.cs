using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Pages
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        [BindProperty]
        public ChangePassword Input { get; set; }

        public ChangePasswordModel(UserManager<Member> userManager, SignInManager<Member> signInManager, ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("Unable to load user.");
                return RedirectToPage("/Login");
            }

            // Change the password
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            // Refresh sign in to update the security stamp.
            await _signInManager.RefreshSignInAsync(user);
            TempData["StatusMessage"] = "Your password has been changed.";
            return RedirectToPage("Index");
        }
    }
}
