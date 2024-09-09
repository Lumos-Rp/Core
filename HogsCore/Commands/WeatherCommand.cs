using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.Game.Models;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib;

namespace AdministrationTools.Commands
{
    internal class WeatherCommand : CommandBase
    {
        public WeatherCommand(string name, string description, ERole role) : base(name, description, role) { }

        public override void Execute(ChatMessage chatMessage)
        {
            try
            {
                Server.GetInstance().world.season = (ESeason) Int32.Parse(chatMessage.GetCommandArguments()[0]);
                chatMessage.player.messaging.Send("Weather was changed.");
            }catch (Exception)
            {
                chatMessage.player.messaging.Send(EChatMessageColor.RED, "Invalid season.");
            }
        }
    }
}
