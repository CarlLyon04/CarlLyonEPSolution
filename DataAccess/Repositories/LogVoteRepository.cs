using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class LogVoteRepository : ILogVoteRepository
    {
        private readonly PollDbContext _context;

        // Constructor that injects the PollDbContext dependency
        public LogVoteRepository(PollDbContext context)
        {
            _context = context;
        }

        // Checks if the specified user has already voted on the given poll
        public bool hasVoted(string userId, int pollId)
        {
            return _context.LogVotes.Any(lv => lv.UserId == userId && lv.PollId == pollId);
        }

        // Logs a new vote by saving the userId and pollId to the database
        public void logVote(string userId, int pollId)
        {
            // Create a new LogVote entry
            var logVote = new LogVote
            {
                UserId = userId,
                PollId = pollId,
            };

            // Save to the Database
            _context.LogVotes.Add(logVote);

            _context.SaveChanges();
        }

    }
}
