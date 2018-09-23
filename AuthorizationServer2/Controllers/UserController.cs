using System;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Dto;
using AuthorizationServer.Interfaces;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.ToListAsync();
            return View(users.Select(u => new UserViewModel(u)));
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _userRepository.ToListAsync();
            var user = users.SingleOrDefault(m => m.SubjectId == id);

            if (user == null)
            {
                return NotFound();
            }
            var vm = new UserViewModel(user);
            return View(vm);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Username,Password,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.AddAsync(user);
                return RedirectToAction(nameof(Index));
            }
            var vm = new UserViewModel(user);
            return View(vm);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.FindByIdAsync((Guid)id);
            if (user == null)
            {
                return NotFound();
            }
            var vm = new UserViewModel(user);
            return View(vm);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SubjectId,Username,Password,FirstName,LastName")] User user)
        {
            if (id != user.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.SubjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.FindByIdAsync((Guid)id);
            if (user == null)
            {
                return NotFound();
            }
            var vm = new UserViewModel(user);
            return View(vm);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _userRepository.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            var user = Task.Run(() => _userRepository.FindByIdAsync(id));
            return (user != null);
        }
    }
}
