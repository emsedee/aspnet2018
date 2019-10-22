using System;
using System.Collections.Generic;
using System.Text;

namespace TorensVanHanoi.Model
{
    public class Schijf
    {
        public Schijf(int diameter)
        {
            if (diameter > 0 && diameter <= 100)
            {
                Diameter = diameter;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int Diameter { get; }
    }
}