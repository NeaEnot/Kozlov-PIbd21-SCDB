using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
    public class Brigade
    {
        int Id { get; set; }

        int WorkTypeId { get; set; }

        WorkType WorkType { get; set; }

        int OrderId { get; set; }

        Order Order { get; set; }
    }
}
