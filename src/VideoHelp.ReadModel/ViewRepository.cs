using System;

namespace VideoHelp.ReadModel
{
    public class ViewRepository : IViewRepository
    {
        private readonly Func<Type, object> _containerResolver;

        public ViewRepository(Func<Type, object> containerResolver)
        {
            _containerResolver = containerResolver;
        }

        public TOutput Load<TInput, TOutput>(TInput input)
        {
            var factory = _containerResolver(typeof(IViewFactory<TInput, TOutput>)) as  IViewFactory<TInput, TOutput>;
            return factory == null ? default(TOutput) : factory.Load(input);
        }
    }
}