using MySqlConnector;
using System.Text.RegularExpressions;

namespace HogWarp.Lib.System.Models
{


    public class RepositoryCommand
    {
        private string _commandText { get; set; }
        private Dictionary<MySqlParameter, bool> _parameters { get; } = new Dictionary<MySqlParameter, bool>();

        public RepositoryCommand(string commandText)
        {
            _commandText = commandText;
        }

        public void AddParameter(string parameterName, bool isString, object value)
        {
            _parameters.Add(new MySqlParameter(parameterName,value), isString);
        }

        public void ClearParameters()
        {
            _parameters.Clear();
        }

        public string GetQuery()
        {
            string queryString = _commandText;

            foreach (MySqlParameter parameter in _parameters.Keys)
            {
                string parameterValue = parameter.Value?.ToString() ?? "NULL";

                if (_parameters.GetValueOrDefault(parameter))
                {
                    parameterValue = $"'{parameterValue}'";
                }

                queryString = Regex.Replace(queryString, $@"\?{parameter.ParameterName}\b", parameterValue);
            }

            return queryString;
        }

        public MySqlCommand GetCommand()
        {
            MySqlCommand cmd = new MySqlCommand(_commandText);

            foreach (MySqlParameter parameter in _parameters.Keys)
            {
                cmd.Parameters.Add(new MySqlParameter(parameter.ParameterName, parameter.Value));
            }

            return cmd;
        }
    }
}
