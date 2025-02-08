using Discussion.Common.Interfaces;
using Discussion.Core.Commands;
using Discussion.Core.Handlers;
using Discussion.DB;
using Discussion.DB.Repositories;
using Discussion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Discussion.UnitTests
{
    public class CreateDiscussionCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_New_Discussion_And_Return_Id()
        {
            // Arrange: Create in-memory DB options.
            var options = new DbContextOptionsBuilder<DiscussionDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            using var context = new DiscussionDbContext(options);
            ICommentRepository repository = new CommentRepository(context);
            var handler = new CreateDiscussionCommandHandler(repository);
            
            var command = new CreateDiscussionCommand 
            { 
                Username = "professor1", 
                Content = "How is your day going so far?" 
            };

            // Act
            var discussionId = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(discussionId > 0);
            var createdComment = await context.Comments.FindAsync(discussionId);
            Assert.NotNull(createdComment);
            Assert.Null(createdComment.ParentId); // top-level discussion
            Assert.Equal(discussionId, createdComment.DiscussionId);
            Assert.Equal("professor1", createdComment.Username);
        }
    }
}