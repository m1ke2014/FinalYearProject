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
using System.Data.Entity.Infrastructure;
using FinalYearProject.ViewModels;

namespace FinalYearProject.Controllers
{
    public class DeclarationOfConformitiesController : Controller
    {
        private ServiceContext db = new ServiceContext();

        // GET: DeclarationOfConformities
        public ActionResult Index()
        {
            IQueryable<Chemical> chemicals = db.Chemicals
                .OrderBy(d => d.ChemicalID);
            var sql = chemicals.ToString();
            return View(db.DOCs.ToList());
        }

        // GET: DeclarationOfConformities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeclarationOfConformity declarationOfConformity = db.DOCs.Find(id);
            if (declarationOfConformity == null)
            {
                return HttpNotFound();
            }
            return View(declarationOfConformity);
        }

        // GET: DeclarationOfConformities/Create
        public ActionResult Create()
        {
            var doc = new DeclarationOfConformity();
            doc.Chemicals = new List<Chemical>();
            PopulateSelectedChemicalsData(doc);
            return View();
        }

        // POST: DeclarationOfConformities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,CustomerName,CustomerAddress,ContactName,ContactNumber,Email,Company,Position,Site,Number,PartNo,SerialNo,Description,DateOfInstallation,EquipmentUsage,EquipmentCleaned,DecontaminationProcess,OperationTime,FailureInformation,PartList,ActionTaken")] DeclarationOfConformity declarationOfConformity, string[] selectedChemicals)
        {
            //if (ModelState.IsValid)
            //{
            //    db.DOCs.Add(declarationOfConformity);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(declarationOfConformity);

            if (selectedChemicals != null)
            {
                declarationOfConformity.Chemicals = new List<Chemical>();
                foreach (var chemical in selectedChemicals)
                {
                    var chemicalToAdd = db.Chemicals.Find(int.Parse(chemical));
                    declarationOfConformity.Chemicals.Add(chemicalToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.DOCs.Add(declarationOfConformity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateSelectedChemicalsData(declarationOfConformity);
            return View(declarationOfConformity);
        }


        // GET: DeclarationOfConformities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeclarationOfConformity declarationOfConformity = db.DOCs
            .Include(i => i.Chemicals)
            .Where(i => i.ID == id)
            .Single();
            PopulateSelectedChemicalsData(declarationOfConformity);

            if (declarationOfConformity == null)
            {
                return HttpNotFound();
            }
            return View(declarationOfConformity);
        }

        private void PopulateSelectedChemicalsData(DeclarationOfConformity declarationOfConformity)
        {
            var allChemicals = db.Chemicals;
            var selectChemicals = new HashSet<int>(declarationOfConformity.Chemicals.Select(c => c.ChemicalID));
            var viewModel = new List<SelectedChemicalsData>();
            foreach (var chemical in allChemicals)
            {
                viewModel.Add(new SelectedChemicalsData
                {
                    ChemicalID = chemical.ChemicalID,
                    Description = chemical.Description,
                    Assigned = selectChemicals.Contains(chemical.ChemicalID)
                });
            }
            ViewBag.Chemicals = viewModel;
        }

        // POST: DeclarationOfConformities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedChemicals)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var docToUpdate = db.DOCs
                .Include(i => i.Chemicals)
                .Where(i => i.ID == id)
                .Single();

            if (TryUpdateModel(docToUpdate, "",
                new string[] { "ID,CustomerID,CustomerName,CustomerAddress,ContactName,ContactNumber,Email,Company,Position,Site,Number,PartNo,SerialNo,Description,DateOfInstallation,EquipmentUsage,Chemicals,EquipmentCleaned,DecontaminationProcess,OperationTime,FailureInformation,PartList,ActionTaken" }))
            {
                try
                {
                    UpdateDOCChemicals(selectedChemicals, docToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            PopulateSelectedChemicalsData(docToUpdate);
            return View(docToUpdate);

            // if (ModelState.IsValid)
            //{
            //     db.Entry(declarationOfConformity).State = EntityState.Modified;
            //     db.SaveChanges();
            //     return RedirectToAction("Index");
            // }
            // return View(declarationOfConformity);
        }

        private void UpdateDOCChemicals(string[] selectedChemicals, DeclarationOfConformity docToUpdate)
        {
            if (selectedChemicals == null)
            {
                docToUpdate.Chemicals = new List<Chemical>();
                return;
            }

            var selectedChemicalsHS = new HashSet<string>(selectedChemicals);
            var docChemicals = new HashSet<int>
                (docToUpdate.Chemicals.Select(c => c.ChemicalID));
            foreach (var chemical in db.Chemicals)
            {
                if (selectedChemicalsHS.Contains(chemical.ChemicalID.ToString()))
                {
                    if (!docChemicals.Contains(chemical.ChemicalID))
                    {
                        docToUpdate.Chemicals.Add(chemical);
                    }
                }
                else
                {
                    if (docChemicals.Contains(chemical.ChemicalID))
                    {
                        docToUpdate.Chemicals.Remove(chemical);
                    }
                }
            }
        }

        // GET: DeclarationOfConformities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeclarationOfConformity declarationOfConformity = db.DOCs.Find(id);
            if (declarationOfConformity == null)
            {
                return HttpNotFound();
            }
            return View(declarationOfConformity);
        }

        // POST: DeclarationOfConformities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeclarationOfConformity declarationOfConformity = db.DOCs.Find(id);
            db.DOCs.Remove(declarationOfConformity);
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