namespace OE.Prog2.Jatek.Szabalyok {
    class MemoriaFa {
        class FaElem {
            public int kulcs;
            public IMemoriabanTarolhato tartalom;
            public FaElem bal, jobb;
        }
        FaElem gyoker;
        int meret;
        public void Beszuras(IMemoriabanTarolhato uj) {
            FaElem tmp = new FaElem();
            tmp.tartalom = uj;
            tmp.kulcs = uj.X * 1000 + uj.Y;
            if (meret > 0) {
                FaElem akt = gyoker;
                bool megvan = false;
                bool marBenneVan = false;
                while (!megvan && !marBenneVan) {
                    if (tmp.kulcs != akt.kulcs)
                        if (tmp.kulcs < akt.kulcs) {
                            if (akt.bal == null)
                                megvan = true;
                            else
                                akt = akt.bal;
                        }
                        else {
                            if (akt.jobb == null)
                                megvan = true;
                            else
                                akt = akt.jobb;
                        }
                    else
                        marBenneVan = true;
                }
                if (!marBenneVan) {
                    if (tmp.kulcs < akt.kulcs)
                        akt.bal = tmp;
                    else
                        akt.jobb = tmp;
                    meret++;
                    if (uj is IMemoriabanTarolhatoEsMegszuno)
                        (uj as IMemoriabanTarolhatoEsMegszuno).Megszunes += Torles;
                }
            }
            else {
                gyoker = tmp;
                if (uj is IMemoriabanTarolhatoEsMegszuno)
                    (uj as IMemoriabanTarolhatoEsMegszuno).Megszunes += Torles;
                meret++;
            }
        }
        public void Torles(IMemoriabanTarolhato torolni) {
            if (meret > 0) {
                int k = torolni.X * 1000 + torolni.Y;
                bool megvan = false;
                bool nincsBenne = false;
                FaElem akt = gyoker;
                FaElem szulo = gyoker;
                while (!megvan && !nincsBenne) {
                    if (akt.kulcs == k)
                        megvan = true;
                    else if (akt.kulcs > k) {
                        if (akt.bal != null) {
                            szulo = akt;
                            akt = akt.bal;
                        }
                        else
                            nincsBenne = true;
                    }
                    else {
                        if (akt.jobb != null) {
                            szulo = akt;
                            akt = akt.jobb;
                        }
                        else
                            nincsBenne = true;
                    }
                }
            }
        }
    }
}
