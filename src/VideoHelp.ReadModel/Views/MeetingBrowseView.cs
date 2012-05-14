using System.Collections.Generic;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingBrowseView
    {
        public int PageSize { get; private set; }
        public int Page { get; private set; }
        public string SearchText { get; private set; }
        public IEnumerable<MeetingBrowseItem> Items { get; private set; }

        public MeetingBrowseView(int pageSize, int page, string searchText, IEnumerable<MeetingBrowseItem> items)
        {
            PageSize = pageSize;
            Page = page;
            SearchText = searchText;
            Items = items;
        }
    }
}