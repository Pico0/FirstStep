using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace Neuro_Final
{
    public partial class Form1 : Form
    {

        Network n = new Network();

        public Form1()
        {
            InitializeComponent();
            
        }   

    private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("X-OR");
            comboBox1.Items.Add("Sinus");

            comboBox1.Text = "Sinus";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Data data = new Data();
            Tools t = new Tools();

            Go(data.getSin_Input(), data.getSin_Output(), 0.005);   // Sinus    sample size 70
            //Go(data.getXOR_Input(), data.getXOR_Output(), 0.005); // X-OR     sample size in Network noch auf 4 setzen !
            t.cout(n.outcome);
        }

        public void Go(List<double[,]> input, List<double[,]> output, double threshhold)
        {            
            Data data = new Data();
            Tools t = new Tools();

            n.start(input[1], output[1], data.getSin_W1(), data.getSin_W2());  // Random weights als Start Funktion

            int counter = 0;  // Für Ausgabe das nicht alles aufgeschrieben werden soll nur alle 30 mal   / geht nicht mit n.count weil dieser nur bis zur SampleSize hochzählt dann wieder zurück auf 0 springt
            double error = 1000;      // Bester bisheriger Error  // = 10 ist ein zufällig hoher Wert weil ich am Anfang abfrage    if (error < ActualError )
            n.count = 0;    // Zählt bis zur SampleSize hoch und geht alles so oft durch
          
            do
            {

                counter++;
                for (int i = 0; i < input.Count; i++)   // Alle Samples durchgehen
                {
                    n.start(input[i], output[i], n.input_hidden_Weights, n.hidden_output_weights);
                }
                n.count = 0;
                if (error > t.CalculateError(n.outcome, output))
                {
                    error = t.CalculateError(n.outcome, output);
                    Console.WriteLine("NEW !!! Error: " + t.CalculateError(n.outcome, output));
                }              
                    if (counter % 30 == 0)   // Wenn kein besserer Error erreicht wird nur alle 30 ausgeben
                    {
                        this.Update();

                        foreach (var series in this.chart1.Series)  // Alle Punkte löschen zum wieder beschreiben
                        {
                            series.Points.Clear();
                        }

                    if (n.s == 70)  // nur bei Sinus
                    {
                        for (int i = 0; i < n.outcome.Length; i++)      // Ausgabe
                        {
                            this.chart1.Series["Sinus"].Points.AddXY(i, Math.Sin(i * Math.PI * 2 / 70));
                            this.chart1.Series["MySinus"].Points.AddXY(i, n.outcome[i]);
                        }
                        errorBox.Text = t.CalculateError(n.outcome, output).ToString(); // Error in Box schreiben
                    }
                }
                Console.WriteLine("Counter: "+counter);
            }
            while (t.CalculateError(n.outcome, output) > threshhold);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {           
                
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
