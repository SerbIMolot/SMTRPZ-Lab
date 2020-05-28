using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SMTRPZ_IT_company.Model
{
    public class Employee
    {
        [Key]
        public int employeeId { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }

        public List<EmplTask> tasks { get; set; }

        public override string ToString()
        {
            return "ID: " + employeeId + " " + firstName + " " + lastName;
        }
    }
}