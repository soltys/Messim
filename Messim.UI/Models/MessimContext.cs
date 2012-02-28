using System.Data.Entity;

namespace Messim.UI.Models
{
    public class MessimContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }
    }


}