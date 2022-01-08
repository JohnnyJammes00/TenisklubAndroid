using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Skupina
    {
        public int ID { get; set; }
        public string ImeSkupine { get; set; }
        public ICollection<IgralecSkupina> IgralecSkupine { get; set; }
    }
}