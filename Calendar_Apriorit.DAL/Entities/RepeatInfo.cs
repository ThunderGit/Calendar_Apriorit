namespace Calendar_Apriorit.DAL.Entities
{
    public enum TypeRepeat : byte
    {
         Week = 1,
         Month = 2,
         Year = 3
        
    }
    public class RepeatInfo
    {
        public int Id { get; set; }
        public bool IsUnlimitedQuanties { get; set; }
        public int QuantityRepeats { get; set; }
        public TypeRepeat TypeRepeat { get; set; }
        public EventInfo EventInfo { get; set; }

    }
}