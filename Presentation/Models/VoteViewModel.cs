using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class VoteViewModel
    {
        /// Properties
        
        public int id { get; set; }

        // Title of the poll
        public string? Title { get; set; }

        // Text of the options
        public string? Option1Text { get; set; }
        public string? Option2Text { get; set; }
        public string? Option3Text { get; set; }

        /// The selected option

        [Range(1, 3, ErrorMessage = "Please select a valid option.")]
        public int chosenOption { get; set; }
    }

}
