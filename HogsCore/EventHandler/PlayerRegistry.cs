using Hogs.Lib.Engine.Repository;
using HogWarp.Lib.Game.Avatar;
using Hogs.Lib.Game.Avatar;
using HogWarp.Lib;
using HogWarp.Lib.System;


namespace Hogs.Core.EventHandler
{
    public class PlayerRegistry
    {
        private PlayerEntityRepository _playerEntityRepository;
        private Logger _logger = new Logger("PlayerRegistry");
        public PlayerRegistry()
        {
            Server.GetInstance().eventManager.playerJoinEventHandler.playerJoinEvent += PlayerJoinEventHandler;
            _playerEntityRepository = new PlayerEntityRepository();
        }

        public void PlayerJoinEventHandler(Player player)
        {
           PlayerEntity playerEntity =  _playerEntityRepository.Get(player);
            _logger.Info($"{playerEntity.name} has {playerEntity.galleons}");
        }
    }
}
