using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
    public class MaterialsSet
    {
        int Id { get; set; }

        int Count { get; set; }

        Order Order { get; set; }

        MaterialsType MaterialsType { get; set; }
    }
}
