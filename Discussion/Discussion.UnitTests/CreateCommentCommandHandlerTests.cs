using Discussion.Common.Interfaces;
using Discussion.Domain.Entities;
using Discussion.Core.Commands;
using Discussion.Core.Handlers;
using Discussion.DB;
using Discussion.DB.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Discussion.UnitTests
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_Reply_To_Existing_Discussion()
        {
            // Arrange: Create an in-memory database.
            var options = new DbContextOptionsBuilder<DiscussionDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            using var context = new DiscussionDbContext(options);
            ICommentRepository repository = new CommentRepository(context);
            
            // First, create a discussion comment (top-level).
            var discussion = new Comment
            {
                Username = "professor1",
                Content = "How is your day going so far?",
                CreatedAt = DateTime.UtcNow,
                ParentId = null
            };
            await repository.AddAsync(discussion);
            await repository.SaveChangesAsync();
            
            // Set the DiscussionId to the discussion's own Id.
            discussion.DiscussionId = discussion.Id;
            await repository.SaveChangesAsync();

            // Now, create a reply command.
            var handler = new CreateCommentCommandHandler(repository);
            var command = new CreateCommentCommand
            {
                Username = "student1",
                ParentId = discussion.Id,
                Content = "It is going great, thanks!"
            };

            // Act
            var commentId = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(commentId > 0);
            var reply = await context.Comments.FindAsync(commentId);
            Assert.NotNull(reply);
            Assert.Equal(discussion.Id, reply.ParentId);
            Assert.Equal(discussion.DiscussionId, reply.DiscussionId);
            Assert.Equal("student1", reply.Username);
        }
    }
}