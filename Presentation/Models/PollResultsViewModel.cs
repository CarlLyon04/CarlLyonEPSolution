namespace Presentation.Models
{
    public class PollResultsViewModel
    {
        /// Properties
        
        // Poll Title
        public string Title { get; set; }

        // Poll Options and Votes count per option
        public string Option1Text { get; set; }
        public int Option1VotesCount { get; set; }

        // Poll Options and Votes count per option

        public string Option2Text { get; set; }
        public int Option2VotesCount { get; set; }

        // Poll Options and Votes count per option

        public string Option3Text { get; set; }
        public int Option3VotesCount { get; set; }

        // Total votes count
        public int TotalVotes =>
            Option1VotesCount + Option2VotesCount + Option3VotesCount;
    }

}
