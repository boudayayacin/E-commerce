using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;
using MiniProjet.Repository;

namespace MiniProjet.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        readonly ArticleContext context;
        public ArticleRepository(ArticleContext context)
        {
            this.context = context;
        }
        public void Add(Article article)
        {
            context.Articles.Add(article);
            context.SaveChanges();
        }
        public void Delete(Article article)
        {
            Article c1 = context.Articles.Find(article.articleId);
            if (c1 != null)
            {
                context.Articles.Remove(c1);
                context.SaveChanges();
            }
        }

        public Article Edit(Article article)
        {
            Article c1 = context.Articles.Find(article.articleId);
            if (c1 == null)
            {
                context.Articles.Add(article);
            }
            else
            {
                c1.Designation = article.Designation;
                c1.Quantite = article.Quantite;
                c1.Prix = article.Prix;
                c1.CategorieId = article.CategorieId;
            }

            context.SaveChanges();

            return article;
        }

        public List<Article> filtrergrand()
        {
            return context.Articles.OrderBy(p => p.Prix).ToList();

        }

        public IList<Article> FindByName(string Designation)
        {
            return context.Articles.Where(s => s.Designation.Contains(Designation)).Include(s => s.categorie).ToList();

        }
        public IList<Article> GetAll()
        {
            return context.Articles.OrderBy(x => x.Designation).Include(x => x.categorie).ToList();
        }
        public IList<Article> GetArticleByCategorieID(int? CategorieId)
        {
            return context.Articles.Where(s => s.CategorieId.Equals(CategorieId)).OrderBy(s => s.Designation).Include(std => std.categorie).ToList();
        }

        public Article GetById(int id)
        {
            return context.Articles.Where(x => x.articleId == id).Include(x => x.categorie).SingleOrDefault();
        }


    }
}
// categorie repository 
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


