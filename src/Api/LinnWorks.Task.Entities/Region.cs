using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LinnWorks.Task.Entities
{
    [Table("Regions")]
    public class Region
    {
        public int RegionId { get; set; }

        [Required]
        [MaxLength(255)]
        public string RegionName { get; set; }
    }
}