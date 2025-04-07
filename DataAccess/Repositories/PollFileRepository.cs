using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
namespace DataAccess.Repositories
{
    public class PollFileRepository : IPollRepository
    {
        private string filePath = "polls.json";

        public IEnumerable<Poll> GetPolls()
        {
            if (System.IO.File.Exists(filePath))
            {
                string pollsJson = System.IO.File.ReadAllText(filePath);
                var polls = JsonConvert.DeserializeObject<IEnumerable<Poll>>(pollsJson);
                return polls.AsQueryable();
            }
            else
            {
                throw new FileNotFoundException("Poll file not found.");
            }
        }
        public void CreatePoll(Poll poll)
        {
            var polls = new List<Poll>();

            if (System.IO.File.Exists(filePath))
            {
                string pollsJson = System.IO.File.ReadAllText(filePath);
                polls = JsonConvert.DeserializeObject<List<Poll>>(pollsJson);
            }
            else
            {
                string pollsJson = JsonConvert.SerializeObject(polls);

            }

            polls.Add(poll);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(polls));
        }

        public void Vote(int id, int chosenOption)
        {
            throw new NotImplementedException();
        }


        // Ignore the below

        public void DeletePoll(int id)
        {
            throw new NotImplementedException();
        }

        public Poll? GetPollById(int id)
        {
            throw new NotImplementedException();
        }



        public void UpdatePoll(Poll poll)
        {
            throw new NotImplementedException();
        }


    }
}
