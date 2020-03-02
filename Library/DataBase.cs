using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Operator { Greater, Less, Equal};
    public enum Type {Text, Int, Double};


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

        public Boolean searchColumnName(string name)
        {
            return false;
        }

        public void deleteColumn(string tablename, string name)
        {

        }

        public Dictionary<string, Column> select (IList<string> columnsNames, string tableName, string columnName, Operator op, string dataToCompare)
        {
            return null;
        }
        public void deleteData(string tableName,string columnName, Operator op, string ValueToCompare) 
        {  
        
        }

        public void update(string tableName, string columnName, string dataToUpdate, Operator op, string valueToCompare )
        { 
        
        }

        public void insert(string nameTable, string nameCol, string dataToInsert) 
        { 
        
        }

        public IList<int> where(string tableName, string columnName, Type type, string valueToCompare)
        {
            return null;
        }





        public void write()
        {
             
            if(File.Exists(Name))
            {
                
                deletefile(Name);
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
                    string line = columnName + "," + column.ColumnType;

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

       public void load(string txtName) 
        {

            
            if(File.Exists(txtName)){

                string[] filas = File.ReadAllLines(txtName);

                string[] line1 = filas[0].Split(',');

                string adminName = line1[0];
               
                string pass = line1[1];

                Admin admiin = new Admin(adminName,pass);

                admin = admiin;
                Boolean newTable=true;

                for (int i =1; i<filas.Length; i++) {
                    Table tab = null;
                    string tableName;

                    if (filas[i] == "")
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

                            String[] line = filas[i + 1].Split(',');
                            string columnName = line[0];
                            string datat = line[1];
                            datat = datat.ToLower();
                            List<string> data = new List<string>();

                            Type tp = dataType(datat);
                            for (int j = 2; j < line.Length; j++)
                            {

                                data.Add(line[j]);
                            }

                            tab.addToTable(columnName, tp, data);
                            Tables.Add(tableName, tab);
                        }
                     
                    }
                   
                    }

                }


            
        
       }

        public Type dataType(string loadData) {

            Type tipo = Type.Text;
            if (loadData == "text")
            {

                tipo = Type.Text;
            }
            else if (loadData == "int")
            {
                tipo = Type.Int;
            }
            else {

                tipo = Type.Double;
                    }



            return tipo;

        }
        public void deletefile(string name)
        {
            File.Delete(name);
        }
    }
}
