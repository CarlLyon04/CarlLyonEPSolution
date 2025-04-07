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
        // Path to the JSON file where polls are stored
        private string filePath = "polls.json";

        // Retrieves all polls from the JSON file
        public IEnumerable<Poll> GetPolls()
        {
            // Check if the file exists
            if (System.IO.File.Exists(filePath))
            {
                // Read the file content as JSON
                string pollsJson = System.IO.File.ReadAllText(filePath);

                // Deserialize the JSON into a collection of Polls
                var polls = JsonConvert.DeserializeObject<IEnumerable<Poll>>(pollsJson);

                // Return the polls as a queryable collection
                return polls.AsQueryable();
            }
            else
            {
                // Throw an error if the file is not found
                throw new FileNotFoundException("Poll file not found.");
            }
        }

        // Adds a new poll to the JSON file
        public void CreatePoll(Poll poll)
        {
            // Initialize an empty list of polls
            var polls = new List<Poll>();

            // If the file exists, read and deserialize existing polls
            if (System.IO.File.Exists(filePath))
            {
                string pollsJson = System.IO.File.ReadAllText(filePath);
                polls = JsonConvert.DeserializeObject<List<Poll>>(pollsJson);
            }
            else
            {
                // If file doesn't exist, serialize the empty list (optional logic here)
                string pollsJson = JsonConvert.SerializeObject(polls);
            }

            // Add the new poll to the list
            polls.Add(poll);

            // Serialize the updated list and write it back to the file
            File.WriteAllText(filePath, JsonConvert.SerializeObject(polls));
        }

        // Placeholder for voting logic (not yet implemented)
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
