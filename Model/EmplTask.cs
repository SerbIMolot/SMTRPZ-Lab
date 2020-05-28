using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMTRPZ_IT_company.Model
{
    public class EmplTask
    {
        [Key]
        public int taskId { get; set; }

        [Required]
        public string task { get; set; }

        [Required]
        public int employeeId { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime deadLine { get; set; }
    }
}