using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Soccer.BLL.DTO;
using Soccer.BLL.Interfaces;
using Soccer.BLL.Infrastructure;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ITeamService teamService;
        public PlayersController(IPlayerService playerserv, ITeamService teamserv)
        {
            playerService = playerserv;
            teamService = teamserv;
        }

        // GET: Players
        public async Task<IActionResult> Index(string position, int team = 0, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            var players = await playerService.GetPlayers();

            int pageSize = 5;

            //фильтрация

            if (team != 0)
            {
                players = players.Where(p => p.TeamId == team);
            }
            if (!string.IsNullOrEmpty(position))
            {
                players = players.Where(p => p.Position == position);
            }

            // сортировка
            players = sortOrder switch
            {
                SortState.NameDesc => players.OrderByDescending(s => s.Name),
                SortState.AgeAsc => players.OrderBy(s => DateTime.Now.Year - s.BirthYear),
                SortState.AgeDesc => players.OrderByDescending(s => DateTime.Now.Year - s.BirthYear),
                SortState.PositionAsc => players.OrderBy(s => s.Position),
                SortState.PositionDesc => players.OrderByDescending(s => s.Position),
                SortState.TeamAsc => players.OrderBy(s => s.Team),
                SortState.TeamDesc => players.OrderByDescending(s => s.Team),
                _ => players.OrderBy(s => s.Name),
            };

            // пагинация
            var list = players.ToList();
            var count = list.Count();
            var items = list.Skip((page - 1) * pageSize).Take(pageSize);

            // формируем модель представления
            var teamlist = await teamService.GetTeams();
            IndexViewModel viewModel = new IndexViewModel(
            items,
            new PageViewModel(count, page, pageSize),
                new FilterViewModel(teamlist.ToList(), team, position),
                new SortViewModel(sortOrder)
            );
            return View(viewModel);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                PlayerDTO player = await playerService.GetPlayer((int)id);
                return View(player);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: Players/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerDTO player)
        {
            if (ModelState.IsValid)
            {
                await playerService.CreatePlayer(player);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                PlayerDTO player = await playerService.GetPlayer((int)id);
                ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name", player.TeamId);
                return View(player);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerDTO player)
        {
            if (ModelState.IsValid)
            {
                await playerService.UpdatePlayer(player);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name", player.TeamId);
            return View(player);
        }


        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                PlayerDTO player = await playerService.GetPlayer((int)id);
                return View(player);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await playerService.DeletePlayer(id);
            return RedirectToAction(nameof(Index));
        }

    }
}