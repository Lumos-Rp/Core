using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib;
using HogWarp.Lib.Game.Models;

namespace SamplePlugin
{
    internal class PingCommand : CommandBase
    {
        public PingCommand(string name, string description, ERole role) : base(name, description, role) { }

        public override void Execute(ChatMessage chatMessage)
        {
            chatMessage.player.messaging.Send("Hi World!");
        }
    }
}
