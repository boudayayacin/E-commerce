using MiniProjet.Models;

namespace MiniProjet.Repository
{
    public interface IArticleRepository
    {
        IList<Article> GetAll();
        Article GetById(int id);
        void Add(Article article);
        public Article Edit(Article article);
        void Delete(Article article);
        IList<Article> GetArticleByCategorieID(int? CategorieId);
        IList<Article> FindByName(string Designation);
        List<Article> filtrergrand();

    }
}
