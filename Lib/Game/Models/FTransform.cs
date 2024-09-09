﻿using System.Runtime.InteropServices;

namespace HogWarp.Lib.Game.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTransform
    {
        public Vector3 Rotation;
        public float W;
        public Vector3 Location;
        public Vector3 Scale;
    }
}
