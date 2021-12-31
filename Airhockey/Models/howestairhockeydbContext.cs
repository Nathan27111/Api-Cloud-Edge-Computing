using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airhockey.Models
{
    public partial class howestairhockeydbContext : DbContext
    {
        public howestairhockeydbContext()
        {
        }

        public howestairhockeydbContext(DbContextOptions<howestairhockeydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AirhockeyTable> AirhockeyTable { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Tournaments> Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirhockeyTable>(entity =>
            {
                entity.ToTable("airhockey_table");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.AirhockeyTable)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__airhockey__tourn__76969D2E");
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.ToTable("games");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FaultsPlayerOne).HasColumnName("faults_player_one");

                entity.Property(e => e.FaultsPlayerTwo).HasColumnName("faults_player_two");

                entity.Property(e => e.GameName)
                    .HasColumnName("game_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsPlaying).HasColumnName("is_playing");

                entity.Property(e => e.PlayerOne)
                    .HasColumnName("player_one")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerTwo)
                    .HasColumnName("player_two")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ScorePlayerOne).HasColumnName("score_player_one");

                entity.Property(e => e.ScorePlayerTwo).HasColumnName("score_player_two");

                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__games__table_id__7C4F7684");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__games__tournamen__7D439ABD");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ranking).HasColumnName("ranking");

                entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__player__tourname__75A278F5");
            });

            modelBuilder.Entity<Tournaments>(entity =>
            {
                entity.ToTable("tournaments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerAmount).HasColumnName("player_amount");

                entity.Property(e => e.TableAmount).HasColumnName("table_amount");

                entity.Property(e => e.TimePerMatch).HasColumnName("time_per_match");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
