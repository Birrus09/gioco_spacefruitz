using System.Windows.Forms;

namespace PosmosVV
{
    public partial class Form1 : Form
    {
        private PosmosVV.Proc.Galaxy ciao;
        private PosmosVV.Proc.Galaxy tt;
        private PosmosVV.GUI.GUI_util utility;
        bool gen = false;
        bool togglething = false;
        int genType = 2;
        public Form1()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            pictureBox1.Location = new Point((this.ClientSize.Width - pictureBox1.Width) / 2, 70);
            panel1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(1920, 1080);
            this.MaximumSize = new Size(1920, 1080);
            this.WindowState = FormWindowState.Maximized;

            startButton.Visible = false;
            galaxySizeTrackBar.Visible = false;
            galaxySizeValueLabel.Visible = false;
            scatterTrackBar.Visible = false;
            scatterValueLabel.Visible = false;
            spreadTrackerBar.Visible = false;
            densityTrackBar.Visible = false;
            spreadValueLabel.Visible = false;
            densityValueLabel.Visible = false;
            checkBox1.Visible = false;
            label1.Visible = false;
            panel1.Dock = DockStyle.Fill;
            pictureBox1.Visible = false;
            panel1.Visible = true;
            errorLabel.Visible = false;

            gen = true;

            panel1.Invalidate();
        }

        private void scatterTrackBar_ValueChanged(object sender, EventArgs e)
        {
            float scatter = scatterTrackBar.Value / 100000f;
            scatterValueLabel.Text = $"Scatter: {scatter:F5}";
            if (panel1.Visible)
            {
                panel1.Invalidate();
            }
        }

        private void galaxySizeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            galaxySizeValueLabel.Text = $"Galaxy size: {galaxySizeTrackBar.Value}";
            if (panel1.Visible)
            {
                panel1.Invalidate();
            }
        }
        private void densityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            float density = densityTrackBar.Value / 100000f;
            densityValueLabel.Text = $"Density: {density:F5}";
            if (panel1.Visible)
            {
                panel1.Invalidate();
            }
        }

        private void spreadTrackerBar_ValueChanged(object sender, EventArgs e)
        {
            float spread = spreadTrackerBar.Value / 100000f;
            spreadValueLabel.Text = $"Spread: {spread:F5}";
            if (panel1.Visible)
            {
                panel1.Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            PosmosVV.Proc.Galaxy ciao = new Proc.Galaxy();

            int galaxySize = galaxySizeTrackBar.Value;
            //ciao.GenerateGalaxy(123, galaxySize, scatterTrackBar.Value / 100000f);
            if (togglething == false)
            {
                ciao.GenerateGalaxy2(6589, galaxySize, scatterTrackBar.Value / 100000f, spreadTrackerBar.Value / 100000f, densityTrackBar.Value / 100000f);
                genType = 2;
                galaxySize /= 4;
            }
            else
            {
                ciao.GenerateGalaxy(6589, galaxySize, scatterTrackBar.Value);
                genType = 1;
            }

            float centerX = panel1.ClientSize.Width / 2f;
            float centerY = panel1.ClientSize.Height / 2f;


            float scale = Math.Min(panel1.ClientSize.Width, panel1.ClientSize.Height) * 0.5f / galaxySize;
            foreach (var s in ciao.systems)
            {
                float x = centerX + s.coordinates[0] * scale;
                float y = centerY + s.coordinates[1] * scale;

                float starSize = Math.Max(1f, 2f * scale);
                if (s.Primary_bodies[0].size > 450)
                {
                    g.FillEllipse(Brushes.White, x - starSize / 2, y - starSize / 2, 4 * starSize, 3 * starSize);
                }
                else if (s.Primary_bodies[0].size > 350)
                {
                    g.FillEllipse(Brushes.White, x - starSize / 2, y - starSize / 2, 4 * starSize, 3 * starSize);
                }
                else
                {
                    g.FillEllipse(Brushes.White, x - starSize / 2, y - starSize / 2, 2 * starSize, 2 * starSize);
                }

            }

            tt = ciao;
        }

        //exp

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            PosmosVV.GUI.GUI_util utility = new GUI.GUI_util();
            if (!gen) { return; }
            int x = e.X;
            int y = e.Y;
            float centerX = panel1.ClientSize.Width / 2f;
            float centerY = panel1.ClientSize.Height / 2f;
            int galaxySize = galaxySizeTrackBar.Value;
            float scale = Math.Min(panel1.ClientSize.Width, panel1.ClientSize.Height) * 0.5f / galaxySize;
            //MessageBox.Show($"X: {x}, Y: {y}");
            utility.Compare(x, y, centerX, centerY, scale, tt, galaxySize, genType);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                togglething = true;
                checkBox1.Text = "Gen 1";
                densityTrackBar.Visible = false;
                spreadTrackerBar.Visible = false;
                densityValueLabel.Visible = false;
                spreadValueLabel.Visible = false;
                errorLabel.Visible = true;
            }
            else
            {
                togglething = false;
                checkBox1.Text = "Gen 2";
                densityTrackBar.Visible = true;
                spreadTrackerBar.Visible = true;
                densityValueLabel.Visible = true;
                spreadValueLabel.Visible = true;
                errorLabel.Visible = false;
            }
        }
    }
}
