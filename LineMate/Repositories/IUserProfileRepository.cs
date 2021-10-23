using LineMate.Models;
using System.Collections.Generic;

namespace LineMate.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByFireBaseUserId(string firebaseUserId);
        void Add(UserProfile userProfile);
        List<UserProfile> GetAllUsers();
    }
}