using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    public class Query
    {

        public static Sentence parse(String sentenc)
        {
            String patterSelect = "SELECT\\s(\\*|\\w+[,*\\w+]*)\\sFROM\\s(\\w*)\\sWHERE\\s(\\w+[<|=|>]\\w+)";
            String patterDelete = "DELETE\\sFROM\\s\\w+\\sWHERE\\s(\\w+[<|=|>]\\w+)";
            String patternInsert = "INSERT\\sINTO\\s\\w+\\sVALUES\\s\\(\\w+(\\w*,*)*\\)";
            String patternUpdate = "UPDATE\\s\\w+\\sSET\\s\\((\\w+=\\w+)(,*(\\w+=\\w+))*\\)\\sWHERE\\s(\\w+[<|=|>]\\w+)";

            if (Regex.IsMatch(sentenc, patterSelect))
            {
                Match match = Regex.Match(sentenc, patterSelect);

                List<String> list = new List<String>();

                String columns = match.Groups[1].Value;
                String[] split = columns.Split(',');
                foreach (string i in split)
                {
                    list.Add(i);
                }


            

            }




            return null;
        }
    }
}
