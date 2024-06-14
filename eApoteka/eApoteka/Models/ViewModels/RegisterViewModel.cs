using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
    }
}