using System;
using System.Collections.Generic;
using System.Text;

namespace TorensVanHanoi.Model
{
    public class Hanoi
    {
        public Hanoi(int AantalSchijven)
        {
            if (AantalSchijven < 0 || AantalSchijven > 100)
                throw new OngeldigAantalSchijvenException();
            if (AantalSchijven == 0)
                throw new TorenLeegException();
            StartToren = new Toren(AantalSchijven);
            MiddelsteToren = new Toren();
            EindToren = new Toren();
        }


        public Toren StartToren { get; set; }
        public Toren MiddelsteToren { get; set; }
        public Toren EindToren { get; set; }

    }
}