namespace AgricoveBilling
{
    partial class SplashForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            this.splashlogo = new System.Windows.Forms.PictureBox();
            this.loadinglabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splashlogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splashlogo
            // 
            this.splashlogo.BackColor = System.Drawing.Color.Transparent;
            this.splashlogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splashlogo.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.splashlogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splashlogo.Image = global::AgricoveBilling.Properties.Resources.agricove_text_only;
            this.splashlogo.Location = new System.Drawing.Point(0, 0);
            this.splashlogo.Name = "splashlogo";
            this.splashlogo.Size = new System.Drawing.Size(817, 472);
            this.splashlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.splashlogo.TabIndex = 0;
            this.splashlogo.TabStop = false;
            // 
            // loadinglabel
            // 
            this.loadinglabel.BackColor = System.Drawing.Color.Transparent;
            this.loadinglabel.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadinglabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.loadinglabel.Location = new System.Drawing.Point(12, 302);
            this.loadinglabel.Name = "loadinglabel";
            this.loadinglabel.Size = new System.Drawing.Size(793, 23);
            this.loadinglabel.TabIndex = 1;
            this.loadinglabel.Text = "Loading...";
            this.loadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loadinglabel.TextChanged += new System.EventHandler(this.loadinglabel_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AgricoveBilling.Properties.Resources.agricove_logo_big;
            this.pictureBox1.Location = new System.Drawing.Point(0, -14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(353, 208);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // SplashForm
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::AgricoveBilling.Properties.Resources.form_background_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(817, 472);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.loadinglabel);
            this.Controls.Add(this.splashlogo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.SystemColors.ControlDarkDark;
            ((System.ComponentModel.ISupportInitialize)(this.splashlogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox splashlogo;
        private System.Windows.Forms.Label loadinglabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}