using MiniProjet.Models;

namespace MiniProjet.Repository
{
    public interface IPanierArt
    {
        void AddToCart(int articleId);
        bool IsInCart(int articleId);
        void RemoveFromCart(int articleId);
        List<PanierArt> GetCartItems();
        public PanierArt EditPanier(PanierArt panierArt);
        PanierArt GetPanierArtById(int panierArtId);
    }
}
