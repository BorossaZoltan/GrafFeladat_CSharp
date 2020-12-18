using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        public void Torles(int cs1, int cs2)
        {
            
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            for (int i = 0; i < elek.Count-1; i++)
            {
                var el = elek[i];
                if ((el.Csucs1 == cs1 && el.Csucs2 == cs2) || (el.Csucs1 == cs2 && el.Csucs2 == cs1))
                {
                    elek.Remove(el);
                    el = elek[i];
                    elek.Remove(el);
                }
            }
        }

        public void SzelessegiBejar(int kezdopont)
        {

            // Kezdetben egy pontot sem jártunk be
            HashSet<int> bejart = new HashSet<int>();

            // A következőnek vizsgált elem a kezdőpont
            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Enqueue(kezdopont);
            bejart.Add(kezdopont);

            // Amíg van következő, addig megyünk
            while (kovetkezok.Count != 0)
            {
                // A sor elejéről vesszük ki
                int k = kovetkezok.Dequeue();

                // Elvégezzük a bejárási műveletet, pl. a konzolra kiírást:
                Console.WriteLine(this.csucsok[k]);

                foreach (var el in this.elek)
                {
                    // Megkeressük azokat az éleket, amelyek k-ból indulnak
                    // Ha az él másik felét még nem vizsgáltuk, akkor megvizsgáljuk
                    if ((el.Csucs1 == k) && (!bejart.Contains(el.Csucs2)))
                    {
                        // A sor végére és a bejártak közé szúrjuk be
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }

                // Jöhet a sor szerinti következő elem
            }
            

        }

        public void MelysegiBejar(int kezdopont)
        {
            // Kezdetben egy pontot sem jártunk be
            HashSet<int> bejart = new HashSet<int>();

            // A következőnek vizsgált elem a kezdőpont
            Stack<int> kovetkezok = new Stack<int>();
            kovetkezok.Push(kezdopont);
            bejart.Add(kezdopont);

            // Amíg van következő, addig megyünk
            while (kovetkezok.Count != 0)
            {
                // A verem tetejéről vesszük le
                int k = kovetkezok.Pop();

                // Elvégezzük a bejárási műveletet, pl. a konzolra kiírást:
                Console.WriteLine(this.csucsok[k]);

                foreach (var el in this.elek)
                {
                    // Megkeressük azokat az éleket, amelyek k-ból indulnak
                    // Ha az él másik felét még nem vizsgáltuk, akkor megvizsgáljuk
                    if ((el.Csucs1 == k) && (!bejart.Contains(el.Csucs2)))
                    {
                        // A verem tetejére és a bejártak közé adjuk hozzá
                        kovetkezok.Push(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
                // Jöhet a sor szerinti következő elem
            }

        }

        public bool Osszefuggo()
        {
            HashSet<int> bejart = new HashSet<int>();

            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Enqueue(0); // Tetszőleges, mondjuk 0 kezdőpont
            bejart.Add(0);

            while (kovetkezok.Count != 0)
            {
                int k = kovetkezok.Dequeue();

                // Bejárás közben nem kell semmit csinálni

                foreach (var el in this.elek)
                {
                    if ((el.Csucs1 == k) && (!bejart.Contains(el.Csucs2)))
                    {
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }
            // A végén megvizsgáljuk, hogy minden pontot bejártunk-e
            if (bejart.Count == this.csucsokSzama)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}