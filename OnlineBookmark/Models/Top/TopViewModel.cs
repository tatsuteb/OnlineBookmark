using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookmark.Models.Bookmarks;

namespace OnlineBookmark.Models.Top
{
    public class TopViewModel
    {
        public IEnumerable<BookmarkViewModel> BookmarkViewModels { get; set; }

        /// <summary>
        /// ブックマーク投稿用
        /// </summary>
        public BookmarkCreationViewModel BookmarkCreationViewModel { get; set; }
    }



    public class BookmarkViewModel
    {
        public string Path { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string UserIconPath { get; set; }
    }
}
