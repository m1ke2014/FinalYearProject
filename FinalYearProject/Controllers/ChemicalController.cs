using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalYearProject.DAL;
using FinalYearProject.Models;

namespace FinalYearProject.Controllers
{
    public class ChemicalController : Controller
    {
        private ServiceContext db = new ServiceContext();

        // GET: Chemical
        public ActionResult Index()
        {
            return View(db.Chemicals.ToList());
        }

        // GET: Chemical/Details/5
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

        // GET: Chemical/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chemical/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Chemical/Edit/5
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

        // POST: Chemical/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Chemical/Delete/5
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

        // POST: Chemical/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chemical chemical = db.Chemicals.Find(id);
            db.Chemicals.Remove(chemical);
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
