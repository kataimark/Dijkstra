using System;
using System.Collections.Generic;
using System.Text;

namespace Feleveshazi_GBJ0CK
{
    class ListaElemNagyonJó<T> where T : TuristaLátványosság
    { 
    
        public T Tartalom { get; set; }

        public ListaElemNagyonJó<T> Kovetkezo { get; set; }

    }
    class LáncoltListaNagyonJó<T> where T: TuristaLátványosság
    {
        private ListaElemNagyonJó<T> fej;

        public void Beszuras(T tartalom)
        {
            ListaElemNagyonJó<T> uj = new ListaElemNagyonJó<T>();
            uj.Tartalom = tartalom;
            uj.Kovetkezo = fej;
            fej = uj;
        }


    }
}
