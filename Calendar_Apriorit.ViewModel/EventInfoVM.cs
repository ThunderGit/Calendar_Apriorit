using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.ViewModel
{
    [DataContract]
    public class EventInfoVM
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]

        public DateTime StartTime { get; set; }
        [DataMember]

        public DateTime EndTime { get; set; }
        [DataMember]

        public bool IsRepeated { get; set; }
        [DataMember]

        public RepeatInfoVM RepeatInfo { get; set; }
    }
}
