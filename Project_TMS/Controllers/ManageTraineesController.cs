using Microsoft.AspNet.Identity;
using Project_TMS.Models;
using Project_TMS.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Protocols;

namespace Project_TMS.Controllers
{
	public class ManageTraineesController : Controller
	{
		private ApplicationDbContext _context;
		public ManageTraineesController()
		{
			_context = new ApplicationDbContext();
		}

		// GET: ManageTrainees
		public ActionResult Index()
		{
			var managetrainees = _context.ManageTrainees.Include(m => m.Topic)
														.Include(m => m.Trainee)
														.ToList();
			return View(managetrainees);
		}

		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new TraineeTopicViewModel
			{
				Topics = _context.Topics.ToList()
			};

			// Lấy data
			// Lấy toàn bộ thể loại:
			/*List<Category> cate = _context.Categories.ToList();*/
			List<ApplicationUser> applicationUsers = _context.Users.ToList();


			// Tạo SelectList
			/*SelectList cateList = new SelectList(cate, "ID", "THELOAI_NAME");*/
			SelectList traineeList = new SelectList(applicationUsers, "Id", "Email");
			// Set vào ViewBag
			/*ViewBag.CategoryList = cateList;*/
			ViewBag.TraineeList = traineeList;
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(Topic topic)
		{
			ManageTrainee manageTrainee = new ManageTrainee
			{
				TraineeId = User.Identity.GetUserId(),
				TopicId	= topic.Id
			};
			
			_context.ManageTrainees.Add(manageTrainee);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}