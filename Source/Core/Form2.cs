using PosmosVV.Proc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PosmosVV
{
    partial class Form2 : Form
    {
        public int temp;
        public int size;
        public int bodies;
        public int inder;
        public int gtrackerbar;

        public int[] coordis = new int[2];
        public float[] unscaled = new float[2];
        private PosmosVV.Proc.Galaxy gal;
        public List<int[]> kords;
        int[] coords;
        public int win;
        public Form2()
        {
            InitializeComponent();
            //panel1.Invalidate();
            waitTime();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            int b = -35;
            int rectSize = (size / 2) + b;
            coords = new int[2];
            Rectangle rect = new Rectangle((panel1.ClientSize.Width - rectSize) / 2, (panel1.ClientSize.Height - rectSize) / 2, rectSize, rectSize);

            kords = new List<int[]>();

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);

                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    if (temp >= 12000)
                    {
                        brush.CenterColor = Color.FromArgb(255, 0, 211, 255);
                    }
                    else if (temp >= 9500)
                    {
                        brush.CenterColor = Color.FromArgb(255, 172, 255, 243);
                    }
                    else if (temp >= 7500)
                    {
                        brush.CenterColor = Color.FromArgb(255, 255, 255, 255);
                    }
                    else if (temp >= 6000)
                    {
                        brush.CenterColor = Color.FromArgb(255, 254, 255, 211);
                    }
                    else if (temp >= 5200)
                    {
                        brush.CenterColor = Color.FromArgb(255, 252, 255, 54);
                    }
                    else if (temp >= 3700)
                    {
                        brush.CenterColor = Color.FromArgb(255, 255, 215, 54);
                    }
                    else if (temp >= 1)
                    {
                        brush.CenterColor = Color.FromArgb(255, 255, 131, 54);
                    }

                    brush.SurroundColors = new Color[]
                    {
                        Color.FromArgb(0, 0, 150, 255)
                    };

                    e.Graphics.FillEllipse(brush, rect);
                }
            }
            for (int i = 1; i < gal.systems[inder].Primary_bodies.Count; i++)
            {
                var body = gal.systems[inder].Primary_bodies[i];

                if (body.distance > 0)
                {
                    int rounded = Math.Max(1, (int)(body.size / 25f));

                    int distance = (body.distance / 4) - 25;
                    int son = (int)(body.size / 2) + 40;

                    int centX = panel1.ClientSize.Width / 2;
                    int centY = panel1.ClientSize.Height / 2;

                    int orbitRadius = distance + son;

                    using (Pen orbitPen = new Pen(Color.FromArgb(100, Color.Gray), 1))
                    {
                        e.Graphics.DrawEllipse(orbitPen, centX - orbitRadius, centY - orbitRadius, orbitRadius * 2, orbitRadius * 2);
                    }

                    double angle = Random.Shared.NextDouble() * Math.PI * 2;

                    int OffX = (int)(Math.Cos(angle) * orbitRadius);
                    int OffY = (int)(Math.Sin(angle) * orbitRadius);

                    int X = centX + OffX - rounded / 2;
                    int Y = centY + OffY - rounded / 2;

                    coords = new int[] { X, Y };

                    Rectangle rect2 = new Rectangle(X, Y, rounded, rounded);

                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, (int)body.RGB[0], (int)body.RGB[1], (int)body.RGB[2])))
                    {
                        e.Graphics.FillEllipse(brush, rect2);
                    }

                    kords.Add(coords);
                    if (body.hasOrbitals)
                    {
                        for (int j = 0; j < body.Orbits.Count; j++)
                        {
                            var orbit = body.Orbits[j];

                            int centrX = X;
                            int centrY = Y;
                            int distanceO = orbit.distance;
                            int orbitR = distanceO;
                            int diam = orbitR * 2 - 45;

                            using (Pen orbitPen2 = new Pen(Color.FromArgb(100, Color.Gray), 1))
                            {
                                e.Graphics.DrawEllipse(orbitPen2, centrX - diam / 2 + 6, centrY - diam / 2 + 6, diam, diam);
                            }

                        }
                    }
                }

                
            }

        }

        private async void waitTime()
        {
            await Task.Delay(250);
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            await Task.Delay(100);
            pictureBox2.Visible = false;
            panel1.Visible = true;
            starSpect.Visible = true;
            trademark.Visible = true;
            set();
            tempT.Visible = true;
            sizeT.Visible = true;
            bodiesT.Visible = true;
            CordsT.Visible = true;
            UnscT.Visible = true;
            panel1.Invalidate();
        }
        public void set()
        {
            string unit = "M";
            int div = 100;
            int bods = bodies - 1;
            if (size < 180)
            {
                size += 40;
                unit = "K";
                div = 1;
            }
            tempT.Text = $"Temperature: {temp} C°";
            sizeT.Text = $"Radius: {size / div}{unit} Km";
            bodiesT.Text = $"Orbiting bodies: {bods}";
            CordsT.Text = $"Coordinates (X/Y): {coordis[0]}; {coordis[1]}";
            UnscT.Text = $"Unscaled Coords (X/Y): {unscaled[0]}; {unscaled[1]}";
        }

        private void tempT_Click(object sender, EventArgs e)
        {

        }

        private void trademark_Click(object sender, EventArgs e)
        {

        }

        public void superserious_function(PosmosVV.Proc.Galaxy galacsi)
        {
            gal = galacsi;
        }

        private void Form2_Click(object sender, MouseEventArgs e)
        {
            PosmosVV.GUI.GUI_util utility = new GUI.GUI_util();
            int x = e.X;
            int y = e.Y;
            float centerX = panel1.ClientSize.Width / 2f;
            float centerY = panel1.ClientSize.Height / 2f;
            int galaxySize = gtrackerbar;
            float scale = Math.Min(panel1.ClientSize.Width, panel1.ClientSize.Height) * 0.5f / galaxySize;
            //compare
            utility.Compare2(x, y, kords, gal, win);
        }
    }
}
// aggiungi slider per i par di galaxy2
