using OE.Prog2.Jatek.Automatizmus;
using OE.Prog2.Jatek.Jatekter;

namespace OE.Prog2.Jatek.Szabalyok {
    class MozgasAdatok : IVisszajatszhato {
        MozgoJatekElem elem;
        int x, y;
        public MozgasAdatok(MozgoJatekElem elem, int x, int y) {
            this.elem = elem;
            this.x = x;
            this.y = y;
        }
        public void Vegrehajt() {
            elem.AtHelyez(x, y);
        }
    }
}
