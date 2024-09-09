using Buffer = HogWarp.Lib.System.Buffer;
using System.Runtime.InteropServices;
using HogWarp.Lib.Game.Avatar;
using HogWarp.Lib.System;

namespace HogWarp.Loader
{
    public class Events
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct InitializationParameters
        {
            internal IntPtr WorldAddress;
            internal IntPtr PlayerManagerAddress;
            internal Player.InitializationFunctionParameters PlayerFunctionParameters;
            internal Buffer.Parameters BufferParameters;
            internal BufferReader.Parameters ReaderParameters;
            internal BufferWriter.Parameters WriterParameters;
            internal PlayerManager.InitializationFunctionParameters PlayerManagerParameters;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ShutdownArgs
        {
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UpdateArgs
        {
            public float Delta;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PlayerArgs
        {
            public IntPtr Ptr;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ChatArgs
        {
            public IntPtr Ptr;
            public IntPtr Message;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MessageArgs
        {
            public IntPtr Ptr;
            public IntPtr Plugin;
            public IntPtr Message;
            public ushort Opcode;
        }
    }
}
