using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; set; } = null!;

        public int AwayTeamId { get; set; }

        [ForeignKey(nameof(AwayTeamId))]
        public virtual Team AwayTeam { get; set; } = null!;

        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public double HomeTeamBetRate { get; set; }
        public double AwayTeamBetRate { get; set; }
        public double DrawBetRate { get; set; }
        public DateTime DateTime { get; set; }
        public string? Result { get; set; }

        public virtual List<PlayerStatistic> PlayersStatistics { get; set; } = new List<PlayerStatistic>();
        public virtual List<Bet> Bets { get; set; } = new List<Bet>();
    }
}
