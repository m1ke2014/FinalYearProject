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
using FinalYearProject.ViewModels;
using System.Data.Entity.Infrastructure;

namespace FinalYearProject.Controllers
{
    public class RMAController : Controller
    {
        private ServiceContext db = new ServiceContext();

        // GET: RMA
        public ActionResult Index()
        {
            RMAIndexData rmaIndex = new RMAIndexData();
            rmaIndex.DOCs = from doc in db.DOCs select doc;
            rmaIndex.RMAs = from rma in db.RMAs select rma;

            return View(rmaIndex);
        }

        // GET: RMA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RMA rMA = db.RMAs.Find(id);
            if (rMA == null)
            {
                return HttpNotFound();
            }
            return View(rMA);
        }

        // GET: RMA/Create
        public ActionResult Create(int declarationOfConformityID)
        {
            PopulatePriorityDropDownList();
            PopulateUserDropDownList();
            var doc = db.DOCs.Find(declarationOfConformityID);

            var rmaCreate = new RMACreate
            {
                DOCID = declarationOfConformityID,
                Customer = doc.CustomerName,
                CustomerAddress = doc.CustomerAddress,
                ContactName = doc.ContactName,
                ContactNumber = doc.ContactNumber,
                Email = doc.Email,
                Company = doc.Company,
                Position = doc.Position,
                Site = doc.Site,
                Number = doc.Number,
                PartNo = doc.PartNo,
                SerialNo = doc.SerialNo,
                Description = doc.Description,
                DateOfInstallation = doc.DateOfInstallation,
                EquipmentUsage = doc.EquipmentUsage,
                ChemicalID = doc.ChemicalID,
                EquipmentCleaned = doc.EquipmentCleaned,
                DecontaminationProcess = doc.DecontaminationProcess,
                OperationTime = doc.OperationTime,
                FailureInformation = doc.FailureInformation,
                PartList = doc.PartList,
                ActionTaken = doc.ActionTaken,
            };

            return View(rmaCreate);
        }

        // POST: RMA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RMAid,TimeTaken,Priorityid")] RMA rMA)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.RMAs.Add(rMA);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            PopulatePriorityDropDownList(rMA.Priorityid);

            PopulateUserDropDownList(rMA.Priorityid);

            return View(rMA);
        }

        // GET: RMA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RMA rMA = db.RMAs.Find(id);
            if (rMA == null)
            {
                return HttpNotFound();
            }
            return View(rMA);
        }

        // POST: RMA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RMAid,Priority,TimeTaken")] RMA rMA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rMA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rMA);
        }

        // GET: RMA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RMA rMA = db.RMAs.Find(id);
            if (rMA == null)
            {
                return HttpNotFound();
            }
            return View(rMA);
        }

        // POST: RMA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RMA rMA = db.RMAs.Find(id);
            db.RMAs.Remove(rMA);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Populates drop down list with priorities
        private void PopulatePriorityDropDownList(object selectedPriority = null)
        {
            var priorityQuery = from priority in db.Priorities
                                orderby priority.Priorityid
                                select priority;
            ViewBag.Priorityid = new SelectList(priorityQuery, "Priorityid", "Description", selectedPriority);
        }

        // Populates drop down list with priorities
        private void PopulateUserDropDownList(object selectedUser = null)
        {
            using (var context = new ApplicationDbContext())
            {
                ViewBag.Users = context.Users.Select(u => u.FirstName + " " + u.Surname).ToList();
            }
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
