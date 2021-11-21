using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Tekma
    {
        public int ID { get; set; }
        public int? Igralec1ID { get; set; }
        public int? Igralec2ID { get; set; }
        public Igralec Igralec1{ get; set; }
        public Igralec Igralec2 { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public int? Winner { get; set; } // 1 winner player 1, -1 winner player 2, 0 draw // TODO
        public DateTime Date { get; set; }
        public int IgrisceID { get; set; }
        public Igrisce Igrisce { get; set; }
    }
}