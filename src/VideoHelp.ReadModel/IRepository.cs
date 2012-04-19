using System;
using System.Linq;

namespace VideoHelp.ReadModel
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> GetAll<T>() where T : class;
        T GetById<T>(Guid id) where T : class;
        void Store<T>(T value) where T : class;
    }
}