using Project_TMS.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Project_TMS.Controllers
{
	public class CategoriesController : Controller
	{
		private ApplicationDbContext _context;

		public CategoriesController()
		{
			_context = new ApplicationDbContext();
		}

		// Categories/Index
		[HttpGet]
		public ActionResult Index(string searchCategory)
		{
			var categories = _context.Categories.ToList();

			if (!String.IsNullOrEmpty(searchCategory))
			{
				categories = categories.FindAll(s => s.Name.Contains(searchCategory));
			}

			return View(categories);
		}

		// Create Category (Categories/Create)
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			//Check if Category Name existed or not
			var isCategoryNameExist = _context.Categories.Any(
				c => c.Name.Contains(category.Name));

			if (isCategoryNameExist)
			{
				return View("Category Name already existed!");
			}

			var newCategory = new Category
			{
				Name = category.Name,
				Descriptions = category.Descriptions,
			};

			_context.Categories.Add(newCategory);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		// Edit Category (Categories/Edit/Id/...)
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}
			return View(categoryInDb);
		}

		[HttpPost]
		public ActionResult Edit(Category category)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}

			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			categoryInDb.Name = category.Name;
			categoryInDb.Descriptions = category.Descriptions;

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		// Delete Category (Categories/Delete/Id/...)
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			_context.Categories.Remove(categoryInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}