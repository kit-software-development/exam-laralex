﻿using System;
using System.Collections.Generic;

using CommonLibrary.Model.Games;
using CommonLibrary.Implementation.Games.Dice.Combos;

namespace CommonLibrary.Implementation.Games.Dice
{
    /// <summary>
    /// Many EventArgs for game events
    /// </summary>

    public class DiceGameTurnSubmittedEventArgs : TurnSubmittedEventArgs
    {
        public int ScoreGained { get; }
        public int TotalScore { get; }
        public bool IsFailure { get; }
        public DiceGameTurnSubmittedEventArgs(IPlayer player, int total_score, int score_gain = 0) : base(player, total_score)
        {
            ScoreGained = Math.Max(0, score_gain);
            TotalScore = total_score;
            IsFailure = (score_gain > 0);
        }
    }

    public class DiceSelectChangedEventArgs : EventArgs
    {
        public List<Die> ChangedDice { get; }
        public DiceSelectChangedEventArgs(List<Die> dice)
        {
            ChangedDice = dice;
        }
        public DiceSelectChangedEventArgs(Die die)
        {
            ChangedDice = new List<Die>();
            ChangedDice.Add(die);
        }

    }

    public class DiceSubmittedEventArgs : EventArgs
    {
        public AllCombosInDice Combos { get; }
        public int TurnScore { get; }
        public DiceSubmittedEventArgs(AllCombosInDice combos, int turn_score)
        {
            Combos = combos;
            TurnScore = turn_score;
        }
    }


    /*public class ServerClosedEventArgs : EventArgs
    {
        public enum ServerCloseType {
            InternalError,
            ClosedByHost,
            DisconnectedFromHost
        }

        public ServerCloseType WhyClosedType;
        public string WhyClosedMessage;
        public ServerClosedEventArgs(ServerCloseType why_closed, string why_closed_msg)
        {
            WhyClosedType = why_closed;
            WhyClosedMessage = why_closed_msg;
        }
    } */

    public class RerollEventArgs : EventArgs
    {
        public List<Die> NewDice { get; }
        public RerollEventArgs(List<Die> new_dice)
        {
            NewDice = new_dice;
        }
    }

}
