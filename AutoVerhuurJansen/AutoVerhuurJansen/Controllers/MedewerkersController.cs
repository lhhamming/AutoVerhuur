﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoVerhuurJansen.Models;
using Microsoft.AspNet.Identity;

namespace AutoVerhuurJansen.Controllers
{
    public class MedewerkersController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: Medewerkers
        public ActionResult Index()
        {
            var medewerkers = db.Medewerkers.Include(m => m.AspNetUsers);
            return View(medewerkers.ToList());
        }

        // GET: Medewerkers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medewerkers medewerkers = db.Medewerkers.Find(id);
            if (medewerkers == null)
            {
                return HttpNotFound();
            }
            return View(medewerkers);
        }

        public ActionResult Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "medewerkerId,voornaam,tussenvoegsel,achternaam")] ChangeMedewerkerInfoVM model)
        {

            var userId = User.Identity.GetUserId();

            var CurrentMw = db.Medewerkers.Where(m => m.AspNetUserID == userId).FirstOrDefault();

            string afkString = Convert.ToString(model.voornaam[0]);
            string afkString2 = Convert.ToString(model.achternaam[0]);

            string afkStringRes = afkString.ToUpper() + afkString2.ToUpper();

            var legevoornaam = model.voornaam;
            var legeachternaam = model.achternaam;
            var legetussenvoegsel = model.tussenvoegsel;
            var legeafkorting = afkString;

            //Als het velt voornaam leeg is word de huidige voornaam behouden
            if (legevoornaam.Equals(""))
            {
                CurrentMw.voornaam = CurrentMw.voornaam;
            }
            else
            {
                CurrentMw.voornaam = model.voornaam;
            }

            if (legeachternaam.Equals(""))
            {
                CurrentMw.achternaam = CurrentMw.achternaam;
            }
            else
            {
                CurrentMw.achternaam = model.achternaam;
            }

            if (legetussenvoegsel.Equals(""))
            {
                CurrentMw.tussenvoegsel = CurrentMw.tussenvoegsel;
            }
            else
            {
                CurrentMw.tussenvoegsel = model.tussenvoegsel;
            }

            if (legeafkorting.Equals(""))
            {
                CurrentMw.afkorting = CurrentMw.afkorting;
            }
            else
            {
                CurrentMw.afkorting = afkStringRes;
            }


            //CurrentMw.achternaam = model.achternaam;
            //CurrentMw.tussenvoegsel = model.tussenvoegsel;
            //CurrentMw.afkorting = afkStringRes;

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
