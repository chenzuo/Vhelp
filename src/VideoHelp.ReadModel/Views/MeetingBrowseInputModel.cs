using System;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingBrowseInputModel
    {
        public MeetingBrowseInputModel(int page, int pageSize, string searchText)
        {
            Page = page;
            PageSize = pageSize;
            SearchText = searchText;
        }

        public MeetingBrowseInputModel(int page, int pageSize) : this(page, pageSize, null){}

        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
    }
}