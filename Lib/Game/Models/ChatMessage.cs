using HogWarp.Lib.Game.Avatar;

namespace HogWarp.Lib.Game.Models
{
    public class ChatMessage
    {
        private static char COMMAND_PREFIX = '!';
        private static char HELP_PREFIX = '?';

        public Player player { get; }
        public string message { get; }

        public ChatMessage(Player player, string message)
        {
            this.player = player;
            this.message = message;
        }

        public string GetCommandName()
        {
            return message.Substring(1).Split(' ')[0];
        }

        public string[] GetCommandArguments()
        {
            return message.Split(' ').Skip(1).ToArray();
        }

        public bool IsCommand()
        {
            return message[0].Equals(COMMAND_PREFIX);
        }
        public bool IsHelp()
        {
            return message[0].Equals(HELP_PREFIX);
        }
    }
}
