using static HogWarp.Lib.Engine.Event.ServerEvents;
using HogWarp.Lib.Game.Avatar;
using HogWarp.Lib.System;

namespace HogWarp.Lib.Engine.Event.Handler
{
    public class PlayerLeaveEventHandler
    {
        private Logger _logger = new Logger("PlayerLeaveEventHandler");
        public event PlayerLeaveDelegate? playerLeaveEvent;

        internal void OnPlayerLeave(Player player)
        {
            if (playerLeaveEvent == null)
                return;

            foreach (PlayerLeaveDelegate handler in playerLeaveEvent!.GetInvocationList())
            {
                try
                {
                    handler?.Invoke(player);
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex.ToString());
                }
            }

        }
    }
}
