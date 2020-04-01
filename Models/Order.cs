using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace Lab5.Models
{
    [Table("order")]
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [Column("delivery_date")]
        public DateTime DeliveryDate { get; set; }

        public List<MaterialsSet> MaterialsSets { get; set; }

        public List<Brigade> Brigades { get; set; }

        public List<Work> Works { get; set; }
    }
}
