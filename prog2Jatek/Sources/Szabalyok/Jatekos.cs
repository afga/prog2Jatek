using OE.Prog2.Jatek.Automatizmus;
using OE.Prog2.Jatek.Jatekter;
using OE.Prog2.Jatek.Megjelenites;

namespace OE.Prog2.Jatek.Szabalyok {
    class Jatekos : MozgoJatekElem, IKirajzolhato, IMegjelenitheto {
        string nev;
        IdoFuggoLancoltLista<MozgasAdatok> felvevo;
        protected MemoriaFa memoria = new MemoriaFa();
        public JatekosValtozasKezelo JatekosValtozas;
        public string Nev { get { return nev; } }
        public override double Meret { get { return 0.2; } }
        public virtual char Alak { get { return Aktiv ? '\u263A' : '\u263B'; } }
        public int[] MegjelenitendoMeret { get { return ter.MegjelenitendoMeret; } }
        public Jatekos(string nev, int x, int y, JatekTer ter)
            : base(x, y, ter) {
            this.nev = nev;
        }
        public override void Utkozes(JatekElem elem) { }
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
            felvevo.Rogzites(new MozgasAdatok(this, X, Y));
            JatekElem[] kozel = ter.MegadottHelyenLevok(X, Y, 5);
            for (int i = 0; i < kozel.Length; i++)
                if (kozel[i] is IMemoriabanTarolhato)
                    memoria.Beszuras(kozel[i] as IMemoriabanTarolhato);
        }
        public IKirajzolhato[] MegjelenitedoElemek() {
            JatekElem[] kozel = ter.MegadottHelyenLevok(X, Y, 5);
            IMemoriabanTarolhato[] jatekosMemoria = memoria.Bejaras();
            int db = 0;
            foreach (JatekElem k in kozel)
                if (k is IKirajzolhato)
                    db++;
            foreach (IMemoriabanTarolhato k in jatekosMemoria)
                if (k is IKirajzolhato)
                    db++;
            IKirajzolhato[] vissza = new IKirajzolhato[db];
            int i = 0;
            foreach (JatekElem k in kozel)
                if (k is IKirajzolhato)
                    vissza[i++] = k as IKirajzolhato;
            foreach (IMemoriabanTarolhato k in jatekosMemoria)
                if (k is IKirajzolhato)
                    vissza[i++] = k as IKirajzolhato;
            return vissza;
        }
        public void RogzitesInditas(OrajelGenerator gen) {
            felvevo = new IdoFuggoLancoltLista<MozgasAdatok>();
            felvevo.RogzitesInditas();
            felvevo.Rogzites(new MozgasAdatok(this, X, Y));
            gen.Felvetel(felvevo);
        }
        public void VisszajatszasInditas() {
            eletero = 100;
            Aktiv = true;
            felvevo.IdozitettBejaras();
        }
    }
    delegate void JatekosValtozasKezelo(Jatekos jatekos, int ujpont, int ujelet);
}
