using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class VoteViewModel
    {
        public int id { get; set; }

        public string Title { get; set; }

        public string Option1Text { get; set; }

        public string Option2Text { get; set; }

        public string Option3Text { get; set; }

        [Range(1, 3, ErrorMessage = "Please select a valid option.")]
        public int chosenOption { get; set; }
    }
}
