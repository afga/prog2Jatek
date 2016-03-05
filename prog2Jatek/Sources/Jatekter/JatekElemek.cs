using OE.Prog2.Jatek.Szabalyok;

namespace OE.Prog2.Jatek.Jatekter {
    public abstract class JatekElem {
        public int X { get; set; }
        public int Y { get; set; }
        protected JatekTer ter;
        public JatekElem(int x, int y, JatekTer ter) {
            this.X = x;
            this.Y = y;
            this.ter = ter;
        }
        public abstract double Meret { get; }
        public abstract void Utkozes(JatekElem elem);
    }

    abstract class RogzitettJatekElem : JatekElem {
        public RogzitettJatekElem(int x, int y, JatekTer ter)
            : base(x, y, ter) {
        }
    }
    abstract class MozgoJatekElem : JatekElem {
        public MozgoJatekElem(int x, int y, JatekTer ter)
            : base(x, y, ter) {
            Aktiv = true;
            ter.Felvesz(this);
        }
        public bool Aktiv { get; set; }
        public void AtHelyez(int ujx, int ujy) {
            JatekElem[] ujHely = ter.MegadottHelyenLevok(ujx, ujy);
            foreach (JatekElem je in ujHely) {
                if (!Aktiv)
                    throw new MozgasHalalMiattNemSikerult(this, ujx, ujy);
                je.Utkozes(this);
            }
            foreach (JatekElem je in ujHely) {
                if (!Aktiv)
                    throw new MozgasHalalMiattNemSikerult(this, ujx, ujy);
                Utkozes(je);
            }
            if (Aktiv) {
                ujHely = ter.MegadottHelyenLevok(ujx, ujy);
                double osszMeret = 0.0;
                foreach (JatekElem je in ujHely)
                    osszMeret += je.Meret;
                if (osszMeret + Meret <= 1.0) {
                    X = ujx;
                    Y = ujy;
                }
                else
                    throw new MozgasHelyHianyMiattNemSikerultKivetel(ujHely, this, ujx, ujy);
            }
        }
    }
}
