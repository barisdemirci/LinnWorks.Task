using System;
using System.Collections.Generic;

namespace LinnWorks.Task.Web.ViewModel
{
    public class FilterParametersViewModel
    {
        public List<DropDownViewModel> Countries { get; set; }

        public List<DropDownViewModel> ItemTypes { get; set; }

        public List<DropDownViewModel> SalesChannels { get; set; }

        public List<DropDownViewModel> OrderPriorities { get; set; }

        public List<DropDownViewModel> Regions { get; set; }
    }
}