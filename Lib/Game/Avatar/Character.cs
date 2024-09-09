using System.Runtime.InteropServices;
using HogWarp.Lib.Game.Data;
using HogWarp.Lib.Game.Models;

namespace HogWarp.Lib.Game.Avatar
{
    public unsafe partial class Character
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Internal
        {
            [FieldOffset(8)] public uint Id;
            [FieldOffset(12)] public EHouse House;
            [FieldOffset(13)] public EGender Gender;
            [FieldOffset(15)] public bool Hooded;
            [FieldOffset(16)] public bool Mounted;
            [FieldOffset(17)] public TimePoint LastMovement;
        }

        public Internal* Address;

        public uint id { get { return Address->Id; } set { Address->Id = value; } }
        public EHouse house { get { return Address->House; } set { Address->House = value; } }
        public EGender gender { get { return Address->Gender; } set { Address->Gender = value; } }
        public bool Hooded { get { return Address->Hooded; } set { Address->Hooded = value; } }
        public bool mounted { get { return Address->Mounted; } set { Address->Mounted = value; } }
        public TimePoint lastMovement { get { return Address->LastMovement; } set { Address->LastMovement = value; } }

        public Character(nint Address)
        {
            this.Address = (Internal*)Address;
        }
    }
}
