using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPollRepository
    {
        // Get all the polls
        IEnumerable<Poll> GetPolls();

        // Get a poll by ID
        Poll? GetPollById(int id);

        // Create a new poll
        void CreatePoll(Poll poll);

        // Vote for a poll option
        void Vote(int id, Poll specificPoll);
    }
}
