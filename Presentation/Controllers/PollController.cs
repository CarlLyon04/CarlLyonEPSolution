using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
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
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                CreatedDate = DateTime.Now
            };

            _pollRepository.CreatePoll(poll);
            return RedirectToAction("Index");
        }

        // GET: /Poll/Vote/5
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
        [HttpPost]
        public IActionResult Vote(VoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var poll = _pollRepository.GetPollById(model.id);
                model.Title = poll.Title;
                model.Option1Text = poll.Option1Text;
                model.Option2Text = poll.Option2Text;
                model.Option3Text = poll.Option3Text;

                return View("Vote", model);
            }

            _pollRepository.Vote(model.id, model.chosenOption);
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

            return View(viewModel);
        }
    }
}
