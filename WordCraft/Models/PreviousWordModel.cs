namespace WordCraft.Models
{
    // PreviousWordModel class handles all previous words from API
    public class PreviousWordModel
    {
        public string Word { get; set; } // Each entry needs a word.
        public int Score { get; set; } // A score.
        public DateTime ScoreDate { get; set; } // A date and time when it was entered.
        public PreviousWordModel() 
        { 

        }
    }
}