using System.ComponentModel.DataAnnotations;

namespace MiniProjet.ViewModels
{
    public class CreateViewModel
    {
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
        public IFormFile ImagePath { get; set; }

        public int CategorieId { get; set; }
    }
}
