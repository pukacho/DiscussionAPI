using MediatR;

namespace Discussion.Core.Commands
{
    public class CreateCommentCommand : IRequest<int>
    {
        // The username of the user creating the reply.
        public string Username { get; set; }
        public int ParentId { get; set; }
        public string Content { get; set; }
    }
}