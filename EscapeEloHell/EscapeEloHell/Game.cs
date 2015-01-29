﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace RelaxedWinner
{
    public class Game
    {
        private static readonly Random Random = new Random(20000);
       
        public static void Game_OnGameStart(EventArgs args)
        {
            LeagueSharp.Game.PrintChat("Relaxed Winner loaded.");

            if (RelaxedWinnerDll.RelaxedWinner.RepeatMaximum(
                    20, RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart))
            {
                if (UserInterface.IsStartMessageTeam)
                {
                    ChatTalk(
                               15000, 35000,
                               RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)
                                   .Message); 
                }

                if (UserInterface.IsStartMessageAll)
                {
                    ChatTalk(
                        38000, 60000,
                        @"/all " +
                        RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)
                            .Message);
                }

            }

            ToogleMuteAll(UserInterface.IsMuteAll, false);

            Files.Save();
        }

        private static void ToogleMuteAll(bool active, bool endGame)
        {
            if (!active)
            {
                return;
            }

            if (endGame)
            {
                LeagueSharp.Game.Say(@"/mute all");
            }
            else
            {
                ChatTalk(61000, 61000, @"/mute all"); 
            }
        }

        private static void ChatTalk(int minDelaxInMs, int maxDelayInMs, string chatMessage)
        {
            Task.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(Random.Next(minDelaxInMs, maxDelayInMs));
                    LeagueSharp.Game.Say(chatMessage);
                });
        }

        public static void GameEnd(EventArgs args)
        {
            if (UserInterface.Menu == null || !UserInterface.IsEnabled)
	        {
	        	 return;
	        }

            var nexus = ObjectManager.Get<Obj_HQ>().FirstOrDefault(n => n.Health < 1);
            if (nexus == null)
            {
                return;
            }

            ToogleMuteAll(UserInterface.IsMuteAll, true);

            if (UserInterface.IsEndMessageAll)
            {
                LeagueSharp.Game.Say(
                     @"/all " +
                     RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd).Message); 
            }

            if (UserInterface.IsEndMessageTeam)
            {
                ChatTalk(
                      3000, 4000,
                      RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd).Message); 
            }

            LeagueSharp.Game.OnGameUpdate -= GameEnd;
        }
    }
}