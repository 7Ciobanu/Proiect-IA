namespace Connect_4
{
    partial class gameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gameForm));
            btnMenu = new Button();
            btnRestart = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnMenu
            // 
            btnMenu.Location = new Point(1012, 12);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(154, 73);
            btnMenu.TabIndex = 0;
            btnMenu.Text = "Menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(1012, 91);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(154, 73);
            btnRestart.TabIndex = 1;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(428, 12);
            label1.MinimumSize = new Size(323, 200);
            label1.Name = "label1";
            label1.Size = new Size(323, 200);
            label1.TabIndex = 4;
            // 
            // gameForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 944);
            Controls.Add(label1);
            Controls.Add(btnRestart);
            Controls.Add(btnMenu);
            Name = "gameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect 4";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMenu;
        private Button btnRestart;
        private Label label1;
    }
}