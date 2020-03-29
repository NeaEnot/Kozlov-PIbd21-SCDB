using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
    public class Order
    {
        int Id { get; set; }

        DateTime RegistrationDate { get; set; }

        DateTime DeliveryDate { get; set; }
    }
}
