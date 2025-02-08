using Discussion.Common.DTOs;
using Discussion.Common.Interfaces;
using Discussion.Domain.Entities;
using Discussion.Core.Handlers;
using Discussion.Core.Queries;
using Discussion.DB;
using Discussion.DB.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Discussion.UnitTests
{
    public class GetDiscussionCommentsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_All_Comments_For_Given_Discussion()
        {
            // Arrange: Create an in-memory database.
            var options = new DbContextOptionsBuilder<DiscussionDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            using var context = new DiscussionDbContext(options);
            ICommentRepository repository = new CommentRepository(context);
            
            // Create a discussion comment (top-level).
            var discussion = new Comment
            {
                Username = "professor1",
                Content = "How is your day going so far?",
                CreatedAt = DateTime.UtcNow,
                ParentId = null
            };
            await repository.AddAsync(discussion);
            await repository.SaveChangesAsync();
            
            // Set DiscussionId for the discussion.
            discussion.DiscussionId = discussion.Id;
            await repository.SaveChangesAsync();

            // Add two replies.
            var reply1 = new Comment
            {
                Username = "student1",
                Content = "It is going great, thanks!",
                CreatedAt = DateTime.UtcNow.AddMinutes(1),
                ParentId = discussion.Id,
                DiscussionId = discussion.DiscussionId
            };
            var reply2 = new Comment
            {
                Username = "student2",
                Content = "Unfortunately, I had a bad day.",
                CreatedAt = DateTime.UtcNow.AddMinutes(2),
                ParentId = discussion.Id,
                DiscussionId = discussion.DiscussionId
            };
            await repository.AddAsync(reply1);
            await repository.AddAsync(reply2);
            await repository.SaveChangesAsync();

            var handler = new GetDiscussionCommentsQueryHandler(repository);
            var query = new GetDiscussionCommentsQuery { DiscussionId = discussion.Id };

            // Act
            List<CommentDto> result = await handler.Handle(query, CancellationToken.None);

            // Assert – expecting 3 comments (discussion + 2 replies).
            Assert.Equal(3, result.Count);
            // Optionally, validate ordering by CreatedAt (discussion should be first).
            Assert.Equal(discussion.Id, result.First().Id);
        }
    }
}