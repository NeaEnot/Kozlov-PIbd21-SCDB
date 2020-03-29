using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
    public class Work
    {
        int Id { get; set; }

        WorkType WorkType { get; set; }

        Order Order { get; set; }
    }
}
