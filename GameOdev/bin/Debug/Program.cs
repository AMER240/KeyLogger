using System;
using System.Windows.Forms;

namespace GameOdev
{
    /// <summary>
    /// Ana program giriş noktası
    /// Eğitim amaçlı keylogger uygulaması
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana giriş noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Windows Forms uygulamasını başlat
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                // Basit uzaktan takip formu başlat (konfigürasyon butonu ile)
                Application.Run(new SimpleRemoteForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uygulama başlatma hatası: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}