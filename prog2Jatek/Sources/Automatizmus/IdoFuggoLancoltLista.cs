namespace OE.Prog2.Jatek.Automatizmus {
    class IdoFuggoLancoltLista<T> : IAutomatikusanMukodo where T : IVisszajatszhato {
        class ListaElem {
            public int ido;
            public T tartalom;
            public ListaElem kovetkezo;
        }
        enum UzemmodTipus {
            Varakozas,
            Rogzites,
            Visszajatszas
        }
        UzemmodTipus uzemmod = UzemmodTipus.Varakozas;
        int ido;
        ListaElem fej;
        ListaElem utolso;
        ListaElem aktualis;
        public int MukodesIntervallum { get { return 1; } }
        public void RogzitesInditas() {
            uzemmod = UzemmodTipus.Rogzites;
            ido = 0;
            fej = null;
            utolso = null;
            aktualis = null;
        }
        public void Rogzites(T uj) {
            if (uzemmod == UzemmodTipus.Rogzites) {
                if (fej != null) {
                    utolso.kovetkezo = new ListaElem();
                    utolso = utolso.kovetkezo;
                    utolso.ido = ido;
                    utolso.tartalom = uj;
                }
                else {
                    fej = new ListaElem();
                    fej.ido = ido;
                    fej.tartalom = uj;
                    utolso = fej;
                }
            }
        }
        public void IdozitettBejaras() {
            aktualis = fej;
            ido = 0;
            uzemmod = UzemmodTipus.Visszajatszas;
        }
        public void Mukodik() {
            ido++;
            if (uzemmod == UzemmodTipus.Visszajatszas) {
                while (aktualis.ido <= ido) {
                    aktualis.tartalom.Vegrehajt();
                    aktualis = aktualis.kovetkezo;
                    if (aktualis == null) {
                        uzemmod = UzemmodTipus.Varakozas;
                        break;
                    }
                }
            }
        }
    }
}
