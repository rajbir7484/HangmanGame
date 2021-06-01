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
    public class LeaderAdapter : BaseAdapter<LeaderData>
    {
        private readonly Activity context;
        private readonly List<LeaderData> datas;

        public LeaderAdapter(Activity context, List<LeaderData> datas)
        {
            this.datas = datas;
            this.context = context;
        }

        public override int Count
        {
            get { return datas.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override LeaderData this[int position]
        {
            get { return datas[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.row_layout, null, false);
            }

            TextView textUserName = row.FindViewById<TextView>(Resource.Id.textUserName);
            TextView textStat = row.FindViewById<TextView>(Resource.Id.textStat);
            TextView textScore = row.FindViewById<TextView>(Resource.Id.textScore);

            textUserName.Text = "User: " + datas[position].UserName;
            textScore.Text = "Total Score: " + datas[position].Score;
            string output = "Win: " + datas[position].Win + " Lose: " + datas[position].Lose;
            textStat.Text = output;

            return row;
        }
    }
}