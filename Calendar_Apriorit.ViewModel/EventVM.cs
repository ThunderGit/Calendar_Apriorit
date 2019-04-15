using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.ViewModel
{
    [DataContract]
    public class EventVM
    {
        [DataMember]

        public int IdEvent { get; set; }
        [DataMember]

        public string Title { get; set; }
        [DataMember]

        public EventInfoVM EventInfo { get; set; }

    }
}
