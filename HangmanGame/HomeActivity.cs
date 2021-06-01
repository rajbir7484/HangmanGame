using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangmanGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class HomeActivity : AppCompatActivity
    {
        string username;
        TextView textWelcome;
        Button btnPlay, btnLeaderBoard, btnLogOut;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_home);
            textWelcome = FindViewById<TextView>(Resource.Id.textWelcome);
            username = Intent.GetStringExtra("UserName");
            textWelcome.Text = "Welcome " + username + "!!!";
            btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            btnPlay.Click += Play_Click;
            btnLeaderBoard = FindViewById<Button>(Resource.Id.btnLeaderBoard);
            btnLeaderBoard.Click += LeaderBoard_Click;
            btnLogOut = FindViewById<Button>(Resource.Id.btnLogOut);
            btnLogOut.Click += LogOut_Click;
        }

        private void Play_Click(object sender, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(GameActivity));
            intent.PutExtra("UserName", username);
            StartActivity(intent);
        }

        private void LeaderBoard_Click(object sender, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(LeaderActivity));
            intent.PutExtra("UserName", username);
            StartActivity(intent);
        }
        private void LogOut_Click(object sender, EventArgs args)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}