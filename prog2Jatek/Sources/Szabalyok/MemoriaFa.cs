using System;

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
                if (megvan) {
                    FaElem torlendo = akt;
                    if (szulo.bal == torlendo) {
                        if (torlendo.bal != null)
                            szulo.bal = torlendo.bal;
                        if (torlendo.jobb != null) {
                            if (szulo.bal == torlendo && torlendo.jobb.kulcs < szulo.kulcs)
                                szulo.bal = torlendo.jobb;
                            else {
                                akt = szulo.jobb;
                                bool siker = false;
                                while (!siker) {
                                    if (akt.kulcs > torlendo.jobb.kulcs)
                                        if (akt.bal == null) {
                                            akt.bal = torlendo.jobb;
                                            siker = true;
                                        }
                                        else
                                            akt = akt.bal;
                                    else {
                                        if (akt.jobb == null) {
                                            akt.jobb = torlendo.jobb;
                                            siker = true;
                                        }
                                        else
                                            akt = akt.jobb;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        if (torlendo.jobb != null)
                            szulo.jobb = torlendo.jobb;
                        if (torlendo.bal != null) {
                            if (szulo.jobb == torlendo && torlendo.bal.kulcs > szulo.kulcs)
                                szulo.jobb = torlendo.bal;
                            else {
                                akt = szulo.bal;
                                bool siker = false;
                                while (!siker) {
                                    if (akt.kulcs > torlendo.bal.kulcs)
                                        if (akt.bal == null) {
                                            akt.bal = torlendo.bal;
                                            siker = true;
                                        }
                                        else
                                            akt = akt.bal;
                                    else {
                                        if (akt.jobb == null) {
                                            akt.jobb = torlendo.bal;
                                            siker = true;
                                        }
                                        else
                                            akt = akt.jobb;
                                    }
                                }
                            }
                        }
                    }
                }
                if (nincsBenne)
                    throw new FabolTorlesSikertelen();
            }
        }
        public IMemoriabanTarolhato[] Bejaras() {
            IMemoriabanTarolhato[] res = new IMemoriabanTarolhato[meret];
            int idx = 0;
            ResztBejar(gyoker, ref idx, res);
            return res;
        }
        void ResztBejar(FaElem gyok, ref int idx, IMemoriabanTarolhato[] res) {
            if (gyok != null) {
                res[idx++] = gyok.tartalom;
                ResztBejar(gyok.bal, ref idx, res);
                ResztBejar(gyok.jobb, ref idx, res);
            }
        }
    }
    class FabolTorlesSikertelen : Exception { }
}
