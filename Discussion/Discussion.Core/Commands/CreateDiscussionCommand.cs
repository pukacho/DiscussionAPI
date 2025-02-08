using MediatR;

namespace Discussion.Core.Commands
{
    public class CreateDiscussionCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
