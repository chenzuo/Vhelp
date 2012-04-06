using System;
using System.Linq;

namespace VideoHelp.ReadModel
{
    public interface IReadRepository
    {
        IQueryable<T> GetAll<T>() where T : class;
        T GetById<T>(Guid id) where T : class;
        void SaveChanges();
    }
}