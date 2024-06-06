using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.TextFormatting;
using WordCraft.Interfaces;
using WordCraft.Models;

namespace WordCraft.Services
{
    // ScoreService implements methods defined in score interface.
    public class ScoreService : IScoreService
    {
        // Define path to JSON file, letter score object list and word score label to return.
        private readonly string _filePath = "../../../Resources/scoringSettings.json";
        private List<LetterScoreModel> _letterScores;
        public string WordScoreLbl;
        public ScoreService() 
        {
            _letterScores = GetAllLetterScores(); // All letter scores only retrieved once, on initialisation.
        }

        // Convert JSON data into list of type LetterScoreModel.
        public List<LetterScoreModel> GetAllLetterScores()
        {
            var json = File.ReadAllText(_filePath);
            _letterScores = JsonSerializer.Deserialize<List<LetterScoreModel>>(json);
            return _letterScores;
        }

        // Find first object in list where 'letter' equals charac 
        public int GetOneLetterScore(char charac)
        {
            var letterScore = _letterScores.FirstOrDefault(x => x.letter.Equals(charac));
            return letterScore == null ? 0 : letterScore.value; // If object is null return 0, else return 'value' field.
        }

        // Calculate total score of a word.
        public string GetWordScore(string word, List<string> specials)
        {
            // Check word is valid.
            if (word == null || word == "") // Is it blank/null.
            {
                MessageBox.Show("Please enter a word.", "Cannot submit blank");
            }
            else if (word.Contains(" ")) // Is it multiple words.
            {
                MessageBox.Show("Please enter only a single word.", "Cannot submit multiple words");
            }
            else if (!word.All(Char.IsLetter)) // Does it contain characters other than letters of the alphabet.
            {
                MessageBox.Show("Please enter a word consisting of letters of the alphabet.", "No numeric or special characters");
            }
            else
            {
                // GetOneLetterScore to get score of each char c in word, total them.
                try
                {
                    int scoreCounter = 0;
                    foreach (char c in word)
                    {
                        scoreCounter += GetOneLetterScore(char.ToUpper(c));
                    }

                    // Double result if word in special list.
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