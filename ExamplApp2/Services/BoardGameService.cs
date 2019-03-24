using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Games.Services
{

    public class BoardGameService
    {
        private BoardGamesContext db;
        private IMemoryCache cache;

        public BoardGameService(BoardGamesContext context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
        }

        public void Initialize()
        {
            if (!db.BoardGames.Any())
            {
                db.BoardGames.Add(new BoardGame { Name = "Monopoly", Price = 12 });
                db.BoardGames.Add(new BoardGame { Name = "Hero Quest", Price = 14 });
                db.SaveChanges();
            }
        }

        public async Task<List<BoardGame>> GetBoardGamesAsync()
        {
            return await db.BoardGames.ToListAsync();
        }

        public void AddBoardGame(BoardGame boardGame)
        {
            db.BoardGames.Add(boardGame);
            int i = db.SaveChanges();
            if (i > 0)
            {
                cache.Set(boardGame.Id, boardGame, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
        }

        public async Task<BoardGame> GetBoardGameAsync(int id)
        {
            BoardGame boardGame = null;

            if (!cache.TryGetValue(id, out boardGame))
            {
                boardGame = await db.BoardGames.FirstOrDefaultAsync(p => p.Id == id);
                if (boardGame != null)
                {
                    cache.Set(boardGame.Id, boardGame,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return boardGame;
        }
    }
}
