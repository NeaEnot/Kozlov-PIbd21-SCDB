using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab5.Models
{
    [Table("Materials_set")]
    public class MaterialsSet
    {
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("count")]
        public int Count { get; set; }

        [Required]
        [Column("materials_type_id")]
        public int MaterialsTypeId { get; set; }

        public MaterialsType MaterialsType { get; set; }

        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
