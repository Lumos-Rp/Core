using HogWarp.Lib.Engine.Plugin;
using HogWarp.Lib.Game.Avatar;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib.System;
using HogWarp.Lib;

namespace SamplePlugin
{
    public class SamplePluginMain : IPluginBase
    {
        public string name => "SamplePlugin";
        public string description => "This SamplePlugin shows how to create a basic plugin";
        private Logger _logger = new Logger("SamplePlugin");
        public void Initialize()
        {
            Server.GetInstance().eventManager.playerJoinEventHandler.playerJoinEvent += PlayerJoinEventHandler;
            TestRepository mySqlConnector = new TestRepository(Server.GetInstance().repository);
        }

        public void PlayerJoinEventHandler(Player player)
        {
            _logger.Warning($"{player.name} Join on the server.");
        }
    }
}
