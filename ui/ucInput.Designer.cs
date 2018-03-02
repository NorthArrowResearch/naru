namespace naru.ui
{
    partial class ucInput
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.cmdAddToMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(0, 1);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(102, 20);
            this.txtPath.TabIndex = 0;
            this.txtPath.TabStop = false;
            // 
            // cmdSelect
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Image = global::naru.Properties.Resources.add_data;
            this.cmdBrowse.Location = new System.Drawing.Point(108, 0);
            this.cmdBrowse.Name = "cmdSelect";
            this.cmdBrowse.Size = new System.Drawing.Size(23, 23);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // cmdAddToMap
            // 
            this.cmdAddToMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddToMap.Image = global::naru.Properties.Resources.AddToMap;
            this.cmdAddToMap.Location = new System.Drawing.Point(135, 0);
            this.cmdAddToMap.Name = "cmdAddToMap";
            this.cmdAddToMap.Size = new System.Drawing.Size(23, 23);
            this.cmdAddToMap.TabIndex = 3;
            this.cmdAddToMap.UseVisualStyleBackColor = true;
            this.cmdAddToMap.Click += new System.EventHandler(this.cmdAddToMap_Click);
            // 
            // ucInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.cmdAddToMap);
            this.Controls.Add(this.txtPath);
            this.Name = "ucInput";
            this.Size = new System.Drawing.Size(158, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAddToMap;
        protected System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button cmdBrowse;
    }
}
