using System;
using System.Collections.Generic;
using System.Text;

namespace Feleveshazi_GBJ0CK
{
     class TuristaLátványosság : ILátványosság
    {
        public int JegyÁr { get; set; }

        public ÉrdekességiSzint Erdekesseg { get; set; }

        public string Nev { get; set; }

        public TuristaLátványosság(ÉrdekességiSzint erdekessegiszint, int jegyar,string nev)
        {

            this.JegyÁr = jegyar;
            this.Erdekesseg = erdekessegiszint;
            this.Nev = nev;
        }

        public ILátványosság[] HasonlóLátványosságok()
        {
            return default;
        }
    }
}
