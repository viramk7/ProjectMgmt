using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMgmt.Web.Data;
using ProjectMgmt.Web.Data.Entities;
using ProjectMgmt.Web.Models;

namespace ProjectMgmt.Web.Controllers
{
    public class ProjectIncomesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectIncomesController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("projects/{projectId}/incomes", Name = "listincomes")]
        public async Task<IActionResult> Index(Guid? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = projectId;

            var incomes = await _context.Income.Where(i => i.ProjectId == projectId).Include(p => p.Project).ToListAsync();
            return View(_mapper.Map<List<ProjectIncomeViewModel>>(incomes));
        }

        [Route("projects/{projectId}/income-details/{id}", Name = "incomedetails")]
        public async Task<IActionResult> Details(Guid? projectId, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectIncome = await _context.Income
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectIncome == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = projectId;
            return View(projectIncome);
        }

        [Route("projects/{projectId}/create-income", Name = "createincome")]
        public IActionResult Create(Guid? projectId)
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: ProjectIncomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,IncomeFrom,Amount,IncomeDate,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] ProjectIncome projectIncome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectIncome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectIncome.ProjectId);
            return View(projectIncome);
        }

        // GET: ProjectIncomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectIncome = await _context.Income.FindAsync(id);
            if (projectIncome == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectIncome.ProjectId);
            return View(projectIncome);
        }

        // POST: ProjectIncomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,IncomeFrom,Amount,IncomeDate,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] ProjectIncome projectIncome)
        {
            if (id != projectIncome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectIncome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectIncomeExists(projectIncome.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectIncome.ProjectId);
            return View(projectIncome);
        }

        // GET: ProjectIncomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectIncome = await _context.Income
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectIncome == null)
            {
                return NotFound();
            }

            return View(projectIncome);
        }

        // POST: ProjectIncomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectIncome = await _context.Income.FindAsync(id);
            _context.Income.Remove(projectIncome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectIncomeExists(int id)
        {
            return _context.Income.Any(e => e.Id == id);
        }
    }
}
