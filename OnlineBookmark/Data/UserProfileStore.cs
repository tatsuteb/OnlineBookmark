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


        public async Task<UserProfile> GetUserProfileByUidAsync(string uid)
        {
            var userProfile = await this._dbContext.UserProfiles
                .FindAsync(uid);

            return userProfile;
        }


        public async Task<bool> DeleteAsync(UserProfile userProfile)
        {
            if (userProfile == null)
                return false;

            try
            {
                this._dbContext
                    .Remove(userProfile);
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
