using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Final
{
    class Network
    {
        public static double learningRate = 0.1;
        public static double alpha = 0.05;

        public double[,] input_hidden_Weights;
        public double[,] hidden_output_weights;

        public int count = 0;   // Zählt bis zur SampleSize hoch

        public static int sampleSize = 70;

        //public List<double> outcome = new List<double>();
        public double[] outcome = new double[sampleSize];

        public double[,] deriv_IH;  // alter Ableitungsvektor zwischen Input und Hidden Layer
        public double[,] deriv_HU;  // alter Ableitungsvektor zwischen Hidden und Output Layer

        public List <double[,]> out_h = new List<double[,]>();
        public List <double[,]> out_o = new List<double[,]>();

        public int s = sampleSize;

        public void start(double [,] input, double [,] output, double [,] weight1, double [,] weight2)  // Forward und Backward für ein Sample
        {

            Tools tool = new Tools();
            Data data = new Data();

            if (count == sampleSize) { count = 0; } // count zählt bis zum sampleSize hoch und dann wieder zurück auf 0

            count++;       

         // FORWARD PASS

            double[,] net_h = tool.matrixMultiplication(input, weight1);   // Bevor Aktivierungsfunktion
            double [,] out_h = tool.ActivationFunction(net_h);

            double[,] net_o = tool.matrixMultiplication(out_h, weight2);   // Nach Aktivierungsfunktion
            double[,] out_o = tool.ActivationFunction(net_o);

            outcome[count - 1] = out_o[0, 0];

         // FORWARD PASS - ENDE


         // BACKWARD - Output-Hidden

            // Formel:  out_h_T * (out_o-target) * out_o * (1-out_o) für Sigmoid
            // Formel:  out_h_T*(out_o-target) * (1-out_o²) für Tangenz Hyperbolicus

            // tanh(x) =>  2 / [ 1+e^(-2x) ] -1
            // Derivative of tanh(x) -> (1-out_o²) 

            double[,] delta = tool.simpleMatrixMultiplication(tool.MatrixSubtraction(output, out_o), tool.MatrixSubtraction(1, tool.simpleMatrixMultiplication(out_o, out_o)) );    // (wishedResult - out_o) * (1-out_o²)

            double[,] derivative = tool.matrixMultiplication(tool.transposeMatrix(out_h),delta);                                          // out_h_T*(out_o-target) * (1-out_o²)

            double[,] newEdges_hidden_output = tool.SkalarTimesMatrix(learningRate, derivative);                                          // learningRate * derivative                                          
            newEdges_hidden_output = tool.MatrixAdd(weight2, newEdges_hidden_output);                                                     // newWeights = oldWeights + learningRate * derivative

            
            if (count != 1)
            {
                newEdges_hidden_output = tool.MatrixAdd(newEdges_hidden_output, tool.SkalarTimesMatrix(alpha, deriv_HU));   // newWeights = oldWeights + learningRate * derivative + alpha * old_derivative
            }


         // BACKWARD - Hidden-Input

            // Formel: i_T * { [ (out_o - target) * out_o * (1-out_o) * w_T ] * out_h * (1-out_h) } für Sigmoid

            // Formel: i_T * { [ (out_o - target) * out_o * (1-out_o) * w_T ] * (1 - out_h²) } für Tangenz Hyperbolicus

            double[,] derivative_activationFunction_hidden = tool.MatrixSubtraction(1, tool.simpleMatrixMultiplication(out_h, out_h));   //  (1 - out_h²)    -> 1 - tanh²(x)

            //double[,] delta_x_weight2 = tool.matrixMultiplication(delta, tool.transposeMatrix(weight2));                                     // Schritt 1 = [ (out_o-target) * out_o * (1-out_o) * w_T ]    (Matrixmultiplikation)
            //double[,] delta_x_weights2_h = tool.simpleMatrixMultiplication(delta_x_weight2, derivative_activationFunction_hidden);           // Schritt 2 = [ (out_o-target) * out_o * (1-out_o) * w_T] * out_h * (1-out_h)  /  eins * out_h * (1-out_h)     (Skalarprodukt)
            //double[,] input_x_delta_x_weights2_h = tool.matrixMultiplication(tool.transposeMatrix(input), delta_x_weights2_h);               // Schritt 3 =  input_T * { [ (out_o-target) * out_o * (1-out_o) ] * weights_T * out_h * (1-out_h) }    /  input_T * zwei    (Matrixmultiplikation)

            double[,] deriv1 = tool.matrixMultiplication(tool.transposeMatrix(input), tool.simpleMatrixMultiplication(tool.matrixMultiplication(delta, tool.transposeMatrix(weight2)), derivative_activationFunction_hidden )); // input_T * { [ (out_o-target) * out_o * (1-out_o) ] * weights_T * out_h * (1-out_h) }

            double[,] newEdges_input_hidden = tool.SkalarTimesMatrix(learningRate, deriv1);                                                   // learningRate * derivative                                          
            newEdges_input_hidden = tool.MatrixAdd(weight1, newEdges_input_hidden);
            
            if (count != 1)
            {
                newEdges_input_hidden = tool.MatrixAdd(newEdges_input_hidden, tool.SkalarTimesMatrix(alpha, deriv_IH));
            }
            
         // BACKWARD ENDE

            //output_all = out_o;
            deriv_HU = derivative;      // Ableitungen abspeichern um sie später zusammen zu rechnen
            deriv_IH = deriv1;


            hidden_output_weights = newEdges_hidden_output;     // Gewichte Lokal abspeichern, sodass sie nicht überschrieben werden
            input_hidden_Weights = newEdges_input_hidden;

        }

    }
}
