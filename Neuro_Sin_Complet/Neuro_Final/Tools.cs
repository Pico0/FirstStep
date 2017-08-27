using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Final
{
    class Tools
    {

        // calculate Error => Quadratischer Fehler -> (result - wishedResult)²      Error für alle Samples Zusammen rechnen


        public double CalculateError(List<double> outcome, List<double[,]> wishedOutcome)   // Error mit Liste
        {
            double error = 0;

            for (int i = 0; i < outcome.Count; i++)
            {
                error += (outcome[i] - wishedOutcome[i][0, 0]) * (outcome[i] - wishedOutcome[i][0, 0]);
            }

            return error;
        }

        public double CalculateError(double[] outcome, List<double[,]> wishedOutcome)   // Error mit Array und Liste
        {
            double error = 0;

            for (int i = 0; i < outcome.Length; i++)    
            {
                error += (outcome[i] - wishedOutcome[i][0,0]) * (outcome[i] - wishedOutcome[i][0, 0]); 
            }

            return error;
        }




        public void cout(double[] x)     // Ausgabe aller Werte für Arrays
        {
            for (int i = 0; i < x.GetLength(0); i++)
            {              
                Console.Write(Math.Round(x[i], 10) + "\t");                
                Console.WriteLine();
            }
        }

        public void cout(double[,] x)    // Ausgabe aller Werte für doppel Arrays
        {
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write(Math.Round(x[i, j], 10) + "\t");
                }
                Console.WriteLine();
            }
        }

        public void cout(List<double> x)     // Ausgabe für Listenelemente
        {
            for (int i = 0; i < x.Count; i++)
            {
                Console.Write(Math.Round(x[i], 10) + "\t");
                Console.WriteLine();
            }
        }


        public double [,] ActivationFunction2(double [,] matrix)        // Actionvation Function - X-OR von  0 bis 1    (Sigmoid)       1 / ( 1 + e^-x)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = 1 / (1 + Math.Exp((-1) * matrix[i, j]) );
                }
            }
            return matrix;
        }

        public double[,] ActivationFunction(double[,] matrix)           // Actionvation Function - Sin von  -1 bis 1    (Tangens Hyperbolicus)  tanh(x) -> 2 / [ 1+e^(-2*x) ] - 1
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = ( 2 / (1 + Math.Exp((-2) * matrix[i, j]) ) - 1 );
                }
            }

            return matrix;
        }

        public void MatrixAddition(double x, double[,] matrix)    // Addiert einen Wert auf alle Werte der Matrix (Bias)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix[i, j] + x;
                }
            }           
        }

       

        public double[,] simpleMatrixMultiplication(double[,] matrix1, double[,] matrix2)   // Skalarprodukt -> a11 * b11 = c11
        {

            if (matrix1.GetLength(0) == matrix2.GetLength(0) && matrix1.GetLength(1) == matrix2.GetLength(1))
            {

                double[,] ret = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

                for (int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {
                        
                        ret[i, j] = matrix1[i, j] * matrix2[i, j];
                    }
                }

               

                return ret;
            }
            else
            {
                Console.WriteLine("WRONG simpleMatrixMultiplication");
                return matrix1;
            }

        }


        public double[,] transposeMatrix(double[,] matrix)  // gibt Transponierte Matrix zurück
        {
            double[,] ret = new double[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    ret[i, j] = matrix[j, i];
                }
            }

            return ret;

        }


        public double[,] MatrixSubtraction(double [,] a, double[,] b)       // Matrix - Matrix  /  a11 - b11
        {
            double[,] ret = new double[b.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    ret[i, j] = a[i,j] - b[i,j];
                }
            }

            return ret;
        }

        public double[,] MatrixSubtraction(double a, double[,] b)       // Matrix =  a - Alle Werte aus Matrix  / a-b11, a-b12 ...
        {
            double[,] ret = new double[b.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    ret[i, j] = a - b[i, j];
                }
            }

            return ret;
        }

        public double[,] MatrixAdd(double a, double[,] b)    // Matrix =  a + Alle Werte aus Matrix  / a+b11, a+b12 ...
        {
            double[,] ret = new double[b.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    ret[i, j] = a + b[i, j];
                }
            }

            return ret;
        }

        public double[,] MatrixAdd(double [,] a, double[,] b)       // Matrix + Matrix  /  a11 + b11
        {
            double[,] ret = new double[b.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    ret[i, j] = a[i,j] + b[i, j];
                }
            }

            return ret;
        }


        public double[,] matrixMultiplication(double[,] a, double[,] b)
        {
            double[,] erg = new double[a.GetLength(0), b.GetLength(1)];

            if (a.GetLength(1) == b.GetLength(0))   // Wenn matrix multiplication möglich ist
            {
                for (int i = 0; i < a.GetLength(0); i++)            // für Matrix a nach unten
                {
                    for (int j = 0; j < b.GetLength(1); j++)        // für Matrix b nach rechts
                    {
                        if (j == 0) { erg[i, j] = 0; }  // irgendeinen Wert im Speicher setzen

                        for (int k = 0; k < b.GetLength(0); k++)    // für alle Werte +...  Matrix a nach rechts und Matrix b nach unten
                        {
                            erg[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }

                return erg;
            }

            else
            {
                Console.WriteLine("WRONG MATRIX MULTIPLICATION");

                return erg;
            }

        }


        public double[,] SkalarTimesMatrix(double scalar, double[,] matrix)
        {
            double[,] ret = new double[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ret[i, j] = matrix[i, j] * scalar;
                }
            }

            return ret;
        }

        public double[,] MatrixDividedByScalar(double [,] matrix, double scalar)
        {
            double[,] ret = new double[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ret[i, j] = matrix[i, j] / scalar;
                }
            }

            return ret;
        }
    }
}