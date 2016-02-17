using System;
using OE.Prog2.Jatek.Megjelenites;

namespace OE.Prog2.Jatek.Jatekter {
    abstract class JatekElem {
        int x, y;
        public int X {
            get { return x; }
            set { x = value; }
        }
        public int Y {
            get { return y; }
            set { y = value; }
        }
        protected JatekTer ter;
        public JatekElem(int x, int y, JatekTer ter) {
            this.x = x;
            this.y = y;
            this.ter = ter;
        }
        public abstract double Meret { get; }
        public abstract void Utkozes(JatekElem elem);
    }
    abstract class RogzitettJatekElem : JatekElem {
        public RogzitettJatekElem(int x, int y, JatekTer ter) : base(x, y, ter) { }
    }
    abstract class MozgoJatekElem : JatekElem {
        public MozgoJatekElem(int x, int y, JatekTer ter) : base(x, y, ter) { }
        bool aktiv;
        public bool Aktiv {
            get { return aktiv; }
            set { aktiv = value; }
        }
        public void AtHelyez(int ujx, int ujy) {
            JatekElem[] ujHely = ter.MegadottHelyenLevok(ujx, ujy);
            foreach (JatekElem je in ujHely) {
                if (!Aktiv)
                    break;
                je.Utkozes(this);
            }
            foreach (JatekElem je in ujHely) {
                if (!Aktiv)
                    break;
                this.Utkozes(je);
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
            }
        }
    }
    class JatekTer : IMegjelenitheto {
        const int MAX_ELEMSZAM = 1000;
        int elemN;
        JatekElem[] elemek = new JatekElem[MAX_ELEMSZAM];
        int meretX;
        public int MeretX {
            get { return meretX; }
        }
        int meretY;
        public int MeretY {
            get { return meretY; }
        }
        public int[] MegjelenitendoMeret { get { return new int[] { meretX, meretY }; } }
        public JatekTer(int meretX, int meretY) {
            this.meretX = meretX;
            this.meretY = meretY;
        }
        public void Felvesz(JatekElem uj) {
            elemek[elemN++] = uj;
        }
        public void Torol(JatekElem torolni) {
            for (int i = 0; i < elemN; i++) {
                if (torolni == elemek[i]) {
                    elemek[i] = elemek[--elemN];
                    elemek[elemN] = null;
                }
            }
        }
        public JatekElem[] MegadottHelyenLevok(int x, int y, int tavolsag) {
            int db = 0;
            JatekElem[] tmp = new JatekElem[elemN];
            for (int i = 0; i < elemN; i++)
                if (Math.Sqrt(Math.Pow(x - elemek[i].X, 2) + Math.Pow(y - elemek[i].Y, 2)) <= tavolsag)
                    tmp[db++] = elemek[i];
            JatekElem[] vissza = new JatekElem[db];
            for (int i = 0; i < db; i++)
                vissza[i] = tmp[i];
            return vissza;
        }
        public JatekElem[] MegadottHelyenLevok(int x, int y) {
            return MegadottHelyenLevok(x, y, 0);
        }
        public IKirajzolhato[] MegjelenitedoElemek() {
            int db = 0;
            foreach (JatekElem k in elemek)
                if (k is IKirajzolhato)
                    db++;
            IKirajzolhato[] vissza = new IKirajzolhato[db];
            int i = 0;
            foreach (JatekElem k in elemek)
                if (k is IKirajzolhato)
                    vissza[i++] = k as IKirajzolhato;
            return vissza;
        }
    }
}
