using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Entities
{
    public class ItemType : BaseEntity
    {
        public int ItemTypeId { get; set; }

        public string ItemTypeName { get; set; }
    }
}