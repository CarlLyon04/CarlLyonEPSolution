using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LogVote
    {
        public int id { get; set; }

        public int PollId { get; set; } 

        public string UserId { get; set; }
    }
}
