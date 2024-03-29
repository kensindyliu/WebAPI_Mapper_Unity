using Entities;
using EntityService.Context;
using EntityService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityService
{
    public class PostService : IPostService
    {
        public async Task AddPost(Post post)
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                serviceDB.Posts.Add(post);
                await serviceDB.SaveChangesAsync();
            }
        }
        public async Task<Post> GetPostsById(int id)
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                return await serviceDB.Posts.FindAsync(id);
            }
        }

        public async Task<List<Post>> GetPosts()
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                return await serviceDB.Posts.ToListAsync();
            }
        }

        public async Task<string> UpdatePost(Post post)
        {
            ServiceDBContext serviceDB = new ServiceDBContext();
            Post p = serviceDB.Posts.FirstOrDefault(po=>po.PostId==post.PostId);
            if(p == null)
            {
                return "Update post failed: Can not find the post";
            }
            serviceDB.Remove(p);
            serviceDB.Add(post);
            await serviceDB.SaveChangesAsync();
            return "Update post Sucessful";
        }


        public async Task<string> DeletePost(int id)
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                Post p = await serviceDB.Posts.FindAsync(id);

                if (p == null)
                {
                    return "Post not found"; 
                }
                serviceDB.Posts.Remove(p);
                List<Comment> comments = await serviceDB.Comments.Where(c => c.PostId == id).ToListAsync();
                serviceDB.Comments.RemoveRange(comments);
                await serviceDB.SaveChangesAsync();
                return "Post deleted successfully"; 
            }
        }


    }
}
