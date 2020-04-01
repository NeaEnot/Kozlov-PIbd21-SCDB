using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("worker")]
    public class Worker
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Column("second_name")]
        public string SecondName { get; set; }

        [Required]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Column("admission_date")]
        public DateTime AdmissionDate { get; set; }

        [Required]
        [Column("position_id")]
        public int? PositionId { get; set; }

        public Position Position { get; set; }

        [Required]
        [Column("brigade_id")]
        public int? BrigadeId { get; set; }

        public Brigade Brigade { get; set; }
    }
}
