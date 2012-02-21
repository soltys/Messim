using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Transactions;
using System.Linq;
namespace Messim.UI.Models
{
    public class MessimContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }
    }


}