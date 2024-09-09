using HogWarp.Lib.Game.Models;
using HogWarp.Lib.Game.Avatar;

namespace HogWarp.Lib.Engine.Event
{
    public class ServerEvents
    {
        public delegate void UpdateDelegate(float deltaSeconds);
        public delegate void ShutdownDelegate();
        public delegate void PlayerJoinDelegate(Player player);
        public delegate void PlayerLeaveDelegate(Player player);
        public delegate void ChatDelegate(ChatMessage chatMessage, ref bool cancel);
        public delegate void MessageDelegate(Player player, ushort opcode, Lib.System.Buffer buffer);
    }
}
