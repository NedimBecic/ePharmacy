using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models.ViewModels
{
    public class AdminPanelViewModel
    {
        [Key] 
        public int Id { get; set; }
        public List<Korisnik> korisnici {  get; set; }
        public List<Apotekar> apotekari { get; set; }
        public List<Dostavljac> dostavljaci { get; set; }
    }
}
