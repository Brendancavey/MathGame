using MathGame.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MathGame.Data
{
    public class MathGameDbContext : IdentityDbContext
    {
        public MathGameDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MathQuestion> mathQuestions { get; set; }

        public DbSet <User> users { get; set; }
    }

}
