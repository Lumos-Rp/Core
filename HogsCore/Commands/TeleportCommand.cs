using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.Game.Models;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib;


namespace AdministrationTools.Commands
{
    internal class TeleportCommand : CommandBase
    {
        public TeleportCommand(string name, string description, ERole role) : base(name, description, role) { }

        public override void Execute(ChatMessage chatMessage)
        {
            try
            {
                chatMessage.player.messaging.Send("Teleported");
            }catch (Exception) 
            {
                chatMessage.player.messaging.Send(EChatMessageColor.RED, "Invalid coords");
            }
        }
    }
}