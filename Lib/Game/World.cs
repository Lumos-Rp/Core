using System.Runtime.InteropServices;
using HogWarp.Lib.Game.Data;

namespace HogWarp.Lib.Game
{
    public unsafe partial class World
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Internal
        {
            [FieldOffset(0)] public double GameTime;
            [FieldOffset(8)] public ESeason Season;
            [FieldOffset(12)] public int Day;
            [FieldOffset(16)] public int Month;
            [FieldOffset(20)] public int Year;
        }

        public Internal* Address;

        public double time { get { return Address->GameTime; } set { Address->GameTime = value; } }
        public ESeason season { get { return Address->Season; } set { Address->Season = value; } }
        public int day { get { return Address->Day; } set { Address->Day = value; } }
        public int month { get { return Address->Month; } set { Address->Month = value; } }
        public int year { get { return Address->Year; } set { Address->Year = value; } }

        internal World(IntPtr Address)
        {
            this.Address = (Internal*)Address;
        }
    }
}
