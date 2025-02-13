using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<Member> _userManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataProtector _protector;

        public IndexModel(UserManager<Member> userManager, ILogger<IndexModel> logger, IDataProtectionProvider protectionProvider)
        {
            _userManager = userManager;
            _logger = logger;
            _protector = protectionProvider.CreateProtector("MySecretKey");
        }

        public Member CurrentUser { get; set; }
        public string DecryptedCreditCard { get; set; }

        public string Decrypt(string encryptedText)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptedText))
                {
                    return "No data to decrypt.";
                }

                return _protector.Unprotect(encryptedText);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Decryption failed: {ex.Message}");
                return "Decryption failed";
            }
        }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            CurrentUser = await _userManager.FindByIdAsync(userId);

            if (CurrentUser != null)
            {
                DecryptedCreditCard = Decrypt(CurrentUser.CreditCardNo);
            }
            else
            {
                _logger.LogError("User data could not be retrieved.");
            }
        }
    }
}
