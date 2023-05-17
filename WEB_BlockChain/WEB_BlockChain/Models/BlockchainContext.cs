using Microsoft.EntityFrameworkCore;

namespace WEB_BlockChain.Models
{
    public class BlockchainContext : DbContext
    {
        public BlockchainContext(DbContextOptions<BlockchainContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Block> Blocks { get; set; }
    }
}
