using nodiceweb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace nodiceweb.Models
{
    public class FakeTeamContext : INoDiceContext
    {
        public FakeTeamContext()
        {
            this.Teams = new FakeTeamSet();
        }

        public IDbSet<Team> Teams { get; private set; }
        public IDbSet<Season> Seasons { get; private set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}