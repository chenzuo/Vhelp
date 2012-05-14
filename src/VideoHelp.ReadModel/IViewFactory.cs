namespace VideoHelp.ReadModel
{
    public interface IViewFactory<TInput, TOutput>
    {
        TOutput Load(TInput input);
    }

    public interface IViewFactory<TOutput>
    {
        TOutput Load();
    }
}