using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBookmark.Data;
using OnlineBookmark.Models.Top;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBookmark.Controllers
{
    public class TopController : Controller
    {
        private readonly OnlineBookmarkDbContext _dbContext;

        public TopController(OnlineBookmarkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            //FIXME: クエリが複雑すぎる。Joinするかテーブルを見直す
            var userBookmarks = this._dbContext.UserBookmarks
                .Where(x => x.IsPrivate == false)
                .Select(x => new
                {
                    x.Uid,
                    x.Bid
                })
                .Select(x => new
                {
                    Bookmark = this._dbContext.Bookmarks
                        .Where(y => y.Bid == x.Bid)
                        .Select(y => new
                        {
                            BaseInfo = this._dbContext.BookmarkBases
                                .Where(z => z.BaseBid == y.BaseBid)
                                .Select(z => new
                                {
                                    z.LinkedUrl,
                                    z.ImageFilePath
                                })
                                .FirstOrDefault(),
                            y.Title,
                            y.Description
                        })
                        .FirstOrDefault(),
                    UserProfile = this._dbContext.UserProfiles
                        .Where(y => y.Uid == x.Uid)
                        .Select(y => new
                        {
                            y.DisplayName,
                            y.IconPath
                        })
                        .FirstOrDefault()
                });

            var bookmarkViewModels = new List<BookmarkViewModel>();
            foreach (var userBookmark in userBookmarks)
            {
                bookmarkViewModels.Add(new BookmarkViewModel()
                {
                    Title = userBookmark.Bookmark.Title,
                    Description = userBookmark.Bookmark.Description,
                    Path = userBookmark.Bookmark.BaseInfo.ImageFilePath,
                    Url = userBookmark.Bookmark.BaseInfo.LinkedUrl,
                    Username = userBookmark.UserProfile.DisplayName,
                    UserIconPath = userBookmark.UserProfile.IconPath
                });
            }

            var vm = new TopViewModel()
            {
                BookmarkViewModels = bookmarkViewModels
            };

            return View(vm);
        }


    }
}
