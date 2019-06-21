namespace AgricoveBilling
{
    partial class Printpreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Printpreview));
            this.print_crystalreport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // print_crystalreport
            // 
            this.print_crystalreport.ActiveViewIndex = -1;
            this.print_crystalreport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.print_crystalreport.Cursor = System.Windows.Forms.Cursors.Default;
            this.print_crystalreport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.print_crystalreport.EnableDrillDown = false;
            this.print_crystalreport.Location = new System.Drawing.Point(0, 0);
            this.print_crystalreport.Name = "print_crystalreport";
            this.print_crystalreport.ShowCloseButton = false;
            this.print_crystalreport.ShowCopyButton = false;
            this.print_crystalreport.ShowGroupTreeButton = false;
            this.print_crystalreport.ShowLogo = false;
            this.print_crystalreport.ShowParameterPanelButton = false;
            this.print_crystalreport.Size = new System.Drawing.Size(555, 692);
            this.print_crystalreport.TabIndex = 0;
            this.print_crystalreport.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // Printpreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 692);
            this.Controls.Add(this.print_crystalreport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Printpreview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Preview";
            this.Load += new System.EventHandler(this.Printpreview_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer print_crystalreport;
    }
}