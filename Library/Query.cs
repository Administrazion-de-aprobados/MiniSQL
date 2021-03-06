﻿
using Library.Sentences.Security;
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

            String patterSelect = "SELECT\\s(\\*|\\w+(?:,*\\w+)*)\\sFROM\\s(\\w+)\\sWHERE\\s(\\w+)([<|=|>])('[^',]+'|-?\\d+\\.?\\d*);";
            String patternSelectAll = "SELECT\\s(\\*|\\w+(?:,*\\w+)*)\\sFROM\\s(\\w+);";
            String patterDelete = "DELETE\\sFROM\\s(\\w+)\\sWHERE\\s(\\w+)([<|=|>])('[^',]+'|-?\\d+\\.?\\d*);";
            String patternInsert = "INSERT\\sINTO\\s(\\w+)\\sVALUES\\s\\(((?:'[^',]+'|-?\\d+\\.?\\d*)(?:,(?:'[^',]+'|-?\\d+\\.?\\d*))*)\\);";
            String patternUpdate = "UPDATE\\s(\\w+)\\sSET\\s(\\w+=(?:'[^',]+'|-?\\d+\\.?\\d*)(?:,?\\w+=(?:'[^',]+'|-?\\d+\\.?\\d*))*)\\sWHERE\\s(\\w+)([<|=|>])('[^',]+'|-?\\d+\\.?\\d*);";
            String patternCreateTable = "CREATE\\sTABLE\\s(\\w+)\\s\\((\\w+\\s[TEXT|INT|DOUBLE]+(?:,?\\w+\\s[TEXT|INT|DOUBLE]+)*)\\);";
            String patterDropTable = "DROP\\sTABLE\\s(\\w+);";

            String patternCreateSecurityProfile = "CREATE\\sSECURITY\\sPROFILE\\s(\\w+);";
            String patternDropSecurityProfile = "DROP\\sSECURITY\\sPROFILE\\s(\\w+);";
            String patternGrantPrivilege = "GRANT\\s(DELETE|INSERT|SELECT|UPDATE)\\sON\\s(\\w+)\\sTO\\s(\\w+);";
            String patternRevokePrivilege = "REVOKE\\s(DELETE|INSERT|SELECT|UPDATE)\\sON\\s(\\w+)\\sTO\\s(\\w+);";
            String patternAddUser = "ADD\\sUSER\\s\\('(\\w+)','(\\w+)',(\\w+)\\);";
            String patternDeleteUser = "DELETE\\sUSER\\s(\\w+);";


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

                Operator op = stringToOperator(match.Groups[4].Value);

                Where where = new Where(match.Groups[3].Value, op, match.Groups[5].Value);

                // Select creation
                sentence = new Select(table, list, where);
            }

            // For the selectAll
            else if (Regex.IsMatch(sentenc, patternSelectAll))
            {
                Match match = Regex.Match(sentenc, patternSelectAll);

                List<String> list = new List<String>();

                // List of columns
                list = stringToList(match.Groups[1].Value, ',');

                // Table name
                String table = match.Groups[2].Value;

                sentence = new SelectAll(table, list);

            }

            // For the delete
            else if (Regex.IsMatch(sentenc, patterDelete))
            {
                Match match = Regex.Match(sentenc, patterDelete);
                String table = match.Groups[1].Value;

                Operator op = stringToOperator(match.Groups[3].Value);

                Where where = new Where(match.Groups[2].Value, op, match.Groups[4].Value);

                sentence = new Delete(table, where);
            }

            // For the insert
            else if (Regex.IsMatch(sentenc, patternInsert))
            {
                Match match = Regex.Match(sentenc, patternInsert);
                String table = match.Groups[1].Value;

                List<String> list = new List<String>();
                // List of columns
                list = stringToList(match.Groups[2].Value, ',');

                sentence = new Insert(table, list);
            }

            // For the update 
            else if (Regex.IsMatch(sentenc, patternUpdate))
            {
                Match match = Regex.Match(sentenc, patternUpdate);
                String table = match.Groups[1].Value;


                List<String> list = new List<String>();

                String valuees = match.Groups[2].Value;
                // List of columns
                var tuple = listToTwoList(stringToList(valuees, ','));

                List<string> colum = tuple.Item1;
                List<string> values = tuple.Item2;


                Operator op = stringToOperator(match.Groups[4].Value);

                Where where = new Where(match.Groups[3].Value, op, match.Groups[5].Value);

                sentence = new Update(table, colum, values, where);
            }

            //For the create table
            else if (Regex.IsMatch(sentenc, patternCreateTable))
            {

                Match match = Regex.Match(sentenc, patternCreateTable);
                String table = match.Groups[1].Value;

                List<String> list = new List<String>();
                // List of columns
                list = stringToList(match.Groups[2].Value, ',');

                sentence = new CreateTable(table, list);
            }

            // For the drop table
            else if (Regex.IsMatch(sentenc, patterDropTable))
            {
                Match match = Regex.Match(sentenc, patterDropTable);
                String table = match.Groups[1].Value;

                sentence = new DropTable(table);
            }


            // For the Create security profile
            else if (Regex.IsMatch(sentenc, patternCreateSecurityProfile))
            {

                Match match = Regex.Match(sentenc, patternCreateSecurityProfile);
                String secutiryProfile = match.Groups[1].Value;


                sentence = new CreateSecurityProfile(secutiryProfile);
            }

            // For the Drop security profile
            else if (Regex.IsMatch(sentenc, patternDropSecurityProfile))
            {

                Match match = Regex.Match(sentenc, patternDropSecurityProfile);
                String secutiryProfile = match.Groups[1].Value;

                sentence = new DropSecurityProfile(secutiryProfile);
            }

            // For grant privilege
            else if (Regex.IsMatch(sentenc, patternGrantPrivilege))
            {

                Match match = Regex.Match(sentenc, patternGrantPrivilege);

                string typeString = match.Groups[1].Value;
                Privilege type = stringToType(typeString);

                string table = match.Groups[2].Value;
                string securityProfile = match.Groups[3].Value;

                sentence = new GrantPrivilege(type, table, securityProfile);
            }

            // For revoke privilege
            else if (Regex.IsMatch(sentenc, patternRevokePrivilege))
            {

                Match match = Regex.Match(sentenc, patternRevokePrivilege);

                string typeString = match.Groups[1].Value;
                Privilege type = stringToType(typeString);

                string table = match.Groups[2].Value;
                string securityProfile = match.Groups[3].Value;

                sentence = new RevokePrivilege(type, table, securityProfile);
            }

            // For Add user
            else if (Regex.IsMatch(sentenc, patternAddUser))
            {

                Match match = Regex.Match(sentenc, patternAddUser);
                string name = match.Groups[1].Value;
                string pass = match.Groups[2].Value;
                string securityProfile = match.Groups[3].Value;

                sentence = new AddUser(name, pass, securityProfile);
            }

            // For delete user
            else if (Regex.IsMatch(sentenc, patternDeleteUser))
            {
                Match match = Regex.Match(sentenc, patternDeleteUser);
                string name = match.Groups[1].Value;

                sentence = new DeleteUser(name);
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
            else if (op == ">")
            {
                operatoor = Operator.Greater;
            }
            else if (op == "<")
            {
                operatoor = Operator.Less;
            }

            return operatoor;
        }


        public static List<String> stringToList(String input, Char spliter)
        {
            List<String> list = new List<string>();


            String[] splitedInput = input.Split(spliter);

            foreach (string i in splitedInput)
            {

                string newi = i.Trim();
                string newi2 = newi.Replace(@"'", "");
                list.Add(newi2);
            }


            return list;
        }

        public static Tuple<List<string>, List<string>> listToTwoList(List<string> list)
        {
            List<string> colum = new List<string>();
            List<string> values = new List<string>();

            foreach (string i in list)
            {
                String[] splitedInput = i.Split('=');

                colum.Add(splitedInput[0]);
                values.Add(splitedInput[1]);
            }

            return Tuple.Create(colum, values);
        }



        public static Privilege stringToType(string typeString)
        {
            Privilege type = Privilege.DELETE;

            if (typeString.Equals("DELETE"))
            {

                type = Privilege.DELETE;

            }
            else if (typeString.Equals("UPDATE"))
            {

                type = Privilege.UPDATE;

            }
            else if (typeString.Equals("SELECT"))
            {

                type = Privilege.SELECT;

            }
            else if (typeString.Equals("INSERT"))
            {

                type = Privilege.INSERT;

            }

            return type;
        }


    }

}
