using System;
using System.Collections.Generic;
using System.Text;

namespace Feleveshazi_GBJ0CK
{
    public enum ÉrdekességiSzint{ Unalmas=1,Átlagos,EgészJó,NagyonJó }
    public interface ILátványosság
    {
        int JegyÁr { get; set; }

        public string Nev { get; set; }

        ÉrdekességiSzint Erdekesseg { get; set; }

        public ILátványosság[] HasonlóLátványosságok();

    }
}
