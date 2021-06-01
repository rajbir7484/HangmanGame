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
    public class GameResultActivity : AppCompatActivity
    {
        Button btnHome;
        ImageView imageResult;
        string username;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_result);
            string result = Intent.GetStringExtra("Status");
            username = Intent.GetStringExtra("UserName");
            btnHome = FindViewById<Button>(Resource.Id.btnHome);
            imageResult = FindViewById<ImageView>(Resource.Id.imageResult);
            if (result != null)
            {
                if (result.Equals("WIN"))
                {
                    imageResult.SetBackgroundResource(Resource.Drawable.won);
                }
                else
                {
                    imageResult.SetBackgroundResource(Resource.Drawable.lost);
                }
            }
            btnHome.Click += BtnHome_Click;

        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(HomeActivity));
            intent.PutExtra("UserName", username);
            StartActivity(intent);
            Finish();
        }
    }
}