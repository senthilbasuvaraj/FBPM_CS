using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fbpm.Models;
using System.Web.Security;

namespace fbpm.Controllers
{ 
    public class CustomerController : Controller
    {
        private fbpmUserEntities db = new fbpmUserEntities();
        private fbpmProjectDetailsEntities db1 = new fbpmProjectDetailsEntities();
        private fbpmUserPaySchedEntities db2 = new fbpmUserPaySchedEntities();
        //
        // GET: /Customer/

        public ViewResult Index()
        {
      //      var id = Membership.GetUser().ProviderUserKey;
            UserDetail userdetail = db.UserDetails.Find("RHA");
            return View(userdetail);
        }

        //Get the reminaing amount
        public string GetRamt(string id)
        {
            var ps = from p in db2.PaymentSchedule
                     where ( p.UserID.Equals(id) && p.RemainingAmount.Value != null )
                     select p;
            PaymentSchedule ps2 = ps.OrderBy(p => p.RemainingAmount).First();
            return ps2.RemainingAmount.ToString();
        }

        public ViewResult GetFlat(string id) {
            string uid = "FF001";
            FlatDetail fd = db1.FlatDetails.First(f => f.FlatID.Equals(uid));
            return View(fd);
        }

        //Get Project Name
        public string GetProjName(string id)
        {
            ProjectDetail pd = db1.ProjectDetails.First(p => p.ProjectID.Equals(id));
            return pd.ProjectName;
        }

        //Get Image

        public FileContentResult GetImage(string id)
        {
            FlatDetail fd = db1.FlatDetails.Single(r => r.FlatID == id);
            if (fd.LayoutImage != null)
            {
                return File(fd.LayoutImage, fd.LayoutImgType);
            }
            else
                return new FileContentResult(new byte[] { }, "JPG");
        }

        //Get Payment Schedule

        public ViewResult paysched(string id) 
        {
            var ps = from s in db2.PaymentSchedule
                     where s.UserID.Equals(id)
                     select s;
            return View(ps.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}