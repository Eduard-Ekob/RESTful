using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Games.Models
{
    public class BoardGamesContext : DbContext
    {
        public DbSet<BoardGame> BoardGames { get; set; }

        public BoardGamesContext(DbContextOptions<BoardGamesContext> options) 
            : base(options)
        { }

    }
}
