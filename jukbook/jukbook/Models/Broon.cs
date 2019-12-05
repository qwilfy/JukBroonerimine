using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace jukbook.Models
{
    public class Broon
    {
        public int BroonId { get; set; }
        // название книги
        public string Nimi { get; set; }
        // автор книги
        [Display(Name = "Kellaeg")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Kellaeg { get; set; }
        public string Teenus { get; set; }
        public int TeenusId { get; set; }

        public int TblUsersID { get; set; }
        public bool Makstud { get; set; }
        public string Email { get; set; }
    }
}