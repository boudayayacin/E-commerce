using MiniProjet.Models;

namespace MiniProjet.Repository
{
    public interface ICommande
    {
        Commande CreateOrder(string email, string phoneNumber, List<PanierArt> panierArticles);
        Commande GetOrderById(int id);
        List<Commande> GetAllOrders();
        Commande UpdateOrder(int id, string email, string phoneNumber, List<PanierArt> panierArticles);
        bool DeleteOrder(int id);
    }
}
