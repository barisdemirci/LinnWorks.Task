using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Entities
{
    public class OrderPriority : BaseEntity
    {
        public int OrderPriorityId { get; set; }

        public string OrderPriorityName { get; set; }
    }
}