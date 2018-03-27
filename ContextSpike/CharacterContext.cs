using Microsoft.EntityFrameworkCore;

namespace ContextSpike
{
    public class CharacterContext : DbContext
    {
        public CharacterContext()
        {
            
        }

        public CharacterContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Character> Characters { get; set; }
    }
}