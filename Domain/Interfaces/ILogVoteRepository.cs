using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILogVoteRepository
    {
        // / Methods to log and check if a user has voted
        bool hasVoted(string userId, int pollId);
        void logVote(string userId, int pollId);
    }
}
