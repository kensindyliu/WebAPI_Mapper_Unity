using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetComment();
        Task<Comment> GetCommentById(int id);
        Task<string> AddComment(Comment comment);
        Task<string> UpdateComment(Comment comment);
        Task<string> DeleteComment(int id);
    }
}
