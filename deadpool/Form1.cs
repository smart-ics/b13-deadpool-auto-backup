using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Timers;

namespace deadpool;

public partial class Form1 : Form
{
    // Timers for different backup types
    private readonly System.Timers.Timer _checkScheduleTimer;
    private DateTime _lastLogBackupTime = DateTime.MinValue;
    private int _minuteHeartBeat = 0;

    // Configurable settings from App.config
    private string _dbServer1;
    private string _dbName1;
    private string _userId1;
    private string _password1;

    private string _dbServer2;
    private string _dbName2;
    private string _userId2;
    private string _password2;

    private string _backupPath;
    private string _productionRestorePath;
    private string _failoverRestorePath;
    private DayOfWeek _fullBackupDay;
    private TimeSpan _fullBackupTime;
    private TimeSpan _diffBackupTime;
    private int _logBackupIntervalMinutes;

    private int _degRotation;
    private Image _deadpoolOri = (Image)Properties.Resources.arrow6_128.Clone();

    private const string FULLDAY_BACKUP_DAY_DEFAULT = "0"; // Sunday
    private const string FULLDAY_BACKUP_HOUR_DEFAULT = "02:00:00";
    private const string DIFF_BACKUP_HOUR_DEFAULT = "03:00:00";
    private const string LOG_BACKUP_INTERVAL_MINUTES_DEFAULT = "15"; // Every 15 minutes

    public Form1()
    {
        InitializeComponent();

        ReadConfigurationSettings();

        _checkScheduleTimer = new System.Timers.Timer(1000);
        _checkScheduleTimer.Elapsed += CheckScheduleTimer_Elapsed;
        _checkScheduleTimer.AutoReset = true;
        _checkScheduleTimer.Start();
        LogMessage($"Loaded config for {_dbName1} on {_dbServer1}. Monitoring started now...", Color.BlueViolet);
    }

    private void ReadConfigurationSettings()
    {
        _dbServer1 = ConfigurationManager.AppSettings["DatabaseServer1"] ?? "-not configured-";
        _dbName1 = ConfigurationManager.AppSettings["DatabaseName1"] ?? "-not configured-";
        _userId1 = ConfigurationManager.AppSettings["UserId1"] ?? "-not configured-";
        _password1 = ConfigurationManager.AppSettings["Password1"] ?? "-not configured-";

        _dbServer2 = ConfigurationManager.AppSettings["DatabaseServer2"] ?? "-not configured-";
        _dbName2 = ConfigurationManager.AppSettings["DatabaseName2"] ?? "-not configured-";
        _userId2 = ConfigurationManager.AppSettings["UserId2"] ?? "-not configured-";
        _password2 = ConfigurationManager.AppSettings["Password2"] ?? "-not configured-";

        _backupPath = ConfigurationManager.AppSettings["BackupStorageLocation"] ?? "-not configured-";
        _productionRestorePath = ConfigurationManager.AppSettings["ProductionRestorePath"] ?? "-not configured-";
        _failoverRestorePath = ConfigurationManager.AppSettings["FailoverRestorePath"] ?? "-not configured-";

        int fullBackupDayInt = int.Parse(ConfigurationManager.AppSettings["FullBackupDay"] ?? FULLDAY_BACKUP_DAY_DEFAULT);
        _fullBackupDay = (DayOfWeek)fullBackupDayInt;
        _fullBackupTime = TimeSpan.Parse(ConfigurationManager.AppSettings["FullBackupTime"] ?? FULLDAY_BACKUP_HOUR_DEFAULT);
        _diffBackupTime = TimeSpan.Parse(ConfigurationManager.AppSettings["DiffBackupTime"] ?? DIFF_BACKUP_HOUR_DEFAULT);
        _logBackupIntervalMinutes = int.Parse(ConfigurationManager.AppSettings["LogBackupIntervalMinutes"] ?? LOG_BACKUP_INTERVAL_MINUTES_DEFAULT);
        lblStatus.Text = $"Loaded config for {_dbName1} on {_dbServer1}. Monitoring...";

    }

