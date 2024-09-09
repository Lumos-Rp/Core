﻿using System.Runtime.InteropServices;

namespace HogWarp.Lib.Game.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Movement
    {
        public Vector3 Position;
        public float Speed;
        public float Direction;
        public bool InAir;
    }
}
