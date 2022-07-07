﻿using Microsoft.EntityFrameworkCore;
using MyHotel.Data;
using MyHotel.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHotel.Persistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly MyHotelDbContext _dbContext;

        public BaseRepository(MyHotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public T GetById(Guid id)
        {
            return  _dbContext.Set<T>().Find(id);
        }

        public IReadOnlyList<T> ListAll()
        {
            return  _dbContext.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
             _dbContext.SaveChangesAsync();
        }

    }
}