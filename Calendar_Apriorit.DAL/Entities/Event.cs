namespace Calendar_Apriorit.DAL.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Group Group { get; set; }
        public virtual EventInfo EventInfo { get; set; }
    }
}