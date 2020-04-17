using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MiniSQL
    {

        public static void tester(string inputFile, string outputName)
        {
            string[] filas = File.ReadAllLines(inputFile);

            StreamWriter sw = File.CreateText(outputName);
            DataBase DB = new DataBase();

            int k = 1;
            for(int i=0; i<filas.Length; i++)
            {
                string sentencee = filas[i];
                String[] line = sentencee.Split(',');
                string name = line[1];
                string pass = line[2];


                if (DB.existUser(name, pass))
                {
                    sw.WriteLine("# TEST " + k);
                    sw.WriteLine("Success");

                    User user = DB.Users[name];

                    DateTime timeStartTest = DateTime.Now;


                    i = i + 1;
                    while (i < filas.Length && filas[i] != "")
                    {
                        DateTime timeStartSentence = DateTime.Now;

                        string sentence = filas[i];

                        string result = DB.output(sentence, user);

                        DateTime timeFinishSentence = DateTime.Now;
                        TimeSpan timeDiffSentence = timeFinishSentence - timeStartSentence;
                        double secondsSentence = timeDiffSentence.TotalMilliseconds / 1000.0;

                        sw.WriteLine(result + " (" + secondsSentence + "s)");
                        i++;
                    }

                    DateTime timeFinishTest = DateTime.Now;
                    TimeSpan timeDiffTest = timeFinishTest - timeStartTest;
                    double secondsTest = timeDiffTest.TotalMilliseconds / 1000.0;

                    sw.WriteLine("TOTAL TIME: " + secondsTest + "s");
                    sw.WriteLine(" ");
                    k = k + 1;
                }
                else
                {
                    sw.WriteLine("# TEST " + k);
                    sw.WriteLine(Constants.SecurityIncorrectLogin);
                    sw.WriteLine(" ");
                    i = i + 1;
                    while (i < filas.Length && filas[i] != "")
                    {
                        i++;
                    }
                }
            }
            sw.Close();
        }

    }
}
