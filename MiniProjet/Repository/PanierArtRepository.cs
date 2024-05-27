using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;

namespace MiniProjet.Repository
{
    public class PanierArtRepository : IPanierArt
    {
        private readonly List<PanierArt> _panier = new List<PanierArt>();
        private readonly ArticleContext _context;
        public PanierArtRepository(ArticleContext context)
        {
            _context = context;
        }

        public void AddToCart(int articleId)
        {
            var panierArt = _context.PanierArts.SingleOrDefault(pa => pa.ArticleId == articleId);
            if (panierArt == null)
            {
                panierArt = new PanierArt
                {
                    ArticleId = articleId,
                    Quantity = 1
                };
                _context.PanierArts.Add(panierArt);
            }
            else
            {
                panierArt.Quantity++;
            }
            _context.SaveChanges();

        }

        public PanierArt EditPanier(PanierArt panierArt)
        {
            PanierArt p = _context.PanierArts.Find(panierArt.Id);
            if (p == null)
            {
                _context.PanierArts.Add(panierArt);
            }
            else
            {
                p.Quantity = panierArt.Quantity;
            }

            _context.SaveChanges();

            return panierArt;

        }

        public List<PanierArt> GetCartItems()
        {
            return _context.PanierArts.Include(pa => pa.Article).ToList();

        }

        public PanierArt GetPanierArtById(int panierArtId)
        { 
            return _context.PanierArts.Include(pa => pa.Article).FirstOrDefault(item => item.Id == panierArtId);
        }

        public bool IsInCart(int articleId)
        {
            return _context.PanierArts.Any(pa => pa.ArticleId == articleId);
        }

        public void RemoveFromCart(int articleId)
        {
            var panierArt = _context.PanierArts.SingleOrDefault(pa => pa.ArticleId == articleId);
            if (panierArt != null)
            {
                _context.PanierArts.Remove(panierArt);
                _context.SaveChanges();
            }

        }
    }
}
