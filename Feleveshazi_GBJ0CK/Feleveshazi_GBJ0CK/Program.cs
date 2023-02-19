using System;
using System.Collections.Generic;

namespace Feleveshazi_GBJ0CK
{

    class Program
    {
        static float ÉlFrissítő(ILátványosság csucs1, ILátványosság csucs2)
        {
            return (float)csucs1.Erdekesseg / (float)csucs2.Erdekesseg;
          
        }
        static void Main(string[] args)
        {
            LáncoltLista lista = new LáncoltLista();
            Múzeum muzeum = new Múzeum(ÉrdekességiSzint.Unalmas, 120,"Nagy Múzeum");
            Fürdő furdo = new Fürdő(ÉrdekességiSzint.NagyonJó, 150,"Szent Györgyi fürdő");
            Piac piac = new Piac(ÉrdekességiSzint.Átlagos, 300,"Ecseri piac");
            TermálFürdő termal = new TermálFürdő(ÉrdekességiSzint.EgészJó, 200,"Szechenyi termálfürdő");
            lista.Beszuras(muzeum);//0
            lista.Beszuras(furdo);//1
            lista.Beszuras(piac);//2
            lista.Beszuras(termal);//3



            IranyitottGraf<TuristaLátványosság> graf = new GrafSzomszedsag<TuristaLátványosság>();
            graf.UjCsucs(muzeum);
            graf.UjCsucs(furdo);
            graf.UjCsucs(piac);
            graf.UjCsucs(termal);


            graf.UjEl(muzeum, furdo, 180);
            graf.UjEl(muzeum, termal, 230);
            graf.UjEl(furdo, piac, 215);
            graf.UjEl(termal, piac, 190);


            

            LáncoltListaNagyonJó<TuristaLátványosság> nagyonjolista = graf.NagyonJoListaBeszuras();
        
            //graf.ÉleketFrissít(ÉlFrissítő);
            
            double km = 0;
            // ha törlünk egy UjCsucsot, azon nem haladhatunk át, mivel nem létzeik se Csús se Él.
            Turista turista = new Turista(muzeum, piac, furdo);
            
            List<TuristaLátványosság> allomasok = graf.Dijkstra(turista.Kiindulopont as TuristaLátványosság, turista.Vegpont as TuristaLátványosság, turista.Közbülsopont as TuristaLátványosság, ref km);
            
            Console.WriteLine($"{turista.Kiindulopont.Nev} - {turista.Vegpont.Nev} útvonalterv:");
            Console.WriteLine($"Távolság:{km} ");
            Console.WriteLine($"Közbülső pont: {turista.Közbülsopont.Nev}");
            Console.WriteLine("Megállók:");
            foreach (TuristaLátványosság item in allomasok)
            {
                Console.WriteLine("\t"+item.Nev);
            }


            Console.ReadLine();
        }
    }
}
