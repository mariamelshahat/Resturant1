using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Restaurantopia.Entities.Models
{
	public class Review
	{
		[Key]
		public int Id { get; set; }
		[StringLength ( 500 )]
		public string? Comment { get; set; }
		public int Rate { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;

		[ForeignKey ( "Order" )]
		public int OrderId { get; set; }
		

		[ForeignKey ("Customer")]
		public int? CustomerId { get; set; }
		public Customer customer { get; set; }

	}
}
