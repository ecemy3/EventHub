using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eventhub.Models
{
    public class Context: DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<EventMember> eventMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageDetail> MessageDetails { get; set; }
  
    }
}