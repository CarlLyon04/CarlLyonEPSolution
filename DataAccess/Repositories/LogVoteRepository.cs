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

        public LogVoteRepository(PollDbContext context)
        {
            _context = context;
        }

        public bool hasVoted(string userId, int pollId)
        {
            return _context.LogVotes.Any(lv => lv.UserId == userId && lv.PollId == pollId);
        }

        public void logVote(string userId, int pollId)
        {
            var logVote = new LogVote
            {
                UserId = userId,
                PollId = pollId,
            };
            _context.LogVotes.Add(logVote);
            _context.SaveChanges();
        }
    }
}
