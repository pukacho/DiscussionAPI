using Discussion.Common.Interfaces;
using MediatR;
using Discussion.Core.Commands;
using Discussion.Domain.Entities;


namespace Discussion.Core.Handlers
{
    public class CreateDiscussionCommandHandler : IRequestHandler<CreateDiscussionCommand, int>
    {
        private readonly ICommentRepository _repository;

        public CreateDiscussionCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateDiscussionCommand request, CancellationToken cancellationToken)
        {
            // Create a top-level discussion (comment with no parent)
            var comment = new Comment
            {
                Username = request.Username,
                Content = request.Content,
                CreatedAt = System.DateTime.UtcNow,
                ParentId = null
            };

            await _repository.AddAsync(comment);
            await _repository.SaveChangesAsync();

            // Set DiscussionId to the comment's own Id after saving.
            comment.DiscussionId = comment.Id;
            await _repository.SaveChangesAsync();

            return comment.Id;
        }
    }
}