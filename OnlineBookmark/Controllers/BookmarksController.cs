using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookmark.Models.Bookmarks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBookmark.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        public BookmarksController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBookmark(BookmarkCreationModel model, string returnUrl)
        {
            // 画像が送られてきたら保存
            if (model.ImageFile != null)
            {
                var imageFilePath = await this.SaveBookmarkImageAsync(model.ImageFile);
                if (imageFilePath == null)
                    return BadRequest("Fail to save the bookmark image.");
            }


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

            // ファイルを保存するディレクトリのパスを作成
            var tempDirPath = Path.Combine(new string[]
            {
                this._hostingEnvironment.WebRootPath,
                "bookmarks",
                "images",
                Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Replace("/", "-")
                    .Replace("+", "_")
                    .Replace("=", "")
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

            // ファイルパスを作成
            var tempFilePath = Path.Combine(new string[] {
                tempDirPath,
                $"{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(imageFile.FileName)}"
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

            return tempFilePath;
        }
    }
}
