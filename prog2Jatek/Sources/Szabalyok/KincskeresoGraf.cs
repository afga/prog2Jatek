using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.Prog2.Jatek.Szabalyok {
    class KincskeresoGraf {
        class GrafPont {
            public int x, y;
            public GrafPont elozo;
            public GrafPont(int x, int y, GrafPont elo) {
                this.x = x;
                this.y = y;
                elozo = elo;
            }
        }
        const int LEPES_KORLAT = 1000;
        IMemoriabanTarolhato[] memoria;
        public KincskeresoGraf(IMemoriabanTarolhato[] mem) {
            memoria = mem;
        }
        List<GrafPont> SzabadSzomszed(GrafPont gp) {
            List<GrafPont> l = new List<GrafPont>();
            foreach(var x in memoria) {
                if (x.X == gp.x && (x.Y == gp.y + 1 || x.Y == gp.y - 1) && !(x is Fal))
                    l.Add(new GrafPont(x.X, x.Y, gp));
                if (x.Y == gp.y && (x.X == gp.x + 1 || x.X == gp.x - 1) && !(x is Fal))
                    l.Add(new GrafPont(x.X, x.Y, gp));
            }
            return l;
        }
        bool CelPont(GrafPont gp) {
            foreach(var x in memoria) {
                if (x.X == gp.x && x.Y == gp.y)
                    if (x is Kincs)
                        return true;
                    else
                        return false;
            }
            return false;
        }
        public int[] JavasoltIrany(int x, int y) {

        } 
    }
}
