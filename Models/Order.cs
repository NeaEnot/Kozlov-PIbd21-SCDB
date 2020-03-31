using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab5.Models
{
    [Table("Order")]
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("registreation_date")]
        public DateTime RegistrationDate { get; set; }

        [Column("delivery_date")]
        public DateTime DeliveryDate { get; set; }

        public List<MaterialsSet> MaterialsSets { get; set; }

        public List<Brigade> Brigades { get; set; }

        public List<Work> Works { get; set; }
    }
}
