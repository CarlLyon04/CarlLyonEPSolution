using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Poll
    {
        // Poll Id
        public int Id { get; set; }

        // Poll Title
        public string Title { get; set; } = string.Empty;

        // Poll Option 1
        public string Option1Text { get; set; } = string.Empty;

        // Poll Option 2
        public string Option2Text { get; set; } = string.Empty;

        // Poll Option 3
        public string Option3Text { get; set; } = string.Empty;

        // Poll Option 1 Votes Count
        public int Option1VotesCount { get; set; }

        // Poll Option 2 Votes Count
        public int Option2VotesCount { get; set; }

        // Poll Option 3 Votes Count
        public int Option3VotesCount { get; set; }

        // Poll Created Date
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
