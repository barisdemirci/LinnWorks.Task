using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Entities
{
    public class Country : BaseEntity
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }
    }
}