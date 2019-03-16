using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LinnWorks.Task.Entities
{
    [Table("ItemTypes")]
    public class ItemType : BaseEntity
    {
        [Key]
        public int ItemTypeId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ItemTypeName { get; set; }
    }
}