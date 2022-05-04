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
using Microsoft.AspNetCore.Authorization;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

namespace Cot.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: List
        [HttpGet]
        [Breadcrumb("Courses")]
        public async Task<IActionResult> Index(ListViewModel<Course> model)
        {
            if (model.PageNumber == null || model.PageNumber < 1)
            {
                model.PageNumber = 1;
            }
            if (model.PageSize == null || model.PageSize < 1)
            {
                model.PageSize = 25;
            }

            model.SortField ??= "Code";
            model.SortOrder ??= "Ascending";

            model.Items = await unitOfWork.Courses.GetPageAsync(model.PageNumber.Value, model.PageSize.Value, model.SortField, model.SortOrder, model.SearchField?.Value, model.SearchText);
            model.PagesCount = model.Items.PageCount;
            model.FirstItemOnPage = model.Items.FirstItemOnPage;
            model.LastItemOnPage = model.Items.LastItemOnPage;
            model.TotalItemsCount = model.Items.TotalItemCount;

            return View(model);
        }

        // GET: Courses/Details/5
        [HttpGet]
        [Breadcrumb("Details", FromAction = "Index")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await unitOfWork.Courses
                .FindAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            var model = new CourseViewModel()
            {
                Course = course,
            };

            return View(model);
        }

        // GET: Courses/Create
        [HttpGet]
        [Breadcrumb("Create", FromAction = "Index")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateModel model)
        {
            if (await unitOfWork.Courses.IsExistAsync(e => e.Code == model.Code))
            {
                ModelState.AddModelError(nameof(model.Code), $"Course Code '{model.Code}' already exists.");
            }

            if (await unitOfWork.Courses.IsExistAsync(e => e.Title == model.Title))
            {
                ModelState.AddModelError(nameof(model.Title), $"Course Title '{model.Title}' already exists.");
            }

            if (ModelState.IsValid)
            {
                var course = new Course()
                {
                    Id = Guid.NewGuid(),
                    Code = model.Code,
                    Title = model.Title,
                    Level = model.Level,
                    Type = model.Type,
                    AddedDateTime = DateTime.Now
                };

                unitOfWork.Courses.Add(course);
                await unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Courses/Edit/5
        [HttpGet]
        [Breadcrumb("Edit", FromAction = "Index")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await unitOfWork.Courses
                .GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var model = new CourseEditModel()
            {
                Id = course.Id,
                Code = course.Code,
                Title = course.Title,
                Level = course.Level,
                Type = course.Type,
                Notes = course.Notes
            };
            return View(model);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CourseEditModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            //check if row exists
            var course = await unitOfWork.Courses
                .GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            //check if code exists for any other course
            var existingCourse = await unitOfWork.Courses.FindAsync(e => e.Id != model.Id && e.Code == model.Code);
            if (existingCourse != null)
            {
                ModelState.AddModelError(nameof(model.Code), $"Course Code '{model.Code}' already exists.");
            }

            //check if title exists for any other course
            existingCourse = await unitOfWork.Courses.FindAsync(e => e.Id != model.Id && e.Title == model.Title);
            if (existingCourse != null)
            {
                ModelState.AddModelError(nameof(model.Title), $"Course Title '{model.Title}' already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    course.Id = model.Id;
                    course.Code = model.Code;
                    course.Title = model.Title;
                    course.Level = model.Level;
                    course.Type = model.Type;
                    course.ModifiedDateTime = DateTime.Now;

                    unitOfWork.Courses.Update(course);
                    await unitOfWork.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await unitOfWork.Courses.IsExistAsync(e => e.Id == model.Id))
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
            return View(model);
        }

        // GET: Courses/Delete/5
        [HttpGet]
        [Breadcrumb("Delete", FromAction = "Index")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await unitOfWork.Courses
                .FindAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var course = await unitOfWork.Courses.GetAsync(id);
            unitOfWork.Courses.Remove(course);
            await unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Validation

        [HttpGet]
        [HttpPost] //or use [AcceptVerbs("Get", "Post")] for handling multiple http methods
        [AllowAnonymous]
        public async Task<IActionResult> ValidateCourseCode(string code)
        {
            if (await unitOfWork.Courses.IsExistAsync(e => e.Code == code))
            {
                return Json($"Course Code '{code}' already exists.");
            }
            return Json(true);
        }

        [HttpGet]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateCourseTitle(string title)
        {
            if (await unitOfWork.Courses.IsExistAsync(e => e.Title == title))
            {
                return Json($"Course Title '{title}' already exists.");
            }
            return Json(true);
        }

        #endregion
    }
}
