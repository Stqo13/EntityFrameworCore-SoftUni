using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
            
        }

        public FootballBettingContext(DbContextOptions<FootballBettingContext> options)
            :base(options)
        {

        }

        public virtual DbSet<Team>? Teams { get; set; }
        public virtual DbSet<Color>? Colors { get; set; }
        public virtual DbSet<Town>? Towns { get; set; }
        public virtual DbSet<Country>? Countries { get; set; }
        public virtual DbSet<Player>? Players { get; set; }
        public virtual DbSet<Position>? Positions { get; set; }
        public virtual DbSet<PlayerStatistic>? PlayersStatistics { get; set; }
        public virtual DbSet<Game>? Games { get; set; }
        public virtual DbSet<Bet>? Bets { get; set; }
        public virtual DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerStatistic>()
                .HasKey(ps => new { ps.PlayerId, ps.GameId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                string connectionString = "Server=DESKTOP-A8P7BPS\\SQLEXPRESS;Database=SoftUni;Integrated Security=true;TrustServerCertificate=true;";

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
