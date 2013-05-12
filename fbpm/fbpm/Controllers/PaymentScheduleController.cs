using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fbpm.Models;
using System.Net.Mime;
using System.IO;
using System.Data.Entity.Validation;
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
        public ViewResult SearchCust() {
            return View();                
        }
        public ViewResult Index(string id)
        {
            var cust = db.UserDetails.Where(c => c.UserID.Contains(id));
            return View(cust.ToList());
        }

        //
        // GET: /PaymentSchedule/Details/

        public ViewResult Details(string id)
        {
            this.HttpContext.Session["USER"] = id;
            var ps = db.PaymentSchedule.OrderByDescending(p => p.RemainingAmount).Where(c => c.UserID.Equals(id));
            var fd = from s in db.PaymentSchedule
                     where s.UserID.Equals(id)
                     select s;

            return View(ps.ToList());
        }

        //
        // GET: /PaymentSchedule/Create

        public ActionResult CreatePaySched()
        {
            ViewBag.Message = this.HttpContext.Session["USER"];
/*            var model = new PaymentSchedule
            {
                UserDetailList = db1.UserDetails.ToList()
            };
  */          return View();
        }

        // Detail: /PaymentSchedule/DetailPS

        public ActionResult DetailPS(Guid id)
        {
            PaymentSchedule paymentschedule = db.PaymentSchedule.Find(id);
            return View(paymentschedule);
        }


        //
        // POST: /PaymentSchedule/Create

        [HttpPost]
        public ActionResult CreatePaySched(PaymentSchedule paymentschedule, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    paymentschedule.ReceiptImage = new byte[file.ContentLength];
                    paymentschedule.ReceiptImageType = file.ContentType;
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    paymentschedule.ReceiptImage = reader.ReadBytes(file.ContentLength);
                }
                paymentschedule.ScheduleID = Guid.NewGuid();
                var mycontroller = new PaymentScheduleController();
                mycontroller.ControllerContext = ControllerContext;
                decimal result = mycontroller.GetBalance(paymentschedule.UserID);
                paymentschedule.RemainingAmount = result - paymentschedule.ScheduleAmount.Value;
                db.PaymentSchedule.Add(paymentschedule);
                db.SaveChanges();
                return RedirectToAction("SearchCust");  
            }

            return View(paymentschedule);
        }

        //Get Remaining Amount

        public decimal GetBalance(string uid)
        {
            decimal result;
            decimal remamt = 0;
            PaymentSchedule ps1 = db.PaymentSchedule.FirstOrDefault(r => r.UserID.Equals(uid));
            PaymentSchedule ps2 = db.PaymentSchedule.OrderByDescending(p => p.RemainingAmount).FirstOrDefault(r => r.UserID.Equals(uid));
            var ps = (from s in db.PaymentSchedule
                     where s.UserID.Equals(uid)
                     select s).ToList();

            UserDetails ud = db.UserDetails.Find(uid);
            if (ps1 == null)
            {
                result = ud.BookedAmount.Value;
            }
            else
            {
                for (var i = 0; i < ps.Count(); i++)
                {
                    remamt = remamt + ps[i].ScheduleAmount.Value;
                }
                result = ud.BookedAmount.Value - remamt;
            }
            return result;
        }

        //Get Image

        public FileContentResult GetImage(Guid id)
        {
            PaymentSchedule ps = db.PaymentSchedule.Single(r => r.ScheduleID.Equals(id));
            if (ps.ReceiptImage != null)
            {
                return File(ps.ReceiptImage, ps.ReceiptImageType);
            }
            else
                return new FileContentResult(new byte[] { }, "JPG");
        }

        
        //
        // GET: /PaymentSchedule/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            PaymentSchedule paymentschedule = db.PaymentSchedule.Find(id);
            return View(paymentschedule);
        }

        //
        // POST: /PaymentSchedule/Edit/5

        [HttpPost]
        public ActionResult Edit(PaymentSchedule paymentschedule, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    paymentschedule.ReceiptImage = new byte[file.ContentLength];
                    paymentschedule.ReceiptImageType = file.ContentType;
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    paymentschedule.ReceiptImage = reader.ReadBytes(file.ContentLength);
                }
                db.Entry(paymentschedule).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    var sb = new System.Text.StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                        ); // Add the original exception as the innerException
                }
                return RedirectToAction("SearchCust");
            }
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
            return RedirectToAction("SearchCust");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}