using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LogVote
    {

        // Id of the LogVote Tag for each user when voting on a poll
        public int id { get; set; }


        // Id of the Poll that the user voted on
        public int PollId { get; set; }

        // Id of the user who voted
        public string UserId { get; set; }
    }
}
