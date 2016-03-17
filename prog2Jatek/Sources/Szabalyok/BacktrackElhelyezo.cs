using OE.Prog2.Jatek.Jatekter;
using System;

namespace OE.Prog2.Jatek.Szabalyok {
    class BacktrackElhelyezo {
        JatekTer ter;
        JatekElem[] elemek;
        int[,] uresPoziciok;
        public BacktrackElhelyezo(JatekTer ter) {
            this.ter = ter;
            int db = (ter.MeretX - 2) * (ter.MeretY - 2);
            uresPoziciok = new int[db, 2];
            int k = 0;
            for (int i = 1; i < ter.MeretX - 1; i++)
                for (int j = 1; j < ter.MeretY - 1; j++) {
                    if (ter.MegadottHelyenLevok(i, j).Length == 0) {
                        uresPoziciok[k, 0] = i;
                        uresPoziciok[k, 1] = j;
                        k++;
                    }
                }
        }
        bool Ft(int szint, int hely) {
            if (elemek[szint] is Jatekos)
                if ((uresPoziciok[hely, 0] == 1 || uresPoziciok[hely, 0] == (ter.MeretX - 2)) && uresPoziciok[hely, 1] > 0 && uresPoziciok[hely, 1] < (ter.MeretY - 1))
                    return true;
                else if ((uresPoziciok[hely, 1] == 1 || uresPoziciok[hely, 1] == (ter.MeretY - 2)) && uresPoziciok[hely, 0] > 0 && uresPoziciok[hely, 0] < (ter.MeretX - 1))
                    return true;
            return (uresPoziciok[hely, 0] > 1 && uresPoziciok[hely, 0] < (ter.MeretX - 2) && uresPoziciok[hely, 1] > 1 && uresPoziciok[hely, 1] < (ter.MeretY - 2));

        }
        bool Fk(int szint, int hely, int k, int khely) {
            if (elemek[szint] is Jatekos && elemek[k] is Jatekos) {
                int xt = Math.Abs(uresPoziciok[hely, 0] - uresPoziciok[khely, 0]);
                int yt = Math.Abs(uresPoziciok[hely, 1] - uresPoziciok[khely, 1]);
                double tav = Math.Sqrt(xt * xt + yt * yt);
                bool xEgyenlo = uresPoziciok[hely, 0] == uresPoziciok[khely, 1];
                bool yEgyenlo = uresPoziciok[hely, 1] == uresPoziciok[khely, 1];
                bool rosszTav = tav <= 5;
                if (rosszTav)
                    return false;
                if (xEgyenlo || yEgyenlo)
                    return false;
                return true;
            }
            else {
                int xt = Math.Abs(uresPoziciok[hely, 0] - uresPoziciok[khely, 0]);
                int yt = Math.Abs(uresPoziciok[hely, 1] - uresPoziciok[khely, 1]);
                double tav = Math.Sqrt(xt * xt + yt * yt);
                return tav > 2;
            }
        }
        void Backtrack(int szint, int[] E, ref bool van) {
            int i = 0;
            if (szint < E.Length)
                while (!van && i < uresPoziciok.GetLength(0)) {
                    if (Ft(szint, i)) {
                        int k = 0;
                        while (k < szint && Fk(szint, i, k, E[k]))
                            k++;
                        if (k == szint) {
                            E[szint] = i;
                            if (szint == elemek.Length)
                                van = true;
                            else
                                Backtrack(szint + 1, E, ref van);
                        }
                    }
                    i++;
                }
            else
                van = true;
        }
        public void Elhelyezes(JatekElem[] elemek) {
            this.elemek = elemek;
            int[] E = new int[elemek.Length];
            bool van = false;
            Backtrack(0, E, ref van);
            if (van)
                for (int i = 0; i < elemek.Length; i++) {
                    elemek[i].X = uresPoziciok[E[i], 0];
                    elemek[i].Y = uresPoziciok[E[i], 1];
                }
            else
                throw new BackTrackNincsMegoldasException();
        }
    }
    class BackTrackNincsMegoldasException : Exception { }
}
