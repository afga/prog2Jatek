using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Megjelenites;

namespace OE.Prog2.Jatek.Szabalyok {
    class Fal : RogzitettJatekElem, IKirajzolhato, IMemoriabanTarolhato {
        public Fal(int x, int y, JatekTer ter)
            : base(x, y, ter) {
        }
        public override double Meret { get { return 1.0; } }
        public override void Utkozes(JatekElem elem) {
        }
        public char Alak { get { return '\u2593'; } }
    }
    class Kincs : RogzitettJatekElem, IKirajzolhato, IMemoriabanTarolhatoEsMegszuno {
        public event KincsFelvetelKezelo KincsFelvetel;
        public event MegszunesKezelo Megszunes;
        public Kincs(int x, int y, JatekTer ter)
            : base(x, y, ter) {
        }
        public override double Meret { get { return 1.0; } }
        public char Alak { get { return '\u2666'; } }
        public override void Utkozes(JatekElem elem) {
            if (elem is Jatekos) {
                (elem as Jatekos).PontotSzerez(50);
                ter.Torol(this);
                if (KincsFelvetel != null)
                    KincsFelvetel(this, elem as Jatekos);
                if (Megszunes != null)
                    Megszunes(this as IMemoriabanTarolhato);
            }
        }
    }
    delegate void KincsFelvetelKezelo(Kincs kincs, Jatekos jatekos);
}
