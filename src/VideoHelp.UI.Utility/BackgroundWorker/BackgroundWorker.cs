using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace VideoHelp.UI.Utility.BackgroundWorker
{
    public class BackgroundWorker
    {
        private readonly Dictionary<string, BackgroundTask> _tasks;
        
        public BackgroundWorker()
        {
            _tasks = new Dictionary<string, BackgroundTask>();
        }

        public BackgroundWorker AddTask(BackgroundTask task)
        {
            _tasks.Add(task.Name, task);
            return this;
        }

        public void Start()
        {
            foreach (var item in _tasks)
            {
                runTask(item.Key, item.Value.RunInterval);
            }
        }

        private void runTask(string taskName, TimeSpan interval)
        {
            HttpRuntime.Cache.Insert(taskName, interval, null, DateTime.Now.AddTicks(interval.Ticks), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, executeCallback);
        }

        private void executeCallback(string taskName, object interval, CacheItemRemovedReason reason)
        {
            _tasks[taskName].Task();

            runTask(taskName, (TimeSpan)interval);
        }
    }
}
