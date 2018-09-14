using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Shared.Models;

namespace TodoApi.Shared.Models
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
    }
}
