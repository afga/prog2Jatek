namespace OE.Prog2.Jatek.Szabalyok {
    interface IMemoriabanTarolhatoEsMegszuno {
        event MegszunesKezelo Megszunes;
    }
    delegate void MegszunesKezelo(IMemoriabanTarolhato megsz);
}
