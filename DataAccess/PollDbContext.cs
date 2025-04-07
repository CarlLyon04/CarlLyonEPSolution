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

        // Models the polls table in the DB
        public DbSet<Poll> Polls { get; set; }

        // Models the logged votes in the DB
        public DbSet<LogVote> LogVotes { get; set; }
    }
}
