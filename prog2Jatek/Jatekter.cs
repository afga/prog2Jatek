using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.Prog2.Jatek.Jatekter {
    class JatekElem {
        int x, y;
        public int X {
            get { return x; }
            set { x = value; }
        }
        public int Y {
            get { return y; }
            set { y = value; }
        }
        JatekTer ter;
        public JatekElem(int x, int y, JatekTer ter) {
            this.x = x;
            this.y = y;
            this.ter = ter;
        }
    }
    class JatekTer {
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
                if (Math.Sqrt(elemek[i].X * elemek[i].X + elemek[i].Y * elemek[i].Y) <= tavolsag)
                    tmp[db++] = elemek[i];
            JatekElem[] vissza = new JatekElem[db];
            for (int i = 0; i < db; i++)
                vissza[i] = tmp[i];
            return vissza;
        }
        public JatekElem[] MegadottHelyenLevok(int x, int y) {
            return MegadottHelyenLevok(x, y, 0);
        }
    }
}
