using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace idea.Models
{
    public class Liked : BaseEntity
    {
        public int LikedId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int IdeaId { get; set; }
        public Idea Idea { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
       