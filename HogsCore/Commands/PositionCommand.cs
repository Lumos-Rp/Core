using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.Game.Models;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib;


namespace AdministrationTools.Commands
{
    internal class PositionCommand : CommandBase
    {
        public PositionCommand(string name, string description, ERole role) : base(name, description, role) { }

        public override void Execute(ChatMessage chatMessage)
        {
            chatMessage.player.messaging.Send($"X: {Math.Round(chatMessage.player.lastMovement.Move.Position.X, 2)} Y: {Math.Round(chatMessage.player.lastMovement.Move.Position.Y, 2)} Z: {Math.Round(chatMessage.player.lastMovement.Move.Position.Z, 2)}");
        }
    }
}
