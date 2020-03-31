using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("Work")]
    public class Work
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("work_type_id")]
        public int? WorkTypeId { get; set; }

        public WorkType WorkType { get; set; }

        [Column("order_id")]
        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
