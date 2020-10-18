using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TMS.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[DisplayName("Category Name")]
		public string Name { get; set; }

		[Required]
		[DisplayName("Category Descriptions")]
		public string Descriptions { get; set; }
	}
}