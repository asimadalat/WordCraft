using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.TextFormatting;
using WordCraft.Interfaces;
using WordCraft.Models;

namespace WordCraft.Services
{
    public class ScoreService : IScoreService
    {
        private readonly string _filePath = "../../../Resources/scoringSettings.json";
        private List<LetterScoreModel> _letterScores;
        public string WordScoreLbl;
        public ScoreService() 
        {
            _letterScores = GetAllLetterScores();
        }

        public List<LetterScoreModel> GetAllLetterScores()
        {
            var json = File.ReadAllText(_filePath);
            _letterScores = JsonSerializer.Deserialize<List<LetterScoreModel>>(json);
            return _letterScores;
        }

        public int GetOneLetterScore(char charac)
        {
            var letterScore = _letterScores.FirstOrDefault(x => x.letter.Equals(charac));
            return letterScore == null ? 0 : letterScore.value;
        }

        public string GetWordScore(string word, List<string> specials)
        {
            if (word == null || word == "")
            {
                MessageBox.Show("Please enter a word.", "Cannot submit blank");
            }
            else if (word.Contains(" "))
            {
                MessageBox.Show("Please enter only a single word.", "Cannot submit multiple words");
            }
            else if (!word.All(Char.IsLetter))
            {
                MessageBox.Show("Please enter a word consisting of letters of the alphabet.", "No numeric or special characters");
            }
            else
            {
                try
                {
                    int scoreCounter = 0;
                    foreach (char c in word)
                    {
                        scoreCounter += GetOneLetterScore(char.ToUpper(c));
                    }

                    if (specials != null && specials.Contains(word, StringComparer.OrdinalIgnoreCase))
                    {
                        scoreCounter *= 2;
                        WordScoreLbl = $"Score: {scoreCounter}\nSpecial word, score doubled";
                    }
                    else
                    {
                        WordScoreLbl = $"Score: {scoreCounter}";
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            return WordScoreLbl;
        }
    }
}