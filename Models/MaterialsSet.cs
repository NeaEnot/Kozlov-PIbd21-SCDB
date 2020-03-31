using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("Materials_set")]
    public class MaterialsSet
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("count")]
        public int Count { get; set; }

        [Column("materials_type_id")]
        public int? MaterialsTypeId { get; set; }

        public MaterialsType MaterialsType { get; set; }

        [Column("order_id")]
        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
