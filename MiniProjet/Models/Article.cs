using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class Article
    {
        public int articleId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Designation { get; set; }
        [Required]
        [Display(Name = "Prix en dinar :")]
        public float Prix { get; set; }
        [Required]
        [Display(Name = "Quantité en unité :")]
        public int Quantite { get; set; }
        [Required]
        [Display(Name = "Image ")]
        public string Image { get; set; }
        public int CategorieId { get; set; }
        public Categorie categorie { get; set; }
    }
}
