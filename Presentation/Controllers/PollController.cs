using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Security.Claims;

namespace Presentation.Controllers
{
    // Handles all poll-related HTTP requests
    public class PollController : Controller
    {
        // Repository for accessing polls from the database
        private readonly IPollRepository _pollRepository;

        // Repository for tracking which users have voted
        private readonly ILogVoteRepository _logVoteRepository;

        // Constructor that injects the poll and vote log repositories
        public PollController(IPollRepository pollRepository, ILogVoteRepository logVoteRepository)
        {
            _pollRepository = pollRepository;
            _logVoteRepository = logVoteRepository;
        }

        // Displays a list of polls, ordered by newest first (date created)
        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls()
                .OrderByDescending(p => p.CreatedDate);

            return View(polls);
        }

        // Returns the view for creating a new poll
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handles form submission to create a new poll
        [HttpPost]
        public IActionResult Create(CreatePollViewModel model)
        {
            // Return the form with validation errors if model is invalid
            if (!ModelState.IsValid)
                return View(model);

            // Map the view model data to the domain model
            var poll = new Poll
            {
                Title = model.Title,
                Option1Text = model.Option1Text,
                Option2Text = model.Option2Text,
                Option3Text = model.Option3Text,
                CreatedDate = DateTime.Now
            };

            // Save the new poll to the database
            _pollRepository.CreatePoll(poll);

            // Redirect to the poll list after creation
            return RedirectToAction("Index");
        }

        // Displays the voting page for a specific poll
        [Authorize] // Only logged-in users can vote
        [HttpGet]
        public IActionResult Vote(int id)
        {
            // Retrieve the poll by ID
            var poll = _pollRepository.GetPollById(id);
            if (poll == null) return NotFound();

            // Create the view model to pass poll options to the view
            var viewModel = new VoteViewModel
            {
                id = poll.Id,
                Title = poll.Title,
                Option1Text = poll.Option1Text,
                Option2Text = poll.Option2Text,
                Option3Text = poll.Option3Text
            };

            return View(viewModel);
        }

        // Handles vote submission and prevents duplicate voting
        [Authorize] // Only logged-in users can submit votes
        [HttpPost]
        public IActionResult Vote(VoteViewModel model)
        {
            // Get the currently logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user has already voted on this poll, show message and redirect
            if (_logVoteRepository.hasVoted(userId, model.id))
            {
                TempData["Message"] = "You have already voted on this poll.";
                return RedirectToAction("Results", new { id = model.id });
            }

            // Register the vote in the database
            _pollRepository.Vote(model.id, model.chosenOption);

            // Log the user's vote to prevent multiple voting
            _logVoteRepository.logVote(userId, model.id);

            // Redirect to the results page after voting
            return RedirectToAction("Results", new { id = model.id });
        }

        // Displays the voting results for a specific poll
        public IActionResult Results(int id)
        {
            // Retrieve the poll by ID
            var poll = _pollRepository.GetPollById(id);
            if (poll == null) return NotFound();

            // Create the view model to show voting results
            var viewModel = new PollResultsViewModel
            {
                Title = poll.Title,
                Option1Text = poll.Option1Text,
                Option1VotesCount = poll.Option1VotesCount,
                Option2Text = poll.Option2Text,
                Option2VotesCount = poll.Option2VotesCount,
                Option3Text = poll.Option3Text,
                Option3VotesCount = poll.Option3VotesCount
            };

            ViewBag.Message = TempData["Message"];
            return View(viewModel);
        }
    }
}

