using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cot.Web.Core.Domain;
using Cot.Web.Persistence;
using Cot.Web.Core;
using X.PagedList;
using Cot.Web.Models;

namespace Cot.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Courses
        public async Task<IActionResult> Index(CoursesListViewModel model)
        {
            var newModel = new CoursesListViewModel()
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                SortField = model.SortField,
                SortOrder = model.SortOrder,
                SearchText = model.SearchText,
                CoursesList = await unitOfWork.Courses.GetAllPagedListAsync(model.PageNumber, model.PageSize, model.SortField, model.SortOrder, model.SearchText)
            };
            return View(newModel);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await unitOfWork.Courses
                .FindAsync(m => m.Code == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Title")] Course course)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(course);
                unitOfWork.Courses.Add(course);
                await unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await unitOfWork.Courses.GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Title")] Course course)
        {
            if (id != course.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //unitOfWork.Update(course);
                    unitOfWork.Courses.Update(course);
                    await unitOfWork.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CourseExistsAsync(course.Code))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await unitOfWork.Courses
                .FindAsync(m => m.Code == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var course = await unitOfWork.Courses.GetAsync(id);
            //context.Courses.Remove(course);
            unitOfWork.Courses.Remove(course);
            await unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CourseExistsAsync(string id)
        {
            //return context.Courses.Any(e => e.Code == id);
            return await unitOfWork.Courses.FindAsync(e => e.Code == id) != null;
        }
    }
}
