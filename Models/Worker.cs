using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
    public class Worker
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string SecondName { get; set; }

        string LastName { get; set; }

        DateTime BirthDate { get; set; }

        DateTime AdmissionDate { get; set; }

        int PositionId { get; set; }

        Position Position { get; set; }

        int BrigadeId { get; set; }

        Brigade Brigade { get; set; }
    }
}
