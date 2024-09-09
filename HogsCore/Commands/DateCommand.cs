using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.Game.Models;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib;

namespace AdministrationTools.Commands
{
    internal class DateCommand : CommandBase
    {
        public DateCommand(string name, string description, ERole role) : base(name, description, role) { }

        public override void Execute(ChatMessage chatMessage)
        {
            chatMessage.player.messaging.Send($"{Server.GetInstance().world.day.ToString("D2")}/{Server.GetInstance().world.month.ToString("D2")}/{Server.GetInstance().world.year}");
        }
    }
}
