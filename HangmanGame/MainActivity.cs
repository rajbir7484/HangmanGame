using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace HangmanGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Button btnLogin, btnRegister;
        EditText etUser, etPassword;
        DataManager manager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            manager = new DataManager();
            etUser = FindViewById<EditText>(Resource.Id.etUserName);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnLogin.Click += Login_Click;
            btnRegister.Click += Register_Click;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void Login_Click(object sender, EventArgs args)
        {
            string username = etUser.Text.Trim();
            string pass = etPassword.Text;
            string message = "";
            if (username.Length == 0 || pass.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                if (manager.CheckUser(username, pass))
                {
                    message = "Welcome " + username;
                    Intent intent = new Intent(this, typeof(HomeActivity));
                    intent.PutExtra("UserName", username);
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    message = "Invalid User Name and Password";
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        private void Register_Click(object sender, EventArgs args)
        {
            StartActivity(typeof(RegisterPlayerActivity));
            Finish();
        }
    }
}