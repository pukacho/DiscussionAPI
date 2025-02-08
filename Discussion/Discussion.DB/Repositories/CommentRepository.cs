using Discussion.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Discussion.Domain.Entities;

namespace Discussion.DB.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DiscussionDbContext _context;

        public CommentRepository(DiscussionDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetCommentsByDiscussionIdAsync(int discussionId)
        {
            return await _context.Comments
                .Where(c => c.DiscussionId == discussionId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}