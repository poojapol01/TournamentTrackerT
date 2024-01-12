using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TrackerLibrary.DataAccess
{
    public static class GlobalConfig
    {
        public static IDataConnection connection { get; private set; }

        public static void InitializeConnections(DatabaseType db)
        {
            switch (db)
            {
                case DatabaseType.Sql:
                    //TODO: Set up the SQL Connector Properly
                    SqlConnector sql = new SqlConnector();
                    connection = sql;
                    break;

                case DatabaseType.TextFile:
                    //TODO: Create the Text Connection
                    TextConnector text = new TextConnector();
                    connection = text;
                    break;

                default:
                    break;
            }
        }

        public static string cnnstring(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
