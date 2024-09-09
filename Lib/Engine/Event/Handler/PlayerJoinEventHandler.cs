using static HogWarp.Lib.Engine.Event.ServerEvents;
using HogWarp.Lib.Game.Avatar;
using HogWarp.Lib.System;

namespace HogWarp.Lib.Engine.Event.Handler
{
    public class PlayerJoinEventHandler
    {
        private Logger _logger = new Logger("PlayerJoinEventHandler");
        public event PlayerJoinDelegate? playerJoinEvent;

        public void OnPlayerJoin(Player player)
        {
            if (playerJoinEvent == null)
                return;

            foreach (PlayerJoinDelegate handler in playerJoinEvent!.GetInvocationList())
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
