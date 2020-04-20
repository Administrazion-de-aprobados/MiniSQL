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


            sw.WriteLine("# TEST 1");

            DateTime timeStartTest1 = DateTime.Now;

            string create = filas[0];
            String[] linee = create.Split(',');
            string database = linee[0];
            string adminName = linee[1];    
            string adminPass = linee[2];

            User firstUser = new User(adminName, adminPass);

            DataBase DB = new DataBase(database, adminName, adminPass);
            DateTime test1 = DateTime.Now;
            TimeSpan Diff = test1 - timeStartTest1;
            double seconds = Diff.TotalMilliseconds / 1000.0;

            sw.WriteLine(Constants.CreateDatabaseSuccess + " (" + seconds + "s)");


            int j = 1;
            while (j < filas.Length && filas[j] != "")
            {
                DateTime timeStartSentence = DateTime.Now;

                string sentence = filas[j];

                string result = DB.output(sentence, firstUser);

                DateTime timeFinishSentence = DateTime.Now;
                TimeSpan timeDiffSentence = timeFinishSentence - timeStartSentence;
                double secondsSentence = timeDiffSentence.TotalMilliseconds / 1000.0;

                sw.WriteLine(result + " (" + secondsSentence + "s)");
               
                j++;
            }

            DateTime timeFinishTest1 = DateTime.Now;
            TimeSpan timeDiffTest1 = timeFinishTest1 - timeStartTest1;
            double secondsTest1 = timeDiffTest1.TotalMilliseconds / 1000.0;

            sw.WriteLine("TOTAL TIME: " + secondsTest1 + "s");
            sw.WriteLine(" ");

            j++;

            int k = 2;
            for(int i=j; i<filas.Length; i++)
            {

                DateTime timelogin1 = DateTime.Now; 

                string sentencee = filas[i];
                String[] line = sentencee.Split(',');
                string name = line[1];
                string pass = line[2];


                if (DB.existUser(name, pass))
                {
                    DateTime timelogin2 = DateTime.Now;
                    TimeSpan timeDiff = timelogin2 - timelogin1;
                    double secondslogin = timeDiff.TotalMilliseconds / 1000.0;

                    sw.WriteLine("# TEST " + k);
                    sw.WriteLine(Constants.OpenDatabaseSuccess + " (" + secondslogin + "s)");

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
                    DateTime timelogin2 = DateTime.Now;
                    TimeSpan timeDiff = timelogin2 - timelogin1;
                    double secondslogin = timeDiff.TotalMilliseconds / 1000.0;

                    sw.WriteLine("# TEST " + k);
                    k = k + 1;
                    sw.WriteLine(Constants.SecurityIncorrectLogin + " (" + secondslogin + "s)");
                    sw.WriteLine("TOTAL TIME: " + secondslogin + "s");
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
