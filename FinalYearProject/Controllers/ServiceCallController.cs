using FinalYearProject.DAL;
using FinalYearProject.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class ServiceCallController : Controller
    {
        private ServiceContext db = new ServiceContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.ServiceCalls.ToList());
        }

        // GET: ServiceCall/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCall serviceCall = db.ServiceCalls.Find(id);
            if (serviceCall == null)
            {
                return HttpNotFound();
            }
            return View(serviceCall);
        }

        // GET: ServiceCall/Create
        public ActionResult Create()
        {
            PopulateRMADropDownList();
            PopulateProductDropDownList();
            return View("Create");
        }

        // POST: ServiceCall/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceCallID,Description,SerialNo,rmaID")] ServiceCall serviceCall)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ServiceCalls.Add(serviceCall);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }

            UpdateRMA(serviceCall.RMAid);
            PopulateRMADropDownList(serviceCall.RMAid);
            PopulateProductDropDownList(serviceCall.ProductID);

            return View(serviceCall);

        }

        // Populates drop down list with products
        private void PopulateProductDropDownList(object selectedProduct = null)
        {
            var productsQuery = from product in db.Products
                                orderby product.ProductID
                                select product;
            ViewBag.ProductID = new SelectList(productsQuery, "ProductID", "ProductDescription", selectedProduct);
        }


        // Populates drop down list with RMA's
        private void PopulateRMADropDownList(object selectedRMA = null)
        {
            var RMAQuery = from rma in db.RMAs
                                orderby rma.RMAid
                                select rma;
            ViewBag.RMAid = new SelectList(RMAQuery, "RMAid", "RMAid", selectedRMA);
        }

        // GET: ServiceCall/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCall serviceCall = db.ServiceCalls.Find(id);
            if (serviceCall == null)
            {
                return HttpNotFound();
            }
            return View(serviceCall);
        }

        // POST: ServiceCall/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceCallID,Description,SerialNo")] ServiceCall serviceCall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceCall).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceCall);
        }

        // GET: ServiceCall/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCall serviceCall = db.ServiceCalls.Find(id);
            if (serviceCall == null)
            {
                return HttpNotFound();
            }
            return View(serviceCall);
        }

        public void UpdateRMA(int rmaID)
        {
            using (var context = new ServiceContext())
            {
                RMA rmaUpdate = context.RMAs.FirstOrDefault(d => d.RMAid == rmaID);
                if (rmaUpdate != null)
                {
                    rmaUpdate.ServiceCallID = 1;
                }
                context.SaveChanges();
            }
        }

        // POST: ServiceCall/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ServiceCall serviceCall = db.ServiceCalls.Find(id);
            db.ServiceCalls.Remove(serviceCall);
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