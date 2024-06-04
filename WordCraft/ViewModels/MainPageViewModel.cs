using WordCraft.ViewModels;
using System;
using System.Windows;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using WordCraft.Models;

namespace WordCraft.ViewModels
{
    public partial class MainPageViewModel : PageViewModel
    {
        private static readonly int[] 
            letterScores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };

        private string _currentWord;
        public string CurrentWord { get => _currentWord;
            set {
                if (_currentWord != value) {
                    _currentWord = value;
                    OnPropertyChanged(nameof(CurrentWord));
                }
            }
        }

        private string _wordScoreLbl = "Score: ";
        public string WordScoreLbl
        {
            get => _wordScoreLbl;
            set
            {
                if (_wordScoreLbl != value)
                {
                    _wordScoreLbl = value;
                    OnPropertyChanged(nameof(WordScoreLbl));
                }
            }
        }
        public List<PreviousWordModel> PreviousWords { get; set; }

        public MainPageViewModel()
        {
            GetApiData();
        }

        private List<string> GetSpecialWords()
        {
            List<string> words = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader("../../../Resources/Words50.csv"))
                {
                    if (!reader.EndOfStream)
                    {
                        reader.ReadLine(); 
                    }

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //MessageBox.Show((line.Split(','))[0]);
                        words.Add((line.Split(','))[0]);
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return words;
        }

        [RelayCommand]
        public Task CalculateScore()
        {
            if (_currentWord == null || _currentWord == "") {
                MessageBox.Show("Please enter a word.", "Cannot submit blank");
            } else if (_currentWord.Contains(" ")) {
                MessageBox.Show("Please enter only a single word.", "Cannot submit multiple words");
            } else if (!_currentWord.All(Char.IsLetter)) {
                MessageBox.Show("Please enter a word consisting of letters of the alphabet.", "No numeric or special characters");
            } else
            {
                try
                {

                    int scoreCounter = 0;
                    foreach (char c in _currentWord)
                    {
                        scoreCounter += char.ToUpper(c) - 'A' + 1;
                    }

                    List<string> specials = GetSpecialWords();
                    if (specials != null && specials.Contains(_currentWord, StringComparer.OrdinalIgnoreCase))
                    {
                        Debug.WriteLine("Special word entered.");
                        scoreCounter *= 2;
                    }

                    WordScoreLbl = $"Score: {scoreCounter}";
                } catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                
                Debug.WriteLine(_currentWord);
            }
            
            return Task.CompletedTask;
        }

        private void GetApiData()
        {
            using (var client = new HttpClient())
            {
                var uri_endpoint = new Uri("https://testapi.sail-dev.com/api/data/getworddata");
                var result = client.GetAsync(uri_endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                PreviousWords = JsonConvert.DeserializeObject<List<PreviousWordModel>>(json);

            }
        }
    }
}

