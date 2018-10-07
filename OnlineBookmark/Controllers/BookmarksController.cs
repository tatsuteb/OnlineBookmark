using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookmark.Areas.Identity.Data;
using OnlineBookmark.Data;
using OnlineBookmark.Data.Models;
using OnlineBookmark.Models;
using OnlineBookmark.Models.Bookmarks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBookmark.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SignInManager<OnlineBookmarkUser> _signInManager;
        private readonly UserManager<OnlineBookmarkUser> _userManager;
        private readonly OnlineBookmarkDbContext _dbContext;


        public BookmarksController(
            IHostingEnvironment hostingEnvironment,
            SignInManager<OnlineBookmarkUser> signInManager,
            UserManager<OnlineBookmarkUser> userManager,
            OnlineBookmarkDbContext dbContext
            )
        {
            this._hostingEnvironment = hostingEnvironment;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._dbContext = dbContext;
        }


        /// <summary>
        /// ブックマークを登録
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBookmark(BookmarkCreationViewModel viewModel, string returnUrl)
        {
            if (!this._signInManager.IsSignedIn(HttpContext.User))
                return Unauthorized();

            var user = await this._userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return BadRequest("User is not found.");

            var bookmarkBase = new BookmarkBase()
            {
                BaseBid = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Replace("/", "-")
                    .Replace("+", "_")
                    .Replace("=", ""),
                OwnerUid = user.Uid,
                LinkedUrl = viewModel.Url
            };

            // 画像が送られてきたら保存
            if (viewModel.ImageFile != null)
            {
                var imageFilePath = await this.SaveBookmarkImageAsync(viewModel.ImageFile);
                if (imageFilePath == null)
                    return BadRequest("Fail to save the bookmark image.");

                bookmarkBase.ImageFilePath = imageFilePath;
            }


            // ブックマークの基本情報をDBに保存
            await this._dbContext.BookmarkBases
                .AddAsync(bookmarkBase);


            // ブックマークをDBに保存
            var bookmark = new Bookmark()
            {
                Bid = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Replace("/", "-")
                    .Replace("+", "_")
                    .Replace("=", ""),
                BaseBid = bookmarkBase.BaseBid,
                Title = viewModel.Title,
                Description = viewModel.Description
            };
            await this._dbContext.Bookmarks
                .AddAsync(bookmark);


            // ユーザーとブックマークの関連をDBに保存
            await this._dbContext.UserBookmarks
                .AddAsync(new UserBookmark()
                {
                    Uid = user.Uid,
                    Bid = bookmark.Bid,
                    IsPrivate = viewModel.IsPrivate
                });


            // DBへの変更を保存
            await this._dbContext.SaveChangesAsync();


            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Top");

            return Redirect(returnUrl);
        }


        /// <summary>
        /// 画像ファイルをbookmarks/images/<base64string>/フォルダに保存して、ファイルパスを返す
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        private async Task<string> SaveBookmarkImageAsync(IFormFile imageFile)
        {
            if (imageFile == null)
                return null;

            // HTMLから参照する相対パスを作成
            var relativePath = Path.Combine(new string[]
            {
                "bookmarks",
                "images",
                Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Replace("/", "-")
                    .Replace("+", "_")
                    .Replace("=", "")
            });

            // ファイルを保存するディレクトリのパスを作成
            var tempDirPath = Path.Combine(new string[]
            {
                this._hostingEnvironment.WebRootPath,
                relativePath
            });

            // フォルダを作成
            if (!Directory.Exists(tempDirPath))
            {
                try
                {
                    Directory.CreateDirectory(tempDirPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

            // ファイル名を作成
            var tempFileName = $"{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(imageFile.FileName)}";
            // ファイルパスを作成
            var tempFilePath = Path.Combine(new string[] {
                tempDirPath,
                tempFileName
            });

            try
            {
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            
            return Path.Combine(new string[]
            {
                relativePath,
                tempFileName
            });
        }
    }
}
