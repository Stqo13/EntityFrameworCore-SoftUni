using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        [Key]
        public int BetId { get; set; }

        [Column(TypeName = "DECIMAL(10, 4)")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public string Prediction { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; } = null!;
    }
}
