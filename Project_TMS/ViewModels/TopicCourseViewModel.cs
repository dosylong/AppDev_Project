using Project_TMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TMS.ViewModels
{
	public class TopicCourseViewModel
	{
		public Topic Topic { get; set; }

		public IEnumerable<Course> Courses { get; set; }
	}
}