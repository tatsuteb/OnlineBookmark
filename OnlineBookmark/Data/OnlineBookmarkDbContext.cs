using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBookmark.Data.Models;

namespace OnlineBookmark.Data
{
    public class OnlineBookmarkDbContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<BookmarkBase> BookmarkBases { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<UserBookmark> UserBookmarks { get; set; }


        public OnlineBookmarkDbContext(DbContextOptions<OnlineBookmarkDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
