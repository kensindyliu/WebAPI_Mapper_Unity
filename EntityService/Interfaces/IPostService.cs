using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPostsById(int id);
        Task AddPost(Post post);
        Task<string> UpdatePost(Post post);
        Task<string> DeletePost(int id);
    }
}
