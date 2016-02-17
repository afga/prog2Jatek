using System;
using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Megjelenites;
using OE.Prog2.Jatek.Automatizmus;

namespace OE.Prog2.Jatek.Szabalyok {
    class Fal : RogzitettJatekElem, IKirajzolhato {
        public Fal(int x, int y, JatekTer ter) : base(x, y, ter) { }
        public override double Meret { get { return 1.0; } }
        public override void Utkozes(JatekElem elem) { }
        public char Alak { get { return '\u2593'; } }
    }
    class Jatekos : MozgoJatekElem, IKirajzolhato, IMegjelenitheto {
        string nev;
        public JatekosValtozasKezelo JatekosValtozas;
        public string Nev { get { return nev; } }
        public Jatekos(string nev, int x, int y, JatekTer ter)
            : base(x, y, ter) {
            this.nev = nev;
        }
        public override double Meret { get { return 0.2; } }
        public virtual char Alak { get { if (Aktiv) return '\u263A'; else return '\u263B'; } }
        public int[] MegjelenitendoMeret { get { return ter.MegjelenitendoMeret; } }
        public override void Utkozes(JatekElem elem) { }
        int eletero = 100;
        public void Serul(int hp) {
            if (eletero != 0) {
                eletero = eletero - hp < 0 ? 0 : eletero - hp;
                if (JatekosValtozas != null)
                    JatekosValtozas(this, pontszam, eletero);
                if (eletero == 0)
                    Aktiv = false;
            }
        }
        int pontszam = 0;
        public void PontotSzerez(int pont) {
            pontszam += pont;
            if (JatekosValtozas != null)
                JatekosValtozas(this, pontszam, eletero);
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
    class GepiJatekos : Jatekos, IAutomatikusanMukodo {
        static Random rnd = new Random();
        public GepiJatekos(string nev, int x, int y, JatekTer ter) : base(nev, x, y, ter) { }
        public void Mozgas() {
            int x = rnd.Next(0, 4);
            switch (x) {
                case 0:
                    Megy(0, -1);
                    break;
                case 1:
                    Megy(1, 0);
                    break;
                case 2:
                    Megy(0, 1);
                    break;
                case 3:
                    Megy(-1, 0);
                    break;
            }
        }
        public override char Alak { get { return '\u2640'; } }
        public void Mukodik() {
            Mozgas();
        }
        public int MukodesIntervallum { get { return 2; } }
    }
    class GonoszGepiJatekos : GepiJatekos {
        public GonoszGepiJatekos(string nev, int x, int y, JatekTer ter) : base(nev, x, y, ter) { }
        public override char Alak { get { return '\u2642'; } }
        public override void Utkozes(JatekElem elem) {
            base.Utkozes(elem);
            if (Aktiv && elem is Jatekos)
                (elem as Jatekos).Serul(10);
        }
    }
    class Kincs : RogzitettJatekElem, IKirajzolhato {
        public event KincsFelvetelKezelo KincsFelvetel;
        public Kincs(int x, int y, JatekTer ter) : base(x, y, ter) { }
        public override double Meret { get { return 1.0; } }
        public char Alak { get { return '\u2666'; } }
        public override void Utkozes(JatekElem elem) {
            if (elem is Jatekos) {
                (elem as Jatekos).PontotSzerez(50);
                ter.Torol(this);
                if (KincsFelvetel != null) {
                    KincsFelvetel(this, elem as Jatekos);
                }
            }
        }
    }
    delegate void KincsFelvetelKezelo(Kincs kincs, Jatekos jatekos);
    delegate void JatekosValtozasKezelo(Jatekos jatekos, int ujpont, int ujelet);
}
