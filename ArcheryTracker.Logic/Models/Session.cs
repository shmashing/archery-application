using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTracker.Logic.Models
{
    public class Session
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int Range { get; set; }
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

        public Session(String id, String userId, DateTime date, int range)
        {
            Id = id;
            UserId = userId;
            Date = date;
            Range = range;

        }
        public Session(string id, String userId, DateTime date, int range, List<Round> roundScores)
        {
            Id = id;
            UserId = userId;
            Date = date;
            Range = range;
            RoundScores = roundScores;
            NumberOfRounds = roundScores.Count;
        }

        public void CalculateStats()
        {
            if (NumberOfRounds == 0)
            {
                return;
            }
            
            BestRoundScore = RoundScores.Select(round => round.TotalScore).Max();
            TotalShots = RoundScores.Select(round => round.Scores.Count).Sum();
            TotalBullseye = RoundScores.Select(round => round.Scores.Count(score => score == 2)).Sum();
            TotalOnTarget = RoundScores.Select(round => round.Scores.Count(score => score > 0)).Sum();
            TotalMisses = RoundScores.Select(round => round.Scores.Count(score => score == 0)).Sum();
            
            AverageRoundScore = Math.Round(RoundScores.Select(round => round.TotalScore).Average(), 0);
            AccuracyOnTarget = Math.Round(TotalOnTarget * 100.0 / TotalShots, 2);
            AccuracyBullseye = Math.Round(TotalBullseye * 100.0 / TotalShots, 2);
        }
    }
}