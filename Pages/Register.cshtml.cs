using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using WebApplication1.ViewModels;

namespace WebApplication1.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<Member> userManager { get; }
        private SignInManager<Member> signInManager { get; }


        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(
            UserManager<Member> userManager, 
            SignInManager<Member> signInManager
            )
        {
            this.userManager = userManager; 
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        //Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                byte[] photoBytes = null;
                if (RModel.Photo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await RModel.Photo.CopyToAsync(memoryStream);
                        photoBytes = memoryStream.ToArray();
                    }
                }

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var user = new Member
                {
                    FirstName = RModel.FirstName,
                    LastName = RModel.LastName,
                    CreditCardNo = protector.Protect(RModel.CreditCardNo),
                    MobileNo = RModel.MobileNo,
                    BillingAddress = RModel.BillingAddress,
                    ShippingAddress = RModel.ShippingAddress,
                    Photo = photoBytes,
                    Email = RModel.Email,
                    UserName = RModel.Email

                };

                var result = await userManager.CreateAsync(user, RModel.Password); 
                
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return Page();
        }

    }
}
