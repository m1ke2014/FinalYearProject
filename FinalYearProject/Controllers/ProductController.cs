using FinalYearProject.DAL;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class ProductController : Controller
    {
        private ServiceContext db = new ServiceContext();

        [Authorize]
        public ActionResult Index(string id, string productID, int? faultID, string searchString)
        {
            // Product Search feature
            var products = from product in db.Products
                           select product;

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    products = products.Where(product => product.ProductID.Contains(searchString));
            //    || product.ProductDescription.Contains(searchString));
            //}

            // Includes viewmodel so both products and faults are displayed
            var viewModel = new FaultsIndexData();
            viewModel.Products = db.Products
                .Include(i => i.Faults.Select(c => c.Product))
                .OrderBy(i => i.ProductID);

            // When a product is selected, the related faults are displayed
            if (id != null)
            {
                ViewBag.ProductID = id;
                viewModel.Faults = viewModel.Products.Where(
                    i => i.ProductID == id).Single().Faults;
            }
            return View(viewModel);
        }

        // GET: Product/Details/
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductDescription")] Product product, string ProductID)
        {
            // Checks if product has already been added
            bool productExists = db.Products.Any(p => p.ProductID.Equals(ProductID));

            if (productExists)
            {
                ViewBag.Message = "Error, Product already exists";
            }
            else
            {
                db.Products.Add(product);
                db.SaveChanges();                   
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Product/Edit/
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public ActionResult EditPost(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productToUpdate = db.Products.Find(id);
            if (TryUpdateModel(productToUpdate, "",
                new string[] { "ProductID", "ProductDescription" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }
            return View(productToUpdate);
        }


        // GET: Product/Delete/
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Close DB connection
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}