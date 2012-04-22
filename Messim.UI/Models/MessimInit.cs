using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Transactions;

namespace Messim.UI.Models
{
    public class MessimInit : IDatabaseInitializer<MessimContext>
    {
        #region IDatabaseInitializer<MessimContext> Members

        public void InitializeDatabase(MessimContext context)
        {
            bool alreadyCreated;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                try
                {
                    alreadyCreated = context.Users.Any();
                }
                catch (Exception)
                {
                    alreadyCreated = false;
                }
            }
            if (!alreadyCreated)
            {
                // remove all tables
                context.Database.ExecuteSqlCommand("EXEC sp_MSforeachtable @command1 = \"DROP TABLE ?\"");

                // create all tables
                var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                context.Database.ExecuteSqlCommand(dbCreationScript);

                Seed(context);
                context.SaveChanges();
            }

        }

        #endregion

        #region Methods

        protected virtual void Seed(MessimContext context)
        {
            context.Users.Add(new User { Username = "soltys", Password = "B1B3773A05C0ED0176787A4F1574FF0075F7521E" });
            context.Users.Add(new User { Username = "john", Password = "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8" });
        }

        #endregion
    }
}