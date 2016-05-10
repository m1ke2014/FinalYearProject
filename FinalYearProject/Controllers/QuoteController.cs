using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FinalYearProject.DAL;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using MvcRazorToPdf;

namespace FinalYearProject.Controllers
{
    public class QuoteController : Controller
    {
        private ServiceContext db = new ServiceContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Quotes.ToList());
        }

        // Create PDF that can be emailed to customer
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return new PdfActionResult(quote);

        }

        // GET: Quote/Create
        public ActionResult Create(int RMAid)
        {
            var rma = db.RMAs.Find(RMAid);

            var quoteCreate = new QuoteCreate
            {
                RMAID = RMAid,
                DOCid = rma.DeclarationOfConformityID,
                Date = DateTime.Now,
                Customer = rma.DOCs.CustomerName,
                CustomerAddress = rma.DOCs.CustomerAddress,
                ContactNumber = rma.DOCs.ContactNumber,
                Email = rma.DOCs.Email,
                FailureInformation = rma.DOCs.FailureInformation,
                CorrectiveAction = rma.CorrectiveAction,
                PartsUsed = rma.PartsUsed,
                TimeTaken = rma.TimeTaken
            };

            return View(quoteCreate);
        }

        // POST: Quote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuoteID,RMAid,DeclarationOfConformityID,Date,LabourCostPerHour,LabourCost,PartsQuantity,PartsCost,TotalPartsCost,TotalCost")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Quotes.Add(quote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quote);
        }

        // GET: Quote/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quote/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuoteID,Date,LabourCostPerHour,LabourCost,PartsQuantity,PartsCost,TotalCost")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quote);
        }

        // GET: Quote/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quote/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote quote = db.Quotes.Find(id);
            db.Quotes.Remove(quote);
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
