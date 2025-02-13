using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class Member : IdentityUser
    {

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
        [DataType(DataType.ImageUrl)]
        public byte[] Photo { get; set; }

    }

}
