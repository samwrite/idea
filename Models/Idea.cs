using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace idea.Models
{
    public class Idea : BaseEntity
    {
        public int IdeaId { get; set; }
        public string content { get; set; }      

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public User Creator { get; set; }
        
        public List<Liked> Likeds { get; set; }

        public Idea(){
            Likeds = new List<Liked>();
        }
    }
}
