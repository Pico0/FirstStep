using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Final
{
    class Data
    {
        int inputCount = 2; // +1 für Bias
        int hiddenCount = 10;
        int outputCount = 1;

        List<double[,]> sinI = new List<double[,]>(); 
        int count1 = 0;
        List<double[,]> sinO = new List<double[,]>(); 
        int count2 = 0;

        public double[,] bias = { { 1 } };   // Bias zwischen Input und Hidden       ???

        Knot k = new Knot();
        Network n = new Network();

        Tools t = new Tools();

        Random r = new Random();

        // TEACHING SAMPLE

        public List<double[,]> getXOR_Input()
        {
            List<double[,]> input = new List<double[,]>();

            double[,] input_XOR_1 = { { 0, 0 } };  // 2x2-Matrix
            double[,] input_XOR_2 = { { 1, 0 } };  // 2x2-Matrix
            double[,] input_XOR_3 = { { 0, 1 } };  // 2x2-Matrix
            double[,] input_XOR_4 = { { 1, 1 } };  // 2x2-Matrix

            input.Add(input_XOR_1);
            input.Add(input_XOR_2);
            input.Add(input_XOR_3);
            input.Add(input_XOR_4);

            return input;
        }

        public List<double[,]> getXOR_Output()
        {
            List<double[,]> output = new List<double[,]>();

            double[,] output_XOR_1 = { { 0 } };
            double[,] output_XOR_2 = { { 1 } };
            double[,] output_XOR_3 = { { 1 } };
            double[,] output_XOR_4 = { { 0 } };

            output.Add(output_XOR_1);
            output.Add(output_XOR_2);
            output.Add(output_XOR_3);
            output.Add(output_XOR_4);

            return output;
        }

        public double[,] get_W1()
        {
            double[,] w1 = new double[inputCount, hiddenCount];   // 1 input - 10 hidden Layers        // inputCount +1 für bias

            for (int i = 0; i < w1.GetLength(0); i++)
            {
                for (int j = 0; j < w1.GetLength(1); j++)
                {
                    w1[i, j] = r.NextDouble();
                }
            }

            return w1;
        }

        public double[,] get_W2()
        {
            double[,] w2 = new double[hiddenCount, outputCount];    // 10 hidden Layers - 1 output

            for (int i = 0; i < w2.GetLength(0); i++)
            {
                for (int j = 0; j < w2.GetLength(1); j++)
                {
                    w2[i, j] = r.NextDouble();
                }
            }

            return w2;
        }

        public List<double[,]> getSin_Input()
        {
            count1++;   // Elemente nur einmal zur Liste hinzufügen sonst nur return machen
            if (count1 == 1)
            {
                for (double x = 0; x < 2 * Math.PI; x += 2 * Math.PI / 70)
                {
                    // Input = 1,x  / 1 = Bias X = Sinus Input 
                    double[,] I = { { 1, x/6.238} }; sinI.Add(I);    //    / 6.238 damit alle Inputs zwischen -1 und 1 sind und nicht alle Werte zu hoch werden                                                                 
                }
            }
            return sinI;
        }

        public List<double[,]> getSin_Output()
        {
            count2++;
            if (count2 == 1)
            {
                for (double x = 0; x < 2 * Math.PI; x += 2 * Math.PI / 70)
                {
                    double[,] O = { { Math.Sin(x) } }; sinO.Add(O);
                }
            }
            return sinO;
        }

        public double[,] getSin_W1()
        {
            double[,] w1_sin = new double[inputCount, hiddenCount];   // 1 input - 10 hidden Layers    // ODER inputCount +1 für bias

            for (int i = 0; i < w1_sin.GetLength(0); i++)
            {
                for (int j = 0; j < w1_sin.GetLength(1); j++)
                {
                    w1_sin[i, j] = r.NextDouble();
                }
            }

            return w1_sin;
        }

        public double[,] getSin_W2()
        {
            double[,] w2_sin = new double[hiddenCount, 1];    // 10 hidden Layers - 1 output

            for (int i = 0; i < w2_sin.GetLength(0); i++)
            {
                for (int j = 0; j < w2_sin.GetLength(1); j++)
                {
                    w2_sin[i, j] = r.NextDouble();
                }
            }

            return w2_sin;
        }

    }
}
