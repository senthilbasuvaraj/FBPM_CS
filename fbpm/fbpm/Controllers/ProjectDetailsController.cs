﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fbpm.Models;

namespace fbpm.Controllers
{
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }
    [NoCache]
    [HandleError]
    public class ProjectDetailsController : Controller
    {
        private fbpmProjectDetailsEntities db = new fbpmProjectDetailsEntities();

        //
        // GET: /ProjectDetails/

        public ViewResult Index()
        {
            ModelState.Clear();
            return View(db.ProjectDetails.ToList());
        }

        //
        // GET: /ProjectDetails/Details/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Details(string id)
        {

            ModelState.Clear();
            var fd = from s in db.FlatDetails
                     where s.ProjectID.Equals(id)
                     select s;

            return View(fd.ToList());
        }

        //
        // GET: /ProjectDetails/Create

        public ActionResult Create()
        {
            ModelState.Clear();
            return View();
        }
        public ActionResult CreateFlat()
        {
            ModelState.Clear();
            return View();
        } 
        //
        // POST: /ProjectDetails/Create

        [HttpPost]
        public ActionResult Create(ProjectDetail projectdetail)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                db.ProjectDetails.Add(projectdetail);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(projectdetail);
        }

        [HttpPost]
        public ActionResult CreateFlat(FlatDetail flatdetail)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                db.Entry(flatdetail).State = EntityState.Modified;
                db.FlatDetails.Add(flatdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flatdetail);
        }

        //
        // GET: /ProjectDetails/Edit/5
 
        public ActionResult Edit(string id)
        {
            ProjectDetail projectdetail = db.ProjectDetails.Find(id);
            return View(projectdetail);
        }

        //
        // POST: /ProjectDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(ProjectDetail projectdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectdetail);
        }

        //
        // GET: /ProjectDetails/Delete/5
 
        public ActionResult Delete(string id)
        {
            ProjectDetail projectdetail = db.ProjectDetails.Find(id);
            return View(projectdetail);
        }

        //
        // POST: /ProjectDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            ProjectDetail projectdetail = db.ProjectDetails.Find(id);
            db.ProjectDetails.Remove(projectdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}