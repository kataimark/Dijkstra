using System;
using System.Collections.Generic;
using System.Text;

namespace Feleveshazi_GBJ0CK
{
    public delegate float SzempontDelegált(ILátványosság csucs1, ILátványosság csucs2);
    abstract class IranyitottGraf<T> where T: TuristaLátványosság
    {
        protected class EL
        {
            public T hova;
            public double suly;
        }

        public abstract void UjCsucs(T tartalom);

        public abstract void UjEl(T honna, T hova, double suly=1, bool iranyitott=false);

        public abstract void UjCsucsTorles(T tartalom);

        public abstract void ÉleketFrissít(SzempontDelegált delegáltpéldány);

        // --------------------------------------------
        // Dijkstra-hoz kell.
        protected abstract List<EL> Szomszedok(T csucs);

        protected abstract T AdottIndexCsucs(int index);

        protected abstract int AdottCsucsIndexe(T csucs);

        protected abstract int CsucsokSzama { get; }
        // --------------------------------------------
        public abstract LáncoltListaNagyonJó<T> NagyonJoListaBeszuras();

        //Közbülső pont kell
        public List<T> Dijkstra(T start, T cel, T erintett, ref double osszsuly)
        {
            double[] d = new double[CsucsokSzama];
            T[] n = new T[CsucsokSzama];
            List<T> S = new List<T>();
            for (int i = 0; i < CsucsokSzama; i++)
            {
                T x = AdottIndexCsucs(i);
                d[i] = double.PositiveInfinity;
                n[i] = default(T);
                S.Add(x);

            }
            
            int iteracio = 0;
            while (iteracio < S.Count && erintett != S[iteracio])
            {
                iteracio++;
            }
             if (iteracio >= S.Count)
             {
                    throw new DijkstraException();
             }

            
            d[AdottCsucsIndexe(start)] = 0;

            while (S.Count != 0)
            {

                T u = MinKivesz(S, d);
                foreach (EL x in Szomszedok(u))
                {
                    int ind_x = AdottCsucsIndexe(x.hova); // ahol éppen állnuk //furdo
                    int ind_u = AdottCsucsIndexe(u); // a minimum ahol áll //muzeum
                    if (d[ind_u] + x.suly < d[ind_x] || u.Equals(erintett))
                    {
                        d[ind_x] = d[ind_u] + x.suly;
                        n[ind_x] = u;
                    }


                }
                
            }
            
            // Összes kilométer
            int celindex = AdottCsucsIndexe(cel);
            osszsuly = d[celindex];
            //Megállók listája.
            List<T> allomasok = new List<T>();
            while (!cel.Equals(start))
            {
                allomasok.Add(cel);
                cel = n[celindex];
                celindex = AdottCsucsIndexe(cel);

            }
            allomasok.Add(start);
            allomasok.Reverse();

            return allomasok;
            
        }

        private T MinKivesz(List<T> S, double[] d)
        {
            int minindex = 0;
            double min = double.PositiveInfinity;
            //debrecen, pécs, siófok
            for (int i = 0; i < S.Count; i++)
            {
                int idx = AdottCsucsIndexe(S[i]);
                double sulyertek = d[idx];
                if (sulyertek < min)
                {
                    min = sulyertek;
                    minindex = i;
                }
            }
            T torlendo = S[minindex];
            S.RemoveAt(minindex);
            return torlendo;
        }
    }

    class GrafSzomszedsag<T> : IranyitottGraf<T> where T : TuristaLátványosság
    {

        List<T> tartalmak;
        List<List<EL>> szomszedok;
        public GrafSzomszedsag()
        {
            tartalmak = new List<T>();
            szomszedok = new List<List<EL>>();
        }

        // Csúccsal együtt az él is törlődik.
        public override void UjCsucsTorles(T tartalom)
        {
            int index = tartalmak.IndexOf(tartalom);
            tartalmak.Remove(tartalom);
            szomszedok.Remove(szomszedok[index]);
        }

        public override void UjCsucs(T tartalom)
        {
            tartalmak.Add(tartalom);
            szomszedok.Add(new List<EL>());
        }

        public override void UjEl(T honnan, T hova, double suly = 1, bool iranyitott = false)
        {
            int index = tartalmak.IndexOf(honnan);
            szomszedok[index].Add(new IranyitottGraf<T>.EL
            {
                hova = hova,
                suly=suly
            }); ;
        }

        public override void ÉleketFrissít(SzempontDelegált delegáltpéldány)
        {
            for (int i = 0; i < szomszedok.Count; i++)
            {
                for (int u = 0; u < szomszedok[i].Count; u++)
                {
                    szomszedok[i][u].suly *= delegáltpéldány(tartalmak[i] as ILátványosság, szomszedok[i][u].hova as ILátványosság);
                }
            }
        }

        // --------------------------------------------
        // Dijkstra-hoz kell.
        protected override int CsucsokSzama
        {
            get { return tartalmak.Count; }
        }

        protected override int AdottCsucsIndexe(T csucs)
        {
            return tartalmak.IndexOf(csucs);
        }

        protected override T AdottIndexCsucs(int index)
        {
            return tartalmak[index];
        }

        protected override List<EL> Szomszedok(T csucs)
        {
            int index = tartalmak.IndexOf(csucs);
            return szomszedok[index];
        }
        // --------------------------------------------

        //NagyonJó érdekességgel rendelkező helyek összegyűjtése.
        public override LáncoltListaNagyonJó<T> NagyonJoListaBeszuras()
        {
            LáncoltListaNagyonJó<T> nagyonlist = new LáncoltListaNagyonJó<T>();
            for (int i = 0; i < tartalmak.Count; i++)
            {             
                if (tartalmak[i].Erdekesseg == ÉrdekességiSzint.NagyonJó)
                {
                    nagyonlist.Beszuras(tartalmak[i]);
                }
            }
            return nagyonlist;
        }
    }
}
