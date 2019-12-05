using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Policy;

namespace jukbook.Models
{
    public class BroonContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Teenus> Teenused { get; set; }
        public DbSet<Broon> Brooneringud { get; set; }

        public BroonContext() : base("Database1")
        {
            if (!Database.Exists()) Database.Create();

            //Users.Add(new User() { login = "admin", password = Hash.ComputeSha256Hash("admin"), roleID = 2, name = "Vadim" });

            //Teenused.Add(new Teenus { MisTeenus = "Soeng", Kirjeldus = "Juuksu soeng", Kestvus = "60 min", Hind = 20 });
            //Teenused.Add(new Teenus { MisTeenus = "Keratiinihooldus", Kirjeldus = "Juuksu keratiinihooldus", Kestvus = "120 min", Hind = 40 });
            //Teenused.Add(new Teenus { MisTeenus = "Flexing", Kirjeldus = "Juuksu flexing", Kestvus = "60 min", Hind = 20 });

            SaveChanges();
        }
    }
}