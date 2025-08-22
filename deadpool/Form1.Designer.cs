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
            lblStatus = new Label();
            rtbLog = new RichTextBox();
            btnFullBackup = new Button();
            btnDiffBackup = new Button();
            btnLogBackup = new Button();
            btnRestoreProduction = new Button();
            btnRestoreFailover = new Button();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(45, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Status:";
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            rtbLog.Location = new Point(12, 37);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(652, 252);
            rtbLog.TabIndex = 1;
            rtbLog.Text = "";
            // 
            // btnFullBackup
            // 
            btnFullBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFullBackup.Location = new Point(12, 295);
            btnFullBackup.Name = "btnFullBackup";
            btnFullBackup.Size = new Size(120, 30);
            btnFullBackup.TabIndex = 2;
            btnFullBackup.Text = "Full Backup Now";
            btnFullBackup.UseVisualStyleBackColor = true;
            btnFullBackup.Click += btnFullBackup_Click;
            // 
            // btnDiffBackup
            // 
            btnDiffBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDiffBackup.Location = new Point(138, 295);
            btnDiffBackup.Name = "btnDiffBackup";
            btnDiffBackup.Size = new Size(120, 30);
            btnDiffBackup.TabIndex = 3;
            btnDiffBackup.Text = "Diff Backup Now";
            btnDiffBackup.UseVisualStyleBackColor = true;
            btnDiffBackup.Click += btnDiffBackup_Click;
            // 
            // btnLogBackup
            // 
            btnLogBackup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogBackup.Location = new Point(264, 295);
            btnLogBackup.Name = "btnLogBackup";
            btnLogBackup.Size = new Size(120, 30);
            btnLogBackup.TabIndex = 4;
            btnLogBackup.Text = "Log Backup  Now";
            btnLogBackup.UseVisualStyleBackColor = true;
            btnLogBackup.Click += btnLogBackup_Click;
            // 
            // btnRestoreProduction
            // 
            btnRestoreProduction.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRestoreProduction.BackColor = Color.FromArgb(192, 255, 192);
            btnRestoreProduction.Location = new Point(390, 295);
            btnRestoreProduction.Name = "btnRestoreProduction";
            btnRestoreProduction.Size = new Size(130, 30);
            btnRestoreProduction.TabIndex = 5;
            btnRestoreProduction.Text = "Restore to Production";
            btnRestoreProduction.UseVisualStyleBackColor = false;
            btnRestoreProduction.Click += btnRestoreProduction_Click;
            // 
            // btnRestoreFailover
            // 
            btnRestoreFailover.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRestoreFailover.BackColor = Color.FromArgb(255, 224, 192);
            btnRestoreFailover.Location = new Point(526, 295);
            btnRestoreFailover.Name = "btnRestoreFailover";
            btnRestoreFailover.Size = new Size(138, 30);
            btnRestoreFailover.TabIndex = 6;
            btnRestoreFailover.Text = "Restore to Failover";
            btnRestoreFailover.UseVisualStyleBackColor = false;
            btnRestoreFailover.Click += btnRestoreFailover_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(676, 337);
            Controls.Add(btnRestoreFailover);
            Controls.Add(btnRestoreProduction);
            Controls.Add(btnLogBackup);
            Controls.Add(btnDiffBackup);
            Controls.Add(btnFullBackup);
            Controls.Add(rtbLog);
            Controls.Add(lblStatus);
            Name = "Form1";
            Text = "SQL Server Backup Scheduler";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private RichTextBox rtbLog;
        private Button btnFullBackup;
        private Button btnDiffBackup;
        private Button btnLogBackup;
        //private Button btnRestoreProduction;
        //private Button button2;
        private Button btnRestoreProduction;
        private Button btnRestoreFailover;
    }
}