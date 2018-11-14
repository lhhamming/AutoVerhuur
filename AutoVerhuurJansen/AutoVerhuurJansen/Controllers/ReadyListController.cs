using AutoVerhuurJansen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoVerhuurJansen.Controllers
{
    public class ReadyListController : Controller
    {

        DB_Jansen db = new DB_Jansen();
        // GET: ReadyList
        public ActionResult Index()
        {

            DateTime today = DateTime.Today;


            var result = db.Verhuren.Where(v => v.beginDatum > today);


            return View(result.ToList());
        }

    }
}