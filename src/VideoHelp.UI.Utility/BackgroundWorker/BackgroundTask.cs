using System;

namespace VideoHelp.UI.Utility.BackgroundWorker
{
    public class BackgroundTask
    {
        public BackgroundTask(string name, TimeSpan runInterval, Action task)
        {
            Name = name;
            RunInterval = runInterval;
            Task = task;
        }

        public string Name { get; set; }
        public TimeSpan RunInterval { get; set; }
        public Action Task { get; set; }
    }
}