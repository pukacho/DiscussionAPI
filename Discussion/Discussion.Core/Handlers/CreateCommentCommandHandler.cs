using Discussion.Common.Interfaces;
using Discussion.Domain.Entities;
using MediatR;
using Discussion.Core.Commands;

namespace Discussion.Core.Handlers
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly ICommentRepository _repository;

        public CreateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the parent comment to get its DiscussionId.
            var parentComment = await _repository.GetByIdAsync(request.ParentId);
            if (parentComment == null)
            {
                throw new System.Exception("Parent comment not found.");
            }

            var comment = new Comment
            {
                Username = request.Username,
                Content = request.Content,
                CreatedAt = System.DateTime.UtcNow,
                ParentId = request.ParentId,
                DiscussionId = parentComment.DiscussionId
            };

            await _repository.AddAsync(comment);
            await _repository.SaveChangesAsync();

            return comment.Id;
        }
    }
}