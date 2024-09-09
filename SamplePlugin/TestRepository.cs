using HogWarp.Lib.System;
using MySqlConnector;

namespace SamplePlugin
{
    internal class TestRepository
    {

        private MySqlRepository _repository;
        public TestRepository(MySqlRepository repository) 
        {
            _repository = repository;
        }
    }
}
