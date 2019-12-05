using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace jukbook.Models
{
    public class Teenus
    {
        public int Id { get; set; }
        public string MisTeenus { get; set; }
        public string Kirjeldus { get; set; }
        public string Kestvus { get; set; }
        public int Hind { get; set; }

    }
}