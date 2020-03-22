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


            Sentence sentence = null;

            String patterSelect = "SELECT\\s(\\*|\\w+(?:,*\\w+)*)\\sFROM\\s(\\w+)\\sWHERE\\s(\\w+[<|=|>]\\w+)";
            String patterDelete = "DELETE\\sFROM\\s(\\w+)\\sWHERE\\s(\\w+[<|=|>]\\w+)";
            String patternInsert = "INSERT\\sINTO\\s(\\w+)\\sVALUES\\s\\((\\w+(?:,?\\w+)*)\\)";
            String patternUpdate = "UPDATE\\s(\\w+)\\sSET\\s(\\w+=\\w+(?:,?\\w+=\\w+)*)\\sWHERE\\s(\\w+[<|=|>]\\w+)";
            String patternCreateTable = "CREATE\\sTABLE\\s(\\w+)\\s\\((\\w+\\s[TEXT|INT|DOUBLE]+(?:,?\\s\\w+\\s[TEXT|INT|DOUBLE]+)*)\\)";
            String patterDropTable = "DROP\\sTABLE\\s(\\w+)";


            // For the select
            if (Regex.IsMatch(sentenc, patterSelect))
            {
                Match match = Regex.Match(sentenc, patterSelect);

                List<String> list = new List<String>();

                // List of columns
                list = stringToList(match.Groups[1].Value, ',');
                
                // Table name
                String table = match.Groups[2].Value;

                // Where creation
                Where where = stringToWhere(match.Groups[3].Value);

                // Select creation
                sentence = new Select(table, list, where);
            }


            // For the delete
            if(Regex.IsMatch(sentenc, patterDelete))
            {
                Match match = Regex.Match(sentenc, patterDelete);
                String table = match.Groups[1].Value;
                Where where = stringToWhere(match.Groups[2].Value);

                sentence = new Delete(table, where);
            }

            // For the insert
            if(Regex.IsMatch(sentenc, patternInsert))
            {
                Match match = Regex.Match(sentenc, patternInsert);
                String table = match.Groups[1].Value;

                List<String> list = new List<String>();
                // List of columns
                list = stringToList(match.Groups[2].Value, ',');

                sentence = new Insert(table, list);
            }

            // For the update 
            if (Regex.IsMatch(sentenc, patternUpdate))
            {
                Match match = Regex.Match(sentenc, patternUpdate);
                String table = match.Groups[1].Value;


                List<String> list = new List<String>();

                String valuees = match.Groups[2].Value;
                // List of columns
                var tuple = listToTwoList(stringToList(valuees, ','));

                List<string> colum = tuple.Item1;
                List<string> values = tuple.Item2;

                Where where = stringToWhere(match.Groups[3].Value);

                sentence = new Update(table, colum, values, where);
            }

            //For the create table
            if(Regex.IsMatch(sentenc, patternCreateTable))
            {

                Match match = Regex.Match(sentenc, patternCreateTable);
                String table = match.Groups[1].Value;

                List<String> list = new List<String>();
                // List of columns
                list = stringToList(match.Groups[2].Value, ',');

                sentence = new CreateTable(table, list);
            }

            // For the drop table
            if(Regex.IsMatch(sentenc, patterDropTable))
            {
                Match match = Regex.Match(sentenc, patterDropTable);
                String table = match.Groups[1].Value;

                sentence = new DropTable(table);

            }

            return sentence;
        }


        public static Operator stringToOperator(String op)
        {
            Operator operatoor = Operator.Equal;

            if (op == "=")
            { 
                   operatoor = Operator.Equal;
            }
            else if (op == ">" )
            {
                operatoor = Operator.Greater;
            }
            else if (op == "<" )
            {
                operatoor = Operator.Less;
            }

            return operatoor;
        }

        public static Where stringToWhere(String input)
        {
            String column = input[0].ToString();
            String ooperator = input[1].ToString();
            String valueToCompare = input[2].ToString();

            Operator op = stringToOperator(ooperator);

            Where where = new Where(column, op, valueToCompare);

            return where;
        }

        public static List<String> stringToList(String input, Char spliter)
        {
            List<String> list = new List<string>();


            String[] splitedInput = input.Split(spliter);

            foreach (string i in splitedInput)
            {

                string newi = i.Trim();
                list.Add(newi);
            }


            return list;
        }

        public static Tuple<List<string>, List<string>> listToTwoList (List<string> list)
        {
            List<string> colum = new List<string>();
            List<string> values = new List<string>();

            foreach(string i in list)
            {
                String[] splitedInput = i.Split('=');

                colum.Add(splitedInput[0]);
                values.Add(splitedInput[1]);
            }

            return Tuple.Create(colum, values);
        }

    }
}
