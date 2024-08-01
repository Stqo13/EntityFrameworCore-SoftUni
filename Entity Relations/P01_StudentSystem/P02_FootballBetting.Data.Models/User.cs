using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(70)]
        public string Username { get; set; } = null!;

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MinLength(10)]
        [MaxLength(25)]
        public string Password { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [Column(TypeName = "DECIMAL(10, 4)")]
        public decimal Balance { get; set; }

        public List<Bet> Bets { get; set; } = new List<Bet>();
    }
}
