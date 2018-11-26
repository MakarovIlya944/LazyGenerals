namespace LazyGeneral
{
	partial class GameWindow
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gameInitGroup = new System.Windows.Forms.GroupBox();
            this.labelA1 = new System.Windows.Forms.Label();
            this.labelA2 = new System.Windows.Forms.Label();
            this.labelA3 = new System.Windows.Forms.Label();
            this.labelA4 = new System.Windows.Forms.Label();
            this.labelA5 = new System.Windows.Forms.Label();
            this.labelAmax = new System.Windows.Forms.Label();
            this.labelAmaxnum = new System.Windows.Forms.Label();
            this.trackBarA1 = new System.Windows.Forms.TrackBar();
            this.trackBarA2 = new System.Windows.Forms.TrackBar();
            this.trackBarA3 = new System.Windows.Forms.TrackBar();
            this.trackBarA4 = new System.Windows.Forms.TrackBar();
            this.trackBarA5 = new System.Windows.Forms.TrackBar();
            this.labelAcur = new System.Windows.Forms.Label();
            this.labelAcurnum = new System.Windows.Forms.Label();
            this.labelA1num = new System.Windows.Forms.Label();
            this.labelA2num = new System.Windows.Forms.Label();
            this.labelA3num = new System.Windows.Forms.Label();
            this.labelA4num = new System.Windows.Forms.Label();
            this.labelA5num = new System.Windows.Forms.Label();
            this.buttonAReady = new System.Windows.Forms.Button();
            this.labelAmaxone = new System.Windows.Forms.Label();
            this.labelAmaxonenum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gameInitGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA5)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.OliveDrab;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // gameInitGroup
            // 
            this.gameInitGroup.Controls.Add(this.buttonAReady);
            this.gameInitGroup.Controls.Add(this.trackBarA5);
            this.gameInitGroup.Controls.Add(this.trackBarA4);
            this.gameInitGroup.Controls.Add(this.trackBarA3);
            this.gameInitGroup.Controls.Add(this.trackBarA2);
            this.gameInitGroup.Controls.Add(this.trackBarA1);
            this.gameInitGroup.Controls.Add(this.labelA5num);
            this.gameInitGroup.Controls.Add(this.labelA4num);
            this.gameInitGroup.Controls.Add(this.labelA3num);
            this.gameInitGroup.Controls.Add(this.labelA2num);
            this.gameInitGroup.Controls.Add(this.labelA1num);
            this.gameInitGroup.Controls.Add(this.labelAcurnum);
            this.gameInitGroup.Controls.Add(this.labelAmaxonenum);
            this.gameInitGroup.Controls.Add(this.labelAmaxnum);
            this.gameInitGroup.Controls.Add(this.labelAcur);
            this.gameInitGroup.Controls.Add(this.labelAmaxone);
            this.gameInitGroup.Controls.Add(this.labelAmax);
            this.gameInitGroup.Controls.Add(this.labelA5);
            this.gameInitGroup.Controls.Add(this.labelA4);
            this.gameInitGroup.Controls.Add(this.labelA3);
            this.gameInitGroup.Controls.Add(this.labelA2);
            this.gameInitGroup.Controls.Add(this.labelA1);
            this.gameInitGroup.Location = new System.Drawing.Point(127, 50);
            this.gameInitGroup.Name = "gameInitGroup";
            this.gameInitGroup.Size = new System.Drawing.Size(492, 319);
            this.gameInitGroup.TabIndex = 1;
            this.gameInitGroup.TabStop = false;
            this.gameInitGroup.Text = "Баланс армии";
            // 
            // labelA1
            // 
            this.labelA1.AutoSize = true;
            this.labelA1.Location = new System.Drawing.Point(10, 40);
            this.labelA1.Name = "labelA1";
            this.labelA1.Size = new System.Drawing.Size(111, 13);
            this.labelA1.TabIndex = 0;
            this.labelA1.Text = "Мощь первой армии";
            // 
            // labelA2
            // 
            this.labelA2.AutoSize = true;
            this.labelA2.Location = new System.Drawing.Point(10, 80);
            this.labelA2.Name = "labelA2";
            this.labelA2.Size = new System.Drawing.Size(110, 13);
            this.labelA2.TabIndex = 0;
            this.labelA2.Text = "Мощь второй армии";
            // 
            // labelA3
            // 
            this.labelA3.AutoSize = true;
            this.labelA3.Location = new System.Drawing.Point(10, 120);
            this.labelA3.Name = "labelA3";
            this.labelA3.Size = new System.Drawing.Size(115, 13);
            this.labelA3.TabIndex = 0;
            this.labelA3.Text = "Мощь третьей армии";
            // 
            // labelA4
            // 
            this.labelA4.AutoSize = true;
            this.labelA4.Location = new System.Drawing.Point(10, 160);
            this.labelA4.Name = "labelA4";
            this.labelA4.Size = new System.Drawing.Size(126, 13);
            this.labelA4.TabIndex = 0;
            this.labelA4.Text = "Мощь четвертой армии";
            // 
            // labelA5
            // 
            this.labelA5.AutoSize = true;
            this.labelA5.Location = new System.Drawing.Point(10, 200);
            this.labelA5.Name = "labelA5";
            this.labelA5.Size = new System.Drawing.Size(104, 13);
            this.labelA5.TabIndex = 0;
            this.labelA5.Text = "Мощь пятой армии";
            // 
            // labelAmax
            // 
            this.labelAmax.AutoSize = true;
            this.labelAmax.Location = new System.Drawing.Point(10, 290);
            this.labelAmax.Name = "labelAmax";
            this.labelAmax.Size = new System.Drawing.Size(177, 13);
            this.labelAmax.TabIndex = 0;
            this.labelAmax.Text = "Максимальная мощь всех армий";
            // 
            // labelAmaxnum
            // 
            this.labelAmaxnum.AutoSize = true;
            this.labelAmaxnum.Location = new System.Drawing.Point(250, 290);
            this.labelAmaxnum.Name = "labelAmaxnum";
            this.labelAmaxnum.Size = new System.Drawing.Size(25, 13);
            this.labelAmaxnum.TabIndex = 0;
            this.labelAmaxnum.Text = "100";
            // 
            // trackBarA1
            // 
            this.trackBarA1.AutoSize = false;
            this.trackBarA1.Location = new System.Drawing.Point(140, 40);
            this.trackBarA1.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarA1.Maximum = 100;
            this.trackBarA1.Minimum = 1;
            this.trackBarA1.Name = "trackBarA1";
            this.trackBarA1.Size = new System.Drawing.Size(235, 20);
            this.trackBarA1.SmallChange = 5;
            this.trackBarA1.TabIndex = 1;
            this.trackBarA1.Value = 1;
            this.trackBarA1.Scroll += new System.EventHandler(this.trackBarA1_Scroll);
            // 
            // trackBarA2
            // 
            this.trackBarA2.AutoSize = false;
            this.trackBarA2.Location = new System.Drawing.Point(140, 80);
            this.trackBarA2.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarA2.Maximum = 100;
            this.trackBarA2.Minimum = 1;
            this.trackBarA2.Name = "trackBarA2";
            this.trackBarA2.Size = new System.Drawing.Size(235, 20);
            this.trackBarA2.SmallChange = 5;
            this.trackBarA2.TabIndex = 1;
            this.trackBarA2.Value = 1;
            this.trackBarA2.Scroll += new System.EventHandler(this.trackBarA2_Scroll);
            // 
            // trackBarA3
            // 
            this.trackBarA3.AutoSize = false;
            this.trackBarA3.Location = new System.Drawing.Point(140, 120);
            this.trackBarA3.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarA3.Maximum = 100;
            this.trackBarA3.Minimum = 1;
            this.trackBarA3.Name = "trackBarA3";
            this.trackBarA3.Size = new System.Drawing.Size(235, 20);
            this.trackBarA3.SmallChange = 5;
            this.trackBarA3.TabIndex = 1;
            this.trackBarA3.Value = 1;
            this.trackBarA3.Scroll += new System.EventHandler(this.trackBarA3_Scroll);
            // 
            // trackBarA4
            // 
            this.trackBarA4.AutoSize = false;
            this.trackBarA4.Location = new System.Drawing.Point(140, 160);
            this.trackBarA4.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarA4.Maximum = 100;
            this.trackBarA4.Minimum = 1;
            this.trackBarA4.Name = "trackBarA4";
            this.trackBarA4.Size = new System.Drawing.Size(235, 20);
            this.trackBarA4.SmallChange = 5;
            this.trackBarA4.TabIndex = 1;
            this.trackBarA4.Value = 1;
            this.trackBarA4.Scroll += new System.EventHandler(this.trackBarA4_Scroll);
            // 
            // trackBarA5
            // 
            this.trackBarA5.AutoSize = false;
            this.trackBarA5.Location = new System.Drawing.Point(140, 200);
            this.trackBarA5.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarA5.Maximum = 100;
            this.trackBarA5.Minimum = 1;
            this.trackBarA5.Name = "trackBarA5";
            this.trackBarA5.Size = new System.Drawing.Size(235, 20);
            this.trackBarA5.SmallChange = 5;
            this.trackBarA5.TabIndex = 1;
            this.trackBarA5.Value = 1;
            this.trackBarA5.Scroll += new System.EventHandler(this.trackBarA5_Scroll);
            // 
            // labelAcur
            // 
            this.labelAcur.AutoSize = true;
            this.labelAcur.Location = new System.Drawing.Point(10, 250);
            this.labelAcur.Name = "labelAcur";
            this.labelAcur.Size = new System.Drawing.Size(145, 13);
            this.labelAcur.TabIndex = 0;
            this.labelAcur.Text = "Текущая мощь всех армий";
            // 
            // labelAcurnum
            // 
            this.labelAcurnum.AutoSize = true;
            this.labelAcurnum.Location = new System.Drawing.Point(250, 250);
            this.labelAcurnum.Name = "labelAcurnum";
            this.labelAcurnum.Size = new System.Drawing.Size(13, 13);
            this.labelAcurnum.TabIndex = 0;
            this.labelAcurnum.Text = "5";
            // 
            // labelA1num
            // 
            this.labelA1num.AutoSize = true;
            this.labelA1num.Location = new System.Drawing.Point(400, 40);
            this.labelA1num.Name = "labelA1num";
            this.labelA1num.Size = new System.Drawing.Size(13, 13);
            this.labelA1num.TabIndex = 0;
            this.labelA1num.Text = "1";
            // 
            // labelA2num
            // 
            this.labelA2num.AutoSize = true;
            this.labelA2num.Location = new System.Drawing.Point(400, 80);
            this.labelA2num.Name = "labelA2num";
            this.labelA2num.Size = new System.Drawing.Size(13, 13);
            this.labelA2num.TabIndex = 0;
            this.labelA2num.Text = "1";
            // 
            // labelA3num
            // 
            this.labelA3num.AutoSize = true;
            this.labelA3num.Location = new System.Drawing.Point(400, 120);
            this.labelA3num.Name = "labelA3num";
            this.labelA3num.Size = new System.Drawing.Size(13, 13);
            this.labelA3num.TabIndex = 0;
            this.labelA3num.Text = "1";
            // 
            // labelA4num
            // 
            this.labelA4num.AutoSize = true;
            this.labelA4num.Location = new System.Drawing.Point(400, 160);
            this.labelA4num.Name = "labelA4num";
            this.labelA4num.Size = new System.Drawing.Size(13, 13);
            this.labelA4num.TabIndex = 0;
            this.labelA4num.Text = "1";
            // 
            // labelA5num
            // 
            this.labelA5num.AutoSize = true;
            this.labelA5num.Location = new System.Drawing.Point(400, 200);
            this.labelA5num.Name = "labelA5num";
            this.labelA5num.Size = new System.Drawing.Size(13, 13);
            this.labelA5num.TabIndex = 0;
            this.labelA5num.Text = "1";
            // 
            // buttonAReady
            // 
            this.buttonAReady.Location = new System.Drawing.Point(347, 265);
            this.buttonAReady.Name = "buttonAReady";
            this.buttonAReady.Size = new System.Drawing.Size(103, 23);
            this.buttonAReady.TabIndex = 2;
            this.buttonAReady.Text = "Готово";
            this.buttonAReady.UseVisualStyleBackColor = true;
            this.buttonAReady.Click += new System.EventHandler(this.buttonAReady_Click);
            // 
            // labelAmaxone
            // 
            this.labelAmaxone.AutoSize = true;
            this.labelAmaxone.Location = new System.Drawing.Point(10, 270);
            this.labelAmaxone.Name = "labelAmaxone";
            this.labelAmaxone.Size = new System.Drawing.Size(184, 13);
            this.labelAmaxone.TabIndex = 0;
            this.labelAmaxone.Text = "Максимальная мощь одной армии";
            // 
            // labelAmaxonenum
            // 
            this.labelAmaxonenum.AutoSize = true;
            this.labelAmaxonenum.Location = new System.Drawing.Point(250, 270);
            this.labelAmaxonenum.Name = "labelAmaxonenum";
            this.labelAmaxonenum.Size = new System.Drawing.Size(19, 13);
            this.labelAmaxonenum.TabIndex = 0;
            this.labelAmaxonenum.Text = "50";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 451);
            this.Controls.Add(this.gameInitGroup);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GameWindow";
            this.Text = "GameWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gameInitGroup.ResumeLayout(false);
            this.gameInitGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA5)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gameInitGroup;
        private System.Windows.Forms.Button buttonAReady;
        private System.Windows.Forms.TrackBar trackBarA5;
        private System.Windows.Forms.TrackBar trackBarA4;
        private System.Windows.Forms.TrackBar trackBarA3;
        private System.Windows.Forms.TrackBar trackBarA2;
        private System.Windows.Forms.TrackBar trackBarA1;
        private System.Windows.Forms.Label labelA5num;
        private System.Windows.Forms.Label labelA4num;
        private System.Windows.Forms.Label labelA3num;
        private System.Windows.Forms.Label labelA2num;
        private System.Windows.Forms.Label labelA1num;
        private System.Windows.Forms.Label labelAcurnum;
        private System.Windows.Forms.Label labelAmaxonenum;
        private System.Windows.Forms.Label labelAmaxnum;
        private System.Windows.Forms.Label labelAcur;
        private System.Windows.Forms.Label labelAmaxone;
        private System.Windows.Forms.Label labelAmax;
        private System.Windows.Forms.Label labelA5;
        private System.Windows.Forms.Label labelA4;
        private System.Windows.Forms.Label labelA3;
        private System.Windows.Forms.Label labelA2;
        private System.Windows.Forms.Label labelA1;
    }
}