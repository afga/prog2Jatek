using System;
using OE.Prog2.Jatek.Jatekter;

public class MainClass {
    public static void Main(string[] args) {
        Random rnd = new Random();
        JatekTer jatekterem = new JatekTer(10, 10);
        JatekElem[] elemek = new JatekElem[3];
        for (int i = 0; i < 3; i++)
            elemek[i] = new JatekElem(rnd.Next(0, 10), rnd.Next(0, 10), jatekterem);
        foreach (JatekElem je in elemek)
            jatekterem.Felvesz(je);
        jatekterem.Torol(elemek[1]);
    }
}
