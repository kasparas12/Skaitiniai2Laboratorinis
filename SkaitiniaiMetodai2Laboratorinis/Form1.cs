using System;
using System.Windows.Forms;
using System.IO;
using TriistrizainesSistemosSprendimoLogikosProjektas;

namespace SkaitiniaiMetodai2Laboratorinis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var duomenys = File.ReadAllLines("matrica.txt");
            var a_input = duomenys[0];
            var b_input = duomenys[1];
            var c_input = duomenys[2];
            var d_input = duomenys[3];

            var a_converted = Array.ConvertAll(a_input.Split(' '), Double.Parse);
            var b_converted = Array.ConvertAll(b_input.Split(' '), Double.Parse);
            var c_converted = Array.ConvertAll(c_input.Split(' '), Double.Parse);
            var d_converted = Array.ConvertAll(d_input.Split(' '), Double.Parse);

            var triistrizaine = new TriistrizainesSistemosSprendimoLogika(a_converted, b_converted, c_converted, d_converted);

            if (triistrizaine.ArKonvergavimoSalygosTenkinamos())
            {
                var ats = triistrizaine.PerkeltiesMetodas();
                MessageBox.Show("Atsakymas: " + string.Join("\t", ats));
            } else
            {
                MessageBox.Show("Konvergavimo salygos netenkinamos!");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
