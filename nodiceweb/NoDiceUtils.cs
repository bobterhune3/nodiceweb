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
        /*    int w = wins.Value;
            int l = loses.Value;
            int w1 = (int)LeaderWins;
            int l1 = (int)LeaderLoses;
            int wx = w1 - w;
            int lx =  l - l1;
            int top = wx + lx;

            float gb = ((float)top) / 2f;
            */
            if (LeaderWins == wins.Value && LeaderLoses == loses.Value)
                return "--";

            float top = LeaderWins - wins.Value + loses.Value - LeaderLoses;
            float gb = top / 2;
            return String.Format("{0:0.0}", gb);
        }

    }
}