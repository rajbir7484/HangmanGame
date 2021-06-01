using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HangmanGame
{
    public class Word
    {
        private List<string> words;

        private Context context;

        public Word(Context context)
        {
            this.context = context;
            words = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(context.Assets.Open("words.txt")))
                {
                    while (!reader.EndOfStream)
                    {
                        string word = reader.ReadLine();
                        words.Add(word.ToUpper());
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string GetRandomWord()
        {
            if (words.Count > 0)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int index = random.Next(0, words.Count);
                return words[index];
            }
            return null;
        }
    }
}