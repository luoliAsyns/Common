using SqlSugar;
using LuoliCommon.Logger;

namespace LuoliDatabase
{

    public class DBConnector
    {

        private readonly ILogger _logger;
        public DBConnector(ILogger logger)
        {
            _logger = logger;
        }
        public DBConnector()
        {

        }

        public SqlSugarScope? SqlClient { get; set; }

        public string? Host { get; set; }
        public int Port { get; set; } = 3306;
        public string? User { get; set; }
        public string? Password { get; set; }
        public string? Database { get; set; }

        public int? DBType { get; set; }

        public bool RecordSql { get; set; } = false;



        public DBConnector(string jsonFile, ILogger logger)
        {
            _logger = logger;
            try
            {
                var dbConnection = System.Text.Json.JsonSerializer.Deserialize<DBConnector>(System.IO.File.ReadAllText(jsonFile));

                SqlClient = new SqlSugarScope(
                                new ConnectionConfig()
                                {
                                    ConnectionString = $"server={dbConnection.Host}; port={dbConnection.Port}; user id={dbConnection.User}; password={dbConnection.Password}; database={dbConnection.Database};AllowLoadLocalInfile=true;",
                                    DbType = (DbType)dbConnection.DBType,
                                    IsAutoCloseConnection = true,
                                },
                               db =>
                               {
                                   //每次Sql执行前事件
                                   db.Aop.OnLogExecuting = (sql, pars) =>
                                   {
                                       if (dbConnection.RecordSql)
                                           _logger.Debug($"SQL: {sql}");
                                   };
                               }
                            );

                _logger.Info($"Connected to one database [{dbConnection.Host}] from [{jsonFile}]");

            }
            catch (Exception ex)
            {
                _logger.Error($"Connected to one database failed. Json file:[{jsonFile}]");
                _logger.Error(ex.Message);
            }
        }
    }
}