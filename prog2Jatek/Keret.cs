using System;
using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Szabalyok;

namespace OE.Prog2.Jatek.Keret {
    class Keret {
        const int PALYA_MERET_X = 21;
        const int PALYA_MERET_Y = 11;
        const int KINCSEK_SZAMA = 10;
        JatekTer ter;
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
                    nemUres = ter.MegadottHelyenLevok(ujx, ujy) != null;
                    jatekos = ujx == 1 && ujy == 1;
                } while (nemUres || jatekos);
                ter.Felvesz(new Kincs(ujx, ujy, ter));
            }
        }
        public Keret() {
            ter = new JatekTer(PALYA_MERET_X, PALYA_MERET_Y);
            PalyaGeneralas();
        }
        bool jatekVege = false;
        public void Futtatas() {
            Jatekos jatekos = new Jatekos("Bela", 1, 1, ter);
            do {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow) jatekos.Megy(-1, 0);
                if (key.Key == ConsoleKey.RightArrow) jatekos.Megy(1, 0);
                if (key.Key == ConsoleKey.UpArrow) jatekos.Megy(0, -1);
                if (key.Key == ConsoleKey.DownArrow) jatekos.Megy(0, 1);
                if (key.Key == ConsoleKey.Escape) jatekVege = true;
            } while (!jatekVege);
        }
    }
}
