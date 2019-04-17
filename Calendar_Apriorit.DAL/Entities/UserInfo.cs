using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.DAL.Entities
{
    /// <summary>
    /// Non-main information, have connects with other classes 
    /// </summary>
    public class UserInfo
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        public string Name { get; set; }
        
        //public Calendar StandartForAllUsersCalendar;//maybe delete this field and add for all users standart calendar in BLL 
        

        public virtual User User { get; set; }
    }
}
