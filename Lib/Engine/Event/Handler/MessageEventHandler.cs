using static HogWarp.Lib.Engine.Event.ServerEvents;
using HogWarp.Lib.Game.Avatar;
using HogWarp.Lib.System;

namespace HogWarp.Lib.Engine.Event.Handler
{
    public class MessageEventHandler
    {
        private Logger _logger = new Logger("MessageDelegateEventHandler");
        private Dictionary<string, HashSet<MessageDelegate>> _messageHandlers = new Dictionary<string, HashSet<MessageDelegate>>();

        public void RegisterMessageHandler(string modName, MessageDelegate messageDelegate)
        {
            HashSet<MessageDelegate>? handlers;
            if (!_messageHandlers.TryGetValue(modName, out handlers))
            {
                handlers = new HashSet<MessageDelegate>();
                _messageHandlers.Add(modName, handlers);
            }

            handlers.Add(messageDelegate);
        }

        public void UnregisterMessageHandler(string modName, MessageDelegate messageDelegate)
        {
            if (_messageHandlers.TryGetValue(modName, out var handlers))
            {
                handlers.Remove(messageDelegate);
            }
        }

        internal void OnMessage(Player player, string modName, ushort opcode, System.Buffer buffer)
        {
            if (_messageHandlers.TryGetValue(modName, out var handlers))
            {
                foreach (var h in handlers)
                {
                    try
                    {
                        h.Invoke(player, opcode, buffer);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warning(ex.ToString());
                    }
                }
            }
        }
    }
}
