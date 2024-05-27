using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;

namespace MiniProjet.Models
{
    public class ArticleContext : IdentityDbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<PanierArt> PanierArts { get; set; }
        public DbSet<MiniProjet.Models.Commande> Commande { get; set; } = default!;
    }
}
