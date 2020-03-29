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

        List<MaterialsSet> MaterialsSets { get; set; }

        List<Brigade> Brigades { get; set; }

        List<Work> Works { get; set; }
    }
}
