using System;
using System.Collections.Generic;
using System.Diagnostics;
using TriistrizainesSistemosSprendimoLogikosProjektas;

namespace SkaitiniaiMetodai2Laboratorinis2Dalis
{
    public class KubinisSplainas
    {
        private TriistrizainesSistemosSprendimoLogika _triistrizaine;
        public Funkcija Funkcija { get; set; }
        public List<double> u_values { get; set; }
        public List<double> e_values { get; set; }
        public List<double> H_values { get; set; }
        public List<double> G_values { get; set; }
        public List<Point> SplainoReiksmes { get; set; }
        public double Artinys { get; set; }

        public KubinisSplainas(Funkcija funkcija, double artinys)
        {
            Funkcija = funkcija;
            Artinys = artinys;
            SplainoReiksmes = new List<Point>();
        }

        public List<double> SprestiSistema()
        {
            double[] a_values = new double[Funkcija.Points.Count];
            double[] b_values = new double[Funkcija.Points.Count];
            double[] c_values = new double[Funkcija.Points.Count];
            double[] d_values = new double[Funkcija.Points.Count];

            a_values[0] = 0;
            a_values[Funkcija.Points.Count-1] = 0;

            c_values[0] = 0;
            c_values[Funkcija.Points.Count-1] = 0;

            b_values[0] = 1;
            b_values[Funkcija.Points.Count-1] = 1;

            d_values[0] = 0;
            d_values[Funkcija.Points.Count-1] = 0;

            for (int i=1;i<Funkcija.Points.Count-1;i++)
            {

                a_values[i] = 1;
                b_values[i] = 4;
                c_values[i] = 1;
                d_values[i] = (6 * ((Funkcija.Points[i + 1].Y - 2 * Funkcija.Points[i].Y + Funkcija.Points[i - 1].Y) / (Math.Pow(Funkcija.h, 2))));
            }


            _triistrizaine = new TriistrizainesSistemosSprendimoLogika(a_values, b_values, c_values, d_values);
            u_values = _triistrizaine.PerkeltiesMetodas();
            return u_values;
        }

        public void Get_Coef()
        {
            var values_e = new List<double>();
            var values_G = new List<double>();
            var values_H = new List<double>();
            for (int i=0;i<=Funkcija.Points.Count-2;i++)
            {
                values_e.Add((Funkcija.Points[i + 1].Y - Funkcija.Points[i].Y) / (Funkcija.h) - u_values[i + 1] * (Funkcija.h / 6) - u_values[i] * (Funkcija.h / 3));
                values_G.Add(u_values[i] / 2);
                values_H.Add((u_values[i + 1] - u_values[i])/ (6 * Funkcija.h));
            }
            e_values = values_e;
            G_values = values_G;
            H_values = values_H;
        }

        public List<Point> Get_Spline_Points()
        {
            for(int i=0; i <= Funkcija.Points.Count - 2; i++)
            {
                var intervalMin = Funkcija.Points[i].X;
                var intervalMax = Funkcija.Points[i + 1].X;

                for (double j = intervalMin; j<intervalMax;j+=0.1)
                {
                    Debug.Write("U values: " + u_values[i] + " ");
                    var reiksme = H_values[i] * Math.Pow(j - Funkcija.Points[i].X, 3) + G_values[i] * Math.Pow(j - Funkcija.Points[i].X, 2) + e_values[i] * (j - Funkcija.Points[i].X) + Funkcija.Points[i].Y;
                    SplainoReiksmes.Add(new Point {
                        X = j,
                        Y = reiksme
                    });
                }
            }

            return SplainoReiksmes;
        }

        public double CalculateSplineValue(double argument)
        {
            int index = Get_Interval(argument);
            var reiksme = H_values[index] * Math.Pow(argument - Funkcija.Points[index].X, 3) + G_values[index] * Math.Pow(argument - Funkcija.Points[index].X, 2) + e_values[index] * (argument - Funkcija.Points[index].X) + Funkcija.Points[index].Y;
            return reiksme;
        }

        private int Get_Interval(double x)
        {
            if (x < Funkcija.MinInterval || x > Funkcija.MaxInterval)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            for (int i=0;i<Funkcija.Points.Count-1;i++)
            {
                if (x >= Funkcija.Points[i].X && x <= Funkcija.Points[i+1].X)
                {
                    return i;
                }
            }

            return Funkcija.Points.Count - 1;
        }

    }
}
