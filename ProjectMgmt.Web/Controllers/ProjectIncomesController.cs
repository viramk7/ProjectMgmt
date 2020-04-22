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

            var incomes = await _context.Incomes.Where(i => i.ProjectId == projectId).Include(p => p.Project).ToListAsync();
            return View(_mapper.Map<List<ProjectIncomeViewModel>>(incomes));
        }

        [Route("projects/{projectId}/income-details/{id}", Name = "incomedetails")]
        public async Task<IActionResult> Details(Guid? projectId, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectIncome = await _context.Incomes.Include(p => p.Project).FirstOrDefaultAsync(m => m.Id == id);
            if (projectIncome == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<IncomeDetailsViewModel>(projectIncome);

            ViewBag.ProjectId = projectId;
            return View(model);
        }

        [Route("projects/{projectId}/create-income", Name = "createincome")]
        public IActionResult Create(Guid projectId)
        {
            return View(new CreateIncomeViewModel(projectId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("projects/{projectId}/create-income", Name = "createincome")]
        public async Task<IActionResult> Create(CreateIncomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var income = _mapper.Map<ProjectIncome>(model);
                income.CreatedBy = User.UserId();
                income.CreatedOn = DateTime.Now;

                await _context.AddAsync(income);
                await _context.SaveChangesAsync();
                return RedirectToRoute("listincomes", new { projectId = model.ProjectId });
            }

            return View(model);
        }

        [Route("edit-income/{id}", Name = "editincome")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectIncome = await _context.Incomes.Include(i => i.Project).FirstOrDefaultAsync(i => i.Id == id);
            if (projectIncome == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditIncomeViewModel>(projectIncome);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit-income/{id}", Name = "editincome")]
        public async Task<IActionResult> Edit(int id, EditIncomeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var income = await _context.Incomes.FindAsync(model.Id);
                    if (income == null)
                    {
                        return NotFound();
                    }

                    var updatingModel = _mapper.Map(model, income);
                    updatingModel.UpdatedBy = User.UserId();
                    updatingModel.UpdatedOn = DateTime.Now;

                    _context.Update(updatingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectIncomeExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToRoute("listincomes", new { projectId = model.ProjectId });
            }

            
            return View(model);
        }

        [Route("delete-income/{id}", Name = "deleteincome")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectIncome = await _context.Incomes.Include(p => p.Project).FirstOrDefaultAsync(m => m.Id == id);
            if (projectIncome == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<IncomeDetailsViewModel>(projectIncome);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete-income/{id}", Name = "deleteincome")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var projectIncome = await _context.Incomes.FindAsync(id);
            _context.Incomes.Remove(projectIncome);
            await _context.SaveChangesAsync();
            return RedirectToRoute("listincomes", new { projectId = projectIncome.ProjectId });
        }

        private bool ProjectIncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.Id == id);
        }
    }
}
