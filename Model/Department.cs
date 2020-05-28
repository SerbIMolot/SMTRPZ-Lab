using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SMTRPZ_IT_company.Model
{
    public class Department
    {
        [Key]
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public int numOfEmployees { get; set; }

        public override string ToString()
        {
            return "ID: " + departmentId + " " + departmentName;
        }
    }
}