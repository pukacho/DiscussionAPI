using Microsoft.AspNetCore.Mvc;
using MediatR;
using Discussion.Core.Commands;
using Discussion.Core.Queries;

namespace Discussion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DiscussionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new discussion.
        /// </summary>
        /// <param name="command">Contains the username and discussion question text.</param>
        /// <returns>New discussion Id</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDiscussion([FromBody] CreateDiscussionCommand command)
        {
            var discussionId = await _mediator.Send(command);
            return Ok(new { DiscussionId = discussionId });
        }

        /// <summary>
        /// Create a reply to an existing discussion or comment.
        /// </summary>
        /// <param name="command">Contains the username, ParentId, and reply content.</param>
        /// <returns>New comment Id</returns>
        [HttpPost("comments")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
        {
            var commentId = await _mediator.Send(command);
            return Ok(new { CommentId = commentId });
        }

        /// <summary>
        /// Retrieve all comments (in a flat list) for the given discussion.
        /// </summary>
        /// <param name="discussionId">Discussion Id</param>
        /// <returns>List of comments</returns>
        [HttpGet("{discussionId}/comments")]
        public async Task<IActionResult> GetDiscussionComments([FromRoute] int discussionId)
        {
            var query = new GetDiscussionCommentsQuery { DiscussionId = discussionId };
            var comments = await _mediator.Send(query);
            return Ok(comments);
        }
    }
}