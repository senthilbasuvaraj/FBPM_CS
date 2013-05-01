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

        public ViewResult Projschedule(string id)
        {
            var ps = from s in db.ProjectSchedules
                     where s.ProjectID.Equals(id)
                     select s;

            return View(ps.ToList());
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
        public ActionResult CreateProjSched()
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
        [HttpPost]
        public ActionResult CreateProjSched(ProjectSchedule projsched)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                db.ProjectSchedules.Add(projsched);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projsched);
        }

        //
        // GET: /ProjectDetails/Edit/5
 
        public ActionResult Edit(string id)
        {
            ProjectDetail projectdetail = db.ProjectDetails.Find(id);
            return View(projectdetail);
        }

        //Edit : Flat Details

        public ActionResult EditFlat(string id)
        {
          //  var flatdetail = from s in db.FlatDetails where s.FlatID.Equals(id)               select s;
            var flatdetail = db.FlatDetails.Single(r => r.FlatID == id);
            return View(flatdetail);
        }

        [HttpPost]
        public ActionResult EditFlat(FlatDetail flatdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flatdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flatdetail);
        }

        //Edit : Project Schedule

        public ActionResult EditProjSched(int id, string pid)
        {
            //  var flatdetail = from s in db.FlatDetails where s.FlatID.Equals(id)               select s;
            var projsched = db.ProjectSchedules.Find(id, pid);
//                Single((r => r.ScheduleID == id) && (r => r.ScheduleID == id));
            return View(projsched);
        }

        [HttpPost]
        public ActionResult EditProjSched(ProjectSchedule projsched)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projsched).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projsched);
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

        // GET: /Flat Details/Delete/5

        public ActionResult DeleteFlat(string id)
        {
            var flatdetail = db.FlatDetails.Single(r => r.FlatID == id);
            return View(flatdetail);
        }

        //
        // POST: /Flat Details/Delete/5

        [HttpPost, ActionName("DeleteFlat")]
        public ActionResult DeleteFlatConfirmed(string id)
        {
            var flatdetail = db.FlatDetails.Single(r => r.FlatID == id);
            db.FlatDetails.Remove(flatdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Proj Schedule Details/Delete/5

        public ActionResult DeleteProjSched(int id, string pid)
        {
            var projsched = db.ProjectSchedules.Find(id, pid);
            return View(projsched);
        }

        //
        // POST: /Flat Details/Delete/5

        [HttpPost, ActionName("DeleteProjSched")]
        public ActionResult DeleteProjSchedConfirmed(int id, string pid)
        {
            var projsched = db.ProjectSchedules.Find(id, pid);
            db.ProjectSchedules.Remove(projsched);
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