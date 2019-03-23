using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinnWorks.Task.Entities
{
    [Table("OrderPriorities")]
    public class OrderPriority
    {
        [Key]
        public int OrderPriorityId { get; set; }

        [Required]
        [MaxLength(255)]
        public string OrderPriorityName { get; set; }
    }
}