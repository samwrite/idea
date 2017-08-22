using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace idea.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        public string Name { get; set; }
        
        public string Alias { get; set; }
       
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public List<Liked> Likeds { get; set; }
        public User()
        {
            Likeds = new List<Liked>();
        }
    }
}
