namespace LazyGeneral
{
	partial class MainWindow
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.buttonStartGame = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pictureBox1.Image = global::LazyGeneral.Properties.Resources.mainmenu;
			this.pictureBox1.InitialImage = global::LazyGeneral.Properties.Resources.mainmenu;
			this.pictureBox1.Location = new System.Drawing.Point(-2, -3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(803, 454);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// buttonStartGame
			// 
			this.buttonStartGame.BackColor = System.Drawing.SystemColors.Control;
			this.buttonStartGame.Font = new System.Drawing.Font("Stencil Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonStartGame.Location = new System.Drawing.Point(317, 77);
			this.buttonStartGame.Name = "buttonStartGame";
			this.buttonStartGame.Size = new System.Drawing.Size(176, 45);
			this.buttonStartGame.TabIndex = 1;
			this.buttonStartGame.Text = "Начать игру";
			this.buttonStartGame.UseVisualStyleBackColor = false;
			this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.BackColor = System.Drawing.SystemColors.Control;
			this.buttonExit.Font = new System.Drawing.Font("Stencil Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonExit.Location = new System.Drawing.Point(317, 332);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(176, 45);
			this.buttonExit.TabIndex = 1;
			this.buttonExit.Text = "Выход";
			this.buttonExit.UseVisualStyleBackColor = false;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonStartGame);
			this.Controls.Add(this.pictureBox1);
			this.Name = "MainWindow";
			this.Text = "LazyGenerals";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button buttonStartGame;
		private System.Windows.Forms.Button buttonExit;
	}
}

