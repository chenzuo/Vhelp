using System;

namespace VideoHelp.ReadModel.Documents
{
    public interface IDocument
    {
        string Id { get; }
        Guid DocumentId { set; get; } 
    }

    public abstract class BaseDocument : IDocument
    {
        public string Id
        {
            get { return this.GetId(); }
        }

        public Guid DocumentId { get; set; }
    }

    public static class RavenDb
    {
        public static string GetId<T>(Guid id) where T : IDocument
        {
            return string.Format("{0}/{1}", typeof(T).Name, id);
        }

        public static string GetId(this IDocument document)
        {
            return string.Format("{0}/{1}", document.GetType().Name, document.DocumentId);
        }
    }
}