    private async void CheckScheduleTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        DateTime now = DateTime.Now;
        _minuteHeartBeat++;
        if (_minuteHeartBeat == 60)
        {
            _minuteHeartBeat = 0;

            if (now.DayOfWeek == _fullBackupDay && now.TimeOfDay >= _fullBackupTime && now.TimeOfDay < _fullBackupTime.Add(TimeSpan.FromMinutes(1)))
                await ExecuteBackup("FULL");

            else if (now.DayOfWeek != _fullBackupDay && now.TimeOfDay >= _diffBackupTime && now.TimeOfDay < _diffBackupTime.Add(TimeSpan.FromMinutes(1)))
                await ExecuteBackup("DIFF");

            else if ((now - _lastLogBackupTime).TotalMinutes >= _logBackupIntervalMinutes)
                await ExecuteBackup("LOG");
        }
        _degRotation += 6;
        if (_degRotation > 360)
            _degRotation = 0;

        RotateImage(deadpoolPictureBox, _degRotation);
    }

    public void LogMessage2(string message, Color? color = null)
    {
        if (rtbLog.InvokeRequired)
        {
            rtbLog.Invoke(new Action(() => LogMessage2(message, color)));
            return;
        }

        color = color ?? Color.Black;

        rtbLog.SelectionStart = rtbLog.TextLength;
        rtbLog.SelectionLength = 0;
        rtbLog.SelectionColor = color.Value;
        rtbLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
        rtbLog.SelectionColor = rtbLog.ForeColor;
        rtbLog.ScrollToCaret();

        Application.DoEvents();
    }

    public void LogMessage(string message, Color? color = null)
    {
        if (rtbLog.InvokeRequired)
        {
            rtbLog.Invoke(new Action(() => LogMessage(message, color)));
            return;
        }

        color = color ?? Color.Black;

        // Remember the default font
        Font regularFont = rtbLog.Font;               // Segoe UI (whatever the control is set to)
        Font monoFont = new Font("Consolas",       // or "Courier New"
                                    regularFont.Size, // keep the same size
                                    FontStyle.Regular);

        rtbLog.SelectionStart = rtbLog.TextLength;
        rtbLog.SelectionLength = 0;

        // 1) Time stamp in monospace
        rtbLog.SelectionFont = monoFont;
        rtbLog.SelectionColor = Color.Gray;          // or any colour you want
        rtbLog.AppendText($"{DateTime.Now:HH:mm:ss} ");

        // 2) Message in the normal font
        rtbLog.SelectionFont = regularFont;
        rtbLog.SelectionColor = color.Value;
        rtbLog.AppendText($"{message}{Environment.NewLine}");

        // Reset for next append
        rtbLog.SelectionColor = rtbLog.ForeColor;
        rtbLog.SelectionFont = regularFont;
        rtbLog.ScrollToCaret();

        monoFont.Dispose();   // tidy up if you create it every time
    }

    private async Task ExecuteBackup(string backupType)
    {
        string backupFileName;
        string sqlCommandText;
        var color = Color.Black;

        switch (backupType)
        {
            case "FULL":
                backupFileName = $"{_dbName1}_Full_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                sqlCommandText = @$"BACKUP DATABASE [{_dbName1}] TO DISK = N'{Path.Combine(_backupPath, backupFileName)}' WITH COMPRESSION, CHECKSUM, STATS = 5;";
                color = Color.Green;
                break;
            case "DIFF":
                backupFileName = $"{_dbName1}_Diff_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                sqlCommandText = @$"BACKUP DATABASE [{_dbName1}] TO DISK = N'{Path.Combine(_backupPath, backupFileName)}' WITH DIFFERENTIAL, COMPRESSION, CHECKSUM, STATS = 5;";
                color = Color.Blue;
                break;
            case "LOG":
                backupFileName = $"{_dbName1}_Log_{DateTime.Now:yyyyMMdd_HHmmss}.trn";
                sqlCommandText = @$"BACKUP LOG [{_dbName1}] TO DISK = N'{Path.Combine(_backupPath, backupFileName)}' WITH COMPRESSION, STATS = 5;";
                _lastLogBackupTime = DateTime.Now; // Update the last run time
                break;
            default:
                throw new ArgumentException("Invalid backup type.");
        }

        string connectionString = $"Server={_dbServer1};Database={_dbName1};User Id={_userId1};Password={_password1};TrustServerCertificate=True";

        this.Invoke((MethodInvoker)delegate
        {
            lblStatus.Text = $"Starting {backupType} backup... {DateTime.Now:HH:mm:ss}";
            var log = $"Starting {backupType} backup... ";
            LogMessage(log);

        });

        try
        {
            if (sqlCommandText == null)
                throw new ArgumentNullException(nameof(sqlCommandText), "sqlCommandText is NULL before passing to SqlCommand!");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
            {
                connection.Open();
                command.CommandTimeout = 0;
                await command.ExecuteNonQueryAsync();
            }
            this.Invoke((MethodInvoker)delegate
            {
                var log = $"Done: {backupFileName}";
                LogMessage(log, color);
                lblStatus.Text = $"Last {backupType} backup succeeded. {DateTime.Now:HH:mm:ss}";
            });
        }
        catch (Exception ex)
        {
            this.Invoke((MethodInvoker)delegate
            {
                var log = $"{backupType} backup FAILED: {ex.Message}";
                LogMessage(log, Color.Red);
                lblStatus.Text = $"Last {backupType} backup FAILED! {DateTime.Now:HH:mm:ss}";
            });
        }
    }

    private async Task ExecuteRestore(string targetServer, string targetDbName, string userId, string password, string targetDataPath, string targetLogPath)
    {
        this.Invoke((MethodInvoker)delegate
        {
            lblStatus.Text = $"Starting restore to {targetDbName} on {targetServer}... {DateTime.Now:HH:mm:ss}";
            var log = $"Starting restore to {targetDbName} on {targetServer}...";
            LogMessage(log, Color.Orange);

        });

        try
        {
            string connectionString = $"Server={targetServer};User Id={userId};Password={password};TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Step 1: Find the latest full backup file
                var backupFiles = Directory.GetFiles(_backupPath, $"{_dbName1}_Full_*.bak")
                                          .OrderByDescending(f => f)
                                          .ToArray();

                if (backupFiles.Length == 0)
                {
                    throw new FileNotFoundException("No full backup files found for restore.");
                }

                string latestFullBackup = backupFiles[0];

                // Step 2: Get file list from backup to understand the structure
                var fileList = GetBackupFileList(connection, latestFullBackup);

                // Step 3: Build MOVE clauses for each file
                string moveClauses = BuildMoveClauses(fileList, targetDataPath, targetLogPath);

                // Step 4: Restore the full backup WITH NORECOVERY
                string restoreFullCommand = $@"
                RESTORE DATABASE [{targetDbName}] 
                FROM DISK = N'{latestFullBackup}'
                WITH NORECOVERY, REPLACE{moveClauses}";

                using (SqlCommand command = new SqlCommand(restoreFullCommand, connection))
                {
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                }

                // Step 5: Find and restore the latest differential backup (if any)
                var diffFiles = Directory.GetFiles(_backupPath, $"{_dbName1}_Diff_*.bak")
                                       .OrderByDescending(f => f)
                                       .ToArray();

                if (diffFiles.Length > 0)
                {
                    string latestDiffBackup = diffFiles[0];
                    string restoreDiffCommand = $@"
                    RESTORE DATABASE [{targetDbName}] 
                    FROM DISK = N'{latestDiffBackup}'
                    WITH NORECOVERY";

                    using (SqlCommand command = new SqlCommand(restoreDiffCommand, connection))
                    {
                        command.CommandTimeout = 0;
                        await command.ExecuteNonQueryAsync();
                    }
                }

                // Step 6: Find and restore all transaction log backups created after the chosen backups
                var logFiles = Directory.GetFiles(_backupPath, $"{_dbName1}_Log_*.trn")
                                      .OrderBy(f => f)
                                      .ToArray();

                // Get creation time of the latest backup used
                DateTime latestBackupTime = GetBackupCreationTime(latestFullBackup);
                if (diffFiles.Length > 0)
                {
                    DateTime diffBackupTime = GetBackupCreationTime(diffFiles[0]);
                    if (diffBackupTime > latestBackupTime)
                        latestBackupTime = diffBackupTime;
                }

                foreach (var logFile in logFiles)
                {
                    // Only restore logs that are newer than our chosen full/diff backups
                    if (GetBackupCreationTime(logFile) > latestBackupTime)
                    {
                        string restoreLogCommand = $@"
                        RESTORE LOG [{targetDbName}] 
                        FROM DISK = N'{logFile}'
                        WITH NORECOVERY";

                        using (SqlCommand command = new SqlCommand(restoreLogCommand, connection))
                        {
                            command.CommandTimeout = 0;
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Step 7: Final recovery
                string recoverCommand = $"RESTORE DATABASE [{targetDbName}] WITH RECOVERY";
                using (SqlCommand command = new SqlCommand(recoverCommand, connection))
                {
                    command.CommandTimeout = 0;
                    await command.ExecuteNonQueryAsync();
                }
            }

            this.Invoke((MethodInvoker)delegate
            {
                var log = $"Restore to {targetDbName} SUCCESS";
                LogMessage(log, Color.DarkOrange);
                lblStatus.Text = $"Restore to {targetDbName} completed successfully. {DateTime.Now:HH:mm:ss}";
            });
        }
        catch (Exception ex)
        {
            this.Invoke((MethodInvoker)delegate
            {
                var log = $"Restore to {targetDbName} FAILED: {ex.Message}";
                if (ex.InnerException != null)
                {
                    log += $"Inner Exception: {ex.InnerException.Message}";
                }
                LogMessage(log, Color.Red);
                lblStatus.Text = $"Restore to {targetDbName} FAILED! {DateTime.Now:HH:mm:ss}";
            });
        }
    }

    private List<BackupFileInfo> GetBackupFileList(SqlConnection connection, string backupPath)
    {
        var fileList = new List<BackupFileInfo>();

        string fileListQuery = $"RESTORE FILELISTONLY FROM DISK = N'{backupPath}'";

        using (SqlCommand command = new SqlCommand(fileListQuery, connection))
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var fileInfo = new BackupFileInfo
                {
                    LogicalName = reader["LogicalName"].ToString(),
                    PhysicalName = reader["PhysicalName"].ToString(),
                    Type = reader["Type"].ToString(),
                    FileGroupName = reader["FileGroupName"].ToString()
                };
                fileList.Add(fileInfo);
            }
        }

        return fileList;
    }

    private string BuildMoveClauses(List<BackupFileInfo> fileList, string targetDataPath, string targetLogPath)
    {
        var moveClauses = new StringBuilder();

        foreach (var file in fileList)
        {
            string newPhysicalPath;

            if (file.Type == "D") // Data file
            {
                string fileName = Path.GetFileName(file.PhysicalName);
                newPhysicalPath = Path.Combine(targetDataPath, fileName);
            }
            else if (file.Type == "L") // Log file
            {
                string fileName = Path.GetFileName(file.PhysicalName);
                newPhysicalPath = Path.Combine(targetLogPath, fileName);
            }
            else
            {
                continue; // Skip other file types
            }

            moveClauses.Append($", MOVE N'{file.LogicalName}' TO N'{newPhysicalPath}'");
        }

        return moveClauses.ToString();
    }

    private DateTime GetBackupCreationTime(string backupFilePath)
    {
        // Extract timestamp from filename: DatabaseName_Type_yyyyMMdd_HHmmss.ext
        string fileName = Path.GetFileNameWithoutExtension(backupFilePath);
        string[] parts = fileName.Split('_');

        if (parts.Length >= 4)
        {
            string datePart = parts[parts.Length - 2];
            string timePart = parts[parts.Length - 1];

            if (DateTime.TryParseExact($"{datePart}_{timePart}", "yyyyMMdd_HHmmss",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
        }

        // Fallback: use file creation time
        return File.GetCreationTime(backupFilePath);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        _checkScheduleTimer.Stop();
        _checkScheduleTimer.Dispose();
    }

    private async void btnFullBackup_Click(object sender, EventArgs e)
    {
        ReadConfigurationSettings();
        var log = $"Starting FULL backup.... ";
        LogMessage(log, Color.Green);
        btnFullBackup.Enabled = false;
        try
        {
            await ExecuteBackup("FULL");
        }
        finally
        {
            btnFullBackup.Enabled = true;
        }
    }

    private async void btnDiffBackup_Click(object sender, EventArgs e)
    {
        ReadConfigurationSettings();
        var log = $"Starting DIFF backup.... ";
        LogMessage(log, Color.Blue);
        btnDiffBackup.Enabled = false;
        try
        {
            await ExecuteBackup("DIFF");
        }
        finally
        {
            btnDiffBackup.Enabled = true;
        }
    }

    private async void btnLogBackup_Click(object sender, EventArgs e)
    {
        ReadConfigurationSettings();
        var log = $"Starting LOG backup.... ";
        LogMessage(log, Color.Black);
        btnLogBackup.Enabled = false;
        try
        {
            await ExecuteBackup("LOG");
        }
        finally
        {
            btnLogBackup.Enabled = true;
        }
    }

    private async void btnRestoreProduction_Click(object sender, EventArgs e)
    {
        ReadConfigurationSettings();

        var msg = "Restore Database From\n" +
                  $"Backup File: {Path.Combine(_backupPath, $"{_dbName1}_Full_*.bak")}\n\n" +
                  "To:\n" +
                  $"Production Server: {_dbServer1}\n" +
                  $"Database: {_dbName1}\n" +
                  $"Restore Path: {_productionRestorePath}\n\n" +
                  "Pastikan data di atas benar. Tekan OK untuk lanjut proses.";
        if (MessageBox.Show(msg, "Confirm Restore", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            return;

        btnRestoreProduction.Enabled = false;
        try
        {
            var log = $"Starting restore to {_dbName1} on {_dbServer1}...";
            LogMessage(log, Color.DarkOrange);
            await ExecuteRestore(_dbServer1, _dbName1, _userId1, _password1, _productionRestorePath, _productionRestorePath);
        }
        finally
        {
            btnRestoreProduction.Enabled = true;
        }
    }

    private async void btnRestoreFailover_Click(object sender, EventArgs e)
    {
        ReadConfigurationSettings();

        var msg = "Restore Database From\n" +
                  $"Backup File: {Path.Combine(_backupPath, $"{_dbName1}_Full_*.bak")}\n\n" +
                  "To:\n" +
                  $"Failover Server: {_dbServer2}\n" +
                  $"Database: {_dbName2}\n" +
                  $"Restore Path: {_failoverRestorePath}\n\n" +
                  "Pastikan data di atas benar. Tekan OK untuk lanjut proses.";
        if (MessageBox.Show(msg, "Confirm Restore", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            return;

        btnRestoreFailover.Enabled = false;
        try
        {
            var log = $"Starting restore to {_dbName2} on {_dbServer2}...";
            LogMessage(log, Color.DarkOrange);
            await ExecuteRestore(_dbServer2, _dbName2, _userId2, _password2, _failoverRestorePath, _failoverRestorePath);
        }
        finally
        {
            btnRestoreFailover.Enabled = true;
        }
    }

    private void RotateImage(PictureBox pictureBox, float angle)
    {
        if (pictureBox.Image == null) return;

        // Clone the original image to avoid "in use" exception
        using (Bitmap original = new Bitmap(_deadpoolOri))
        {
            // Create a new empty bitmap to hold rotated image
            Bitmap rotated = new Bitmap(original.Width, original.Height);
            rotated.SetResolution(original.HorizontalResolution, original.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotated))
            {
                g.TranslateTransform(rotated.Width / 2f, rotated.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-rotated.Width / 2f, -rotated.Height / 2f);

                g.DrawImage(original, new Point(0, 0));
            }

            // Replace image in PictureBox
            pictureBox.Image = rotated;
        }
    }

    private void ShowBackupFileButton_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", _backupPath);
    }
}

// Helper class to store backup file information
public class BackupFileInfo
{
    public string LogicalName { get; set; }
    public string PhysicalName { get; set; }
    public string Type { get; set; } // D = Data, L = Log
    public string FileGroupName { get; set; }
}
