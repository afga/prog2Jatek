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
            int falN = 1;
            Fal[] falak = new Fal[PALYA_MERET_X * PALYA_MERET_Y];
            bool[] kesz = new bool[falak.Length];
            falak[0] = new Fal(2, 2, ter);
            ter.Felvesz(falak[0]);
            Fal aktualis = falak[0];
            bool vege = false;
            while (!vege) {
                int dir = R.Next(0, 4);
                bool siker = false;
                bool fail = false;
                int failC = 0;
                while (!siker && !fail) {
                    bool flag = false;
                    switch (dir) {
                        case 0:
                            for (int i = 0; i < falN; i++)
                                if (aktualis.Y - 2 < 2 || (falak[i].Y == aktualis.Y - 2 && falak[i].X == aktualis.X))
                                    flag = true;
                            if (!flag) {
                                falak[falN++] = new Fal(aktualis.X, aktualis.Y - 2, ter);
                                ter.Felvesz(falak[falN - 1]);
                                Fal tmp = new Fal(aktualis.X, aktualis.Y - 1, ter);
                                ter.Felvesz(tmp);
                                int kov = R.Next(0, falN - 1);
                                while (kesz[kov])
                                    kov = R.Next(0, falN - 1);
                                aktualis = falak[kov];
                                siker = true;
                            }
                            else {
                                failC++;
                                if (failC == 4) {
                                    fail = true;
                                    int ix = 0;
                                    while (falak[ix] != aktualis) ix++;
                                    if (falak[ix + 1] != null)
                                        aktualis = falak[ix + 1];
                                    else
                                        vege = true;
                                    kesz[ix] = true;
                                }
                                else {
                                    dir++;
                                    dir %= 4;
                                }
                            }
                            break;
                        case 1:
                            for (int i = 0; i < falN; i++)
                                if (aktualis.Y + 2 >= PALYA_MERET_Y - 2 || (falak[i].Y == aktualis.Y + 2 && falak[i].X == aktualis.X))
                                    flag = true;
                            if (!flag) {
                                falak[falN++] = new Fal(aktualis.X, aktualis.Y + 2, ter);
                                ter.Felvesz(falak[falN - 1]);
                                Fal tmp = new Fal(aktualis.X, aktualis.Y + 1, ter);
                                ter.Felvesz(tmp);
                                int kov = R.Next(0, falN - 1);
                                while (kesz[kov])
                                    kov = R.Next(0, falN - 1);
                                aktualis = falak[kov];
                                siker = true;
                            }
                            else {
                                failC++;
                                if (failC == 4) {
                                    fail = true;
                                    int ix = 0;
                                    while (falak[ix] != aktualis) ix++;
                                    if (falak[ix + 1] != null)
                                        aktualis = falak[ix + 1];
                                    else
                                        vege = true;
                                    kesz[ix] = true;
                                }
                                else {
                                    dir++;
                                    dir %= 4;
                                }
                            }
                            break;
                        case 2:
                            for (int i = 0; i < falN; i++)
                                if (aktualis.X + 2 >= PALYA_MERET_X - 2 || (falak[i].X == aktualis.X + 2 && falak[i].Y == aktualis.Y))
                                    flag = true;
                            if (!flag) {
                                falak[falN++] = new Fal(aktualis.X + 2, aktualis.Y, ter);
                                ter.Felvesz(falak[falN - 1]);
                                Fal tmp = new Fal(aktualis.X + 1, aktualis.Y, ter);
                                ter.Felvesz(tmp);
                                int kov = R.Next(0, falN - 1);
                                while (kesz[kov])
                                    kov = R.Next(0, falN - 1);
                                aktualis = falak[kov];
                                siker = true;
                            }
                            else {
                                failC++;
                                if (failC == 4) {
                                    fail = true;
                                    int ix = 0;
                                    while (falak[ix] != aktualis) ix++;
                                    if (falak[ix + 1] != null)
                                        aktualis = falak[ix + 1];
                                    else
                                        vege = true;
                                    kesz[ix] = true;
                                }
                                else {
                                    dir++;
                                    dir %= 4;
                                }
                            }
                            break;
                        case 3:
                            for (int i = 0; i < falN; i++)
                                if (aktualis.X - 2 < 2 || (falak[i].X == aktualis.X - 2 && falak[i].Y == aktualis.Y))
                                    flag = true;
                            if (!flag) {
                                falak[falN++] = new Fal(aktualis.X - 2, aktualis.Y, ter);
                                ter.Felvesz(falak[falN - 1]);
                                Fal tmp = new Fal(aktualis.X - 1, aktualis.Y, ter);
                                ter.Felvesz(tmp);
                                int kov = R.Next(0, falN - 1);
                                while (kesz[kov])
                                    kov = R.Next(0, falN - 1);
                                aktualis = falak[kov];
                                siker = true;
                            }
                            else {
                                failC++;
                                if (failC == 4) {
                                    fail = true;
                                    int ix = 0;
                                    while (falak[ix] != aktualis) ix++;
                                    if (falak[ix + 1] != null)
                                        aktualis = falak[ix + 1];
                                    else
                                        vege = true;
                                    kesz[ix] = true;
                                }
                                else {
                                    dir++;
                                    dir %= 4;
                                }
                            }
                            break;
                    }
                }
                int idx = 0;
                while (kesz[idx]) idx++;
                if (idx == falN) vege = true;
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
            for (int i = 4; i < elemek.Length; i++) {
                elemek[i] = new Kincs(-1, -1, ter);
                ter.Felvesz(elemek[i]);
                (elemek[i] as Kincs).KincsFelvetel += KincsFelvetelTortent;
            }
            BacktrackElhelyezo bte = new BacktrackElhelyezo(ter);
            bool siker = false;
            while (!siker) {
                try {
                    bte.Elhelyezes(elemek);
                    siker = true;
                }
                catch (BackTrackNincsMegoldasException) {
                    siker = false;
                    int tx = R.Next(2, PALYA_MERET_X - 2);
                    int ty = R.Next(2, PALYA_MERET_Y - 2);
                    while (!(ter.MegadottHelyenLevok(tx, ty)[0] is Fal)) {
                        tx = R.Next(2, PALYA_MERET_X - 2);
                        ty = R.Next(2, PALYA_MERET_Y - 2);
                    }
                    Fal tor = ter.MegadottHelyenLevok(tx, ty)[0] as Fal;
                }
            }
            for (int i = 0; i <= ELLENFELEK_SZAMA; i++)
                (elemek[i] as Jatekos).RogzitesInditas(generator);
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
                jatekVege = jatekVege || key.Key == ConsoleKey.Escape;
            } while (!jatekVege);
            for (int i = 1; i <= ELLENFELEK_SZAMA; i++)
                generator.Levetel((elemek[i] as GepiJatekos));
            for (int i = 0; i <= ELLENFELEK_SZAMA; i++)
                (elemek[i] as Jatekos).VisszajatszasInditas();
        }
        void KincsFelvetelTortent(Kincs kincs, Jatekos jatekos) {
            megtalaltKincsek++;
            jatekVege = jatekVege || megtalaltKincsek == KINCSEK_SZAMA;
        }
        void JatekosValtozasTortent(Jatekos jatekos, int ujpont, int ujelet) {
            jatekVege = jatekVege || ujelet == 0;
        }
    }
}
