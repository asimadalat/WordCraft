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
        private readonly IScoreService _scoreService;

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

        public MainPageViewModel(IScoreService scoreService)
        {
            _scoreService = scoreService;
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
        public Task Submit()
        {
            List<string> specialWords = GetSpecialWords();
            WordScoreLbl = _scoreService.GetWordScore(_currentWord, specialWords);
            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task GoToHistory()
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.Show();
            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task GoToScoring()
        {
            ScoringWindow scoringWindow = new ScoringWindow();
            scoringWindow.Show();
            return Task.CompletedTask;
        }
    }
}

