using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LinnWorks.Task.Entities
{
    [Table("Countries")]
    public class Country : BaseEntity
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(255)]
        public string CountryCode { get; set; }

        [Required]
        [MaxLength(255)]
        public string CountryName { get; set; }
    }
}