﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calendar_Apriorit.DAL.Entities
{
    /// <summary>
    /// Need Add realization
    /// 
    /// </summary>
    public class Calendar
    {

        public int Id { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual User User { get; set; }



    }
}
