using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PosmosVV
{
    public partial class Form3 : Form
    {
        public int distance;
        public int bodies;
        public int temperature;
        public int size;
        public decimal rotation;
        public decimal revolution;
        public string type;

        public SolidBrush brushete = new SolidBrush(Color.White);
        public Form3()
        {
            InitializeComponent();
            waitTime();
        }


        private async void waitTime()
        {
            await Task.Delay(350);
            set();
            planetSpect.Visible = true;
            trademark.Visible = true;
            tempT.Visible = true;
            sizeT.Visible = true;
            bodiesT.Visible = true;
            DistT.Visible = true;

            RotT.Visible = true;
            RevoT.Visible = true;
            TypeT.Visible = true;

            panel1.Visible = true;

            panel1.Invalidate();
        }

        public void set()
        {
            tempT.Text = $"Temperature: {temperature} C°";
            sizeT.Text = $"Diameter: {size}K Km";
            bodiesT.Text = $"Moons: {bodies}";
            DistT.Text = $"Distance from star: {distance}M Km";
            RotT.Text = $"Rotation speed: {rotation}K Km/s";
            RevoT.Text = $"Revolution speed: {revolution} K Km/s";
            TypeT.Text = $"Planet type: {type}";
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            int b = 15;
            int rectSize = ((size / 2) + b) * 2;
            Rectangle rect = new Rectangle((panel1.ClientSize.Width - rectSize) / 2, (panel1.ClientSize.Height - rectSize) / 2, rectSize, rectSize);
            g.FillEllipse(brushete, rect);

        }
    }
}
