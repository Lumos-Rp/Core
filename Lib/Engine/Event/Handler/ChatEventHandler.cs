using static HogWarp.Lib.Engine.Event.ServerEvents;
using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.System;
using HogWarp.Lib.Game.Models;

namespace HogWarp.Lib.Engine.Event.Handler
{
    public class ChatEventHandler
    {
        private Logger _logger = new Logger("ChatEventHandler");
        private CommandHandler? _commandHandler;
        public event ChatDelegate? chatEvent;

        public ChatEventHandler(CommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        internal void OnChat(ChatMessage chatMessage, out bool cancel)
        {
            cancel = false;

            _commandHandler!.Execute(chatMessage, ref cancel);

            if (cancel)
                return;

            if (chatEvent == null)
                return;

            foreach (ChatDelegate handler in chatEvent!.GetInvocationList())
            {
                try
                {
                    handler?.Invoke(chatMessage, ref cancel);
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex.ToString());
                }
            }
        }
    }
}
