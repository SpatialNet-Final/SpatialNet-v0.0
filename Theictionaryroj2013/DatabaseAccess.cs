using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SQLite;

namespace TheDictionaryTrainingSetProj2013
{


    class DatabaseAccess
    {
        
        static string ConnectionString()
        {
            return "";
        }

        public static object SelectRows_WordsIntuitiveCloud(string query)
        {
            
            object ros= new object();

            return ros;
        }

        public static int SelectNumberOfRows_WordsIntuitiveCloud()
        {
            string queryString = "SELECT count(*) from WordsIntuitiveCloud";
            int ros;
            using (IDbConnection conn = new SQLiteConnection(ConnectionString()))
            {
                ros = conn.Query(queryString).Count();
            }


            return ros;
        }



        public static int UpdateRows_WordsIntuitiveCloud()
        {
            int numRows = 0;

            return numRows;
        }



        static int InsertRows_WordsIntuitiveCloud()
        {
            int numRows = 0;

            return numRows;
        }


    }
}
