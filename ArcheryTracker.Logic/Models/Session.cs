using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.App.Data
{
    public class Session
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Range { get; set; }
        public int NumberOfRounds { get; set; }
        public List<Round> RoundScores { get; set; }
        public int BestRoundScore { get; set; }
        public double AverageRoundScore { get; set; }
        public int TotalShots { get; set; }
        public int TotalBullseye { get; set; }
        public int TotalOnTarget { get; set; }
        public int TotalMisses { get; set; }
        public double AccuracyOnTarget { get; set; }
        public double AccuracyBullseye { get; set; }

        public Session(DateTime date, String range)
        {
            Id = Guid.NewGuid().ToString("N");
            Date = date;
            Range = range;

        }
        public Session(string id, DateTime date, string range, List<Round> roundScores)
        {
            Id = id;
            Date = date;
            Range = range;
            RoundScores = roundScores;
            NumberOfRounds = roundScores.Count;
            BestRoundScore = roundScores.Select(round => round.TotalScore).Max();
            TotalShots = roundScores.Select(round => round.Scores.Count).Sum();
            TotalBullseye = roundScores.Select(round => round.Scores.Count(score => score == 2)).Sum();
            TotalOnTarget = roundScores.Select(round => round.Scores.Count(score => score > 0)).Sum();
            TotalMisses = roundScores.Select(round => round.Scores.Count(score => score == 0)).Sum();
            
            AverageRoundScore = Math.Round(roundScores.Select(round => round.TotalScore).Average(), 0);
            AccuracyOnTarget = Math.Round(TotalOnTarget * 100.0 / TotalShots, 2);
            AccuracyBullseye = Math.Round(TotalBullseye * 100.0 / TotalShots, 2);
        }
    }
}