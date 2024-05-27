using MiniProjet.Models;

namespace MiniProjet.Repository
{
    public interface ICategorieRepository
    {
        IList<Categorie> GetAll();
        Categorie GetById(int id);
        void Add(Categorie s);
        void Edit(Categorie s);
        void Delete(Categorie s);
        int ArticleCount(int CategorieId);

    }
}
