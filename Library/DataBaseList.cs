using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DataBaseList
    {
        private static DataBaseList mDataBaseList = new DataBaseList();
        private Dictionary<string, DataBase> DataBaseList;

        public DataBaseList()
        {
            DataBaseList = new Dictionary<string, DataBase>();
        }

        public static DataBaseList GetDataBaseList()
        {
            return mDataBaseList;
        }


    }
}
