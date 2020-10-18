using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TMS.Models
{
	public class Topic
	{
		public int Id { get; set; }

		[Required]
		[DisplayName("Topic Name")]
		public string Name { get; set; }

		[Required]
		[DisplayName("Topic Description")]
		public string Descriptions { get; set; }

		[Required]
		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}