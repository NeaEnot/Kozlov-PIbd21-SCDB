using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("Work_type")]
    public class WorkType
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        public List<Work> Works { get; set; }

        public List<Brigade> Brigades { get; set; }
    }
}
