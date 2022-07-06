using System;
using System.Collections.Generic;

namespace MyHotel.Domain.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(Guid id);
        IReadOnlyList<T> ListAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
