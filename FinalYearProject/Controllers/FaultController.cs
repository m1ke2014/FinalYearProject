using FinalYearProject.DAL;
using FinalYearProject.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class FaultController : Controller
    {
        private ServiceContext db = new ServiceContext();

        [Authorize]
        public ActionResult Index(string searchString)
        {
            // Add search ability
            var faults = from fault in db.Faults
                         select fault;

            if (!String.IsNullOrEmpty(searchString))
            {
                faults = faults.Where(fault => fault.FaultDescription.Contains(searchString));
            }

            return View(faults.ToList());
        }

        // GET: Fault/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fault fault = db.Faults.Find(id);
            if (fault == null)
            {
                return HttpNotFound();
            }
            return View(fault);
        }

        // GET: Fault/Create
        public ActionResult Create()
        {
            PopulateProductDropDownList();
            return View("Create");
        }

        // POST: Fault/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FaultID,FaultDescription,Solution,ProductID")] Fault fault)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Faults.Add(fault);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }

            PopulateProductDropDownList(fault.ProductID);

            return View(fault);
        }

        // Populates drop down list with products
        private void PopulateProductDropDownList(object selectedProduct = null)
        {
            var productsQuery = from product in db.Products
                                orderby product.ProductID
                                select product;
            ViewBag.ProductID = new SelectList(productsQuery, "ProductID", "ProductDescription", selectedProduct);
        }

        // GET: Fault/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fault fault = db.Faults.Find(id);
            if (fault == null)
            {
                return HttpNotFound();
            }
            return View(fault);
        }

        // POST: Fault/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FaultID,FaultDescription,Solution")] Fault fault)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fault).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fault);
        }

        // GET: Fault/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fault fault = db.Faults.Find(id);
            if (fault == null)
            {
                return HttpNotFound();
            }
            return View(fault);
        }

        // POST: Fault/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fault fault = db.Faults.Find(id);
            db.Faults.Remove(fault);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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