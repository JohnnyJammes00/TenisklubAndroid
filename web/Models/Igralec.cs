using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Igralec
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public ICollection<IgralecSkupina> IgralecSkupine { get; set; }
        public ICollection<Rezervacija> Rezervacije { get; set; }
    }
}