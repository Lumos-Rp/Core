﻿using static HogWarp.Loader.PluginManager;
using System.Runtime.InteropServices;
using static HogWarp.Loader.Events;
using HogWarp.Lib.Game.Avatar;
using HogWarp.Lib.Game.Models;
using HogWarp.Lib.System;
using HogWarp.Lib.Game;
using HogWarp.Lib;

namespace HogWarp.Loader
{
    public static class EntryPoint
    {
        private static Server? _server;

        [UnmanagedCallersOnly]
        public static void Initialize(InitializationParameters Params)
        {
            Player.Initialize(Params.PlayerFunctionParameters);
            Lib.System.Buffer.Initialize(Params.BufferParameters);
            BufferReader.Initialize(Params.ReaderParameters);
            BufferWriter.Initialize(Params.WriterParameters);
            PlayerManager.Initialize(Params.PlayerManagerParameters);

            var world = new World(Params.WorldAddress);
            var playerManager = new PlayerManager(Params.PlayerManagerAddress);

            _server = new Server(world, playerManager);
            
            if (!System.IO.Directory.Exists("plugins")) System.IO.Directory.CreateDirectory("plugins");
            LoadFromBase("plugins");

            InitializePlugins();
        }

        [UnmanagedCallersOnly]
        public static void Shutdown(ShutdownArgs args)
        {
            _server!.eventManager.shutdownEventHandler.OnShutdown();
        }

        [UnmanagedCallersOnly]
        public static void Update(UpdateArgs args)
        {
            _server!.eventManager.updateEventHandler.OnUpdate(args.Delta);
        }

        [UnmanagedCallersOnly]
        public static void OnPlayerJoined(PlayerArgs args)
        {
            var player = new Player(args.Ptr);
            _server!.playerManager.Add(player);

            _server!.eventManager.playerJoinEventHandler.OnPlayerJoin(player);
        }

        [UnmanagedCallersOnly]
        public static void OnPlayerLeft(PlayerArgs args)
        {
            var player = _server!.playerManager.Remove(args.Ptr);
            if(player != null)
                _server!.eventManager.playerLeaveEventHandler.OnPlayerLeave(player!);
        }

        [UnmanagedCallersOnly]
        public static int OnPlayerChat(ChatArgs args)
        {
            string message = Marshal.PtrToStringUTF8(args.Message)!;
            var player = _server!.playerManager.GetByPtr(args.Ptr)!;

            _server!.eventManager.chatEventHandler.OnChat(new ChatMessage(player, message), out bool cancel);

            return cancel ? 1 : 0;
        }

        [UnmanagedCallersOnly]
        public static void OnMessage(MessageArgs args)
        {
            string modName = Marshal.PtrToStringUTF8(args.Plugin)!;

            var buffer = Lib.System.Buffer.FromAddress(args.Message);
            var player = _server!.playerManager.GetByPtr(args.Ptr)!;

            _server!.eventManager.messageDelegateEventHandler.OnMessage(player, modName, args.Opcode, buffer);
        }
    }
}
