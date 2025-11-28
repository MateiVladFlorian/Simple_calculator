namespace Simple_calculator
{
    partial class SideBar
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
            this.DateCalculation = new System.Windows.Forms.Button();
            this.scientific = new System.Windows.Forms.Button();
            this.standard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DateCalculation
            // 
            this.DateCalculation.BackColor = System.Drawing.Color.LightSteelBlue;
            this.DateCalculation.Location = new System.Drawing.Point(39, 312);
            this.DateCalculation.Name = "DateCalculation";
            this.DateCalculation.Size = new System.Drawing.Size(136, 33);
            this.DateCalculation.TabIndex = 2;
            this.DateCalculation.Text = "Date calculation";
            this.DateCalculation.UseVisualStyleBackColor = false;
            // 
            // scientific
            // 
            this.scientific.BackColor = System.Drawing.Color.LightSteelBlue;
            this.scientific.Location = new System.Drawing.Point(39, 222);
            this.scientific.Name = "scientific";
            this.scientific.Size = new System.Drawing.Size(136, 33);
            this.scientific.TabIndex = 3;
            this.scientific.Text = "Scientific";
            this.scientific.UseVisualStyleBackColor = false;
            // 
            // standard
            // 
            this.standard.BackColor = System.Drawing.Color.LightSteelBlue;
            this.standard.Location = new System.Drawing.Point(39, 125);
            this.standard.Name = "standard";
            this.standard.Size = new System.Drawing.Size(136, 36);
            this.standard.TabIndex = 4;
            this.standard.Text = "Standard";
            this.standard.UseVisualStyleBackColor = false;
            // 
            // SideBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.standard);
            this.Controls.Add(this.scientific);
            this.Controls.Add(this.DateCalculation);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SideBar";
            this.Size = new System.Drawing.Size(216, 472);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button DateCalculation;
        private System.Windows.Forms.Button scientific;
        private System.Windows.Forms.Button standard;
    }
}
