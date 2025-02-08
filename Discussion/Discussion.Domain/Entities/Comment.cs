using System;
using System.Collections.Generic;

namespace Discussion.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        // For a top-level discussion, ParentId is null.
        public int? ParentId { get; set; }

        // For every comment (including replies), this holds the top-level discussion ID.
        public int DiscussionId { get; set; }

        // The username of the person who created the comment.
        public string Username { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Comment Parent { get; set; }
        public ICollection<Comment> Children { get; set; }
    }
}