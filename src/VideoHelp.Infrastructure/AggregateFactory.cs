using System;
using CommonDomain;
using CommonDomain.Persistence;

namespace VideoHelp.Infrastructure
{
    /// <summary>
    /// Factory for creating aggregates from with their Id using a private constructor that accespts
    /// only one paramenter, the id of the aggregate to create.
    /// This factory is used by the event store to create an aggregate prior to replaying it's events.
    /// </summary>
    public class AggregateFactory : IConstructAggregates
    {
        //public IAggregate Build (Type type, Guid id, IMemento snapshot)
        //{
        //    ConstructorInfo constructor = type.GetConstructor(
        //        BindingFlags.NonPublic | BindingFlags.Instance, null, new [] { typeof(Guid) }, null);

        //    return constructor.Invoke(new object[] { id }) as IAggregate;
        //}

        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            //return Activator.CreateInstance(type, id) as IAggregate; 
            return Activator.CreateInstance(type) as IAggregate; // todo
        }
    }
}