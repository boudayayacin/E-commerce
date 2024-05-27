using MiniProjet.Models;

namespace MiniProjet.Repository
{
    public class CategorieRepository : ICategorieRepository
    {
        readonly ArticleContext context;
        public CategorieRepository(ArticleContext context)
        {
            this.context = context;
        }

        public void Add(Categorie s)
        {
            context.Categories.Add(s);
            context.SaveChanges();

        }

        public int ArticleCount(int CategorieId)
        {
            return context.Articles.Where(s => s.CategorieId == CategorieId).Count();
        }

        public void Delete(Categorie s)
        {
            Categorie c1 = context.Categories.Find(s.CategorieId);
            if (c1 != null)
            {
                context.Categories.Remove(c1);
                context.SaveChanges();
            }

        }

        public void Edit(Categorie s)
        {
            Categorie c1 = context.Categories.Find(s.CategorieId);
            if (c1 != null)
            {
                c1.Namecategorie = s.Namecategorie;
                context.SaveChanges();
            }

        }

        public IList<Categorie> GetAll()
        {
            return context.Categories.OrderBy(s => s.Namecategorie).ToList();

        }

        public Categorie GetById(int id)
        {
            return context.Categories.Find(id);

        }
    }
}
