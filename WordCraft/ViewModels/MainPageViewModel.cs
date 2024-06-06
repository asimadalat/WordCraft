using WordCraft.ViewModels;
using WordCraft.Views;
using WordCraft.Models;
using System.Windows.Controls;
using System.Windows.Navigation;
using System;
using System.Windows;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using WordCraft.Interfaces;
using WordCraft.Services;

namespace WordCraft.ViewModels
{
    public partial class MainPageViewModel : PageViewModel
    {
        // Define score service to use for scoring.
        private readonly IScoreService _scoreService;

        // Define currentword to hold the word currently in text box.
        private string _currentWord;
        public string CurrentWord { get => _currentWord;
            set {
                if (_currentWord != value) {
                    _currentWord = value;
                    OnPropertyChanged(nameof(CurrentWord));
                }
            }
        }

        // Define currentword to hold the score message, including the final score.
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

        public MainPageViewModel(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        // Convert first column of CSV file excluding header row to list of string type.
        private List<string> GetSpecialWords()
        {
            List<string> words = new List<string>();
            try 
            {
                // Define file path.
                using (StreamReader reader = new StreamReader("../../../Resources/Words50.csv"))
                {
                    if (!reader.EndOfStream) // Exclude the header row.
                    {
                        reader.ReadLine(); 
                    }

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        words.Add((line.Split(','))[0]); // Split the row by ',' character, add first element to list.
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return words;
        }

        // Modify WordScoreLbl to include score for _currentWord using _scoreService.
        [RelayCommand]
        public Task Submit()
        {
            List<string> specialWords = GetSpecialWords();
            WordScoreLbl = _scoreService.GetWordScore(_currentWord, specialWords);
            return Task.CompletedTask;
        }

        // Initalise and display history window.
        [RelayCommand]
        public Task GoToHistory()
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.Show();
            return Task.CompletedTask;
        }

        // Initalise and display scoring window.
        [RelayCommand]
        public Task GoToScoring()
        {
            ScoringWindow scoringWindow = new ScoringWindow();
            scoringWindow.Show();
            return Task.CompletedTask;
        }
    }
}

