using System;
using System.Collections.Generic;
using System.Text;

namespace TorensVanHanoi.Model
{
    public class Toren
    {
        private Stack<Schijf> _Schijven;

        public Toren() : this(0)
        {

        }

        internal Toren(int AantalSchijven)
        {
            _Schijven = new Stack<Schijf>(AantalSchijven);
            for (int i = 0; i < AantalSchijven; i++)
            {
                LegSchijf(new Schijf(AantalSchijven - i));
            }
        }

        public void LegSchijf(Schijf Schijf)
        {
            if ((BovensteSchijf?.Diameter ?? int.MaxValue) <= Schijf.Diameter)
                throw new SchijfTeGrootException();
            _Schijven.Push(Schijf);
        }

        public Schijf BovensteSchijf
        {
            get
            {
                if (_Schijven.Count == 0)
                {
                    return null;
                }
                return _Schijven.Peek();
            }
        }

        public Schijf NeemSchijf()
        {
            if (BovensteSchijf == null)
            {
                throw new TorenLeegException();
            }
            var Schijf = BovensteSchijf;
            _Schijven.Pop();
            return Schijf;
        }
    }
}