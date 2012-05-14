namespace VideoHelp.ReadModel
{
    public interface IViewRepository
    {
        TOutput Load<TInput, TOutput>(TInput input);
    }
}