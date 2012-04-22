using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Messim.UI.Models;

namespace Messim.UI.Repository
{
    public class UserRepository : IRepository<User>
    {
        public IEnumerable<User> FindAll()
        {
            using (var db = new MessimContext())
            {
                return db.Users.ToList();
            }
        }

        public IEnumerable<User> FindBy(Func<User, bool> predicate)
        {
            
            using (var db = new MessimContext())
            {
                return db.Users.Where(predicate.Invoke);
            }
        }

        public User FindById(int id)
        {
            using (var db = new MessimContext())
            {
                return db.Users.SingleOrDefault(user => user.ID == id);
            }
        }

        public void Add(User newEntity)
        {
            using (var db = new MessimContext())
            {
                db.Users.Add(newEntity);
                db.SaveChanges();
            }
        }

        public void Edit(User entity)
        {
            using (var db = new MessimContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Remove(User entity)
        {
            using (var db = new MessimContext())
            {
                db.Users.Remove(entity);
                db.SaveChanges();
            }
        }
    }
}