using System;
using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Szabalyok;
using OE.Prog2.Jatek.Megjelenites;
using OE.Prog2.Jatek.Automatizmus;

namespace OE.Prog2.Jatek.Keret {
    class Keret {
        const int PALYA_MERET_X = 21;
        const int PALYA_MERET_Y = 11;
        const int KINCSEK_SZAMA = 10;
        JatekTer ter;
        OrajelGenerator generator;
        int megtalaltKincsek = 0;
        void PalyaGeneralas() {
            Random R = new Random();
            for (int i = 0; i < PALYA_MERET_X; i++) {
                ter.Felvesz(new Fal(i, 0, ter));
                ter.Felvesz(new Fal(i, PALYA_MERET_Y - 1, ter));
            }
            for (int i = 1; i < PALYA_MERET_Y - 1; i++) {
                ter.Felvesz(new Fal(0, i, ter));
                ter.Felvesz(new Fal(PALYA_MERET_X - 1, i, ter));
            }
            for (int i = 0; i < KINCSEK_SZAMA; i++) {
                int ujx, ujy;
                bool nemUres = true;
                bool jatekos = true;
                do {
                    ujx = R.Next(1, PALYA_MERET_X - 1);
                    ujy = R.Next(1, PALYA_MERET_Y - 1);
                    nemUres = (ter.MegadottHelyenLevok(ujx, ujy)).Length != 0;
                    jatekos = ujx == 1 && ujy == 1;
                } while (nemUres || jatekos);
                Kincs k = new Kincs(ujx, ujy, ter);
                k.KincsFelvetel += KincsFelvetelTortent;
                ter.Felvesz(k);
            }
        }
        public Keret() {
            ter = new JatekTer(PALYA_MERET_X, PALYA_MERET_Y);
            generator = new OrajelGenerator();
            PalyaGeneralas();
        }
        bool jatekVege = false;
        public void Futtatas() {
            Jatekos jatekos = new Jatekos("Bela", 1, 1, ter);
            jatekos.JatekosValtozas += JatekosValtozasTortent;
            GepiJatekos gJatekos = new GepiJatekos("Kati", 1, 2, ter);
            GonoszGepiJatekos ggJatekos = new GonoszGepiJatekos("Laci", PALYA_MERET_X / 2, PALYA_MERET_Y / 2, ter);
            KonzolosMegjelenito km = new KonzolosMegjelenito(0, 0, ter);
            KonzolosMegjelenito plM = new KonzolosMegjelenito(25, 0, jatekos);
            KonzolosEredmenyAblak kea = new KonzolosEredmenyAblak(0, 12, 5);
            kea.JatekosFeliratkozas(jatekos);
            generator.Felvetel(gJatekos);
            generator.Felvetel(ggJatekos);
            generator.Felvetel(km);
            generator.Felvetel(plM);
            do {
                ConsoleKeyInfo key = Console.ReadKey(true);
                try {
                    if (key.Key == ConsoleKey.LeftArrow) jatekos.Megy(-1, 0);
                    if (key.Key == ConsoleKey.RightArrow) jatekos.Megy(1, 0);
                    if (key.Key == ConsoleKey.UpArrow) jatekos.Megy(0, -1);
                    if (key.Key == ConsoleKey.DownArrow) jatekos.Megy(0, 1);
                }
                catch (MozgasHelyHianyMiattNemSikerultKivetel k) {
                    Console.Beep(500 + k.Elemek.Length * 100, 10);
                }
                if (key.Key == ConsoleKey.Escape) jatekVege = true;
            } while (!jatekVege);
        }
        void KincsFelvetelTortent(Kincs kincs, Jatekos jatekos) {
            megtalaltKincsek++;
            if (megtalaltKincsek == KINCSEK_SZAMA)
                jatekVege = true;
        }
        void JatekosValtozasTortent(Jatekos Jatekos, int ujpont, int ujelet) {
            if (ujelet == 0)
                jatekVege = true;
        }
    }
}
