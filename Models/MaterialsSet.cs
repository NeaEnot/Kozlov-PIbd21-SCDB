using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
    public class MaterialsSet
    {
        int Id { get; set; }

        int Count { get; set; }

        int MaterialsTypeId { get; set; }

        MaterialsType MaterialsType { get; set; }

        int OrderId { get; set; }

        Order Order { get; set; }
    }
}
