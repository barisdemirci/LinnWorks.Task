using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Entities
{
    public class SalesChannel : BaseEntity
    {
        public int SalesChannelId { get; set; }

        public string SalesChannelName { get; set; }
    }
}