namespace VideoHelp.ReadModel
{
    public interface IViewFactory<TInput, TOutput>
    {
        TOutput Load(TInput input);
    }
}