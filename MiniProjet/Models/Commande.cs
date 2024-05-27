using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime DateCommande { get; set; } = DateTime.Now;

        public List<PanierArt> PanierArticles { get; set; }
    }
}
