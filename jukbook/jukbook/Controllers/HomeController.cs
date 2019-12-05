using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using jukbook.Models;

namespace jukbook.Controllers
{
    public class HomeController : Controller
    {
        BroonContext db = new BroonContext();

        public ActionResult EmailRequestAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broon broon = db.Brooneringud.Find(id);
            if (broon == null)
            {
                return HttpNotFound();
            }
            return View(broon);
        }

        [HttpPost]
        public ActionResult EmailRequestAdmin()
        {
            return View();
        }

        public ActionResult ProcessRequestAdmin()
        {
            return View();
        }

        public ActionResult EmailRequest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broon broon = db.Brooneringud.Find(id);
            if (broon == null)
            {
                return HttpNotFound();
            }
            return View(broon);
        }

        [HttpPost]
        public ActionResult EmailRequest()
        {
            return View();
        }

        public ActionResult ProcessRequest()
        {
            return View();
        }

        public ActionResult Index()
        {
            // получаем из бд все объекты klientid
            IEnumerable<Teenus> teenused = db.Teenused;
            // передаем все объекты в динамическое свойство Klient в ViewBag
            ViewBag.Teenused = teenused;
            //DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            // возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult Buy(GetDataTeenus getdatateenus)
        {
            ViewBag.TeenusId = getdatateenus.Id;
            ViewBag.MisTeenus = getdatateenus.Teenus;
            //ViewBag.datetime = getdatateenus.DateT;
            return View();
        }
        public class GetDataTeenus
        {
            //[Display(Name = "DateT")]
            //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
            //public DateTime DateT { get; set; }
            public int Id { get; set; }
            public string Teenus { get; set; }
        }
        [HttpPost]
        public ActionResult Buy(Broon purchase, GetDataTeenus getdatateenus)
        {
            if (Convert.ToInt32(Session["roleid"]) >= 1)
            {
                db.Brooneringud.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("UserBron");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Register()
        {
            ViewBag.Title = "Registreerimine";
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public ActionResult Success()
        {
            ViewBag.Title = "Вы успешно зарегистрировались";
            return View();
        }

        // ../../Home/Logout
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Admin()
        {

            if (Convert.ToInt32(Session["roleid"]) != 2)
                return RedirectToAction("Index");
            else
            {
                IEnumerable<Broon> broons = db.Brooneringud;
                // передаем все объекты в динамическое свойство Klient в ViewBag
                ViewBag.Brooneringud = broons;
                return View();
            }
        }

        public ActionResult UserBron()
        {

            if (Convert.ToInt32(Session["roleid"]) != 1)
                return RedirectToAction("Index");
            else
            {
                IEnumerable<Broon> broons = db.Brooneringud;
                // передаем все объекты в динамическое свойство Klient в ViewBag
                ViewBag.Brooneringud = broons;
                return View();
            }
        }
        public ActionResult EditIndex(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teenus teenus = db.Teenused.Find(id);
            if (teenus == null)
            {
                return HttpNotFound();
            }
            return View(teenus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIndex([Bind(Include = "ID,MisTeenus,Kirjeldus,Kestvus,Hind")] Teenus teenus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teenus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teenus);
        }

        public ActionResult AddIndex()
        {
            ViewBag.Teenused = new SelectList(db.Teenused, "Id", "MisTeenus", "Kestvus");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddIndex([Bind(Include = "Id,MisTeenus,Kirjeldus,Kestvus,Hind")] Teenus teenus)
        {
            if (ModelState.IsValid)
            {
                db.Teenused.Add(teenus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teenus);
        }

        public ActionResult AdminDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broon broon = db.Brooneringud.Find(id);
            if (broon == null)
            {
                return HttpNotFound();
            }
            return View(broon);
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult AdminDelete(int id)
        {
            Broon broon = db.Brooneringud.Find(id);
            db.Brooneringud.Remove(broon);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        public ActionResult userBronDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broon broon = db.Brooneringud.Find(id);
            if (broon == null)
            {
                return HttpNotFound();
            }
            return View(broon);
        }

        [HttpPost]
        public ActionResult userBronDelete(int id)
        {
            Broon broon = db.Brooneringud.Find(id);
            db.Brooneringud.Remove(broon);
            db.SaveChanges();
            return RedirectToAction("UserBron");
        }
    }
}