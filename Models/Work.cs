using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab5.Models
{
    [Table("Work")]
    public class Work
    {
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("work_type_id")]
        public int WorkTypeId { get; set; }

        public WorkType WorkType { get; set; }

        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
