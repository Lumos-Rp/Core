using HogWarp.Lib.System.Models;
using MySqlConnector;
using System.Data;

namespace HogWarp.Lib.System
{
    public class MySqlRepository : IDisposable
    {
        private MySqlConnection _connection;
        public readonly DatabaseConfiguration databaseConfiguration;
        private Logger _logger = new Logger("MySqlRepository");

        internal MySqlRepository(DatabaseConfiguration configuration)
        {
            databaseConfiguration = configuration;
            var connectionString = new MySqlConnectionStringBuilder
            {
                Server = configuration.host,
                Port = Convert.ToUInt32(configuration.port),
                UserID = configuration.user,
                Password = configuration.password,
                Database = configuration.database,
                SslMode = MySqlSslMode.None
            }.ToString();

            _connection = new MySqlConnection(connectionString);

            CheckHealthConnection();
        }

        public void CheckHealthConnection()
        {
            try
            {
                _connection.Open();
                _logger.Successful($"Connected to the database {databaseConfiguration.database} successfully.");
                _connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to connect to the database: {ex.ToString()}");
            }
        }

        public void ExecuteNonQuery(RepositoryCommand cmd)
        {
            try
            {
                _connection.Open();
                MySqlCommand mysqlCommand = cmd.GetCommand();
                mysqlCommand.Connection = _connection;
                mysqlCommand.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to ExecuteNonQuery: {ex.ToString()}");
            }
        }

        public Dictionary<string, object>? ExecuteReaderOne(RepositoryCommand cmd)
        {
            Dictionary<string, object>? result = null;

            try
            {
                _connection.Open();
                MySqlCommand mysqlCommand = cmd.GetCommand();
                mysqlCommand.Connection = _connection;

                using (var reader = mysqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = GetRowData(reader);
                    }
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to ExecuteReaderOne: {ex.ToString()}");
            }

            return result;
        }

        public List<Dictionary<string, object>>? ExecuteReader(RepositoryCommand cmd)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            try
            {
                _connection.Open();
                MySqlCommand mysqlCommand = cmd.GetCommand();
                mysqlCommand.Connection = _connection;

                using (var reader = mysqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(GetRowData(reader));
                    }
                }

                _connection.Close();

            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to ExecuteReader: {ex.ToString()}");
            }

            return results.Count > 0 ? results : null;
        }

        private Dictionary<string, object> GetRowData(MySqlDataReader reader)
        {
            var row = new Dictionary<string, object>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string columnName = reader.GetName(i);
                object columnValue = reader.GetValue(i);
                row[columnName] = columnValue;
            }

            return row;
        }

        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
