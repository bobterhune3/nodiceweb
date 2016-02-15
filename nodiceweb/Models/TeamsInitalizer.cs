using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nodiceweb.Models;

namespace nodiceweb.DAL
{
    public class TeamsInitializer : System.Data.Entity.DropCreateDatabaseAlways<ApplicationDbContext>
 //   public class TeamsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
 //   public class TeamsInitializer : System.Data.Entity.
    //public class TeamsInitializer : System.Data.Entity.CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var teams = new List<Team>
            {
                buildTeam( "New York",      "B", "AL", "E", "NYB"),
                buildTeam( "Toronto",       "G", "AL", "E", "TOG"),
                buildTeam( "Baltimore",     "J", "AL", "E", "BLJ"),
                buildTeam( "Cleveland",     "M", "AL", "E", "CLM"),         
                buildTeam( "Tampa Bay",     "M", "AL", "E", "TBM"),
                buildTeam( "Detroit",       "B", "AL", "E", "DTB"),
                buildTeam( "Boston",        "S", "AL", "E", "BSS"),
                buildTeam( "Minnesota",     "M", "AL", "W", "MNB"),
                buildTeam( "Seattle",       "G", "AL", "W", "SEG"),
                buildTeam( "Texas",         "G", "AL", "W", "TXG"),
                buildTeam( "Chicago (AL)",  "S", "AL", "W", "CXS"),
                buildTeam( "Anaheim",       "S", "AL", "W", "ANS"),
                buildTeam( "Kansas City",   "J", "AL", "W", "KCJ"),
                buildTeam( "Oakland",       "M", "AL", "W", "OKM"),
                buildTeam( "Philadelphia",  "M", "NL", "E", "PHM"),
                buildTeam( "Washington",    "G", "NL", "E", "WSG"),
                buildTeam( "St. Louis",     "B", "NL", "E", "SLB"),
                buildTeam( "Milwaukee",     "G", "NL", "E", "MLG"),
                buildTeam( "Pittsburgh",    "S", "NL", "E", "PTS"),
                buildTeam( "Chicago (NL)",  "J", "NL", "E", "CHJ"),
                buildTeam( "Miami",         "S", "NL", "E", "MMS"),
                buildTeam( "San Diego",     "G", "NL", "W", "SDG"),
                buildTeam( "Houston",       "J", "NL", "W", "HSJ"),
                buildTeam( "Arizona",       "B", "NL", "W", "AZB"),
                buildTeam( "Colorado",      "M", "NL", "W", "CRM"),
                buildTeam( "Atlanta",       "S", "NL", "W", "ATS"),
                buildTeam( "San Francisco", "J", "NL", "W", "SFJ"),
                buildTeam( "Los Angeles",   "M", "NL", "W", "LAM"),
            };

            teams.ForEach(s => context.Teams.Add(s));
            context.SaveChanges();
            /*
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            new Course{CourseID=1045,Title="Calculus",Credits=4,},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            new Course{CourseID=2021,Title="Composition",Credits=3,},
            new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollments = new List<Enrollment>
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050,},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
            */
        }

        private Team buildTeam(string name, string manager, string league, string division, string code)
        {
            Team t = new Team
            {
                Name = name,
                Manager = manager,
                League = league,
                Division = division,
                Code = code
            };
            return t;
        }
    }
}