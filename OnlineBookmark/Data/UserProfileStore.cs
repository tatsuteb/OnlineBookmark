using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookmark.Data.Interfaces;
using OnlineBookmark.Data.Models;

namespace OnlineBookmark.Data
{
    public class UserProfileStore : IUserProfileStore
    {
        private readonly OnlineBookmarkDbContext _dbContext;


        public UserProfileStore(OnlineBookmarkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task<bool> CreateAsync(UserProfile userProfile)
        {
            try
            {
                await this._dbContext.UserProfiles.AddAsync(userProfile);
                await this._dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}
