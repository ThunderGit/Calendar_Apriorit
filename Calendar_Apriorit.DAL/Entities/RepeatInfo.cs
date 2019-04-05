namespace Calendar_Apriorit.DAL.Entities
{
    public class RepeatInfo
    {
        public int Id { get; set; }
        //if event repeats every week, we need WeekDayForRepeat
        public string WeekDayForRepeat { get; set; }//maybe need enum
        //need for events,that repeats every year
        public string MonthForRepeat { get; set; }//maybe need enum
        public bool IsUnlimitedQuanties { get; set; }
        public int QuantityRepeats { get; set; }

    }
}