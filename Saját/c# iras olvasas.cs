using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace verseny2
{
    class Program
    {
        class Match
        {
            public List<string> Steps;

            public Match()
            {
                Steps = new List<string>();
            }
        }

        static List<Match> Matches = new List<Match>();


        static void Main(string[] args)
        {
            ReadFromFile();
            WriteToFile();

            //Test();

            //Ex1Apart();
            //Ex1Bpart();
            //Ex1Cpart();

            //Ex2Apart();
            //Ex2Bpart();
            //Ex2Cpart();
            //Ex2Dpart();
            //Ex2Epart();
            //Ex2Fpart();

            Console.ReadLine();
        }

        public static void ReadFromFile()
        {
            try
            {
                StreamReader matchReader = new StreamReader("jatszmak.txt");
                while (!matchReader.EndOfStream)
                {
                    List<string> StepsList = new List<string>();
                    string[] row = matchReader.ReadLine().Split('\t');
                    for (int i = 0; i < row.Length; i++)
                    {
                        StepsList.Add(row[i]);
                    }
                    Matches.Add(new Match());
                    StepsList.ForEach(steps =>
                    {
                        if (steps.Length > 1)
                        {
                            Matches[Matches.Count - 1].Steps.Add(steps);
                        }
                    });
                }
            }
            catch (IOException)
            {
                Console.WriteLine("a kurva anyádért nem tudod beolvasni");
            }
        }

        public static void WriteToFile()
        {
            StreamWriter matchWriter = new StreamWriter("atlathato.txt", false);
            Matches.ForEach(match =>
            {
                matchWriter.WriteLine("--------------{0}--------------", Matches.IndexOf(match) + 1);
                for (int i = 0; i < match.Steps.Count; i++)
                {
                    matchWriter.Write("{0} ", match.Steps[i]);
                }
                matchWriter.WriteLine("");
            });
            matchWriter.Close();
        }

        public static void Test()
        {
            List<int> sorozat = new List<int>();
            int szam = 1;
            for (int i = 1; i < 30; i++)
            {
                sorozat.Add(szam);
                szam *= 2;
            }

            Console.WriteLine(sorozat[8]);
        }




        public static void Ex1Apart()
        {
            List<int> sorozat = new List<int>();
            int szam = 1;
            for (int i = 1; i < 30; i++)
            {
                sorozat.Add(szam);
                szam *= 2;
            }

            Console.WriteLine(sorozat[8]);
        }


        public static void Ex1Bpart()
        {
            double szam = 10;
            Console.WriteLine(szam);
            for (int i = 0; i < 30; i++)
            {
                szam *= 2;
                Console.WriteLine(szam);
            }

            if (szam.ToString().Contains("216816212162121681684816816"))
            {
                Console.WriteLine("ez az");
            }
        }

        public static void Ex1Cpart()
        {
            string Number = "11";
            Console.WriteLine("{0}. : {1}", 1, Number);

            for (int i = 2; i < 32; i++)
            {
                int sum = 0;
                for (int x = Number.Length; x > 0; x--)
                {
                    sum += Convert.ToInt32(Number.Substring(Number.Length - x, 1));
                }

                Number += sum.ToString();
                Console.WriteLine("{0}. : {1}", i, Number);
            }
        }




        public static void Ex2Apart()
        {
            Matches.ForEach(match =>
            {
                //Console.WriteLine("------{0}------", Matches.IndexOf(match) + 1);
                if (match.Steps.Count % 2 == 1)
                {
                    Console.Write("s");
                }
                else
                {
                    Console.Write("v");
                }
                /*
                for (int i = 0; i < match.Steps.Count; i++)
                {
                    if (i % 2 == 1)
                    {
                        Console.WriteLine("s {0}", match.Steps[i]);
                    }
                    else
                    {
                        Console.WriteLine("v : {0}", match.Steps[i]);
                    }
                }
                */
            });
        }

        public static void Ex2Bpart()
        {
            int stepCount = 0;

            Matches.ForEach(match =>
            {
                match.Steps.ForEach(step =>
                {
                    if (step[0] == 'H')
                    {
                        stepCount += 4;
                    }
                });
            });

            Console.WriteLine(stepCount);
        }

        public static void Ex2Cpart()
        {
            Matches.ForEach(match =>
            {
                for (int i = 0; i < match.Steps.Count; i++)
                {
                    if (match.Steps[i].Contains('x') && LastPieceStandingOnCoordinate(Matches.IndexOf(match), i, match.Steps[i].Substring(match.Steps[i].Length - 2, 2)))
                    {
                        Console.Write("{0};", Matches.IndexOf(match) + 1);
                        break;
                    }
                }
            });
        }

        //Checking which figure is standing on the Coordinate
        public static bool LastPieceStandingOnCoordinate(int match, int step, string coord)
        {
            for (int i = 0; i < step; i++)
            {
                if (Matches[match].Steps[i].Substring(Matches[match].Steps[i].Length - 2, 2) == coord && Matches[match].Steps[i][0] == 'V')
                {
                    return true;
                }
            }

            return false;
        }


        public static void Ex2Dpart()
        {
            List<char> LetterCoord = new List<char>()
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
            };

            int allSteps = 0;

            Matches.ForEach(match =>
            {
                int matchWhiteSteps = 0;

                string lastWhiteCoord = "d1";

                //WhiteSteps 
                match.Steps.ForEach(step =>
                {
                    if (step[0] == 'V' && match.Steps.IndexOf(step) % 2 == 0)
                    {
                        if (step.Substring(step.Length - 1, 1)[0] != lastWhiteCoord[1] && step.Substring(step.Length - 2, 1)[0] == lastWhiteCoord[0])
                        {
                            //Moving vertically
                            matchWhiteSteps += Math.Abs(Convert.ToInt32(step.Substring(step.Length - 1, 1)[0]) - Convert.ToInt32(lastWhiteCoord[1]));
                        }
                        else if (step.Substring(step.Length - 2, 1)[0] != lastWhiteCoord[0] && step.Substring(step.Length - 1, 1)[0] == lastWhiteCoord[1])
                        {
                            //Moving horizontally
                            matchWhiteSteps += Math.Abs(LetterCoord.IndexOf(step.Substring(step.Length - 2, 1)[0]) - LetterCoord.IndexOf(lastWhiteCoord[0]));
                        }
                        else if (step.Substring(step.Length - 2, 1)[0] != lastWhiteCoord[0] && step.Substring(step.Length - 1, 1)[0] != lastWhiteCoord[1])
                        {
                            //Moving across
                            matchWhiteSteps += (Math.Abs(Convert.ToInt32(step.Substring(step.Length - 1, 1)[0]) - Convert.ToInt32(lastWhiteCoord[1])) + Math.Abs(LetterCoord.IndexOf(step.Substring(step.Length - 2, 1)[0]) - LetterCoord.IndexOf(lastWhiteCoord[0]))) / 2;
                        }

                        lastWhiteCoord = step.Substring(step.Length - 2, 2);
                    }
                });

                //DarkSteps

                int matchDarkSteps = 0;

                string lastDarkCoord = "d8";

                match.Steps.ForEach(step =>
                {
                    if (step[0] == 'V' && match.Steps.IndexOf(step) % 2 == 1)
                    {
                        if (step.Substring(step.Length - 1, 1)[0] != lastDarkCoord[1] && step.Substring(step.Length - 2, 1)[0] == lastDarkCoord[0])
                        {
                            //Moving vertically
                            matchDarkSteps += Math.Abs(Convert.ToInt32(step.Substring(step.Length - 1, 1)[0]) - Convert.ToInt32(lastDarkCoord[1]));
                        }
                        else if (step.Substring(step.Length - 2, 1)[0] != lastDarkCoord[0] && step.Substring(step.Length - 1, 1)[0] == lastDarkCoord[1])
                        {
                            //Moving horizontally
                            matchDarkSteps += Math.Abs(LetterCoord.IndexOf(step.Substring(step.Length - 2, 1)[0]) - LetterCoord.IndexOf(lastDarkCoord[0]));
                        }
                        else if (step.Substring(step.Length - 2, 1)[0] != lastDarkCoord[0] && step.Substring(step.Length - 1, 1)[0] != lastDarkCoord[1])
                        {
                            //Moving across
                            matchDarkSteps += (Math.Abs(Convert.ToInt32(step.Substring(step.Length - 1, 1)[0]) - Convert.ToInt32(lastDarkCoord[1])) + Math.Abs(LetterCoord.IndexOf(step.Substring(step.Length - 2, 1)[0]) - LetterCoord.IndexOf(lastDarkCoord[0]))) / 2;
                        }

                        lastDarkCoord = step.Substring(step.Length - 2, 2);
                    }
                });

                //Adding up
                allSteps += matchWhiteSteps;
                allSteps += matchDarkSteps;
            });

            Console.WriteLine(allSteps);
        }

        public static void Ex2Epart()
        {
            int allMatches = 21;
            int countOfMatches = 0;

            Matches.ForEach(match =>
            {
                for (int i = 0; i < match.Steps.Count; i++)
                {
                    if (match.Steps[i][0] == 'K' || match.Steps[i][0] == 'O' && i % 2 == 0)
                    {
                        Console.WriteLine(match.Steps[i][0]);
                        countOfMatches++;
                        break;
                    }
                }
            });

            Console.WriteLine(allMatches - countOfMatches);
        }

        public static void Ex2Fpart()
        {
            int countOfMatches = 0;

            Matches.ForEach(match =>
            {
                int countOfDeaths = 0;

                match.Steps.ForEach(step =>
                {
                    if (step.Contains('x'))
                    {
                        countOfDeaths++;
                    }
                });

                if (countOfDeaths < 16)
                {
                    countOfMatches++;
                }
            });

            Console.WriteLine(countOfMatches);
        }
    }
}