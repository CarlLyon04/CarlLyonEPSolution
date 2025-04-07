using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls();

            polls.OrderByDescending(p => p.CreatedDate);
            return View(polls);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Poll poll)
        {
            if (ModelState.IsValid)
            {
                _pollRepository.CreatePoll(poll);
                return RedirectToAction("Index");
            }
            return View(poll);
        }

        public IActionResult Read(int id)
        {
            var polls = _pollRepository.GetPolls();

            var specificPoll = polls.FirstOrDefault(p => p.Id == id);

            if (specificPoll == null)
            {
                return NotFound();
            }

            return View(specificPoll);

        }
    }
}
