using System;
using System.Collections.Generic;
using System.Text;

namespace Feleveshazi_GBJ0CK
{
    class ListaElem
    {
        public ListaElem(ILátványosság tartalom)
        {
            Tartalom = tartalom;
        }

        public ILátványosság Tartalom {get; set;}
        public ListaElem Előző {get; set; }
        public ListaElem Következő {get; set;}
    }
    public class LáncoltLista
    {
        private ListaElem fej;
        public void Beszuras(ILátványosság latvanyossag)
        {
            ListaElem uj = new ListaElem(latvanyossag);
            ListaElem p;
            // Ha a lista üres.
            if (fej == null)
                fej = uj;

            // ha a csomópontot az elejére kell beilleszteni
            else if (fej.Tartalom.Erdekesseg.CompareTo(uj.Tartalom.Erdekesseg)>=0 )
            {
                uj.Következő = fej;
                uj.Következő.Előző = uj;
                fej = uj;
            }

            else
            {
                p = fej;
                //keresse meg azt a csomópontot, amely után az új csomópontot be kell illeszteni
                while (p.Következő != null && p.Következő.Tartalom.Erdekesseg.CompareTo(uj.Tartalom.Erdekesseg) < 0)
                {
                    p = p.Következő;
                }

                uj.Következő = p.Következő;

                // ha az új csomópont nincs beszúrva a lista végén
                if (p.Következő != null)
                {
                    uj.Következő.Előző = uj;
                }

                p.Következő = uj;
                uj.Előző = p;
            }
        }

    }
}
