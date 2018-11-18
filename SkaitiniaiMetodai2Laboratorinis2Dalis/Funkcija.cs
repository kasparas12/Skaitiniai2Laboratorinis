using System;
using System.Collections.Generic;

namespace SkaitiniaiMetodai2Laboratorinis2Dalis
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Funkcija
    {
        public double MinInterval { get; set; }
        public double MaxInterval { get; set; }
        public List<Point> Points { get; set; }
        public int N { get; set; }
        public double X { get; set; }
        public double h { get; set; }

        public Funkcija(double min, double max, int n)
        {
            MinInterval = min;
            MaxInterval = max;
            N = n;
            Points = new List<Point>();
            h = Get_h();
        }

        public double Get_h()
        {
            h = (Math.Abs(MinInterval) + Math.Abs(MaxInterval)) / N;
            return h;
        }

        public void GetAllPoints(bool pirmaUzduotis)
        {
            var temp = MinInterval;

            if (pirmaUzduotis)
            {


                Points.Add(new Point
                {
                    X = temp,
                    Y = CalculateY(temp)
                });
                while (temp < MaxInterval)
                {
                    if (temp + h > MaxInterval)
                    {
                        temp = MaxInterval;
                        Points.Add(new Point
                        {
                            X = temp,
                            Y = CalculateY(temp)
                        });
                    }
                    else
                    {
                        temp = temp + h;
                        Points.Add(new Point
                        {
                            X = temp,
                            Y = CalculateY(temp)
                        });
                    }
                }

            } else
            {
                var studentoNumerisBase2 = Convert.ToString(10, 2);
                studentoNumerisBase2 = String.Concat("00", studentoNumerisBase2);

                int index = 0;
                foreach(var character in studentoNumerisBase2)
                {
                    if (character == '1') {
                        Points.Add(new Point
                        {
                            X = index,
                            Y = 209
                        });
                    } else
                    {
                        Points.Add(new Point
                        {
                            X = index,
                            Y = 0
                        });
                    }
                    index++;
                }
            }

        }

        // duotosios funkcijos israiska - mano atveju (x-3)*cos^2(x)
        public double CalculateY(double x)
        {
            //return 10 * Math.Pow(Math.E, -x) * (Math.Pow(x, 3) - 2 * x + 1);
            //return (x - 3) * Math.Pow(Math.Cos(x), 2);
            return x;
        }
    }
}
