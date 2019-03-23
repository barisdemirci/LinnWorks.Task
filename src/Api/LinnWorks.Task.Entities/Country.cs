using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LinnWorks.Task.Entities
{
    [Table("Countries")]
    public class Country
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