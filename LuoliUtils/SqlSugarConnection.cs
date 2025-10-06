using LuoliUtils;


namespace LuoliHelper.Utils
{

    public class SqlSugarConnection
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public int DBType { get; set; }

        public bool RecordSql { get; set; } = false;

        public SqlSugarConnection()
        {

        }

        public SqlSugarConnection(string jsonFile)
        {
            ActionsOperator.TryCatchAction(() =>
            {
                var dbConnection = System.Text.Json.JsonSerializer.Deserialize<SqlSugarConnection>(System.IO.File.ReadAllText(jsonFile));
                Host = dbConnection.Host;
                Port = dbConnection.Port;
                User = dbConnection.User;
                Password = dbConnection.Password;
                Database = dbConnection.Database;
                RecordSql = dbConnection.RecordSql;
                DBType = dbConnection.DBType;

               Console.WriteLine($"parsed one database [{dbConnection.Host}] from [{jsonFile}]");

            },
            () =>
            {
                Console.WriteLine($"parsed to one database failed. Json file:[{jsonFile}]");
            });
        }

    }
}
