using System.Runtime.Serialization;

namespace Calendar_Apriorit.ViewModel
{
    [DataContract]
    public class RepeatInfoVM
    {
        [DataMember]

        public string WeekDayForRepeat { get; set; }
        [DataMember]
        public string MonthForRepeat { get; set; }
        [DataMember]

        public bool IsUnlimitedQuanties { get; set; }
        [DataMember]

        public int QuantityRepeats { get; set; }
    }
}