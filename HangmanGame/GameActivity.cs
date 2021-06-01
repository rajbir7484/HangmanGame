using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangmanGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class GameActivity : Activity
    {
        string username;
        Word list;
        TextView textGuess, textScore, textMaxScore, textWrongAttempt;
        GameLogic logic;
        int wrong;
        DataManager manager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_game);
            manager = new DataManager();
            list = new Word(this);
            logic = new GameLogic(list.GetRandomWord());
            username = Intent.GetStringExtra("UserName");
            textGuess = FindViewById<TextView>(Resource.Id.textGuess);
            textScore = FindViewById<TextView>(Resource.Id.textScore);
            textMaxScore = FindViewById<TextView>(Resource.Id.textMaxScore);
            textWrongAttempt = FindViewById<TextView>(Resource.Id.textWrongAttempt);
            textGuess.Text = logic.GetGuessString();
            textMaxScore.Text = " Max Score: " + logic.GetWordPoints();
            wrong = logic.WrongAllowed();
            textWrongAttempt.Text = " Remaining Wrong Attempt: " + wrong;
        }

        [Export("ButtonClick")]
        public void ButtonClick(View view)
        {
            if (view is Button)
            {
                Button btn = view as Button;
                if (btn.Enabled)
                {
                    char ch = btn.Text[0];
                    btn.Enabled = false;
                    if (logic.ProcessCharacter(ch))
                    {
                        textGuess.Text = logic.GetGuessString();
                        textScore.Text = " Your Score: " + logic.GetPlayerPoints();
                        if (logic.Compare())
                        {
                            ProcessResult();
                        }
                    }
                    else
                    {
                        wrong--;
                        if (wrong == -1)
                        {
                            ProcessResult();
                        }
                        else
                        {
                            textWrongAttempt.Text = " Remaining Wrong Attempt: " + wrong;
                        }

                    }
                }
            }
        }

        public void ProcessResult()
        {
            GameStats stats = new GameStats();
            stats.TotalScore = logic.GetPlayerPoints();
            stats.UserName = username;
            if (logic.Compare())
            {
                stats.GameStatus = "WIN";
            }
            else
            {
                stats.GameStatus = "LOSE";
            }
            manager.AddNewUserGame(stats);
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Hangman Game");
            alert.SetMessage("Click OK to Proceed");
            alert.SetIcon(Resource.Drawable.abc_btn_borderless_material);
            alert.SetButton("OK", (c, ev) =>
            {
                Intent intent = new Intent(this, typeof(GameResultActivity));
                intent.PutExtra("Status", stats.GameStatus);
                intent.PutExtra("UserName", username);
                StartActivity(intent);
                Finish();
            });
            alert.Show();
        }
    }
}