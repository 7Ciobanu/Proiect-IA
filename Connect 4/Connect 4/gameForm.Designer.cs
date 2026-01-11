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
            btnMenu = new Button();
            btnRestart = new Button();
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
            // gameForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 944);
            Controls.Add(btnRestart);
            Controls.Add(btnMenu);
            Name = "gameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect 4";
            Load += gameForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnMenu;
        private Button btnRestart;
    }
}