using Discussion.Domain.Entities;

namespace Discussion.Common.Interfaces;

public interface ICommentRepository
{
    Task<Comment> GetByIdAsync(int id);
    Task AddAsync(Comment comment);
    Task SaveChangesAsync();
    Task<List<Comment>> GetCommentsByDiscussionIdAsync(int discussionId);
}