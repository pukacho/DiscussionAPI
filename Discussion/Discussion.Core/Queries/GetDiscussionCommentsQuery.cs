using MediatR;
using Discussion.Common.DTOs;
using System.Collections.Generic;

namespace Discussion.Core.Queries
{
    public class GetDiscussionCommentsQuery : IRequest<List<CommentDto>>
    {
        public int DiscussionId { get; set; }
    }
}