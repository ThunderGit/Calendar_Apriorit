using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.ViewModel
{
    public class EventVM
    {
        public int IdEvent { get; set; }
        public string Title { get; set; }
        public EventInfoVM EventInfo { get; set; }

    }
}
