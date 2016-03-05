using OE.Prog2.Jatek.Automatizmus;
using OE.Prog2.Jatek.Jatekter;
using System;

namespace OE.Prog2.Jatek.Szabalyok {
    class GepiJatekos : Jatekos, IAutomatikusanMukodo {
        static Random rnd = new Random();
        public GepiJatekos(string nev, int x, int y, JatekTer ter)
            : base(nev, x, y, ter) {
        }
        public void Mozgas() {
            bool sikerult = false;
            int iter = 1;
            int x = rnd.Next(0, 4);
            while (!sikerult) {
                try {
                    switch (x) {
                        case 0:
                            Megy(0, -1);
                            sikerult = true;
                            break;
                        case 1:
                            Megy(1, 0);
                            sikerult = true;
                            break;
                        case 2:
                            Megy(0, 1);
                            sikerult = true;
                            break;
                        case 3:
                            Megy(-1, 0);
                            sikerult = true;
                            break;
                    }
                }
                catch (MozgasHelyHianyMiattNemSikerultKivetel) {
                    if (iter < 4) {
                        iter++;
                        x++;
                        x %= 4;
                    }
                    else
                        sikerult = true;
                }
            }
        }
        public override char Alak { get { return '\u2640'; } }
        public void Mukodik() {
            Mozgas();
        }
        public int MukodesIntervallum { get { return 2; } }
    }
    class GonoszGepiJatekos : GepiJatekos {
        public GonoszGepiJatekos(string nev, int x, int y, JatekTer ter)
            : base(nev, x, y, ter) {
        }
        public override char Alak { get { return '\u2642'; } }
        public override void Utkozes(JatekElem elem) {
            base.Utkozes(elem);
            if (Aktiv && elem is Jatekos)
                (elem as Jatekos).Serul(10);
        }
    }
}
