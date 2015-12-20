using Microsoft.VisualStudio.TestTools.UnitTesting;
using nodiceweb.Models;
using nodiceweb.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nodiceweb.parser.Tests
{
    [TestClass()]
    public class StratOResultFileTests
    {
        [TestMethod()]
        public void StratOResultFileTest()
        {
            StratOResultFile file = new StratOResultFile("2014Final.PRT");

            Dictionary<String, List<String>>  h = file.getPrimarySections();

            foreach (String s in h.Keys)
            {
                Console.Out.WriteLine(s);
            }

            Assert.AreEqual(h.Count, 13);
            List<String> lines = file.getLines(StratOResultFile.S_STANDINGS);
            Assert.AreEqual(lines.Count, 237);

            Dictionary<String, Season> results = file.getPrimaryTotals();
        }
    }
}