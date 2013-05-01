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
    public class PaymentScheduleController : Controller
    {
        private fbpmUserPaySchedEntities db = new fbpmUserPaySchedEntities();
        private fbpmUserEntities db1 = new fbpmUserEntities();
        //
        // GET: /User List/

        public ViewResult Index()
        {
            return View(db.UserDetails.ToList());
        }

        //
        // GET: /PaymentSchedule/Details/5

        public ViewResult Details(string id)
        {
            var fd = from s in db.PaymentSchedule
                     where s.UserID.Equals(id)
                     select s;

            return View(fd.ToList());
        }

        //
        // GET: /PaymentSchedule/Create

        public ActionResult CreatePaySched()
        {
            var model = new PaymentSchedule
            {
                UserDetailList = db1.UserDetails.ToList()
            };
            return View(model);
        } 

        //
        // POST: /PaymentSchedule/Create

        [HttpPost]
        public ActionResult CreatePaySched(PaymentSchedule paymentschedule)
        {
            if (ModelState.IsValid)
            {
                paymentschedule.ScheduleID = Guid.NewGuid();
                db.PaymentSchedule.Add(paymentschedule);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(paymentschedule);
        }
        
        //
        // GET: /PaymentSchedule/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            PaymentSchedule paymentschedule = db.PaymentSchedule.Find(id);
            ViewBag.UserDetailsUserID = new SelectList(db.UserDetails, "UserID", "Password", paymentschedule.UserDetailsUserID);
            return View(paymentschedule);
        }

        //
        // POST: /PaymentSchedule/Edit/5

        [HttpPost]
        public ActionResult Edit(PaymentSchedule paymentschedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentschedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserDetailsUserID = new SelectList(db.UserDetails, "UserID", "Password", paymentschedule.UserDetailsUserID);
            return View(paymentschedule);
        }

        //
        // GET: /PaymentSchedule/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            PaymentSchedule paymentschedule = db.PaymentSchedule.Find(id);
            return View(paymentschedule);
        }

        //
        // POST: /PaymentSchedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            PaymentSchedule paymentschedule = db.PaymentSchedule.Find(id);
            db.PaymentSchedule.Remove(paymentschedule);
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