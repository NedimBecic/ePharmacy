using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models.ViewModels
{
    public class SearchViewModel
    {
        [Key] 
        public int Id { get; set; }
        public string Search { get; set; }
    }
}
