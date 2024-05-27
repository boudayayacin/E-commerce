namespace MiniProjet.Models
{
    public class Categorie
    {
        public int CategorieId { get; set; }
        public String Namecategorie { get; set; }
        public ICollection<Article> articles { get; set; }

    }
}
