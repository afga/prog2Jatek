using System;
using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Megjelenites;
using OE.Prog2.Jatek.Automatizmus;

namespace OE.Prog2.Jatek.Szabalyok {
    class Fal : RogzitettJatekElem, IKirajzolhato {
        public Fal(int x, int y, JatekTer ter)
            : base(x, y, ter) {
        }
        public override double Meret { get { return 1.0; } }
        public override void Utkozes(JatekElem elem) {
        }
        public char Alak { get { return '\u2593'; } }
    }

    class Jatekos : MozgoJatekElem, IKirajzolhato, IMegjelenitheto {
        string nev;
        public JatekosValtozasKezelo JatekosValtozas;
        public string Nev { get { return nev; } }
        public Jatekos(string nev, int x, int y, JatekTer ter)
            : base(x, y, ter) {
            this.nev = nev;
        }
        public override double Meret { get { return 0.2; } }
        public virtual char Alak { get { return Aktiv ? '\u263A' : '\u263B'; } }
        public int[] MegjelenitendoMeret { get { return ter.MegjelenitendoMeret; } }
        public override void Utkozes(JatekElem elem) {
        }
        int eletero = 100;
        public void Serul(int hp) {
            if (eletero != 0) {
                eletero = eletero - hp < 0 ? 0 : eletero - hp;
                if (JatekosValtozas != null)
                    JatekosValtozas(this, pontszam, eletero);
                Aktiv = eletero != 0;
            }
        }
        int pontszam;
        public void PontotSzerez(int pont) {
            pontszam += pont;
            if (JatekosValtozas != null)
                JatekosValtozas(this, pontszam, eletero);
        }
        public void Megy(int rx, int ry) {
            int ujx = X + rx;
            int ujy = Y + ry;
            AtHelyez(ujx, ujy);
        }
        public IKirajzolhato[] MegjelenitedoElemek() {
            JatekElem[] kozel = ter.MegadottHelyenLevok(X, Y, 5);
            int db = 0;
            foreach (JatekElem k in kozel)
                if (k is IKirajzolhato)
                    db++;
            IKirajzolhato[] vissza = new IKirajzolhato[db];
            int i = 0;
            foreach (JatekElem k in kozel)
                if (k is IKirajzolhato)
                    vissza[i++] = k as IKirajzolhato;
            return vissza;
        }
    }

    class GepiJatekos : Jatekos, IAutomatikusanMukodo {
        static Random rnd = new Random();
        public GepiJatekos(string nev, int x, int y, JatekTer ter)
            : base(nev, x, y, ter) {
        }
        public void Mozgas() {
            bool sikerult = false;
            int iter = 1;
            int x = rnd.Next(0, 4);
            while (!sikerult) {
                try {
                    switch (x) {
                        case 0:
                            Megy(0, -1);
                            sikerult = true;
                            break;
                        case 1:
                            Megy(1, 0);
                            sikerult = true;
                            break;
                        case 2:
                            Megy(0, 1);
                            sikerult = true;
                            break;
                        case 3:
                            Megy(-1, 0);
                            sikerult = true;
                            break;
                    }
                }
                catch (MozgasHelyHianyMiattNemSikerultKivetel k) {
                    if (iter < 4) {
                        iter++;
                        x++;
                        x %= 4;
                    }
                    else
                        sikerult = true;
                }
            }
        }
        public override char Alak { get { return '\u2640'; } }
        public void Mukodik() {
            Mozgas();
        }
        public int MukodesIntervallum { get { return 2; } }
    }

    class GonoszGepiJatekos : GepiJatekos {
        public GonoszGepiJatekos(string nev, int x, int y, JatekTer ter)
            : base(nev, x, y, ter) {
        }
        public override char Alak { get { return '\u2642'; } }
        public override void Utkozes(JatekElem elem) {
            base.Utkozes(elem);
            if (Aktiv && elem is Jatekos)
                (elem as Jatekos).Serul(10);
        }
    }

    class Kincs : RogzitettJatekElem, IKirajzolhato {
        public event KincsFelvetelKezelo KincsFelvetel;
        public Kincs(int x, int y, JatekTer ter)
            : base(x, y, ter) {
        }
        public override double Meret { get { return 1.0; } }
        public char Alak { get { return '\u2666'; } }
        public override void Utkozes(JatekElem elem) {
            if (elem is Jatekos) {
                (elem as Jatekos).PontotSzerez(50);
                ter.Torol(this);
                if (KincsFelvetel != null) {
                    KincsFelvetel(this, elem as Jatekos);
                }
            }
        }
    }
    delegate void KincsFelvetelKezelo(Kincs kincs, Jatekos jatekos);
    delegate void JatekosValtozasKezelo(Jatekos jatekos, int ujpont, int ujelet);
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

    public class BacktrackElhelyezo {
        JatekTer ter;
        JatekElem[] elemek;
        int[,] uresPoziciok;
        public BacktrackElhelyezo(JatekTer ter) {
            this.ter = ter;
            int db = (ter.MeretX * ter.MeretY) - ter.ElemN;
            uresPoziciok = new int[db, 2];
            int k = 0;
            for (int i = 0; i < ter.MeretX; i++)
                for (int j = 0; j < ter.MeretY; j++)
                    if (ter.MegadottHelyenLevok(i, j).Length == 0) {
                        uresPoziciok[k, 0] = i;
                        uresPoziciok[k++, 1] = j;
                    }
        }
        bool Ft(int szint, int hely) {
            if (elemek[szint] is Jatekos)
                return (uresPoziciok[hely, 0] > 0 && uresPoziciok[hely, 0] < (ter.MeretX - 1) && uresPoziciok[hely, 1] > 0 && uresPoziciok[hely, 1] < (ter.MeretY - 1));
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
                return tav <= 2;
            }
        }
        void Backtrack(int szint, int[] E, ref bool van) {
            int i = 0;
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
            }
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
