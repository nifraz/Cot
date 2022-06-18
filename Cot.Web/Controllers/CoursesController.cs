using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ClosedXML.Excel;
using ClosedXML.Extensions;
using Cot.Data.Core;
using Cot.Data.Core.Domain;
using Cot.Web.Extensions;
using Cot.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cot.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly INotyfService notifyService;

        public CoursesController(IUnitOfWork unitOfWork, IMapper mapper, INotyfService notifyService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.notifyService = notifyService;
        }

        // GET: Courses
        [HttpGet]
        [AllowAnonymous]
        [Breadcrumb("Courses")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Courses/Manage?...
        [HttpGet]
        [Breadcrumb("Manage", FromAction = "Index")]
        public async Task<IActionResult> Manage(ListViewModel<Course> model)
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

            model.SearchFields = new List<SelectListItem>
            {
                new SelectListItem { Value = "Code", Text = "Code" },
                new SelectListItem { Value = "Title", Text = "Title"  }
            };

            model.Items = await unitOfWork.Courses.GetPageAsync(model.PageNumber.Value, model.PageSize.Value, model.SortField, model.SortOrder, model.SearchField, model.SearchText);
            model.PagesCount = model.Items.PageCount;
            model.FirstItemOnPage = model.Items.FirstItemOnPage;
            model.LastItemOnPage = model.Items.LastItemOnPage;
            model.TotalItemsCount = model.Items.TotalItemCount;

            return View(model);
        }

        // GET: Courses/Details/abcd-1234
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
        [Breadcrumb("Create", FromAction = "Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateModel model)
        {
            if (await unitOfWork.Courses.IsExistingAsync(e => e.Code == model.Code))
            {
                ModelState.AddModelError(nameof(model.Code), $"Course Code '{model.Code}' already exists.");
            }

            if (await unitOfWork.Courses.IsExistingAsync(e => e.Title == model.Title))
            {
                ModelState.AddModelError(nameof(model.Title), $"Course Title '{model.Title}' already exists.");
            }

            if (ModelState.IsValid)
            {
                var course = mapper.Map<Course>(model);
                course.Id = Guid.NewGuid();
                course.AddedDateTime = DateTime.Now;

                unitOfWork.Courses.Add(course);
                await unitOfWork.CompleteAsync();
                notifyService.Success("Course saved!");
                return RedirectToAction(nameof(Details), new { course.Id });
            }
            notifyService.Error("Course cannot be saved!");
            return View(model);
        }

        // GET: Courses/Edit/abcd-1234
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

            var model = mapper.Map<CourseEditModel>(course);
            return View(model);
        }

        // POST: Courses/Edit/abcd-1234
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Breadcrumb("Edit", FromAction = "Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CourseEditModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            //check if course exists
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
                mapper.Map(model, course);
                course.ModifiedDateTime = DateTime.Now;

                unitOfWork.Courses.Update(course);
                await unitOfWork.CompleteAsync();
                notifyService.Success("Course updated!");
                return RedirectToAction(nameof(Details), new { course.Id });
            }
            notifyService.Error("Course cannot be updated!");
            return View(model);
        }

        // GET: Courses/Delete/abcd-1234
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

        // POST: Courses/Delete/abcd-1234
        [HttpPost, ActionName("Delete")]
        [Breadcrumb("Delete", FromAction = "Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //check if course exists
            var course = await unitOfWork.Courses
                .GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            unitOfWork.Courses.Remove(course);
            await unitOfWork.CompleteAsync();

            notifyService.Success("Course deleted!");
            return RedirectToAction(nameof(Manage));
        }

        // GET: Download?...
        [HttpGet]
        [Breadcrumb("Download", FromAction = "Index")]
        public async Task<IActionResult> Download(ListViewModel<Course> model)
        {
            var items = await unitOfWork.Courses.GetAllAsync(model.SortField, model.SortOrder, model.SearchField, model.SearchText);

            using (var wb = new XLWorkbook())
            {
                var ws = wb.AddWorksheet(items.ToDataTable(), "List");
                return wb.Deliver($"Courses_List_({DateTime.Now:yyyy_MM_dd_HH_mm_ss})_College_of_Technology.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        #region Validation

        [HttpGet]
        public async Task<IActionResult> ValidateCourseCode(string code)
        {
            if (await unitOfWork.Courses.IsExistingAsync(e => e.Code == code))
            {
                notifyService.Warning("Please choose a different Code!");
                return Json($"Course Code '{code}' already exists.");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> ValidateCourseTitle(string title)
        {
            if (await unitOfWork.Courses.IsExistingAsync(e => e.Title == title))
            {
                notifyService.Warning("Please choose a different Title!");
                return Json($"Course Title '{title}' already exists.");
            }
            return Json(true);
        }

        #endregion
    }
}
