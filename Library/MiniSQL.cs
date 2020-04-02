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

            int k = 1;
            for(int i=0; i<filas.Length; i++)
            {
                DataBase DB = new DataBase();

                DateTime timeStartTest = DateTime.Now;
                
                sw.WriteLine("# TEST "+k);

                int num = filas.Length;

                while (i < num && filas[i] != "")
                {
                    DateTime timeStartSentence = DateTime.Now;

                    string sentence = filas[i];

                    string result = DB.output(sentence);

                    DateTime timeFinishSentence = DateTime.Now;
                    TimeSpan timeDiffSentence =  timeFinishSentence - timeStartSentence;
                    double secondsSentence = timeDiffSentence.TotalMilliseconds / 1000.0;

                    sw.WriteLine(result+" ("+secondsSentence+"s)");
                    i++;
                }

                DateTime timeFinishTest = DateTime.Now;
                TimeSpan timeDiffTest = timeFinishTest - timeStartTest;
                double secondsTest = timeDiffTest.TotalMilliseconds / 1000.0;

                sw.WriteLine("TOTAL TIME: " + secondsTest+"s");
                sw.WriteLine(" ");
                k = k + 1;
            }
            sw.Close();
        }

    }
}
