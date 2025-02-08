using System;

namespace Discussion.Common.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}