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
                
                // Konfigürasyon butonlu ana formu başlat
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uygulama başlatma hatası: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}