﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinnWorks.Task.Entities
{
    [Table("SalesChannels")]
    public class SalesChannel
    {
        [Key]
        public int SalesChannelId { get; set; }

        [Required]
        [MaxLength(255)]
        public string SalesChannelName { get; set; }
    }
}