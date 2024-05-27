using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjet.Models;
using MiniProjet.Repository;

namespace MiniProjet.Controllers
{
    public class PanierController : Controller
    {
        readonly IArticleRepository _articlerepository;
        readonly IPanierArt _panier;
        // GET: PanierController
        public PanierController(IArticleRepository articleRepository, IPanierArt panier)
        {
            _articlerepository = articleRepository;
            _panier = panier;
        }

        // GET: PanierController
        public ActionResult Index()
        {
            var cartItems = _panier.GetCartItems();
            return View(cartItems);

        }

        // GET: PanierController/Details/5
        public ActionResult Details(int id)
        {
            var article = _articlerepository.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);

        }
        [HttpPost]
        public ActionResult RemoveFromCart(int articleId)
        {
            _panier.RemoveFromCart(articleId);
            return RedirectToAction(nameof(Index));
        }

        // GET: PanierController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PanierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                _articlerepository.Add(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }
        [HttpPost]
        public ActionResult AddToCart(int articleId)
        {
            _panier.AddToCart(articleId);
            return RedirectToAction(nameof(Index));
        }
        // GET: PanierController/Edit/5
        public ActionResult EditPanier(int id)
        {
            var panierArt = _panier.GetPanierArtById(id);
            if (panierArt == null)
            {
                return NotFound();
            }
            return View(panierArt);
        }


        // GET: PanierController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PanierController/Edit/5
        // POST: PanierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPanier(int id, PanierArt panierArt)
        {
            if (id != panierArt.ArticleId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _panier.EditPanier(panierArt); // Supposons que vous ayez une méthode Update dans votre repository
                return RedirectToAction(nameof(Index));
            }
            return View(panierArt);
        }


        // GET: PanierController/Delete/5

        // GET: PanierController/Delete/5
        public ActionResult Delete(int id)
        {
            var article = _articlerepository.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: PanierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
