using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab5.Models
{
    [Table("Position")]
    public class Position
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("salary")]
        public int Salary { get; set; }

        public List<Worker> Workers { get; set; }
    }
}
