using Project_TMS.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Project_TMS.ViewModels;

namespace Project_TMS.Controllers
{
	public class CoursesController : Controller
	{
		private ApplicationDbContext _context;

		public CoursesController()
		{
			_context = new ApplicationDbContext();
		}

		// Courses/Index
		[HttpGet]
		public ActionResult Index(string searchCourse)
		{
			var courses = _context.Courses.Include(co => co.Category);

			if (!String.IsNullOrEmpty(searchCourse))
			{
				courses = courses.Where(
					s => s.Name.Contains(searchCourse) ||
					s.Category.Name.Contains(searchCourse));
			}

			return View(courses);
		}

		// Create Course (Courses/Create)
		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new CourseCategoryViewModel
			{
				Categories = _context.Categories.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			//Check if Course Name existed or not
			var isCourseNameExist = _context.Courses.Any(
				c => c.Name.Contains(course.Name));

			if (isCourseNameExist)
			{
				return View("Course Name already existed!");
			}

			var newCourse = new Course
			{
				Name = course.Name,
				Descriptions = course.Descriptions,
				CategoryId = course.CategoryId,
			};

			_context.Courses.Add(newCourse);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		// Edit Course (Courses/Edit/Id/...)
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(co => co.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new CourseCategoryViewModel
			{
				Course = courseInDb,
				Categories = _context.Categories.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var courseInDb = _context.Courses.SingleOrDefault(co => co.Id == course.Id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			courseInDb.Name = course.Name;
			courseInDb.Descriptions = course.Descriptions;
			courseInDb.CategoryId = course.CategoryId;

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		// Delete Course (Courses/Delete/Id/...)
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(co => co.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}