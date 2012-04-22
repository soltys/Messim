using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Messim.UI.Models;

namespace Messim.UI.Repository
{
    public class MessageRepository : IRepository<Message>
    {
        public IEnumerable<Message> FindAll()
        {
            using (var db = new MessimContext())
            {
                return db.Messages.ToList();
            }
        }

        public IEnumerable<Message> FindBy(Func<Message, bool> predicate)
        {
            using (var db = new MessimContext())
            {
                return db.Messages.Where(predicate.Invoke);
            }
        }

        public Message FindById(int id)
        {
            using (var db = new MessimContext())
            {
                return db.Messages.SingleOrDefault(user => user.ID == id);
            }
        }

        public void Add(Message newEntity)
        {
            using (var db = new MessimContext())
            {
                db.Messages.Add(newEntity);
                db.SaveChanges();
            }
        }

        public void Edit(Message entity)
        {
            using (var db = new MessimContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Remove(Message entity)
        {
            using (var db = new MessimContext())
            {
                db.Messages.Remove(entity);
                db.SaveChanges();
            }
        }
    }
}