using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("Worker")]
    public class Worker
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("second_name")]
        public string SecondName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("admission_date")]
        public DateTime AdmissionDate { get; set; }

        [Column("position_id")]
        public int? PositionId { get; set; }

        public Position Position { get; set; }

        [Column("brigade_id")]
        public int? BrigadeId { get; set; }

        public Brigade Brigade { get; set; }
    }
}
