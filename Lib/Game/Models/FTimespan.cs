using System.Runtime.InteropServices;

namespace HogWarp.Lib.Game.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTimespan
    {
        public int Days;
        public int Hours;
        public int Minutes;
        public int Seconds;
        public int Milliseconds;
    }
}