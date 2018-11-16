using AutoVerhuurJansen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutoVerhuurJansen.Controllers
{
    [Authorize(Roles = "Manager, Applicatiebeheerder")]
    public class ActivityController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: Activity
        public ActionResult Index()
        {
            return View(db.Medewerkers.ToList());
        }

        // GET: Activity/Deactivate
        public ActionResult Deactivate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medewerkers medewerker = db.Medewerkers.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        // POST: Activity/Deactivate/5
        public ActionResult DeactivateConfirm(int? id)
        {
            
            Medewerkers medewerker = db.Medewerkers.Find(id);
            //set actief to false
            medewerker.Actief = false;
            db.SaveChanges();

            return RedirectToAction("Index");

        }


        // GET: Activity/Activate/5
        public ActionResult Activate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medewerkers medewerker = db.Medewerkers.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        
        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        // POST: Activity/Activate/medewerker
        public ActionResult ActivateConfirm(int? id)
        {
            Medewerkers medewerker = db.Medewerkers.Find(id);
            //Set actief to true
            medewerker.Actief = true;
            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}