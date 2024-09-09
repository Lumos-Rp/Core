using HogWarp.Lib.Game.Data;
using HogWarp.Lib.Game.Models;

namespace HogWarp.Lib.Engine.Commad
{
    public abstract class CommandBase
    {
        public string name { get; set; }
        public string description { get; set; }
        public ERole role { get; set; }

        protected CommandBase(string name, string description, ERole role)
        {
            this.name = name;
            this.description = description;
            this.role = role;
            Server.GetInstance().commandHandler.Registry(this);
        }

        public abstract void Execute(ChatMessage chatMessage);
    }
}
