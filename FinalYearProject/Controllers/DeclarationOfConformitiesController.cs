﻿using FinalYearProject.DAL;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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

        // GET: DeclarationOfConformities/Details/
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
            doc.RMACreated = false;
            return View();
        }

        // POST: DeclarationOfConformities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,CustomerName,CustomerAddress,ContactName,ContactNumber,Email,Company,Position,Site,Number,PartNo,SerialNo,Description,DateOfInstallation,EquipmentUsage,EquipmentCleaned,DecontaminationProcess,OperationTime,FailureInformation,PartList,ActionTaken")] DeclarationOfConformity declarationOfConformity, string[] selectedChemicals)
        {

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

                // Send email to Service Manager
                //var email = new NewDocEmail
                //{
                //    To = "serviceManager@mksinst.com",
                //    Customer = declarationOfConformity.CustomerName,
                //    Comment = "New DOC"
                //};

                //email.Send();

                return RedirectToAction("SubmitConfirm");
            }
            PopulateSelectedChemicalsData(declarationOfConformity);
            return View(declarationOfConformity);
        }

        private void SendEmail (Comment model)
        {

        }
        // GET: DeclarationOfConformities/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeclarationOfConformity declarationOfConformity = db.DOCs
            .Include(i => i.Chemicals)
            .Where(i => i.DeclarationOfConformityID == id)
            .Single();
            PopulateSelectedChemicalsData(declarationOfConformity);

            if (declarationOfConformity == null)
            {
                return HttpNotFound();
            }
            return View(declarationOfConformity);
        }

        // Show all chemicals in Chemical table
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
                .Where(i => i.DeclarationOfConformityID == id)
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

        }

        // Chemicals selected by the user
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

        // GET: DeclarationOfConformities/Delete/
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

        // POST: DeclarationOfConformities/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeclarationOfConformity declarationOfConformity = db.DOCs.Find(id);
            db.DOCs.Remove(declarationOfConformity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Navigate to submit confirmation
        public ActionResult SubmitConfirm()
        {
            return View("SubmitConfirm");
        }

        // Navigate to home page
        public ActionResult DirectToHome()
        {
            return RedirectToAction("../Home/Index");
        }

        // Navigate to customer home screen
        public ActionResult CustomerHome()
        {
            return View("CustomerHome");
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