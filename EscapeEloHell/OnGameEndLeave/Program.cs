﻿using System;
using System.Runtime.InteropServices;
using LeagueSharp;
using LeagueSharp.Common;

namespace OnGameEndLeave
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnUpdate -= Game.OnUpdate;
            CustomEvents.Game.OnGameEnd -= RemoveEventHandler;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();
            RegisterEvents();
            LeagueSharp.Game.PrintChat("On Game End Leave loaded.");
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnUpdate += Game.OnUpdate;
            LeagueSharp.Game.OnNotify += Game.OnNotify;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }
    }
}