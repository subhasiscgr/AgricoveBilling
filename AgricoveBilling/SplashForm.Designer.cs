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
            ((System.ComponentModel.ISupportInitialize)(this.splashlogo)).BeginInit();
            this.SuspendLayout();
            // 
            // splashlogo
            // 
            this.splashlogo.BackColor = System.Drawing.Color.Transparent;
            this.splashlogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splashlogo.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.splashlogo.Image = ((System.Drawing.Image)(resources.GetObject("splashlogo.Image")));
            this.splashlogo.Location = new System.Drawing.Point(0, 0);
            this.splashlogo.Name = "splashlogo";
            this.splashlogo.Size = new System.Drawing.Size(611, 306);
            this.splashlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.splashlogo.TabIndex = 0;
            this.splashlogo.TabStop = false;
            // 
            // loadinglabel
            // 
            this.loadinglabel.BackColor = System.Drawing.Color.Transparent;
            this.loadinglabel.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadinglabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.loadinglabel.Location = new System.Drawing.Point(2, 255);
            this.loadinglabel.Name = "loadinglabel";
            this.loadinglabel.Size = new System.Drawing.Size(609, 23);
            this.loadinglabel.TabIndex = 1;
            this.loadinglabel.Text = "Loading...";
            this.loadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loadinglabel.TextChanged += new System.EventHandler(this.loadinglabel_TextChanged);
            // 
            // SplashForm
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::AgricoveBilling.Properties.Resources.form_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(611, 306);
            this.Controls.Add(this.loadinglabel);
            this.Controls.Add(this.splashlogo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            ((System.ComponentModel.ISupportInitialize)(this.splashlogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox splashlogo;
        private System.Windows.Forms.Label loadinglabel;
    }
}