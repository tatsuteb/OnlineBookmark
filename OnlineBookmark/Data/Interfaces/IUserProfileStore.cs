using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookmark.Data.Models;

namespace OnlineBookmark.Data.Interfaces
{
    public interface IUserProfileStore
    {
        Task<bool> CreateAsync(UserProfile userProfile);
    }
}
