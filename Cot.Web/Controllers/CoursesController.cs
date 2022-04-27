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
            model.FilterFields = new List<SelectListItem>
            {
                new SelectListItem {Text = "(All Fields)", Value = null},
                new SelectListItem {Text = "Code", Value = "Code"},
                new SelectListItem {Text = "Title", Value = "Title"},
            };
            //model.FilterField ??= "All";
            model.Items = await unitOfWork.Courses.GetPageAsync(model.PageNumber.Value, model.PageSize.Value, model.SortField, model.SortOrder, model.FilterField?.Value, model.FilterText);
            model.PagesCount = model.Items.PageCount;
            model.FirstItemOnPage = model.Items.FirstItemOnPage;
            model.LastItemOnPage = model.Items.LastItemOnPage;
            model.TotalItemsCount = model.Items.TotalItemCount;

            return View(model);
        }

        // GET: Courses/Details/5
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
                Id = course.Id.ToString(),
                Code = course.Code,
                Title = course.Title,
                Level = course.Level.ToString(),
                Type = course.Type.ToString(),
                AddedDate = course.AddedDate?.ToString() ?? "N/A",
                ModifiedDate = course.ModifiedDate?.ToString() ?? "N/A",
            };

            return View(model);
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
        public async Task<IActionResult> Create(CourseCreateModel model)
        {
            //string houseNo = Request.Form["HomeAddress.HouseNumber"];   //request value from input name="HomeAddress.HouseNumber"
            if (ModelState.IsValid)
            {
                var course = new Course()
                {
                    Id = Guid.NewGuid(),
                    Code = model.Code,
                    Title = model.Title,
                    Level = model.Level,
                    AddedDate = DateTime.Now
                };

                unitOfWork.Courses.Add(course);
                await unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Code,Title,Type,AddedDateTime,ModifiedDateTime")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Courses.Update(course);
                    await unitOfWork.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CourseExistsAsync(course.Id))
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

        private async Task<bool> CourseExistsAsync(Guid id)
        {
            //return context.Courses.Any(e => e.Code == id);
            return await unitOfWork.Courses.FindAsync(e => e.Id == id) != null;
        }
    }
}
