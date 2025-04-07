using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess
{
    public class PollDbContext : IdentityDbContext<User>
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options)
        {
        }
        public DbSet<Poll> Polls { get; set; }

        public DbSet<LogVote> LogVotes { get; set; }
    }
}
