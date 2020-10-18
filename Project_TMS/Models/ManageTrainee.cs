using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TMS.Models
{
	public class ManageTrainee
	{
		public int Id { get; set; }
		public string TraineeId { get; set; }
		public ApplicationUser Trainee { get; set; }
		public int TopicId { get; set; }
		public Topic Topic { get; set; }
	}
}