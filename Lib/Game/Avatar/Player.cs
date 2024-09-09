using HogWarp.Lib.Interop.Attributes;
using System.Runtime.InteropServices;
using HogWarp.Lib.Game.Data;
using System.Text;

namespace HogWarp.Lib.Game.Avatar
{
    public unsafe partial class Player : Character
    {
        [Function]
        private partial nint GetDiscordId();

        [Function]
        private partial nint GetName();

        [Function]
        public partial void Kick();

        [Function(Generate = false)]
        private partial void SendMessage(byte[] data, ulong length);

        public string discordId { get; private set; }
        public string name { get; private set; }
        public ERole role { get; private set; }
        public PlayerMessaging messaging { get; private set; }


        internal Player(IntPtr Address)
            : base(Address)
        {
            messaging = new PlayerMessaging(this);
            discordId = Marshal.PtrToStringUTF8(GetDiscordId())!;
            name = Marshal.PtrToStringUTF8(GetName())!;
        }
        
        public bool HavePermissions(ERole role)
        {
            return this.role >= role;
        }

        internal void SendMessage(string data)
        {
            var d = Encoding.UTF8.GetBytes(data);
            SendMessage(d, (ulong)d.Length);
        }

        private partial void SendMessage(byte[] data, ulong length)
        {
            SendMessageInternal((IntPtr)Address, data, length);
        }
    }
}
