namespace deadpool
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnFullBackup = new Button();
            btnDiffBackup = new Button();
            btnLogBackup = new Button();
            btnRestoreProduction = new Button();
            btnRestoreFailover = new Button();
            panel1 = new Panel();
            rtbLog = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            lblStatus = new Label();
            deadpoolPictureBox = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)deadpoolPictureBox).BeginInit();
            SuspendLayout();
            // 
            // btnFullBackup
            // 
            btnFullBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFullBackup.BackColor = Color.FromArgb(63, 193, 201);
            btnFullBackup.FlatAppearance.BorderColor = Color.DimGray;
            btnFullBackup.FlatAppearance.BorderSize = 2;
            btnFullBackup.FlatStyle = FlatStyle.Flat;
            btnFullBackup.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnFullBackup.ForeColor = Color.FromArgb(249, 250, 251);
            btnFullBackup.Location = new Point(7, 327);
            btnFullBackup.Name = "btnFullBackup";
            btnFullBackup.Size = new Size(122, 30);
            btnFullBackup.TabIndex = 0;
            btnFullBackup.Text = "Full Backup";
            btnFullBackup.UseVisualStyleBackColor = false;
            btnFullBackup.Click += btnFullBackup_Click;
            // 
            // btnDiffBackup
            // 
            btnDiffBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDiffBackup.BackColor = Color.FromArgb(63, 193, 201);
            btnDiffBackup.FlatAppearance.BorderColor = Color.DimGray;
            btnDiffBackup.FlatAppearance.BorderSize = 2;
            btnDiffBackup.FlatStyle = FlatStyle.Flat;
            btnDiffBackup.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDiffBackup.ForeColor = Color.FromArgb(249, 250, 251);
            btnDiffBackup.Location = new Point(7, 363);
            btnDiffBackup.Name = "btnDiffBackup";
            btnDiffBackup.Size = new Size(122, 30);
            btnDiffBackup.TabIndex = 1;
            btnDiffBackup.Text = "Diff Backup";
            btnDiffBackup.UseVisualStyleBackColor = false;
            btnDiffBackup.Click += btnDiffBackup_Click;
            // 
            // btnLogBackup
            // 
            btnLogBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogBackup.BackColor = Color.FromArgb(63, 193, 201);
            btnLogBackup.FlatAppearance.BorderColor = Color.DimGray;
            btnLogBackup.FlatAppearance.BorderSize = 2;
            btnLogBackup.FlatStyle = FlatStyle.Flat;
            btnLogBackup.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLogBackup.ForeColor = Color.FromArgb(249, 250, 251);
            btnLogBackup.Location = new Point(7, 398);
            btnLogBackup.Name = "btnLogBackup";
            btnLogBackup.Size = new Size(122, 30);
            btnLogBackup.TabIndex = 2;
            btnLogBackup.Text = "Log Backup";
            btnLogBackup.UseVisualStyleBackColor = false;
            btnLogBackup.Click += btnLogBackup_Click;
            // 
            // btnRestoreProduction
            // 
            btnRestoreProduction.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRestoreProduction.BackColor = Color.FromArgb(252, 81, 133);
            btnRestoreProduction.FlatAppearance.BorderColor = Color.DimGray;
            btnRestoreProduction.FlatAppearance.BorderSize = 2;
            btnRestoreProduction.FlatStyle = FlatStyle.Flat;
            btnRestoreProduction.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnRestoreProduction.ForeColor = Color.FromArgb(248, 249, 250);
            btnRestoreProduction.Location = new Point(141, 327);
            btnRestoreProduction.Name = "btnRestoreProduction";
            btnRestoreProduction.Size = new Size(122, 30);
            btnRestoreProduction.TabIndex = 0;
            btnRestoreProduction.Text = "Production";
            btnRestoreProduction.UseVisualStyleBackColor = false;
            btnRestoreProduction.Click += btnRestoreProduction_Click;
            // 
            // btnRestoreFailover
            // 
            btnRestoreFailover.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRestoreFailover.BackColor = Color.FromArgb(252, 81, 133);
            btnRestoreFailover.FlatAppearance.BorderColor = Color.DimGray;
            btnRestoreFailover.FlatAppearance.BorderSize = 2;
            btnRestoreFailover.FlatStyle = FlatStyle.Flat;
            btnRestoreFailover.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnRestoreFailover.ForeColor = Color.FromArgb(248, 249, 250);
            btnRestoreFailover.Location = new Point(141, 363);
            btnRestoreFailover.Name = "btnRestoreFailover";
            btnRestoreFailover.Size = new Size(122, 30);
            btnRestoreFailover.TabIndex = 1;
            btnRestoreFailover.Text = "Failover";
            btnRestoreFailover.UseVisualStyleBackColor = false;
            btnRestoreFailover.Click += btnRestoreFailover_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(63, 193, 201);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(rtbLog);
            panel1.Location = new Point(6, 28);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(6);
            panel1.Size = new Size(441, 272);
            panel1.TabIndex = 6;
            // 
            // rtbLog
            // 
            rtbLog.BackColor = Color.White;
            rtbLog.BorderStyle = BorderStyle.None;
            rtbLog.Dock = DockStyle.Fill;
            rtbLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            rtbLog.Location = new Point(6, 6);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(427, 258);
            rtbLog.TabIndex = 3;
            rtbLog.Text = "";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(6, 309);
            label1.Name = "label1";
            label1.Size = new Size(93, 13);
            label1.TabIndex = 7;
            label1.Text = "Manual Backup";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(141, 309);
            label2.Name = "label2";
            label2.Size = new Size(111, 13);
            label2.TabIndex = 8;
            label2.Text = "Restore To Server";
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblStatus.ForeColor = Color.WhiteSmoke;
            lblStatus.Location = new Point(7, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(440, 23);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "[Status]";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // deadpoolPictureBox
            // 
            deadpoolPictureBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deadpoolPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            deadpoolPictureBox.Image = Properties.Resources.arrow6_128;
            deadpoolPictureBox.Location = new Point(325, 312);
            deadpoolPictureBox.Name = "deadpoolPictureBox";
            deadpoolPictureBox.Size = new Size(116, 116);
            deadpoolPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            deadpoolPictureBox.TabIndex = 10;
            deadpoolPictureBox.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(54, 79, 107);
            ClientSize = new Size(453, 437);
            Controls.Add(deadpoolPictureBox);
            Controls.Add(lblStatus);
            Controls.Add(btnRestoreFailover);
            Controls.Add(btnRestoreProduction);
            Controls.Add(label2);
            Controls.Add(btnLogBackup);
            Controls.Add(btnDiffBackup);
            Controls.Add(btnFullBackup);
            Controls.Add(label1);
            Controls.Add(panel1);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Deadpool - Auto Backup and One Click Restore";
            FormClosing += Form1_FormClosing;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)deadpoolPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnFullBackup;
        private Button btnDiffBackup;
        private Button btnLogBackup;
        //private Button btnRestoreProduction;
        //private Button button2;
        private Button btnRestoreProduction;
        private Button btnRestoreFailover;
        private Panel panel1;
        private RichTextBox rtbLog;
        private Label label1;
        private Label label2;
        private Label lblStatus;
        private PictureBox deadpoolPictureBox;
    }
}