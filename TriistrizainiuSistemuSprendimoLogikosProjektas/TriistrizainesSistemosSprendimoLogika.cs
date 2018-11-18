using System;
using System.Collections.Generic;

namespace TriistrizainesSistemosSprendimoLogikosProjektas
{
    public class TriistrizainesSistemosSprendimoLogika
    {
        private readonly List<double> _A_dalis = new List<double>();
        private readonly List<double> _B_dalis = new List<double>();
        private readonly List<double> _C_dalis = new List<double>();
        private readonly List<double> _D_dalis = new List<double>();

        public TriistrizainesSistemosSprendimoLogika(double[] A_dalis, double[] B_dalis, double[] C_dalis, double[] D_dalis)
        {
            var length = B_dalis.Length;
            for (var i=0;i<length;i++)
            {
                if (i == 0)
                {
                    _A_dalis.Add(0);
                    _B_dalis.Add(B_dalis[i]);
                    _C_dalis.Add(C_dalis[i]);
                    _D_dalis.Add(D_dalis[i]);
                } else
                {
                    if (i==length-1)
                    {
                        _C_dalis.Add(0);
                        _B_dalis.Add(B_dalis[i]);
                        _A_dalis.Add(A_dalis[i]);
                        _D_dalis.Add(D_dalis[i]);
                    } else
                    {
                        _A_dalis.Add(A_dalis[i]);
                        _B_dalis.Add(B_dalis[i]);
                        _C_dalis.Add(C_dalis[i]);
                        _D_dalis.Add(D_dalis[i]);
                    }
                }
            }
        }

        public bool ArKonvergavimoSalygosTenkinamos()
        {
            var length = _B_dalis.Count;
            int counter = 0;
            for (var i=0;i<length;i++)
            {
                if (Math.Abs(_B_dalis[i]) > Math.Abs(_A_dalis[i]) + Math.Abs(_C_dalis[i]))
                {
                    counter++;
                } else
                {
                    if (Math.Abs(_B_dalis[i]) < Math.Abs(_A_dalis[i]) + Math.Abs(_C_dalis[i]))
                    {
                        counter = -1;
                    }
                }
            }

            return counter > 0 ? true : false;
        }

        public List<double> PerkeltiesMetodas()
        {
            List<List<double>> CD_koef = surastiCDKoef();
            var C_koef = CD_koef[0];
            var D_koef = CD_koef[1];

            var atsakymas = new List<double>();
            atsakymas.Add(D_koef[D_koef.Count-1]);

            for (var i=D_koef.Count-2;i>=0;i--)
            {
                var kintamasis = C_koef[i] * atsakymas[atsakymas.Count-1] + D_koef[i];
                atsakymas.Add(kintamasis);
            }
            atsakymas.Reverse();
            return atsakymas;
        }

        private List<List<double>> surastiCDKoef()
        {
            var pirmasisCKoef = -_C_dalis[0] / _B_dalis[0];
            var pirmasisDKoef = _D_dalis[0] / _B_dalis[0];
            var listasC = new List<double>();
            var listasD = new List<double>();
            listasC.Add(pirmasisCKoef);
            listasD.Add(pirmasisDKoef);
            for (int i=1; i<_B_dalis.Count;i++)
            {
                var vardiklis = _A_dalis[i] * pirmasisCKoef + _B_dalis[i];
                if (i< _B_dalis.Count - 1)
                {
                    pirmasisCKoef = -_C_dalis[i] / vardiklis;
                    pirmasisDKoef = (_D_dalis[i] - (_A_dalis[i] * pirmasisDKoef)) / vardiklis;
                    listasC.Add(pirmasisCKoef);
                    listasD.Add(pirmasisDKoef);
                } else

                {
                    pirmasisDKoef = (_D_dalis[i] - (_A_dalis[i] * pirmasisDKoef)) / vardiklis;
                    listasD.Add(pirmasisDKoef);
                }
            }

            var pagalbiniuSarasas = new List<List<double>>();
            pagalbiniuSarasas.Add(listasC);
            pagalbiniuSarasas.Add(listasD);

            return pagalbiniuSarasas;
        }
             
    }
}
