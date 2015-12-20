using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using nodiceweb.Models;
using System.Text;

namespace nodiceweb.parser
{
    public class StratOResultFile
    {
        private Dictionary<String, List<String>> hPrimarySections = new Dictionary<String, List<String>>();

        public const String S_LEAGUE_LEADERS = "LEAGUE LEADERS FOR";
        public const String S_STANDINGS = "LEAGUE STANDINGS FOR";
        public const String S_PRIMARY_TOTALS = "LEAGUE GRAND TOTALS (primary report) FOR";
        public const String S_SECONDARY_TOTALS = "LEAGUE GRAND TOTALS (secondary report) FOR";
        public const String S_LEAGUE_GRAND_TOTAL = "LEAGUE GRAND TOTALS (fielding report) FOR";
        public const String S_MANAGER_PROFILES = "MANAGER PROFILES FOR";
        public const String S_BOARD_GAME = "BOARD GAME BREAKDOWNS FOR";
        public const String S_INJURIES = "INJURY/MINOR LEAGUE REPORT FOR";
        public const String S_STATS = "DATE VIS  R H  E HOME R H  E INN  WI";
        public const String S_NEWS_GAME = "NEWSPAPER GAME STORIES FOR";
        public const String S_NEWS_RECAP = "NEWSPAPER STYLE RECAP FOR";
        public const String S_AWARDS = "AWARDS VOTING FOR";
        public const String S_RECORD_BOOK = "RECORD BOOK FOR FOR";

        private String[] SECTION_TITLES = {
            S_LEAGUE_LEADERS,
            S_STANDINGS,
            S_PRIMARY_TOTALS,
            S_SECONDARY_TOTALS,
            S_LEAGUE_GRAND_TOTAL,
            S_MANAGER_PROFILES ,
            S_BOARD_GAME,
            S_INJURIES,
            S_STATS,
            S_NEWS_GAME,
            S_NEWS_RECAP,
            S_AWARDS,
            S_RECORD_BOOK };


        public StratOResultFile(String filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach(String section in SECTION_TITLES)
                hPrimarySections.Add(section, new List<String>());

            List<string> list = new List<string>(lines);
            buildSections(list, hPrimarySections);
        }

        public StratOResultFile(MemoryStream rawData)
        {
            List<string> lines = convertStreamToLines(rawData);

            foreach (String section in SECTION_TITLES)
                hPrimarySections.Add(section, new List<String>());

            buildSections(lines, hPrimarySections);
        }

        private List<string> convertStreamToLines(MemoryStream rawData)
        {
            rawData.Position = 0; // Rewind!
            List<string> rows = new List<string>();
            // Are you *sure* you want ASCII?
            using (var reader = new StreamReader(rawData, Encoding.ASCII))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    rows.Add(line);
                }
            }
            return rows;
        }

        public Dictionary<String, List<String>> getPrimarySections() { return hPrimarySections;  }

        private void buildSections(List<String> lines, Dictionary<String, List<String>> sections)
        {
            String title = "";
            List<String> dataLines = new List<String>();

            foreach (String line in lines)
            {
                if (line.StartsWith("[1]"))
                {
                    String thisTitle = line.Substring(3);

                    if (isMajorSection(thisTitle, sections))
                    {
                        if (title.Length > 0)
                        {
                            sections[title].AddRange(dataLines);
                            dataLines.Clear();
                        }
                        
                        title = getOfficialTitleName(thisTitle);
                    }
                    else
                    {
                        dataLines.Add(line);
                    }
                }
                else
                {
                    dataLines.Add(line);
                }

            }

            sections[title].AddRange(dataLines);

        }

        private bool isMajorSection(String title, Dictionary<String, List<String>> sections)
        {
            try
            {
                String t = getOfficialTitleName(title);
                List<String> section = sections[t];
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                return false;
            }
        }

        private String getOfficialTitleName(String title)
        {
            int i = title.IndexOf("FOR");
            if (i > 0)
                return title.Substring(0, i + 3);
            else
                return title;
        }

        public List<String> getLines(String section)
        {
            return hPrimarySections[section];
        }

        public Dictionary<String, Season> getPrimaryTotals()
        {
            List<String> primaryData = hPrimarySections[S_PRIMARY_TOTALS];
            Dictionary<String, List<String>> hSubSections = new Dictionary<String, List<String>>();
            hSubSections.Add("TEAM                   AVG    AB    R     H   2B  3B   HR  RBI   SB   CS    E", new List<String>());
            hSubSections.Add("TEAM                   ERA   W   L      IP     H    R   ER   HR   BB    SO OAVG", new List<String>());
            buildSections(primaryData, hSubSections);

            List<String> data = hSubSections["TEAM                   ERA   W   L      IP     H    R   ER   HR   BB    SO OAVG"];
            Dictionary<String, Season> results = new Dictionary<string, Season>();
            foreach (String line in data)
            {
                buildResults(results, line);
                //"2014 Houston         [4] 2.52  97  65  1494.2  1140  468  419  101  440  1367 .211"

          //      Console.Out.WriteLine(d);
            }
            return results;
        }

        private void buildResults(Dictionary<String, Season>  results, String line)
        {
            Season season = new Season();
            int year = 0;
            String[] lineData = line.Split(' ');

            Int32.TryParse(lineData[0], out year);
            if (year > 0)
            {
                string team = lineData[1];
                if (lineData[2].Length > 0)
                    team += " " + lineData[2];

                // Find the [4] line, this means data is soon after
                int idx = 3;
                for (; idx < lineData.Length; idx++)
                {
                    if (lineData[idx].Equals("[4]")) break;
                }

                int wins = 0;
                int loses = 0;
                int runsAllowed = 0;
                Int32.TryParse(lineData[idx + 3], out wins);
                Int32.TryParse(lineData[idx + 5], out loses);
                Int32.TryParse(lineData[idx + 11], out runsAllowed);

                season.Year = year;
                season.Win = wins;
                season.Lost = loses;

                results.Add(team, season);
            }
        }


    }
}