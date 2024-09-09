using HogWarp.Lib.Engine.Commad;
using HogWarp.Lib.Engine.Event.Handler;

namespace HogWarp.Lib.Engine.Event
{
    public class EventManager
    {
        public ChatEventHandler chatEventHandler;
        public MessageEventHandler messageDelegateEventHandler;
        public PlayerJoinEventHandler playerJoinEventHandler;
        public PlayerLeaveEventHandler playerLeaveEventHandler;
        public ShutdownEventHandler shutdownEventHandler;
        public UpdateEventHandler updateEventHandler;

        public EventManager(CommandHandler commandHandler)
        {
            chatEventHandler = new ChatEventHandler(commandHandler);
            messageDelegateEventHandler = new MessageEventHandler();
            playerJoinEventHandler = new PlayerJoinEventHandler();
            playerLeaveEventHandler = new PlayerLeaveEventHandler();
            shutdownEventHandler = new ShutdownEventHandler();
            updateEventHandler = new UpdateEventHandler();
        }
    }
}
