using HogWarp.Lib.Game.Avatar;
using Hogs.Lib.Game.Avatar;
using HogWarp.Lib.System;
using HogWarp.Lib;
using HogWarp.Lib.System.Models;

namespace Hogs.Lib.Engine.Repository
{
    public class PlayerEntityRepository
    {
        private MySqlRepository _repository;
        private static readonly string INSERT_PLAYER_QUERY = "INSERT INTO players (discordId, name, galleons, house, gender) VALUES (?discordId, ?name, ?galleons, ?house, ?gender);";
        private static readonly string SELECT_PLAYER_QUERY = "SELECT discordId, name, galleons, house, gender from players WHERE discordId = ?discordId;";
        private Logger _logger = new Logger("PlayerEntityRepository");

        public PlayerEntityRepository()
        {
            _repository = Server.GetInstance().repository;
        }

        public PlayerEntity Get(Player player)
        {
            try
            {
                _logger.Debug($"{player.name} is trayin yo loggin.");
                RepositoryCommand cmd = new RepositoryCommand(SELECT_PLAYER_QUERY);
                cmd.AddParameter("discordId", true, player.discordId);

                Dictionary<string, object> result = _repository.ExecuteReaderOne(cmd);

                if (result != null && result.Count > 0)
                {
                    int galleons = Convert.ToInt32(result["galleons"]);
                    return new PlayerEntity(player.id, player.name, player.discordId, player.house, player.gender, galleons);
                }
                _logger.Debug("New player registry.");
                PlayerEntity playerEntity = new PlayerEntity(player.id, player.name, player.discordId, player.house, player.gender, 0);
                Save(playerEntity);
                return playerEntity;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error Getting PlayerEntity: {ex.ToString()}");
            }
            return new PlayerEntity(player.id, player.name, player.discordId, player.house, player.gender, 0);
        }

        public void Save(PlayerEntity playerEntity)
        {
            try
            {
                _logger.Debug($"{playerEntity.name} is trayin yo persist.");

                RepositoryCommand cmd = new RepositoryCommand(INSERT_PLAYER_QUERY);
                cmd.AddParameter("discordId", true, playerEntity.discordId);
                cmd.AddParameter("name", true, playerEntity.name);
                cmd.AddParameter("galleons", false, playerEntity.galleons);
                cmd.AddParameter("house", false, (int)playerEntity.house);
                cmd.AddParameter("gender", false, (int)playerEntity.gender);

                Server.GetInstance().repository.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error saving PlayerEntity: {ex.ToString()}");
            }
        }
    }
}
