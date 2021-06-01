using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangmanGame
{
    public class LeaderData
    {
        public string UserName { get; set; }

        public int Score { get; set; }

        public int Win { get; set; }

        public int Lose { get; set; }

        public LeaderData()
        {

        }

        public LeaderData(string username, int score, int win, int lose)
        {
            UserName = username;
            Win = win;
            Lose = lose;
            Score = score;
        }
    }
}