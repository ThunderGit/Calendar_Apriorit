using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.ViewModel
{
    public class EventInfoVM
    {
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsRepeated { get; set; }
        public RepeatInfoVM RepeatInfo { get; set; }
    }
}
