using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;
        private readonly ILogVoteRepository _logVoteRepository;

        public PollController(IPollRepository pollRepository, ILogVoteRepository logVoteRepository)
        {
            _pollRepository = pollRepository;
            _logVoteRepository = logVoteRepository;
        }

        // GET: /Poll
        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls()
                .OrderByDescending(p => p.CreatedDate);

            return View(polls);
        }

        // GET: /Poll/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Poll/Create
        [HttpPost]
        public IActionResult Create(CreatePollViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var poll = new Poll
            {
                Title = model.Title,
                Option1Text = model.Option1Text,
                Option2Text = model.Option2Text,
                Option3Text = model.Option3Text,
                CreatedDate = DateTime.Now
            };

            _pollRepository.CreatePoll(poll);
            return RedirectToAction("Index");
        }

        // GET: /Poll/Vote/5
        [Authorize]
        [HttpGet]
        public IActionResult Vote(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null) return NotFound();

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

        // POST: /Poll/Vote
        [Authorize]
        [HttpPost]
        public IActionResult Vote(VoteViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_logVoteRepository.hasVoted(userId, model.id))
            {
                TempData["Message"] = "You have already voted on this poll.";
                return RedirectToAction("Results", new { id = model.id });
            }

            _pollRepository.Vote(model.id, model.chosenOption);
            _logVoteRepository.logVote(userId, model.id);

            return RedirectToAction("Results", new { id = model.id });
        }

        // GET: /Poll/Results/5
        public IActionResult Results(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null) return NotFound();

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
