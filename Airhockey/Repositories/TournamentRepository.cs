using Airhockey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airhockey.Repositories;
using System.Diagnostics;

namespace Airhockey.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly howestairhockeydbContext db;

        public TournamentRepository(howestairhockeydbContext tournamentDbContext)
        {
            db = tournamentDbContext;
        }
        public Tournaments CreateTournament(Tournaments tournaments)
        {
            tournaments.IsActive = false;
            tournaments.Code = RandomString(6);
            db.Tournaments.Add(tournaments);
            db.SaveChanges();

            return tournaments;
        }

        public Tournaments GetTournament(string code)
        {
           return db.Tournaments.Where(t => t.Code == code).Select(t => t).FirstOrDefault();
        }

        public Tournaments UpdateActive(string code)
        {
            Tournaments oldTournament = GetTournament(code);
            if(oldTournament != null)
            {
                oldTournament.IsActive = !oldTournament.IsActive;
                Console.WriteLine("hallo");
                db.SaveChanges();
                if (oldTournament.IsActive)
                {
                    Console.WriteLine("fd");
                    CreateFirstRound(oldTournament);
                }
            }
            return GetTournament(oldTournament.Code);
        }

        private void CreateFirstRound(Tournaments tournament)
        {
            IQueryable<Player> players = db.Player.Where(p => p.TournamentId == tournament.Id).Select(p => p);
            Console.WriteLine("ds");
            if (players.Count() >= 2)
            {
                for(int i=0; i<players.Count(); i += 2)
                {
                    Player[] gamePlayers = players.Skip(i).Take(2).ToArray();
                    Console.WriteLine(gamePlayers);
                    Player player1 = gamePlayers[0];
                    Player player2 = gamePlayers[1];
                    Console.WriteLine("qdsff");
                    Games game = new Games();
                    game.GameName = player1.Nickname + " - " + player2.Nickname;
                    game.PlayerOne = player1.Nickname;
                    game.PlayerTwo = player2.Nickname;
                    game.ScorePlayerOne = 0;
                    game.ScorePlayerTwo = 0;
                    game.FaultsPlayerOne = 0;
                    game.FaultsPlayerTwo = 0;
                    game.TournamentId = tournament.Id;
                    game.IsPlaying = AreTablesAvailable(tournament);
                    Console.WriteLine("test");
                    game.TableId = GetTableId(tournament, game.IsPlaying);
                    Console.WriteLine("joe mamam");
                    db.Games.Add(game);
                    db.SaveChanges();
                }
            }
        }

        private bool AreTablesAvailable(Tournaments tournament)
        {
            IQueryable<AirhockeyTable> tables = db.AirhockeyTable.Where(t => t.TournamentId == tournament.Id).Select(t=>t);
            return tables.Count() < tournament.TableAmount;

        }

        private int GetTableId(Tournaments tournament, bool isAvailable)
        {
            if (isAvailable)
            {
                AirhockeyTable table = new AirhockeyTable();
                table.TournamentId = tournament.Id;
                db.AirhockeyTable.Add(table);
                db.SaveChanges();
                return table.Id;
            }
            else
            {
                IQueryable<AirhockeyTable> tables = db.AirhockeyTable.Where(t => t.TournamentId == tournament.Id).Select(t => t);
                IQueryable<Games> games = db.Games.Where(g => g.TournamentId == tournament.Id).Select(g => g);
                int highestAmountofGamesQueued = -1;
                foreach(AirhockeyTable table in tables)
                {
                    int queuedGamesOnTable = games.Where(g => g.TableId == table.Id).Select(g => g).Count();
                    if (queuedGamesOnTable == 1)
                    {
                        return table.Id;
                    }
                    else
                    {
                        if(queuedGamesOnTable < highestAmountofGamesQueued)
                        {
                            return table.Id;
                        }

                        if(highestAmountofGamesQueued < queuedGamesOnTable)
                        {
                            highestAmountofGamesQueued = queuedGamesOnTable;
                        }
                    }
                }
            }
            return -1;
        }

        private string RandomString(int size, bool lowerCase = false)
        {
            Random _random = new Random();
            StringBuilder builder = new StringBuilder(size);
            
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
