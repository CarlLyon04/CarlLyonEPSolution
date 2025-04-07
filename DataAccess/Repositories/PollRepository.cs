using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class PollRepository : IPollRepository
    {
        // Private readonly field to store the database context
        private readonly PollDbContext _context;

        // Constructor injection of PollDbContext
        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        // Retrieves all polls from the database
        public IEnumerable<Poll> GetPolls()
        {
            return _context.Polls.ToList();
        }

        // Retrieves a specific poll by its ID
        public Poll? GetPollById(int id)
        {
            return _context.Polls.Find(id);
        }

        // Adds a new poll to the database
        public void CreatePoll(Poll poll)
        {
            _context.Polls.Add(poll);
            _context.SaveChanges();
        }

        // Updates an existing poll in the database
        public void UpdatePoll(Poll poll)
        {
            _context.Polls.Update(poll);
            _context.SaveChanges();
        }

        // Deletes a poll by its ID if it exists
        public void DeletePoll(int id)
        {
            var poll = GetPollById(id);
            if (poll != null)
            {
                _context.Polls.Remove(poll);
                _context.SaveChanges();
            }
        }

        // Records a vote for a given option on a specific poll
        public void Vote(int id, int chosenOption)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == id);
            if (poll == null) return;

            // Increments the vote count for the chosen option
            switch (chosenOption)
            {
                case 1:
                    poll.Option1VotesCount++;
                    break;
                case 2:
                    poll.Option2VotesCount++;
                    break;
                case 3:
                    poll.Option3VotesCount++;
                    break;
                default:
                    return; // invalid option
            }

            // Save to the database
            _context.Polls.Update(poll);
            _context.SaveChanges();
        }

    }
}
