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
    [Activity(Label = "Leader Board", Theme = "@style/AppTheme")]
    public class LeaderActivity : AppCompatActivity
    {
        ListView listLeaderBoard;
        DataManager manager;
        LeaderAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_leaderboard);
            manager = new DataManager();
            // Create your application here
            listLeaderBoard = FindViewById<ListView>(Resource.Id.listLeaderBoard);
            adapter = new LeaderAdapter(this, manager.GetAllLeaderData());
            listLeaderBoard.Adapter = adapter;
        }
    }
}