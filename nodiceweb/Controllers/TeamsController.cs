using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using nodiceweb.Models;

namespace nodiceweb.Controllers
{
    public class TeamsController : Controller
    {
        private INoDiceContext db = null;

        public TeamsController()
        {
            this.db = new ApplicationDbContext();
        }


        public TeamsController(INoDiceContext context)
        {
            this.db = context;
        }

        // GET: Teams
        public ViewResult Index()
        {
            var data = db.Teams.OrderBy(ent => ent.League ).ThenBy( ent => ent.Division ).ThenBy( ent => ent.Name);
            return View(data);
       //     return View(db.Teams.ToList());
        }

        public ViewResult Group(String league, String division)
        {
            using (var context = new ApplicationDbContext())
            {
               // Query for the Blog named ADO.NET Blog 
                var data = context.Teams
                                .Where(b => b.League == league).Where(b => b.Division == division)
                                .FirstOrDefault();
                return View(data);
            }
     //       var data = db.Teams.OrderBy(ent => ent.League).ThenBy(ent => ent.Division).ThenBy(ent => ent.Name);

            //     return View(db.Teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Manager,League,Division,Code")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Manager,League,Division,Code")] Team team)
        {
            if (ModelState.IsValid)
            {
                ((ApplicationDbContext)db).Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db is IDisposable)
            {
                ((ApplicationDbContext)db).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
