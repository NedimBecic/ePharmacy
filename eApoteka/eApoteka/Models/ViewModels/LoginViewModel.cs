using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models.ViewModels
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        public string username;
        public string password;
        
    }
}
