﻿using System;
using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Megjelenites;

namespace OE.Prog2.Jatek.Szabalyok {
    class Fal : RogzitettJatekElem, IKirajzolhato {
        public Fal(int x, int y, JatekTer ter) : base(x, y, ter) { }
        public override double Meret { get { return 1.0; } }
        public override void Utkozes(JatekElem elem) { }
        public char Alak { get { return '\u2593'; } }
    }
    class Jatekos : MozgoJatekElem, IKirajzolhato {
        string nev;
        public string Nev { get { return nev; } }
        public Jatekos(string nev, int x, int y, JatekTer ter) : base(x, y, ter) {
            this.nev = nev;
        }
        public override double Meret { get { return 0.2; } }
        public char Alak { get { if (Aktiv) return '\u263A'; else return '\u263B'; } }
        public override void Utkozes(JatekElem elem) { }
        int eletero = 100;
        public void Serul(int hp) {
            if (eletero != 0) {
                eletero = eletero - hp < 0 ? 0 : eletero - hp;
                if (eletero == 0)
                    Aktiv = false;
            }
        }
        int pontszam = 0;
        public void PontotSzerez(int pont) {
            pontszam += pont;
        }
        public void Megy(int rx, int ry) {
            int ujx = X + rx;
            int ujy = Y + ry;
            AtHelyez(ujx, ujy);
        }
    }
    class Kincs : RogzitettJatekElem, IKirajzolhato {
        public Kincs(int x, int y, JatekTer ter) : base(x, y, ter) { }
        public override double Meret { get { return 1.0; } }
        public char Alak { get { return '\u2666'; } }
        public override void Utkozes(JatekElem elem) {
            if (elem is Jatekos) {
                (elem as Jatekos).PontotSzerez(50);
                ter.Torol(this);
            }
        }
    }
}
