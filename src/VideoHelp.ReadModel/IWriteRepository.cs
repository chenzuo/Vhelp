namespace VideoHelp.ReadModel
{
    public interface IWriteRepository
    {
        void Add<T>(T value);
        void SaveChanges();
    }
}