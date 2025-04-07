﻿using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly PollDbContext _context;
        public PollRepository(PollDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Poll> GetPolls()
        {
            return _context.Polls.ToList();
        }
        public Poll? GetPollById(int id)
        {
            return _context.Polls.Find(id);
        }
        public void CreatePoll(Poll poll)
        {
            _context.Polls.Add(poll);
            _context.SaveChanges();
        }
        public void UpdatePoll(Poll poll)
        {
            _context.Polls.Update(poll);
            _context.SaveChanges();
        }
        public void DeletePoll(int id)
        {
            var poll = GetPollById(id);
            if (poll != null)
            {
                _context.Polls.Remove(poll);
                _context.SaveChanges();
            }
        }
    }
}
