using System;
using System.ComponentModel.DataAnnotations;

namespace Calendar_Apriorit.DAL.Entities
{
    public class EventInfo
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool IsRepeated { get; set; }
        public virtual RepeatInfo RepeatInfo { get; set; }
        public virtual Event EventForThisInfo { get; set; }

    }
}