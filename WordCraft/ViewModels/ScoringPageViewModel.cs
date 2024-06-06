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
        // Define score service and letter score object list to obtain, binded to data grid.
        private readonly IScoreService _scoreService;
        public List<LetterScoreModel> LetterScoreList { get; set; }

        public ScoringPageViewModel(IScoreService scoreService) 
        {
            // Refresh letter score values on opening the scoring page.
            _scoreService = scoreService;
            LetterScoreList = _scoreService.GetAllLetterScores();
        }

        
    }
}
