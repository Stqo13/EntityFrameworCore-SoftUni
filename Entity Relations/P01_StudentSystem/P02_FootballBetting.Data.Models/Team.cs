using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR")]
        public string Name { get; set; } = null!;

        [Column("NVARCHAR")]
        public string LogoUrl { get; set; } = null!;

        [Required]
        [MaxLength(5)]
        [Column(TypeName = "NVARCHAR")]
        public string Initials { get; set; } = null!;

        [Required]
        [Column(TypeName = "DECIMAL(10, 4)")]
        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }
        [ForeignKey(nameof(PrimaryKitColorId))]
        public virtual Color PrimaryKitColor { get; set; } = null!;

        public int SecondaryKitColorId { get; set; }
        [ForeignKey(nameof(SecondaryKitColorId))]
        public virtual Color SecondaryKitColor { get;set; } = null!;

        public int TownId { get; set; }

        public virtual Town Town { get; set;} = null!;

        [InverseProperty("HomeTeam")]
        public List<Game> HomeGames { get; set; } = new List<Game>();

        [InverseProperty("AwayTeam")]
        public List<Game> AwayGames { get; set; } = new List<Game>();

        public List<Player> Players { get; set; } = new List<Player>(); 
    }
}
