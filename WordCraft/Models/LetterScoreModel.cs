namespace WordCraft.Models
{
    // LetterScoreModel class handles all letter scores from scoringSettings.json.
    public class LetterScoreModel
    {
        public char letter { get; set; } // Each entry needs a letter.
        public int value { get; set; } // And a corresponding score value.
        public LetterScoreModel()
        {

        }
    }
}
