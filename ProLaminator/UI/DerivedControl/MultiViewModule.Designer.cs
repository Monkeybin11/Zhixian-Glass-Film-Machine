namespace ProLaminator.UI.DerivedControl
{
    partial class MultiViewModule
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
            this.pcRoot = new DevExpress.XtraEditors.PanelControl();
            this.tbllpRoot = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pcRoot)).BeginInit();
            this.pcRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcRoot
            // 
            this.pcRoot.Controls.Add(this.tbllpRoot);
            this.pcRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcRoot.Location = new System.Drawing.Point(0, 0);
            this.pcRoot.Name = "pcRoot";
            this.pcRoot.Size = new System.Drawing.Size(1146, 762);
            this.pcRoot.TabIndex = 0;
            // 
            // tbllpRoot
            // 
            this.tbllpRoot.ColumnCount = 2;
            this.tbllpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbllpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbllpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbllpRoot.Location = new System.Drawing.Point(2, 2);
            this.tbllpRoot.Name = "tbllpRoot";
            this.tbllpRoot.RowCount = 2;
            this.tbllpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbllpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbllpRoot.Size = new System.Drawing.Size(1142, 758);
            this.tbllpRoot.TabIndex = 0;
            // 
            // MultiViewModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.Controls.Add(this.pcRoot);
            this.Name = "MultiViewModule";
            this.Size = new System.Drawing.Size(1146, 762);
            ((System.ComponentModel.ISupportInitialize)(this.pcRoot)).EndInit();
            this.pcRoot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.PanelControl pcRoot;
        public System.Windows.Forms.TableLayoutPanel tbllpRoot;
    }
}
