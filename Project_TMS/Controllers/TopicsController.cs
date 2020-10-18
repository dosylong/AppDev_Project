using Project_TMS.Models;
using Project_TMS.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Project_TMS.Controllers
{
	public class TopicsController : Controller
	{
		private ApplicationDbContext _context;

		public TopicsController()
		{
			_context = new ApplicationDbContext();
		}

		// Topics/Index
		[HttpGet]
		public ActionResult Index(string searchTopic)
		{
			var topics = _context.Topics.Include(t => t.Course);

			if (!String.IsNullOrEmpty(searchTopic))
			{
				topics = topics.Where(
					s => s.Name.Contains(searchTopic) ||
					s.Course.Name.Contains(searchTopic));
			}

			return View(topics);
		}

		// Create Topic (Topics/Create)
		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new TopicCourseViewModel
			{
				Courses = _context.Courses.ToList()
			};
			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Create(Topic topic)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			//Check if Category Name existed or not
			var isTopicNameExist = _context.Topics.Any(
				c => c.Name.Contains(topic.Name));

			if (isTopicNameExist)
			{
				return View("Topic Name already existed!");
			}

			var newTopic = new Topic
			{
				Name = topic.Name,
				Descriptions = topic.Descriptions,
				CourseId = topic.CourseId,
			};

			_context.Topics.Add(newTopic);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		// Edit Topic (Topics/Edit/Id/...)
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var topicInDb = _context.Topics.SingleOrDefault(t => t.Id == id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new TopicCourseViewModel
			{
				Topic = topicInDb,
				Courses = _context.Courses.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(Topic topic)
		{
			var topicInDb = _context.Topics.SingleOrDefault(t => t.Id == topic.Id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			topicInDb.Name = topic.Name;
			topicInDb.Descriptions = topic.Descriptions;
			topicInDb.CourseId = topic.CourseId;

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		// Delete Topic (Topics/Delete/Id/...)
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var topicInDb = _context.Topics.SingleOrDefault(t => t.Id == id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			_context.Topics.Remove(topicInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}