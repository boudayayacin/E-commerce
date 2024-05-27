using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using MiniProjet.Models;
using MiniProjet.Repository;
using MiniProjet.ViewModels;

namespace MiniProjet.Controllers
{
   // [Authorize]
    public class ArticleController : Controller
    {
        readonly IArticleRepository _articlerepository;
        readonly ICategorieRepository _categoryrepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ArticleController(IArticleRepository articlerepository, ICategorieRepository categoryrepository , IWebHostEnvironment hostingEnvironment)
        {
            _articlerepository = articlerepository;
            _categoryrepository = categoryrepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: ArticleController
        public ActionResult Index()
        {
            var articles = _articlerepository.GetAll();
            ViewBag.CategorieId = new SelectList(_categoryrepository.GetAll(), "CategorieId", "Namecategorie");

            return View(articles);

        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            var article = _articlerepository.GetById(id);
            return View(article);


        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            ViewBag.CategorieId = new SelectList(_categoryrepository.GetAll(), "CategorieId", "Namecategorie");

            var viewModel = new CreateViewModel();
            return View(viewModel);

        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.ImagePath != null)
                {
                    // Votre logique pour télécharger et enregistrer l'image
                    // Par exemple :
                    uniqueFileName = Guid.NewGuid().ToString() + "" + model.ImagePath.FileName;
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Article newArticle = new Article
                {
                    Designation = model.Designation,
                    Prix = model.Prix,
                    Quantite = model.Quantite,
                    Image = uniqueFileName, // Enregistrer le nom du fichier de l'image
                    CategorieId = model.CategorieId
                };

                // Ajouter le nouvel article à votre repository ou à votre contexte de base de données
                _articlerepository.Add(newArticle);

                // Rediriger vers l'action "Index" ou toute autre action souhaitée
                return RedirectToAction("Index");
            }

            // Si le modèle n'est pas valide, retourner la vue avec le modèle pour afficher les erreurs de validation
            return View(model);
        }


        // POST: ArticleController/Edit/5
        // GET: ArticleController/Edit/5
        public ActionResult Edit(int id)
        {
            Article product = _articlerepository.GetById(id);
            EditViewModel productEditViewModel = new EditViewModel
            {
                Id = product.articleId,
                Designation = product.Designation,
                Prix = product.Prix,
                Quantite = product.Quantite,
                ExistingImagePath = product.Image
            };
            return View(productEditViewModel);
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the product being edited from the database
                Article product = _articlerepository.GetById(model.Id);
                // Update the product object with the data in the model object
                product.Designation = model.Designation;
                product.Prix = model.Prix;
                product.Quantite = model.Quantite;
                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.ImagePath != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the product object which will be
                    // eventually saved in the database
                    product.Image = ProcessUploadedFile(model);
                }
                // Call update method on the repository service passing it the

                // product object to update the data in the database table
                Article updatedProduct = _articlerepository.Edit(product);
                if (updatedProduct != null)
                    return RedirectToAction("Index");
                else
                    return NotFound();

            }
            return View(model);
        }
        [NonAction]
        private string ProcessUploadedFile(EditViewModel model)
        {
            string uniqueFileName = null;
            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: ArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_articlerepository.GetById(id));
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Article a)
        {
            try
            {
                _articlerepository.Delete(a);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string Designation, int? CategorieId)
        {
            var result = _articlerepository.GetAll();
            if (!string.IsNullOrEmpty(Designation))
            {
                result = _articlerepository.FindByName(Designation);
            }
            else if (CategorieId != null)
            {
                result = _articlerepository.GetArticleByCategorieID(CategorieId);
            }

            var categories = _categoryrepository.GetAll(); // Assurez-vous que cette méthode existe et renvoie les catégories
            if (categories == null || !categories.Any())
            {
                throw new InvalidOperationException("Aucune catégorie trouvée."); // Vous pouvez personnaliser ce message d'erreur
            }

            ViewBag.CategorieId = new SelectList(categories, "CategorieId", "Namecategorie");
            return View("Index", result);
        }
        public ActionResult FiltrerByQuantite(bool filtrer)
        {
            List<Article> products;
            if (filtrer)
            {
                products = _articlerepository.filtrergrand();
            }
            else
            {
                products = _articlerepository.filtrergrand();
            }
            return View("Index", products);
        }

    }

}


