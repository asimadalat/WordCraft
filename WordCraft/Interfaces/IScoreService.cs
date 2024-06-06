using WordCraft.Models;

namespace WordCraft.Interfaces
{
    // Interface IScoreService to be used by program with methods defined.
    public interface IScoreService
    {
        List<LetterScoreModel> GetAllLetterScores();
        int GetOneLetterScore (char charac);
        string GetWordScore(string word, List<string> specials);
    }
}