﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Messim.UI.Models;

namespace Messim.UI.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindById(int id);
        void Add(T newEntity);
        void Remove(T entity);
    }
}