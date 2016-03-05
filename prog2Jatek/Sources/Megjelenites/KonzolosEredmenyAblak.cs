using OE.Prog2.Jatek.Szabalyok;

namespace OE.Prog2.Jatek.Megjelenites {
    class KonzolosEredmenyAblak {
        int pozX, pozY;
        int maxSorSzam;
        int sor;
        public KonzolosEredmenyAblak(int pozX, int pozY, int maxSorSzam) {
            this.pozX = pozX;
            this.pozY = pozY;
            this.maxSorSzam = maxSorSzam;
        }
        void JatekosValtozasTortent(Jatekos jatekos, int ujpont, int ujelet) {
            SzalbiztosKonzol.KiirasXY(pozX, pozY + sor, string.Format("játékos neve: {0}, pontszáma: {1}, életereje: {2}                ", jatekos.Nev, ujpont, ujelet));
            sor = sor + 1 >= maxSorSzam ? 0 : sor + 1;
        }
        public void JatekosFeliratkozas(Jatekos jatekos) {
            jatekos.JatekosValtozas += JatekosValtozasTortent;
        }
    }
}
