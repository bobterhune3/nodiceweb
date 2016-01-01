using nodiceweb.Models;
using nodiceweb.parser;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace nodiceweb.Controllers
{
    public class HomeController : Controller
    {
        private INoDiceContext db = null;

        public HomeController()
        {
            this.db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var data = db.Teams.OrderBy(ent => ent.League).ThenBy(ent => ent.Division).ThenByDescending(ent => ent.Seasons.FirstOrDefault().Win);

            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Upload()
        {
            ViewBag.Message = "Select PRT file from SOM Game that contains stats";

            return View();
        }

        [HttpPost]
        public ActionResult DoUpload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    StratOResultFile stratFile = new StratOResultFile(ms);
                    Dictionary<String, Season> results = stratFile.getPrimaryTotals();

                    List<Team> teamsInDB = new List<Team>();

                    using (var context = new ApplicationDbContext())
                    {
                        foreach (String team in results.Keys)
                        {

                            IQueryable<Team> query = context.Teams.Where(b => b.Name == team);

                            foreach (Team result in query)
                            {
                                teamsInDB.Add(result);
                            }
    
                        }

                        foreach (Team team in teamsInDB)
                        {
                            Season season = results[team.Name];
                            season.TeamId = team.Id;
                            context.Seasons.Add(season);
                   //         context.Entry(team).CurrentValues.SetValues(season);
                  //          team.Seasons.Add(season);
                        }
                        context.SaveChanges();
                    }




                }



            }

            return RedirectToAction("Index");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ViewResult Group(String league, String division)
        {
            using (var context = new ApplicationDbContext())
            {
                // Query for the Blog named ADO.NET Blog 
                var data = context.Teams
                            .Join(context.Seasons,
                                c => c.Id,
                                cm => cm.TeamId,
                                (c, cm) => new {c, cm})
                            .Where(a => a.c.League == league).Where(a => a.c.Division == division)
                            .FirstOrDefault();
                return View(data);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db is IDisposable)
            {
                ((ApplicationDbContext)db).Dispose();
            }
            base.Dispose(disposing);
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
    }
}