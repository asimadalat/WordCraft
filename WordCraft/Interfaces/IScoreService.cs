using WordCraft.Models;

namespace WordCraft.Interfaces
{
    public interface IScoreService
    {
        List<LetterScoreModel> GetAllLetterScores();
        int GetOneLetterScore (char charac);
        string GetWordScore(string word, List<string> specials);
    }
}