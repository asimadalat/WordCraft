using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WordCraft.Interfaces;
using WordCraft.Models;

namespace WordCraft.ViewModels
{
    public partial class ScoringPageViewModel : PageViewModel
    {
        private readonly IScoreService _scoreService;
        public List<LetterScoreModel> LetterScoreList { get; set; }

        public ScoringPageViewModel(IScoreService scoreService) 
        {
            _scoreService = scoreService;
            LetterScoreList = _scoreService.GetAllLetterScores();
        }

        
    }
}
