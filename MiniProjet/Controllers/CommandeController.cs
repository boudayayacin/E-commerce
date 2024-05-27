using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;
using MiniProjet.Repository;

namespace MiniProjet.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ArticleContext _context;
        private readonly ICommande _commandeService;
        public CommandeController(ICommande commandeService , ArticleContext context)
        {
            _commandeService = commandeService;
            _context = context;
        }
        // GET: CommandeController

        public async Task<IActionResult> Index()
        {
            var commandes = await _context.Commande.ToListAsync();
            return View(commandes);
        }

        // GET: CommandeController/Details/5
        public ActionResult Details(int id)
        {
            Commande commande = _commandeService.GetOrderById(id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }

        // GET: CommandeController/Create
        public ActionResult Create()
        {
            var panierArticles = _context.PanierArts
            .Include(pa => pa.Article) // Inclure les données de l'article associé
            .ToList();
            var model = new Commande
            {
                PanierArticles = panierArticles
            };

            return View(model);
        }

        // POST: CommandeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,PhoneNumber,DateCommande")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commande);
        }

        // GET: CommandeController/Edit/5
        public ActionResult Edit(int id)
        {
            Commande commande = _commandeService.GetOrderById(id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }

        // POST: CommandeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Commande commande)
        {
            if (id != commande.CommandeId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _commandeService.UpdateOrder(id, commande.Email, commande.PhoneNumber, commande.PanierArticles);
                return RedirectToAction(nameof(Index));
            }
            return View(commande);
        }

        // GET: CommandeController/Delete/5
        public ActionResult Delete(int id)
        {
            Commande commande = _commandeService.GetOrderById(id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }

        // POST: CommandeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _commandeService.DeleteOrder(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
