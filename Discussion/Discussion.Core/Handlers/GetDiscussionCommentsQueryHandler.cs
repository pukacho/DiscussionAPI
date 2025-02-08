using MediatR;
using Discussion.Core.Queries;
using Discussion.Common.DTOs;
using Discussion.Common.Interfaces;


namespace Discussion.Core.Handlers
{
    public class GetDiscussionCommentsQueryHandler : IRequestHandler<GetDiscussionCommentsQuery, List<CommentDto>>
    {
        private readonly ICommentRepository _repository;

        public GetDiscussionCommentsQueryHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CommentDto>> Handle(GetDiscussionCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetCommentsByDiscussionIdAsync(request.DiscussionId);
            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                ParentId = c.ParentId,
                Username = c.Username,
                Content = c.Content,
                CreatedAt = c.CreatedAt
            }).ToList();
        }
    }
}
