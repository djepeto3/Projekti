using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;

namespace PredictingPlayersPerformances
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable table = new DataTable();
            table.Columns.Add("WinLose");
            table.Columns.Add("Player1", typeof(double));
            table.Columns.Add("Player2", typeof(double));
            table.Columns.Add("Player3", typeof(double));
            
            LinearRegression regression1 = new LinearRegression();
            LinearRegression regression2 = new LinearRegression();
            LinearRegression regression3 = new LinearRegression();
            TeamRank rank=new TeamRank();
            List<string> teams = new List<string>();
            foreach (var pair in rank.defensiveRatings1415)
            {
                teams.Add(pair.Key);
                
            }


            string[] lines = File.ReadAllLines(@"./../../data/shot_logs.csv");
            lines = lines.Skip(1).ToArray();

            string[] players = { "danny green", "tim duncan", "manu ginobili" };//choose player - all small letters  
            //string playerName = "marc gasol";
            string teamName = "SAS";//choose team - all big letters and a short name of it(CLE, HOU, SAS, GSW, CHI, NYK...)

            

            Dictionary<string, int> gamePoints1 = new Dictionary<string, int>();
            Dictionary<string, string> gameWin1 = new Dictionary<string, string>();
            Dictionary<string, int> gamePoints2 = new Dictionary<string, int>();            
            Dictionary<string, int> gamePoints3 = new Dictionary<string, int>();
            
            
            foreach (string player in players)
            {
                Dictionary<string, int> gamePoints = new Dictionary<string, int>();
                Dictionary<string, string> gameWin = new Dictionary<string, string>();
                foreach (string line in lines)
                {
                    string[] parameters = line.Split(',');
                    if (parameters[21].Contains(player))
                    {
                        if (!gamePoints.ContainsKey(parameters[2]))
                        {
                            gamePoints.Add(parameters[2], int.Parse(parameters[20]));
                            gameWin.Add(parameters[2],parameters[4]);
                            
                        }
                        else
                            gamePoints[parameters[2]] += int.Parse(parameters[20]);
                    }
                }
                if (gamePoints1.Count == 0) 
                {
                    gamePoints1 = gamePoints;
                    gameWin1 = gameWin;
                }
                else if (gamePoints2.Count == 0)
                {
                    gamePoints2 = gamePoints;
                    gameWin1 = gameWin;
                }
                else
                {
                    gamePoints3 = gamePoints;
                    gameWin1 = gameWin;
                }
            }
            

            foreach (var game in gamePoints1)
            {
                if(gamePoints2.ContainsKey(game.Key))
                {
                    if (gamePoints3.ContainsKey(game.Key))
                    {
                        table.Rows.Add(gameWin1[game.Key], gamePoints1[game.Key], gamePoints2[game.Key], gamePoints3[game.Key]);
                        Console.WriteLine(gameWin1[game.Key] + " " + gamePoints1[game.Key] + " " + gamePoints2[game.Key] + " " + gamePoints3[game.Key]);
                    }
                }
            }
            Classifier classifier = new Classifier();
            classifier.TrainClassifier(table);

            //Console.WriteLine(classifier.Classify(new double[] { 15, 5, 10 }));
            //Console.Read();





            Dictionary<string, double> regressionResult1 = new Dictionary<string, double>();
            Dictionary<string, double> regressionResult2 = new Dictionary<string, double>();
            Dictionary<string, double> regressionResult3 = new Dictionary<string, double>();


            foreach (string player in players)
            {
                List<double> x = new List<double>();
                List<double> y = new List<double>();
                foreach (string team in teams)
                {
                    double defensiveRank = rank.getTeamDefensiveRank1415(team);
                    //int pointsInExactGame = 0;
                    Dictionary<string, int> games = new Dictionary<string, int>();

                    foreach (string line in lines)
                    {
                        string[] parameters = line.Split(',');

                        if (parameters[21].Contains(player))
                        {
                            if (parameters[2].Contains("@ " + team) || parameters[2].Contains("vs. " + team))
                            {
                                if (!games.ContainsKey(parameters[2]))
                                {
                                    games.Add(parameters[2], int.Parse(parameters[20]));
                                    //pointsInExactGame += int.Parse(parameters[20]);
                                }
                                else
                                    games[parameters[2]] += int.Parse(parameters[20]);
                            }
                        }
                    }
                    foreach (var pair in games)
                    {
                        x.Add(defensiveRank);
                        y.Add(pair.Value);
                    }
                }
                if (!regression1.fited)
                {
                    regression1.fit(x.ToArray(), y.ToArray());//make a line for a player
                    foreach (var team in teams)
                    {
                        double regressionResult = regression1.predict(rank.getTeamDefensiveRank1516(team));//make a prediction for given team
                        Console.WriteLine(player + " will score approximately " + regressionResult + " points against " + team + "\n");
                    }
                }
                else if (!regression2.fited)
                {
                    regression2.fit(x.ToArray(), y.ToArray());//make a line for a player
                    foreach (var team in teams)
                    {
                        double regressionResult = regression2.predict(rank.getTeamDefensiveRank1516(team));//make a prediction for given team
                        Console.WriteLine(player + " will score approximately " + regressionResult + " points against " + team + "\n");
                    }
                }
                else
                {
                    regression3.fit(x.ToArray(), y.ToArray());//make a line for a player
                    foreach (var team in teams)
                    {
                        double regressionResult = regression3.predict(rank.getTeamDefensiveRank1516(team));//make a prediction for given team
                        Console.WriteLine(player + " will score approximately " + regressionResult + " points against " + team + "\n");
                    }
                }
                
            }

            string comma=",";
            string filePath=@"./../../data/results.csv";
            string[] columns = { "AGAINST_TEAM", "PLAYER1_SCORES", "PLAYER2_SCORES", "PLAYER3_SCORES", "PREDICTED_W_L", "ACTUAL_W_L" };
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(comma,columns));
            //File.WriteAllText(filePath, sb.ToString());



            StringBuilder sb1 = new StringBuilder();
            List<string> subCol = new List<string>();
            subCol.Add("against");
            foreach (var player in players)
            {
                subCol.Add(player);
            }
            subCol.Add("-----"); subCol.Add("-----");
            sb1.AppendLine(string.Join(comma, subCol.ToArray()));
            foreach (string team in teams)
            {
                if (team != teamName)
                {
                    List<string> row = new List<string>();
                    row.Add(team);
                    double regResult1 = regression1.predict(rank.getTeamDefensiveRank1516(team));
                    double regResult2 = regression2.predict(rank.getTeamDefensiveRank1516(team));
                    double regResult3 = regression3.predict(rank.getTeamDefensiveRank1516(team));
                    row.Add(regResult1.ToString()); row.Add(regResult2.ToString()); row.Add(regResult3.ToString());
                    row.Add((classifier.Classify(new double[] { regResult1, regResult2, regResult3 })).ToString());
                    Console.WriteLine(regResult1 + " " + regResult2 + " " + regResult3 + " ");
                    Console.WriteLine(team + " " + classifier.Classify(new double[] { regResult1, regResult2, regResult3 }));
                    sb1.AppendLine(string.Join(comma, row.ToArray()));
                }
            }
            File.AppendAllText(filePath, sb1.ToString());
            Console.ReadKey();

        }

        
        
        
        
    }
}
