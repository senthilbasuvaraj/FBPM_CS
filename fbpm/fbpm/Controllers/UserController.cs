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
    
    [NoCacheAttributeUser]
    [HandleError]
    public class UserController : Controller
    {
        private fbpmUserEntities db = new fbpmUserEntities();
        private fbpmProjectDetailsEntities db1 = new fbpmProjectDetailsEntities();
        //
        // GET: /User/

        public ViewResult Index()
        {
           return View(db.UserDetails.ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(string id)
        {
            UserDetail userdetail = db.UserDetails.Find(id);
            return View(userdetail);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            var model = new UserDetail
            {
                ProjectDetail = db1.ProjectDetails.ToList()
            };
            return View(model);
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(UserDetail userdetail)
        {
            if (ModelState.IsValid)
            {
                db.UserDetails.Add(userdetail);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(userdetail);
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(string id)
        {
            UserDetail userdetail = db.UserDetails.Find(id);
            userdetail.ProjectDetail = db1.ProjectDetails.ToList();
            return View(userdetail);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(UserDetail userdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userdetail);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(string id)
        {
            UserDetail userdetail = db.UserDetails.Find(id);
            return View(userdetail);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            UserDetail userdetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userdetail);
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