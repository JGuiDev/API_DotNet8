using CURSO_API.Models;

namespace CURSO_API.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetIDAsync(int id);
        Task<Comment> CreateCommentAsync(Comment commentModel);
        Task<Comment?> UpdateCommentAsync(int id, Comment commentModel);
        Task<Comment?> DeleteCommentAsync(int id);
    }
}
