using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class Register
    {
        [Required][DataType(DataType.EmailAddress)] public string Email { get; set; }

        [Required][DataType(DataType.Password)] public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.CreditCard)]
        [Required]
        public string CreditCardNo { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string MobileNo { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string BillingAddress { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ShippingAddress { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg" })]
        public IFormFile? Photo { get; set; }
    }

}
