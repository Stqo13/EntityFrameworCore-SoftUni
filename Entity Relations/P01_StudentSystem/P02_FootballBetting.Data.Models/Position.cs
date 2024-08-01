using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
