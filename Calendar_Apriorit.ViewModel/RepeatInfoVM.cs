namespace Calendar_Apriorit.ViewModel
{
    public class RepeatInfoVM
    {
        public string WeekDayForRepeat { get; set; }//maybe need enum
        //need for events,that repeats every year
        public string MonthForRepeat { get; set; }//maybe need enum
        public bool IsUnlimitedQuanties { get; set; }
        public int QuantityRepeats { get; set; }
    }
}