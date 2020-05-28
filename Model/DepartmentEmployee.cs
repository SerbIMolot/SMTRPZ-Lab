using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SMTRPZ_IT_company.Model
{
    public class DepartmentEmployee
    {
        [Key, Column(Order = 1)]
        public int employeeId { get; set; }
        [Key, Column(Order = 2)]
        public int departmentId { get; set; }

        public Employee employee { get; set; }
        public Department department { get; set; }
    }
}