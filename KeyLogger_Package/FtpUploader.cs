using System;
using System.IO;
using System.Net;
using System.Text;

namespace GameOdev
{
    /// <summary>
    /// FTP ile log dosyası yükleme sınıfı
    /// Eğitim amaçlı - izinli kullanım için
    /// </summary>
    public class FtpLogUploader
    {
        private string _ftpServer;
        private string _username;
        private string _password;
        private string _remoteDirectory;

        public FtpLogUploader(string ftpServer, string username, string password, string remoteDirectory = "/logs/")
        {
            _ftpServer = ftpServer;
            _username = username;
            _password = password;
            _remoteDirectory = remoteDirectory;
        }

        /// <summary>
        /// Log dosyasını FTP sunucusuna yükle
        /// </summary>
        public bool UploadLogFile(string localFilePath)
        {
            try
            {
                string fileName = Path.GetFileName(localFilePath);
                string remoteFileName = $"{Environment.MachineName}_{Environment.UserName}_{DateTime.Now:yyyyMMdd_HHmmss}_{fileName}";
                string ftpUrl = $"ftp://{_ftpServer}{_remoteDirectory}{remoteFileName}";

                // FTP isteği oluştur
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(_username, _password);
                request.UseBinary = true;

                // Dosyayı oku ve yükle
                byte[] fileContents = File.ReadAllBytes(localFilePath);
                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                // Yanıt al
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"FTP Upload Status: {response.StatusDescription}");
                    return response.StatusCode == FtpStatusCode.ClosingData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FTP yükleme hatası: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Log içeriğini doğrudan FTP'ye yaz
        /// </summary>
        public bool UploadLogContent(string logContent, string fileName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = $"log_{Environment.MachineName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                }

                string ftpUrl = $"ftp://{_ftpServer}{_remoteDirectory}{fileName}";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(_username, _password);

                byte[] contentBytes = Encoding.UTF8.GetBytes(logContent);
                request.ContentLength = contentBytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(contentBytes, 0, contentBytes.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == FtpStatusCode.ClosingData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FTP içerik yükleme hatası: {ex.Message}");
                return false;
            }
        }
    }
}
