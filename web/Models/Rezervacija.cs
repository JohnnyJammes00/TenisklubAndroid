using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Rezervacija
    {
        public int ID { get; set; }

        [Display(Name = "Datum rezervacije")]
        public DateTime DatumRezervacije { get; set; }
        public int IgralecID { get; set; }
        public int IgrisceID { get; set; }
        public Igralec Igralec { get; set; }
        public Igrisce Igrisce { get; set; }
        
        [Display(Name = "Trajanje(h)")]
        public int Tranjanje { get; set; } // število v urah

        //TODO Add duration of reservation (ex. 1 hour) => int value that represents hours max value 12 hours.
    }
}