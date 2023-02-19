using System;
using System.Collections.Generic;
using System.Text;

namespace Feleveshazi_GBJ0CK
{
    class Turista
    {
        public ILátványosság Kiindulopont { get; set; }

        public ILátványosság Vegpont { get; set; }

        public ILátványosság Közbülsopont { get; set; }

        public Turista(ILátványosság kiindulopont, ILátványosság cel, ILátványosság közbülsopont)
        {
            Kiindulopont = kiindulopont;
            Vegpont = cel;
            Közbülsopont = közbülsopont;
        }
    }
}
