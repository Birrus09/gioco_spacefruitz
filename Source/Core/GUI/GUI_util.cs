using PosmosVV.Proc;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace PosmosVV.GUI
{
    class GUI_util
    {
        public List<float> res;
        public List<int> inds;
        public void Compare(float X, float Y, float centerX, float centerY, float scale, PosmosVV.Proc.Galaxy galaxy, int galaxySize, int genType)
        {
            if (genType == 2)
            {
                scale *= 4;
            }
            
            res = new List<float>();
            inds = new List<int>();


            float Xorig = (float)(X - centerX) / scale;
            float Yorig = (float)(Y - centerY) / scale;
            int integ = -1;
            float maxOffset = 10f / scale;

            for (int i = 0; i < galaxy.systems.Count; i++)
            {
                if (Math.Abs(Xorig - galaxy.systems[i].coordinates[0]) < maxOffset && Math.Abs(Yorig - galaxy.systems[i].coordinates[1]) < maxOffset)
                {
                    integ ++;
                    float Xdelta = Math.Abs(Xorig - galaxy.systems[i].coordinates[0]);
                    float Ydelta = Math.Abs(Yorig - galaxy.systems[i].coordinates[1]);
                    res.Add(Xdelta + Ydelta);
                    inds.Add(i);
                }
            }

            if (integ != -1)
            {
                float winner = res.Min();
                int winnerIndex = res.IndexOf(winner);
                int winnerwinner = inds[winnerIndex];
                var systemu = galaxy.systems[winnerwinner];
                //MessageBox.Show($"we chose {winner}");

                Form2 display = new Form2();
                display.temp = (int)systemu.Primary_bodies[0].temperature;
                display.size = systemu.Primary_bodies[0].size;
                display.bodies = systemu.Primary_bodies.Count;
                display.inder = winnerwinner;
                display.gtrackerbar = galaxySize;

                display.coordis[0] = systemu.coordinates[0];
                display.coordis[1] = systemu.coordinates[1];
                display.unscaled[0] = (float)systemu.coordinates[0] / galaxySize;
                display.unscaled[1] = (float)(systemu.coordinates[1] / galaxySize);
                display.win = winnerwinner;
                display.superserious_function(galaxy);
                display.ShowDialog();
                
            }
            else
            {
                DialogResult resul = MessageBox.Show("There's no star in these coordinates", "airball", MessageBoxButtons.OK);
            }
        }

        public void Compare2(float X, float Y, List<int[]> planets, PosmosVV.Proc.Galaxy galaxy, int e)
        {
            float maxOffset = 20f;
            res = new List<float>();
            inds = new List<int>();

            int integ = -1;
            for (int i = 0; i < planets.Count; i++)
            {
                if (Math.Abs(X - planets[i][0]) < maxOffset && Math.Abs(Y - planets[i][1]) < maxOffset)
                {
                    integ ++;
                    float Xdelta = Math.Abs(X - planets[i][0]);
                    float Ydelta = Math.Abs(Y - planets[i][1]);
                    res.Add(Xdelta + Ydelta);
                    inds.Add(i);
                }
            }
            if (integ != -1)
            {
                int giuvide = 35;
                float winner = res.Min();
                int winnerIndex = res.IndexOf(winner);
                int winnerwinner = inds[winnerIndex];
                Form3 displayP = new Form3();
                var body = galaxy.systems[e].Primary_bodies[winnerwinner + 1];

                displayP.distance = body.distance;
                displayP.size = body.size - giuvide;
                if(body.Orbits == null)
                {
                    displayP.bodies = 0;
                }
                else
                {
                    displayP.bodies = body.Orbits.Count;
                }
                displayP.temperature = (int)body.temperature;
                displayP.revolution = (decimal)body.revolution_speed;
                displayP.rotation = (decimal)body.rotation_speed;
                displayP.type = body.type;
                displayP.brushete = new SolidBrush(Color.FromArgb(255, (int)body.RGB[0], (int)body.RGB[1], (int)body.RGB[2]));
                displayP.ShowDialog();
            }
            else
            {
                DialogResult resul = MessageBox.Show("There is no planet in these coordinates", "airball 2.0", MessageBoxButtons.OK);
            }
        }
    }
}
