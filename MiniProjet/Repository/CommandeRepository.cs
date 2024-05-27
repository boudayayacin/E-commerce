using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;

namespace MiniProjet.Repository
{
    public class CommandeRepository : ICommande
    {
        private readonly ArticleContext _context;
        private List<Commande> _commandes = new List<Commande>();
        public CommandeRepository(ArticleContext context)
        {
            _context = context;
        }
        public Commande CreateOrder(string email, string phoneNumber, List<PanierArt> panierArticles)
        {
            var newOrder = new Commande
            {
                CommandeId = _commandes.Count + 1,
                Email = email,
                PhoneNumber = phoneNumber,
                DateCommande = DateTime.Now,
                PanierArticles = panierArticles
            };

            _commandes.Add(newOrder);
            return newOrder;
        }

        public bool DeleteOrder(int id)
        {
            var order = _commandes.FirstOrDefault(c => c.CommandeId == id);
            if (order != null)
            {
                _commandes.Remove(order);
                return true;
            }
            return false;
        }

        public List<Commande> GetAllOrders()
        {
            return _context.Commande.ToList();
        }

        public Commande GetOrderById(int id)
        {
            return _commandes.FirstOrDefault(c => c.CommandeId == id);
        }

        public Commande UpdateOrder(int id, string email, string phoneNumber, List<PanierArt> panierArticles)
        {
            var order = _commandes.FirstOrDefault(c => c.CommandeId == id);
            if (order != null)
            {
                order.Email = email;
                order.PhoneNumber = phoneNumber;
                order.PanierArticles = panierArticles;
            }
            return order;
        }
    }
}
