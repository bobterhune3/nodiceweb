using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace nodiceweb
{
    public static class NoDiceUtils
    {
        private static float LeaderWins = 0;
        private static float LeaderLoses = 0;
        private static List<String> ProcessedDivisions = new List<String>();

        public static String GetWinningPercentage(int? wins, int? loses)
        {

            double wpct = (double)wins.Value / ((double)wins.Value + (double)loses.Value);
            return String.Format("{0:0.000}", wpct);

        }

        public static String ResetLeaderInformation()
        {
            LeaderWins = 0f;
            LeaderLoses = 0f;
            return "";
        }

        public static String CheckHeaderInformation(int? wins, int? loses)
        {
            if(LeaderWins == 0)
            {
                LeaderWins = wins.Value;
                LeaderLoses = loses.Value;
            }
            return "";
        }

        public static String CalculateGamesBehind(int? wins, int? loses)
        {
            if (LeaderWins == wins.Value && LeaderLoses == loses.Value)
                return "--";

            float gb = (LeaderWins - wins.Value + loses.Value - LeaderLoses) / 2;
            return String.Format("{0:0.0}", gb);
        }

        public static String CalculateDraftOrderGamesBehind(int? wins, int? loses)
        {
            if (LeaderWins == wins.Value && LeaderLoses == loses.Value)
                return "--";
            float gb = (wins.Value - LeaderWins + LeaderLoses - loses.Value) / 2;
            return String.Format("{0:0.0}", gb);
        }

        public static String ResetWildCardDataPage()
        {
            ProcessedDivisions.Clear();
            return "";
        }

        public static Boolean DivisonLeader( string league, string division )
        {
            foreach( String storedDiv in ProcessedDivisions)
            {
                if (storedDiv.Equals(league+division))
                    return false;
            }

            ProcessedDivisions.Add(league+division);
            return true;
        }

        public static String ExpandValue(string key)
        {
            if (key.Equals("E"))
                return "East";
            if (key.Equals("W"))
                return "West";
            if (key.Equals("NL"))
                return "National League";
            if (key.Equals("AL"))
                return "American League";
            return key;
        }

        public static String CalculatePythagorean(int? rs, int? ra)
        {
            Double pyTheory= Math.Pow(rs.Value, 2.0) / (Math.Pow(rs.Value, 2.0) + (Math.Pow(ra.Value, 2.0)));
            return String.Format("{0:0.000}", pyTheory);
        }

    }
}