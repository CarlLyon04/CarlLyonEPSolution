namespace Presentation.Models
{
    public class VoteViewModel
    {
        public int id { get; set; }

        public string Title { get; set; }

        public string Option1Text { get; set; }

        public string Option2Text { get; set; }

        public string Option3Text { get; set; }

        public int chosenOption { get; set; }
    }
}
