using System.Runtime.Serialization;
 public enum TypeRepeat : byte
    {
         Week = 1,
         Month = 2,
         Year = 3
        
    }

namespace Calendar_Apriorit.ViewModel
{
    [DataContract]
    public class RepeatInfoVM
    {
        [DataMember]
        public TypeRepeat TypeRepeat { get; set; }
        [DataMember]
        public bool IsUnlimitedQuanties { get; set; }
        [DataMember]
        public int QuantityRepeats { get; set; }
    }
}