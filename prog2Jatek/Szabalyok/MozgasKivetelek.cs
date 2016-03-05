using OE.Prog2.Jatek.Jatekter;
using System;

namespace OE.Prog2.Jatek.Szabalyok {
    class MozgasNemSikerult : Exception {
        JatekElem jatekElem;
        public JatekElem JatekElem { get { return JatekElem; } }
        int x, y;
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public MozgasNemSikerult(JatekElem jatekElem, int x, int y) {
            this.jatekElem = jatekElem;
            this.x = x;
            this.y = y;
        }
    }
    class MozgasHalalMiattNemSikerult : MozgasNemSikerult {
        public MozgasHalalMiattNemSikerult(JatekElem jatekElem, int x, int y)
            : base(jatekElem, x, y) {
        }
    }
    class MozgasHelyHianyMiattNemSikerultKivetel : MozgasNemSikerult {
        JatekElem[] elemek;
        public JatekElem[] Elemek { get { return elemek; } }
        public MozgasHelyHianyMiattNemSikerultKivetel(JatekElem[] elemek, JatekElem jatekElem, int x, int y)
            : base(jatekElem, x, y) {
            this.elemek = elemek;
        }
    }
}
