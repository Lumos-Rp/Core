using static HogWarp.Lib.Engine.Event.ServerEvents;
using HogWarp.Lib.System;

namespace HogWarp.Lib.Engine.Event.Handler
{
    public class ShutdownEventHandler
    {
        private Logger _logger = new Logger("ShutdownEventHandler");
        public event ShutdownDelegate? shutdownEvent;

        public void OnShutdown()
        {
            if (shutdownEvent == null)
                return;

            foreach (ShutdownDelegate handler in shutdownEvent!.GetInvocationList())
            {
                try
                {
                    handler?.Invoke();
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex.ToString());
                }
            }
        }
    }
}
