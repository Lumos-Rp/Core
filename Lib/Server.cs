using System.Runtime.CompilerServices;
using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.System.Models;
using HogWarp.Lib.Engine.Event;
using HogWarp.Lib.Game.Avatar;
using HogWarp.Lib.System;
using HogWarp.Lib.Game;

[assembly: InternalsVisibleTo("HogWarp.Loader")]
namespace HogWarp.Lib
{
    public class Server
    {
        private static Server? _instance;

        public readonly World world;
        public readonly PlayerManager playerManager;
        public CommandHandler commandHandler = new CommandHandler();
        public EventManager eventManager;
        public MySqlRepository repository;

        internal Server(World world, PlayerManager playerManager)
        {
            ConfigurationReader<DatabaseConfiguration> configurationReader = new ConfigurationReader<DatabaseConfiguration>("database.json");
            repository = new MySqlRepository(configurationReader.ReadFile()!);
            this.world = world;
            this.playerManager = playerManager;
            eventManager = new EventManager(commandHandler);
            _instance = this;
        }

        public static Server GetInstance()
        {
            return _instance!;
        }
    }
}