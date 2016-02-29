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
        const int ELLENFELEK_SZAMA = 3;
        JatekTer ter;
        OrajelGenerator generator;
        int megtalaltKincsek;
        Random R = new Random();
        void PalyaGeneralas() {
            for (int i = 0; i < PALYA_MERET_X; i++) {
                ter.Felvesz(new Fal(i, 0, ter));
                ter.Felvesz(new Fal(i, PALYA_MERET_Y - 1, ter));
            }
            for (int i = 1; i < PALYA_MERET_Y - 1; i++) {
                ter.Felvesz(new Fal(0, i, ter));
                ter.Felvesz(new Fal(PALYA_MERET_X - 1, i, ter));
            }
        }
        public Keret() {
            ter = new JatekTer(PALYA_MERET_X, PALYA_MERET_Y);
            generator = new OrajelGenerator();
            PalyaGeneralas();
        }
        bool jatekVege;
        public void Futtatas() {
            JatekElem[] elemek = new JatekElem[1 + ELLENFELEK_SZAMA + KINCSEK_SZAMA];
            Jatekos jatekos = new Jatekos("Bela", -1, -1, ter);
            elemek[0] = jatekos;
            for (int i = 1; i < ELLENFELEK_SZAMA + 1; i++) {
                if (R.Next(1, 101) > 50)
                    elemek[i] = new GonoszGepiJatekos(String.Format("Gepi{0}", i), -1, -1, ter);
                else
                    elemek[i] = new GepiJatekos(String.Format("Gepi{0}", i), -1, -1, ter);
                generator.Felvetel(elemek[i] as GepiJatekos);
            }
            for (int i = 4; i < elemek.Length; i++)
                elemek[i] = new Kincs(-1, -1, ter);
            BacktrackElhelyezo bte = new BacktrackElhelyezo(ter);
            bool siker = false;
            while (!siker) {
                try {
                    bte.Elhelyezes(elemek);
                    siker = true;
                }
                catch (BackTrackNincsMegoldasException e) {
                    siker = false;
                    //Belső fal törlés?
                }
            }
            jatekos.JatekosValtozas += JatekosValtozasTortent;
            KonzolosMegjelenito km = new KonzolosMegjelenito(0, 0, ter);
            KonzolosMegjelenito plM = new KonzolosMegjelenito(25, 0, jatekos);
            KonzolosEredmenyAblak kea = new KonzolosEredmenyAblak(0, 12, 5);
            kea.JatekosFeliratkozas(jatekos);
            generator.Felvetel(km);
            generator.Felvetel(plM);
            do {
                ConsoleKeyInfo key = Console.ReadKey(true);
                try {
                    if (key.Key == ConsoleKey.LeftArrow)
                        jatekos.Megy(-1, 0);
                    if (key.Key == ConsoleKey.RightArrow)
                        jatekos.Megy(1, 0);
                    if (key.Key == ConsoleKey.UpArrow)
                        jatekos.Megy(0, -1);
                    if (key.Key == ConsoleKey.DownArrow)
                        jatekos.Megy(0, 1);
                }
                catch (MozgasHelyHianyMiattNemSikerultKivetel k) {
                    Console.Beep(500 + k.Elemek.Length * 100, 10);
                }
                jatekVege = key.Key == ConsoleKey.Escape;
            } while (!jatekVege);
        }
        void KincsFelvetelTortent(Kincs kincs, Jatekos jatekos) {
            megtalaltKincsek++;
            jatekVege = megtalaltKincsek == KINCSEK_SZAMA;
        }
        void JatekosValtozasTortent(Jatekos jatekos, int ujpont, int ujelet) {
            jatekVege = ujelet == 0;
        }
    }
}
