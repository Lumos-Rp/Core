using HogWarp.Lib.Interop.Attributes;
using HogWarp.Lib.Game.Data;

namespace HogWarp.Lib.Game.Avatar
{
    public class PlayerMessaging
    {
        private Player _player;

        public PlayerMessaging(Player player)
        {
            _player = player;
        }

        public void Send(string data)
        {
            _player.SendMessage($"{data}");
        }
        public void Send(EChatMessageColor color, string data)
        {
            _player.SendMessage($"{color.GetStringValue()} {data}");
        }
        public void SendFrom(EChatMessageColor color, string data, string soruce)
        {
            _player.SendMessage($"{color.GetStringValue()} {soruce}: {data}");
        }
        public void SendFromSystem(string data)
        {
            _player.SendMessage($"{EChatMessageColor.RED} System: {data}");
        }
        public void SendFromStaff(string data)
        {
            _player.SendMessage($"{EChatMessageColor.YELLOW} Staff: {data}");
        }
    }
}
