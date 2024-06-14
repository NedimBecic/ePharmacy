
using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models.ViewModels
{
    public class PlaceOrderViewModel
    {   
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Delivery Options")]
        public string DeliveryOptions { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "No.")]
        public string Number { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Postal")]
        public string PostalCode { get; set; }

        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
