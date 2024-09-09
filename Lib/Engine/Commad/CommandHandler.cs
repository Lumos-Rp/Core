using HogWarp.Lib.Game.Models;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib.System;

namespace HogWarp.Lib.Engine.Commad
{
    public class CommandHandler
    {
        private Dictionary<string, CommandBase> _commands = new Dictionary<string, CommandBase>();
        private Logger _logger = new Logger("CommandHandler");

        public void Registry(CommandBase command)
        {
            _commands.Add(command.name, command);
            _logger.Debug($"Command {command.name} registred.");
        }

        public bool CommandExist(ChatMessage chatMessage)
        {
            return _commands.ContainsKey(chatMessage.GetCommandName());
        }

        public void Execute(ChatMessage chatMessage, ref bool cancel)
        {

            if (!(chatMessage.IsCommand() || chatMessage.IsHelp()))
                return;

            if (!CommandExist(chatMessage)) 
            {
                chatMessage.player.messaging.Send(EChatMessageColor.RED, "Invalid Command.");
                cancel = true;
                return;
            }

            CommandBase command = _commands[chatMessage.GetCommandName()];
            if (!chatMessage.player.HavePermissions(command.role))
            {
                chatMessage.player.messaging.Send(EChatMessageColor.RED, "Insufficient permissions.");
                cancel = true;
                return;
            }
            if (chatMessage.IsHelp())
            {
                chatMessage.player.messaging.Send(EChatMessageColor.OTHER, command.description);
                cancel = true;
                return;
            }
            command.Execute(chatMessage);
            cancel = true;
        }
    }
}
