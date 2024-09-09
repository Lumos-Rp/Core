using static HogWarp.Lib.Engine.Event.ServerEvents;
using HogWarp.Lib.System;

namespace HogWarp.Lib.Engine.Event.Handler
{
    public class UpdateEventHandler
    {
        private Logger _logger = new Logger("UpdateEventHandler");
        public event UpdateDelegate? updateEvent;

        internal void OnUpdate(float deltaSeconds)
        {
            if (updateEvent == null)
                return;

            foreach (UpdateDelegate handler in updateEvent!.GetInvocationList())
            {
                try
                {
                    handler?.Invoke(deltaSeconds);
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex.ToString());
                }
            }
        }
    }
}
