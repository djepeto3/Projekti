using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredictingPlayersPerformances
{
    class TeamRank
    {
        public Dictionary<string, double> defensiveRatings1415 { get; set; }
        public Dictionary<string, double> defensiveRatings1516 { get; set; }

        public TeamRank()
        {
            defensiveRatings1415 = new Dictionary<string, double>();
            defensiveRatings1415.Add("TOR", 100.9);
            defensiveRatings1415.Add("BOS", 101.2);
            defensiveRatings1415.Add("BKN", 100.9);
            defensiveRatings1415.Add("PHI", 101);
            defensiveRatings1415.Add("NYK", 101.2);
            defensiveRatings1415.Add("CLE", 98.7);
            defensiveRatings1415.Add("CHI", 97.8);
            defensiveRatings1415.Add("MIL", 97.4);
            defensiveRatings1415.Add("IND", 97);
            defensiveRatings1415.Add("DET", 99.5);
            defensiveRatings1415.Add("ATL", 97.1);
            defensiveRatings1415.Add("WAS", 97.8);
            defensiveRatings1415.Add("MIA", 97.3);
            defensiveRatings1415.Add("CHA", 97.3);
            defensiveRatings1415.Add("ORL", 101.4);
            defensiveRatings1415.Add("POR", 98.6);
            defensiveRatings1415.Add("OKC", 101.8);
            defensiveRatings1415.Add("UTA", 94.9);
            defensiveRatings1415.Add("DEN", 105);
            defensiveRatings1415.Add("MIN", 106.5);
            defensiveRatings1415.Add("GSW", 99.9);
            defensiveRatings1415.Add("LAC", 100.1);
            defensiveRatings1415.Add("PHX", 103.3);
            defensiveRatings1415.Add("SAC", 105);
            defensiveRatings1415.Add("LAL", 105.3);
            defensiveRatings1415.Add("HOU", 100.5);
            defensiveRatings1415.Add("SAS", 97);
            defensiveRatings1415.Add("MEM", 95.1);
            defensiveRatings1415.Add("DAL", 102.3);
            defensiveRatings1415.Add("NOP", 98.6);

            defensiveRatings1516 = new Dictionary<string, double>();
            defensiveRatings1516.Add("TOR", 98.2);
            defensiveRatings1516.Add("BOS", 102.5);
            defensiveRatings1516.Add("BKN", 106);
            defensiveRatings1516.Add("PHI", 107.6);
            defensiveRatings1516.Add("NYK", 101.1);
            defensiveRatings1516.Add("CLE", 98.3);
            defensiveRatings1516.Add("CHI", 103.1);
            defensiveRatings1516.Add("MIL", 103.2);
            defensiveRatings1516.Add("IND", 100.5);
            defensiveRatings1516.Add("DET", 101.4);
            defensiveRatings1516.Add("ATL", 99.2);
            defensiveRatings1516.Add("WAS", 104.6);
            defensiveRatings1516.Add("MIA", 98.4);
            defensiveRatings1516.Add("CHA", 100.7);
            defensiveRatings1516.Add("ORL", 103.7);
            defensiveRatings1516.Add("POR", 104.3);
            defensiveRatings1516.Add("OKC", 102.9);
            defensiveRatings1516.Add("UTA", 95.9);
            defensiveRatings1516.Add("DEN", 105);
            defensiveRatings1516.Add("MIN", 106);
            defensiveRatings1516.Add("GSW", 104.1);
            defensiveRatings1516.Add("LAC", 100.2);
            defensiveRatings1516.Add("PHX", 107.5);
            defensiveRatings1516.Add("SAC", 109.1);
            defensiveRatings1516.Add("LAL", 106.9);
            defensiveRatings1516.Add("HOU", 106.4);
            defensiveRatings1516.Add("SAS", 92.9);
            defensiveRatings1516.Add("MEM", 101.3);
            defensiveRatings1516.Add("DAL", 102.6);
            defensiveRatings1516.Add("NOP", 106.5);
        }
        public int getTeamDefensiveRank1415(string name)
        {

            double rating = defensiveRatings1415[name];
            //return rating;
            int position = 1;
            foreach(var pair in defensiveRatings1415)
            {
                if (rating > pair.Value)
                    position++;
            }
            
            return position;
        }

        public double getTeamDefensiveRank1516(string name)
        {
            double rating = defensiveRatings1516[name];
            //return rating;
            int position = 1;
            foreach (var pair in defensiveRatings1516)
            {
                if (rating > pair.Value)
                    position++;
            }

            return position;
        }

        public int getEveryTeamRank(string name)
        {
            return 0;
        }
    }
}
