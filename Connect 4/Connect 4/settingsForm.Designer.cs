namespace Connect_4
{
    partial class settingsForm
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
            labelDepth = new Label();
            numericDepth = new NumericUpDown();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)numericDepth).BeginInit();
            SuspendLayout();
            // 
            // labelDepth
            // 
            labelDepth.AutoSize = true;
            labelDepth.Location = new Point(140, 102);
            labelDepth.Name = "labelDepth";
            labelDepth.Size = new Size(127, 25);
            labelDepth.TabIndex = 0;
            labelDepth.Text = "Search Depth :";
            labelDepth.Click += label1_Click;
            // 
            // numericDepth
            // 
            numericDepth.Location = new Point(273, 100);
            numericDepth.Maximum = new decimal(new int[] { 7, 0, 0, 0 });
            numericDepth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericDepth.Name = "numericDepth";
            numericDepth.Size = new Size(180, 31);
            numericDepth.TabIndex = 1;
            numericDepth.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // btnSave
            // 
            btnSave.Location = new Point(250, 300);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // settingsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 444);
            Controls.Add(btnSave);
            Controls.Add(numericDepth);
            Controls.Add(labelDepth);
            Name = "settingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect 4";
            Load += settingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericDepth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDepth;
        private NumericUpDown numericDepth;
        private Button btnSave;
    }
}