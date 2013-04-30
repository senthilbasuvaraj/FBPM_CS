using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fbpm.Models;

namespace fbpm.Controllers
{ 
    public class ProjectDetailsController : Controller
    {
        private fbpmProjectDetailsEntities db = new fbpmProjectDetailsEntities();

        //
        // GET: /ProjectDetails/

        public ViewResult Index()
        {
            return View(db.ProjectDetails.ToList());
        }

        //
        // GET: /ProjectDetails/Details/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Details(string id)
        {
            var fd = from s in db.FlatDetails
                     where s.ProjectID.Equals(id)
                     select s;

            return View(fd.ToList());
        }

        //
        // GET: /ProjectDetails/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ProjectDetails/Create

        [HttpPost]
        public ActionResult Create(ProjectDetail projectdetail)
        {
            if (ModelState.IsValid)
            {
                db.ProjectDetails.Add(projectdetail);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(projectdetail);
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