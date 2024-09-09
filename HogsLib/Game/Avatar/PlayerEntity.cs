using HogWarp.Lib.Game.Data;

namespace Hogs.Lib.Game.Avatar
{
    public class PlayerEntity
    {
        public uint id { get; private set; }
        public string name { get; private set; }
        public string discordId { get; private set; }
        public EHouse house { get; private set; }
        public EGender gender { get; private set; }
        public int galleons { get; set; }

        public PlayerEntity(uint id, string name, string discordId, EHouse house, EGender gender, int galleons)
        {
            this.id = id;
            this.name = name;
            this.discordId = discordId;
            this.house = house;
            this.gender = gender;
            this.galleons = galleons;
        }
    }
}
    