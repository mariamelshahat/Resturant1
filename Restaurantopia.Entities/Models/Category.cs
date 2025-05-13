using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopia.Entities.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[StringLength ( 50 )]
		public string CategoryName { get; set; }
		public ICollection<Item> Items { get; set; }
	}
}
