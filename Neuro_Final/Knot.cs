using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Final
{
    class Knot
    {
        public void hier()
        {
            Console.WriteLine();
        }

        public double value;       // 
        public double value_net;   // Vor Aktivierungsfunktion
        public double value_out;   // Nach Aktivierungsfunktion
        public double bias;
        public double bias2;

        double weight;
    }
}
