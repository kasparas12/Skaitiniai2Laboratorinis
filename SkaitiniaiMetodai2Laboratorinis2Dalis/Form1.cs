using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SkaitiniaiMetodai2Laboratorinis2Dalis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
            chart2.Series.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var funkcija = new Funkcija(-2, 1, 10);
            var funkcija = new Funkcija(-3, 3, 10);
            funkcija.GetAllPoints(true);
            var splainas = new KubinisSplainas(funkcija,-2);
            splainas.SprestiSistema();
            splainas.Get_Coef();
            splainas.Get_Spline_Points();

            chart1.Series.Clear();
            chart1.Series.Add("Points");
            chart1.Series.Add("Function");
            chart1.Series.Add("Spline");
            chart1.Series[0].ChartType = SeriesChartType.Point;
            chart1.Series[1].ChartType = SeriesChartType.Spline;
            chart1.Series[2].ChartType = SeriesChartType.Spline;
            chart1.Series[0].Color = Color.Blue;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[2].Color = Color.Orange;

            foreach (var point in funkcija.Points)
            {
                chart1.Series[0].Points.AddXY(point.X, point.Y);
                chart1.Series[1].Points.AddXY(point.X, point.Y);
            }

            foreach (var point in splainas.SplainoReiksmes)
            {
                chart1.Series[2].Points.AddXY(point.X, point.Y);
            }


            label1.Text = "Funkcijos artinio reiksme taske " + splainas.Artinys + " yra " + splainas.CalculateSplineValue(splainas.Artinys);
            label2.Text = "Tiksli reiksme taske " + splainas.Artinys + " yra " + splainas.Funkcija.CalculateY(splainas.Artinys);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var funkcija = new Funkcija(0, 5, 5);
            funkcija.GetAllPoints(false);
            var splainas = new KubinisSplainas(funkcija, 2.5);
            splainas.SprestiSistema();
            splainas.Get_Coef();
            splainas.Get_Spline_Points();

            chart2.Series.Clear();
            chart2.Series.Add("Points");
            chart2.Series.Add("Function");
            chart2.Series.Add("Spline");
            chart2.Series[0].ChartType = SeriesChartType.Point;
            chart2.Series[1].ChartType = SeriesChartType.Line;
            chart2.Series[2].ChartType = SeriesChartType.Spline;
            chart2.Series[0].Color = Color.Blue;
            chart2.Series[1].Color = Color.Red;
            chart2.Series[2].Color = Color.Orange;

            foreach (var point in funkcija.Points)
            {
                chart2.Series[0].Points.AddXY(point.X, point.Y);
                chart2.Series[1].Points.AddXY(point.X, point.Y);
            }

            foreach (var point in splainas.SplainoReiksmes)
            {
                chart2.Series[2].Points.AddXY(point.X, point.Y);
            }

            label3.Text = "Funkcijos artinio reiksme taske " + splainas.Artinys + " yra " + splainas.CalculateSplineValue(splainas.Artinys);

        }
    }
}
