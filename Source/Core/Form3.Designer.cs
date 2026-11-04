namespace PosmosVV
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RotT = new Label();
            DistT = new Label();
            bodiesT = new Label();
            trademark = new Label();
            sizeT = new Label();
            tempT = new Label();
            planetSpect = new Label();
            panel1 = new Panel();
            RevoT = new Label();
            TypeT = new Label();
            SuspendLayout();
            // 
            // RotT
            // 
            RotT.AutoSize = true;
            RotT.BackColor = Color.Black;
            RotT.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RotT.ForeColor = Color.Lavender;
            RotT.Location = new Point(575, 212);
            RotT.Name = "RotT";
            RotT.Size = new Size(160, 17);
            RotT.TabIndex = 16;
            RotT.Text = "Rot speed goes here";
            RotT.Visible = false;
            // 
            // DistT
            // 
            DistT.AutoSize = true;
            DistT.BackColor = Color.Black;
            DistT.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DistT.ForeColor = Color.Lavender;
            DistT.Location = new Point(575, 77);
            DistT.Name = "DistT";
            DistT.Size = new Size(120, 17);
            DistT.TabIndex = 15;
            DistT.Text = "Dist goes here";
            DistT.Visible = false;
            // 
            // bodiesT
            // 
            bodiesT.AutoSize = true;
            bodiesT.BackColor = Color.Black;
            bodiesT.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bodiesT.ForeColor = Color.Lavender;
            bodiesT.Location = new Point(575, 172);
            bodiesT.Name = "bodiesT";
            bodiesT.Size = new Size(120, 17);
            bodiesT.TabIndex = 14;
            bodiesT.Text = "Bodies go here";
            bodiesT.Visible = false;
            // 
            // trademark
            // 
            trademark.AutoSize = true;
            trademark.BackColor = Color.Black;
            trademark.Font = new Font("Candara", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            trademark.ForeColor = Color.Orange;
            trademark.Location = new Point(793, 48);
            trademark.Name = "trademark";
            trademark.Size = new Size(25, 15);
            trademark.TabIndex = 13;
            trademark.Text = "TM";
            trademark.Visible = false;
            // 
            // sizeT
            // 
            sizeT.AutoSize = true;
            sizeT.BackColor = Color.Black;
            sizeT.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sizeT.ForeColor = Color.Lavender;
            sizeT.Location = new Point(575, 143);
            sizeT.Name = "sizeT";
            sizeT.Size = new Size(120, 17);
            sizeT.TabIndex = 12;
            sizeT.Text = "Size goes here";
            sizeT.Visible = false;
            // 
            // tempT
            // 
            tempT.AutoSize = true;
            tempT.BackColor = Color.Black;
            tempT.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tempT.ForeColor = Color.Lavender;
            tempT.Location = new Point(575, 106);
            tempT.Name = "tempT";
            tempT.Size = new Size(120, 17);
            tempT.TabIndex = 11;
            tempT.Text = "Temp goes here";
            tempT.Visible = false;
            // 
            // planetSpect
            // 
            planetSpect.AutoSize = true;
            planetSpect.BackColor = Color.Black;
            planetSpect.Font = new Font("Candara", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            planetSpect.ForeColor = Color.Orange;
            planetSpect.Location = new Point(556, 25);
            planetSpect.Name = "planetSpect";
            planetSpect.Size = new Size(241, 45);
            planetSpect.TabIndex = 10;
            planetSpect.Text = "PlanetSpector";
            planetSpect.Visible = false;
            // 
            // panel1
            // 
            panel1.Location = new Point(50, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 500);
            panel1.TabIndex = 18;
            panel1.Visible = false;
            panel1.Paint += panel1_Paint;
            // 
            // RevoT
            // 
            RevoT.AutoSize = true;
            RevoT.BackColor = Color.Black;
            RevoT.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RevoT.ForeColor = Color.Lavender;
            RevoT.Location = new Point(575, 241);
            RevoT.Name = "RevoT";
            RevoT.Size = new Size(168, 17);
            RevoT.TabIndex = 19;
            RevoT.Text = "Revo speed goes here";
            RevoT.Visible = false;
            // 
            // TypeT
            // 
            TypeT.AutoSize = true;
            TypeT.BackColor = Color.Black;
            TypeT.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TypeT.ForeColor = Color.Lavender;
            TypeT.Location = new Point(575, 283);
            TypeT.Name = "TypeT";
            TypeT.Size = new Size(120, 17);
            TypeT.TabIndex = 20;
            TypeT.Text = "Type goes here";
            TypeT.Visible = false;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(884, 561);
            Controls.Add(TypeT);
            Controls.Add(RevoT);
            Controls.Add(panel1);
            Controls.Add(RotT);
            Controls.Add(DistT);
            Controls.Add(bodiesT);
            Controls.Add(trademark);
            Controls.Add(sizeT);
            Controls.Add(tempT);
            Controls.Add(planetSpect);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(900, 600);
            MinimumSize = new Size(900, 600);
            Name = "Form3";
            Text = "PlanetSpector";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label RotT;
        private Label DistT;
        private Label bodiesT;
        private Label trademark;
        private Label sizeT;
        private Label tempT;
        private Label planetSpect;
        private Panel panel1;
        private Label RevoT;
        private Label TypeT;
    }
}
