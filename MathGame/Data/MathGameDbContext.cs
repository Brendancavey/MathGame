using MathGame.Models;
using Microsoft.EntityFrameworkCore;

namespace MathGame.Data
{
    public class MathGameDbContext : DbContext
    {
        public MathGameDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MathQuestion> mathQuestions { get; set; }

        public DbSet <User> users { get; set; }
    }

}
