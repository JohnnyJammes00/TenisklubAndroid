using System;
using System.Collections.Generic;

namespace web.Models
{
    public class IgralecSkupina
    {
        public int ID { get; set; }
        public int IgralecID { get; set; }
        public int SkupinaID { get; set; }
        public Igralec Igralec { get; set; }
        public Skupina Skupina { get; set; }

        public DateTime? DateJoined { get; set; }

        public DateTime? DateEdited { get; set; }
        public ApplicationUser? Owner { get; set; }
    }
}