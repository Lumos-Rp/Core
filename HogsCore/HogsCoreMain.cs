using AdministrationTools.Commands;
using Hogs.Core.EventHandler;
using HogWarp.Lib.Engine.Plugin;
using HogWarp.Lib.Game.Data;
using SamplePlugin;

namespace Hogs.Core
{
    public class HogsCoreMain : IPluginBase
    {
        public string name => "GLSCore";
        public string description => "This SamplePlugin shows how to create a basic plugin";
        private PlayerRegistry _playerRegistry;
        public void Initialize()
        {
            _playerRegistry = new PlayerRegistry();
            WeatherCommand _weatherCommand = new WeatherCommand("weather", "Allows you to change the server climateAllows you to change the server weather", ERole.None);
            PositionCommand _positionCommand = new PositionCommand("pos", "Get yours Vector3 Coords.", ERole.None);
            IdCommand _idCommand = new IdCommand("id", "Get your Player Id", ERole.None);
            DateCommand _dateCommand = new DateCommand("date", "Get current time server", ERole.None);
            TeleportCommand _teleportCommand = new TeleportCommand("tp", "WIP", ERole.None);
            PingCommand a = new PingCommand("ping", "Chek system works", ERole.None);

        }
    }
}
