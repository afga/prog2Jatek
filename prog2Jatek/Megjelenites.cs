using System;
using OE.Prog2.Jatek.Automatizmus;
using OE.Prog2.Jatek.Szabalyok;

namespace OE.Prog2.Jatek.Megjelenites {
    public interface IKirajzolhato {
        int X { get; }
        int Y { get; }
        char Alak { get; }
    }
    public interface IMegjelenitheto {
        int[] MegjelenitendoMeret { get; }
        IKirajzolhato[] MegjelenitedoElemek();
    }
    class KonzolosMegjelenito : IAutomatikusanMukodo {
        IMegjelenitheto forras;
        int pozX;
        int pozY;
        public KonzolosMegjelenito(int pozX, int pozY, IMegjelenitheto forras) {
            this.pozX = pozX;
            this.pozY = pozY;
            this.forras = forras;
        }
        public void Megjelenites() {
            int[] meret = forras.MegjelenitendoMeret;
            IKirajzolhato[] kirajzolni = forras.MegjelenitedoElemek();
            for (int i = 0; i < meret[0]; i++) {
                for (int j = 0; j < meret[1]; j++) {
                    bool flag = false;
                    foreach (IKirajzolhato k in kirajzolni)
                        if (k.X == i && k.Y == j) {
                            SzalbiztosKonzol.KiirasXY(pozX + i, pozY + j, k.Alak);
                            flag = true;
                            break;
                        }
                    if (!flag)
                        SzalbiztosKonzol.KiirasXY(pozX + i, pozY + j, ' ');
                }
            }
        }
        public void Mukodik() {
            Megjelenites();
        }
        public int MukodesIntervallum { get { return 1; } }
    }
    class KonzolosEredmenyAblak {
        int pozX, pozY;
        int maxSorSzam;
        int sor = 0;
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
