using Entities;
using Entities.DTOs;
using EntityService.Context;
using EntityService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService
{
    public class CommentService : ICommentService
    {


        public async Task<List<Comment>> GetComment()
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                return await serviceDB.Comments.ToListAsync();
            }
        }
        public async Task<Comment> GetCommentById(int id)
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                return await serviceDB.Comments.FindAsync(id);
            }
        }

        public async Task<string> UpdateComment(Comment comment)
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                Comment c = await serviceDB.Comments.FindAsync(comment.CommentId);
                if (c == null)
                {
                    return "Update comment failed: Comment can not be found";
                }

                Post p = await serviceDB.Posts.FindAsync(comment.PostId);

                if(p == null)
                {
                    return "Update comment failed: PostId can not be found.";
                }
                serviceDB.Comments.Remove(c);
                serviceDB.Comments.Add(comment);

                await serviceDB.SaveChangesAsync();
                return "Update comment Sucessful";
            }
        }

        public async Task<string> AddComment(Comment comment)
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                Post p = await serviceDB.Posts.FindAsync(comment.PostId);
                if (p == null)
                {
                    return "Update comment failed: PostId can not be found.";
                }
                serviceDB.Comments.Add(comment);
                await serviceDB.SaveChangesAsync();
                return "Add a comment successful.";
            }


        }
        public async Task<string> DeleteComment(int id)
        {
            using (ServiceDBContext serviceDB = new ServiceDBContext())
            {
                Comment comment = await serviceDB.Comments.FindAsync(id);

                if (comment == null)
                {
                    return "Delete comment failed: Comment can not be found.";
                }

                serviceDB.Comments.Remove(comment);
                await serviceDB.SaveChangesAsync();
                return "Comment deleted successfully";
            }
        }


    }
}
