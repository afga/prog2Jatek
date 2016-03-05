namespace OE.Prog2.Jatek.Megjelenites {
    public interface IKirajzolhato {
        int X { get; }
        int Y { get; }
        char Alak { get; }
    }
    public interface IMegjelenitheto {
        int[] MegjelenitendoMeret { get; }
        IKirajzolhato[] MegjelenitedoElemek();
    }
}
