using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class EmployeePayoutsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeePayoutsController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("project/{projectId}/payouts", Name = "listemppayouts")]
        public async Task<IActionResult> Index(Guid projectId)
        {
            var model =
                await _context.EmployeePayouts
                    .Where(p => p.ProjectId == projectId)
                    .Select(s => new EmployeePayoutViewModel
                    {
                        Id = s.Id,
                        ProjectName = s.Project.Name,
                        EmployeeName = s.Employee.Name,
                        Amount = s.Amount,
                        PaymentDateStr = s.PaymentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                    }).ToListAsync();

            return View(model);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayout = await _context.EmployeePayouts
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
           
            if (employeePayout == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EmployeePayoutDetailsViewModel>(employeePayout);

            return View(model);
        }

        // GET: EmployeePayouts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: EmployeePayouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,EmployeeId,Amount,PaymentDate,Notes,Id,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] EmployeePayout employeePayout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeePayout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", employeePayout.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", employeePayout.ProjectId);
            return View(employeePayout);
        }

        // GET: EmployeePayouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayout = await _context.EmployeePayouts.FindAsync(id);
            if (employeePayout == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", employeePayout.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", employeePayout.ProjectId);
            return View(employeePayout);
        }

        // POST: EmployeePayouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,EmployeeId,Amount,PaymentDate,Notes,Id,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn")] EmployeePayout employeePayout)
        {
            if (id != employeePayout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeePayout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeePayoutExists(employeePayout.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", employeePayout.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", employeePayout.ProjectId);
            return View(employeePayout);
        }

        // GET: EmployeePayouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayout = await _context.EmployeePayouts
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeePayout == null)
            {
                return NotFound();
            }

            return View(employeePayout);
        }

        // POST: EmployeePayouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeePayout = await _context.EmployeePayouts.FindAsync(id);
            _context.EmployeePayouts.Remove(employeePayout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeePayoutExists(int id)
        {
            return _context.EmployeePayouts.Any(e => e.Id == id);
        }
    }
}
