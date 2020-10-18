using Project_TMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TMS.ViewModels
{
	public class TraineeTopicViewModel
	{
		public Topic Topic { get; set; }
		public IEnumerable<Topic> Topics { get; set; }

		public IEnumerable<ApplicationUser> Users { get; set; }
	}
}