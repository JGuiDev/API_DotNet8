using CURSO_API.Data;
using CURSO_API.Interfaces;
using CURSO_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CURSO_API.Respository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDBContext _context;

        public CommentRepository(AppDBContext context) 
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _context.Commnets.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Commnets.FirstOrDefaultAsync(x => x.Id == id);

            if(comment == null)
            {
                return null;
            }

            _context.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Commnets.ToListAsync();
        }

        public async Task<Comment?> GetIDAsync(int id)
        {
            var comment = await _context.Commnets.FindAsync(id);

            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Commnets.FindAsync(id);

            if(existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;


        }
    }
}
