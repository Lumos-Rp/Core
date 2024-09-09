using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.Game.Models;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib;

namespace AdministrationTools.Commands
{
    internal class IdCommand : CommandBase
    {
        public IdCommand(string name, string description, ERole role) : base(name, description, role) { }

        public override void Execute(ChatMessage chatMessage)
        {
            chatMessage.player.messaging.Send($"Your Id is: {chatMessage.player.id}");
        }
    }
}
