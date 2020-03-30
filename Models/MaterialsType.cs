using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab5.Models
{
    [Table("Materials_type")]
    public class MaterialsType
    {
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("price_per_unit")]
        public int PricePerUnit { get; set; }

        public List<MaterialsSet> MaterialsSets { get; set; }
    }
}
