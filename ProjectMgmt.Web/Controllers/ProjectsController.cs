using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMgmt.Web.Data;
using ProjectMgmt.Web.Data.Entities;
using ProjectMgmt.Web.Models;

namespace ProjectMgmt.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(_mapper.Map<List<ProjectViewModel>>(projects));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProjectDetailsViewModel>(project));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateViewModel project)
        {
            if (ModelState.IsValid)
            {
                var duplicateProject = await _context.Projects.AnyAsync(p => p.Name == project.Name);
                if (duplicateProject)
                {
                    ModelState.AddModelError(nameof(project.Name), "Project with same name already exists");
                    return View(project);
                }

                var newProject = _mapper.Map<Project>(project);
                newProject.CreatedBy = User.UserId();
                newProject.CreatedOn = DateTime.Now;

                await _context.AddAsync(newProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EditProjectViewModel>(project));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditProjectViewModel project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var duplicateProject = await _context.Projects.AnyAsync(p => p.Name == project.Name);
                    if (duplicateProject)
                    {
                        ModelState.AddModelError(nameof(project.Name), "Project with same name already exists");
                        return View(project);
                    }

                    var updatingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);
                    if (updatingProject == null)
                    {
                        return NotFound();
                    }

                    var updatedProject = _mapper.Map(project, updatingProject);
                    updatedProject.UpdatedBy = User.UserId();
                    updatedProject.UpdatedOn = DateTime.Now;

                    _context.Update(updatedProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProjectDetailsViewModel>(project));
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
