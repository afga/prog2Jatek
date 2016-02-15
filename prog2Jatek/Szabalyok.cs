using System;
using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Megjelenites;

namespace OE.Prog2.Jatek.Szabalyok {
    class Fal : RogzitettJatekElem, IKirajzolhato {
        public Fal(int x, int y, JatekTer ter) : base(x, y, ter) { }
        public override double Meret { get { return 1.0; } }
        public override void Utkozes(JatekElem elem) { }
        public char Alak { get { return '\u2593'; } }
    }
    class Jatekos : MozgoJatekElem, IKirajzolhato, IMegjelenitheto {
        string nev;
        public string Nev { get { return nev; } }
        public Jatekos(string nev, int x, int y, JatekTer ter) : base(x, y, ter) {
            this.nev = nev;
        }
        public override double Meret { get { return 0.2; } }
        public char Alak { get { if (Aktiv) return '\u263A'; else return '\u263B'; } }
        public int[] MegjelenitendoMeret { get { return ter.MegjelenitendoMeret; } }
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
        public IKirajzolhato[] MegjelenitedoElemek() {
            JatekElem[] kozel = ter.MegadottHelyenLevok(X, Y, 5);
            int db = 0;
            foreach (JatekElem k in kozel)
                if (k is IKirajzolhato)
                    db++;
            IKirajzolhato[] vissza = new IKirajzolhato[db];
            int i = 0;
            foreach (JatekElem k in kozel)
                if (k is IKirajzolhato)
                    vissza[i++] = k as IKirajzolhato;
            return vissza;
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
