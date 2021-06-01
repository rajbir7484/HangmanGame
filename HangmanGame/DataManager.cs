using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HangmanGame
{
    public class DataManager
    {
        private SQLiteConnection conn;

        public DataManager()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            conn = new SQLiteConnection(Path.Combine(path, "hangmans.db"));
            if (!CheckTable())
            {
                conn.CreateTable<Users>();
                conn.CreateTable<GameStats>();
            }

        }

        public bool AddNewUser(Users user)
        {
            try
            {
                conn.Insert(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddNewUserGame(GameStats stat)
        {
            try
            {
                conn.Insert(stat);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Check existing User
        public bool CheckUser(string username, string password)
        {
            List<Users> users = conn.Query<Users>("Select * from Users");
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserName.Equals(username) && users[i].Password.Equals(password))
                {
                    return true;
                }

            }
            return false;
        }

        public List<LeaderData> GetAllLeaderData()
        {
            List<LeaderData> datas = new List<LeaderData>();
            try
            {
                List<Users> users = conn.Query<Users>("Select * from Users");
                for (int index = 0; index < users.Count; index++)
                {
                    datas.Add(GetLeaderData(users[index].UserName));
                }
            }
            catch (Exception ex)
            {
            }
            if (datas.Count > 0)
            {
                for (int i = 0; i < datas.Count - 1; i++)
                {
                    for (int j = 0; j < datas.Count - i - 1; j++)
                    {
                        if (datas[j].Score < datas[j + 1].Score)
                        {
                            LeaderData temp = datas[j];
                            datas[j] = datas[j + 1];
                            datas[j + 1] = temp;
                        }
                    }
                }
            }
            return datas;
        }

        public LeaderData GetLeaderData(string username)
        {
            LeaderData data = new LeaderData();
            data.UserName = username;
            try
            {
                string query = "Select * from GameStats Where UserName='" + username + "'";
                List<GameStats> stats = conn.Query<GameStats>(query);
                for (int index = 0; index < stats.Count; index++)
                {
                    data.Score += stats[index].TotalScore;
                    if (stats[index].GameStatus.Equals("WIN"))
                    {
                        data.Win += 1;
                    }
                    else
                    {
                        data.Lose += 1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        private bool CheckTable()
        {
            try
            {
                conn.Get<Users>(1);
                conn.Get<GameStats>(1);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}