using FinalYearProject.DAL;
using FinalYearProject.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class ChemicalController : Controller
    {
        private ServiceContext db = new ServiceContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Chemicals.ToList());
        }

        //Chemical/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return HttpNotFound();
            }
            return View(chemical);
        }

        // Chemical/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Chemical/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChemicalID,Description")] Chemical chemical)
        {
            if (ModelState.IsValid)
            {
                db.Chemicals.Add(chemical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chemical);
        }

        // GET: Chemical/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return HttpNotFound();
            }
            return View(chemical);
        }

        // POST: Chemical/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChemicalID,Description")] Chemical chemical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chemical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chemical);
        }

        // Chemical/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return HttpNotFound();
            }
            return View(chemical);
        }

        // POST: Chemical/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chemical chemical = db.Chemicals.Find(id);
            db.Chemicals.Remove(chemical);
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
