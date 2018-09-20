using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineBookmark.Models.Bookmarks
{
    public class BookmarkCreationModel
    {
        public IFormFile ImageFile { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate = false;
    }
}
