using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Operator { Greater, Less, Equal};


    public class DataBase
    {
        public Dictionary<string, User> Users;
        public Dictionary<string, Table> Tables;
        private Admin admin;
        public string Name;

        public DataBase(string name, string adminName, string pass)
        {
            Users = new Dictionary<string, User>();
            Tables = new Dictionary<string, Table>();

            Name = name;
            admin = new Admin(adminName, pass);

        }
        public DataBase()
        {
            Users = new Dictionary<string, User>();
            Tables = new Dictionary<string, Table>();
        }



        public void createTable(string name)
        {
            Table table = new Table(name);
            Tables.Add(name, table);
        }

        public void dropTable(string name)
        {
            Tables.Remove(name);
        }

        public Boolean searchColumnName(string name) {
            return false;
        }

        public void deleteColumn(string tablename, string name) { 
        
        }

        public void update(string tableName, string columnName, string dataToUpdate, Operator op, String valueToCompare ) { 
        
        }

        public void insert(string nameTable, string nameCol, string dataToInsert) { 
        
        }

        public void writte()
        {
             
            if(File.Exists(Name))
            {
                
                deletefile();
            }

            //create the txt
            StreamWriter sw = File.CreateText(Name);

            //admin name and password for the txt
            sw.WriteLine(admin.Name + "," + admin.Password);
            
            //iterates the keys from the hastable
            foreach(KeyValuePair<string, Table> entry in Tables)
            {
                string tableName = entry.Key;
                sw.WriteLine(tableName);

                Table table = entry.Value;
                foreach(KeyValuePair<string, Column> entry2 in table.Columns)
                {
                    string columnName = entry2.Key; //the key from the line 
                    Column column = entry2.Value;//the value from the line 
                    string line = columnName + "," + column.columnType + ",";

                    foreach(string value in column.list)
                    {
                         line = line +","+ value;
                    }
                    sw.WriteLine(line);


                }
                sw.WriteLine(" ");

            }
            sw.Close();

        }

       public void load(string txtName) {

            
            if(File.Exists(txtName)){

                string[] filas = File.ReadAllLines(txtName);

                string[] line1 = filas[0].Split(',');

                string adminName = line1[0];

                string pass = line1[1];

                Boolean newTable=true;

                for (int i =1; i<=filas.Length; i++) {
                    Table tab = null;
                    string tableName;

                    if (filas[i] == " ")
                    {
                        newTable = true;
                    }

                    else
                    {
                        if (newTable == true)
                        {

                            tableName = filas[i];
                            newTable = false;
                            tab = new Table(tableName);

                        }




                        String[] line = filas[i].Split(',');
                        string columnName = line[0];
                        string dataType = line[1];
                        List<string> data = new List<string>();
                        for (int j = 2; j <= line.Length; j++)
                        {

                            data.Add(line[j]);
                        }

                        tab.addToTable(columnName, data);


                    }
                    


                    
                    
                    }

                }


            
        
       }
        public void deletefile(){
        
        
        }

        public void BDcreatioon(DataBase database)
        {
            
            database.createTable("table");
            Table table;

            if (database.Tables.ContainsKey("table"))
            {
                table = database.Tables["table"];
                table.createColumn("column");

            }

            database.writte();

        }
    }
}
