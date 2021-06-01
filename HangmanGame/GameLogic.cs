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
    public class GameLogic
    {
        private char[] word;
        private char[] guess;
        private int points;
        private int playerpoints;

        public GameLogic(string word)
        {
            this.word = word.ToCharArray();
            CalculateWordPoints();
            PrepareGuess();
        }

        public int WrongAllowed()
        {
            return word.Length / 2 + 1;
        }

        public int GetWordPoints()
        {
            return points;
        }

        public int GetPlayerPoints()
        {
            return playerpoints;
        }

        public string GetGuessString()
        {
            string guess_string = "";
            foreach (char ch in guess)
            {
                guess_string += ch + " ";
            }
            return guess_string;
        }

        private void CalculateWordPoints()
        {
            points = 0;
            for (int index = 0; index < word.Length; index++)
            {
                points += LetterPoint(word[index]);
            }
        }

        public bool ProcessCharacter(char ch)
        {
            bool found = false;
            for (int index = 0; index < word.Length; index++)
            {
                if (ch == word[index])
                {
                    guess[index] = word[index];
                    playerpoints += LetterPoint(ch);
                    found = true;
                }
            }
            return found;
        }

        private void PrepareGuess()
        {
            guess = new char[word.Length];
            for (int index = 0; index < word.Length; index++)
            {
                if (char.IsLetter(word[index]))
                {
                    guess[index] = '_';
                }
                else
                {
                    guess[index] = word[index];
                }
            }
        }

        public bool Compare()
        {
            for (int index = 0; index < word.Length; index++)
            {
                if (word[index] != guess[index])
                {
                    return false;
                }
            }
            return true;
        }



        private int LetterPoint(char ch)
        {
            if (char.IsLetter(ch))
            {
                switch (ch)
                {
                    case 'Y':
                    case 'B':
                    case 'C':
                    case 'F':
                    case 'G':
                    case 'M':
                    case 'P':
                    case 'U':
                    case 'W':
                        return 2;
                    case 'J':
                    case 'K':
                    case 'Q':
                    case 'V':
                    case 'X':
                    case 'Z':
                        return 5;
                    default:
                        return 1;
                }
            }
            return 0;
        }
    }
}