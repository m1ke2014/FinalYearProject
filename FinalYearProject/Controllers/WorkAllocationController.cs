﻿using System;
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

namespace FinalYearProject.Controllers
{
    public class WorkAllocationController : Controller
    {
        private ServiceContext db = new ServiceContext();

        public ActionResult Index(int? id, int? StaffID)
        {
            // View model to show staff and their related jobs
            var viewModel = new WorkAllocationIndex();
            viewModel.StaffMembers = db.StaffMembers
                .Include(i => i.RMAs.Select(r => r.StaffMembers))
                .OrderBy(i => i.Surname);

            if (id != null)
            {
                ViewBag.StaffID = id;
                viewModel.RMAs = viewModel.StaffMembers.Where(
                    i => i.StaffID == id).Single().RMAs;
            }

            return View(viewModel);
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
