# 🔍 Uzaktan Takip KeyLogger - Kullanım Rehberi

## ⚠️ ÖNEMLİ UYARI
Bu yazılım **sadece eğitim amaçlıdır** ve **açık yazılı izin** alınmış kişilerde kullanılmalıdır. İzinsiz kullanım yasalara aykırıdır.

---

## 🚀 Hızlı Başlangıç

### 1. Uygulamayı Başlatma
```
GameOdev.exe dosyasını çalıştırın
```

### 2. Konfigürasyon
- **⚙️ Konfigürasyon** butonuna tıklayın
- E-posta, FTP ve Webhook ayarlarını yapın
- **💾 Kaydet** butonuna tıklayın

### 3. Uzaktan Takibi Başlatma
- **🚀 Uzaktan Takibi Başlat** butonuna tıklayın
- İzin onayı ekranında **Yes** seçin
- Tuş vuruşları otomatik olarak uzaktan gönderilmeye başlar

---

## 📧 E-posta Konfigürasyonu

### Gmail Kullanımı
```
SMTP Sunucu: smtp.gmail.com
Port: 587
Gönderen E-posta: your-email@gmail.com
Şifre: Gmail App Password (2FA gerekli)
```

### Gmail App Password Alma
1. Google Hesabınızda 2FA'yı etkinleştirin
2. https://myaccount.google.com/apppasswords adresine gidin
3. "Uygulama şifreleri" bölümünden yeni şifre oluşturun
4. Bu şifreyi "Şifre/App Password" alanına girin

### Outlook/Hotmail Kullanımı
```
SMTP Sunucu: smtp-mail.outlook.com
Port: 587
Gönderen E-posta: your-email@outlook.com
Şifre: Normal şifreniz
```

---

## 🌐 FTP Konfigürasyonu

### Ücretsiz FTP Sunucuları
- **FileZilla Server** (kendi sunucunuz)
- **000webhost** (ücretsiz hosting)
- **InfinityFree** (ücretsiz hosting)

### FTP Ayarları
```
FTP Sunucu: ftp.example.com
Kullanıcı: ftp_username
Şifre: ftp_password
Uzak Dizin: /logs/ (varsayılan)
```

---

## 📱 Webhook Konfigürasyonu

### Discord Webhook
1. Discord sunucunuzda bir kanal seçin
2. Kanal ayarları > Entegrasyonlar > Webhooklar
3. "Yeni Webhook" oluşturun
4. Webhook URL'sini kopyalayın

### Slack Webhook
1. Slack workspace'inizde bir kanal seçin
2. Kanal ayarları > Entegrasyonlar > Gelen Webhooklar
3. "Webhook Ekle" butonuna tıklayın
4. Webhook URL'sini kopyalayın

---

## 🔧 Gelişmiş Özellikler

### Otomatik Gönderim
- **E-posta**: 30 dakikada bir otomatik gönderim
- **FTP**: 15 dakikada bir otomatik yükleme
- **Webhook**: Önemli tuşlar için anlık bildirim

### Önemli Tuşlar
Webhook bildirimleri şu tuşlar için gönderilir:
- `[ENTER]` - Enter tuşu
- `[TAB]` - Tab tuşu
- `[ESC]` - Escape tuşu
- `[DELETE]` - Delete tuşu
- `[CTRL]` - Ctrl tuşu
- `[ALT]` - Alt tuşu
- `[WIN]` - Windows tuşu

### Queue Sistemi
- Maksimum 1000 tuş vuruşu bellekte tutulur
- Bellek dolduğunda eski veriler silinir
- Uygulama kapanırken kalan veriler gönderilir

---

## 📊 Veri Formatları

### E-posta İçeriği
```
Konu: 🔍 KeyLogger Log - COMPUTER_NAME - 2025-01-05 20:30
İçerik:
Eğitim amaçlı keylogger log verisi.

🖥️ Bilgisayar: COMPUTER_NAME
👤 Kullanıcı: USER_NAME
🕒 Tarih: 2025-01-05 20:30:15
📊 Tuş Sayısı: 150

⚠️ Bu log sadece eğitim amaçlıdır ve izinli kullanımdır.

📝 Log İçeriği:
[20:30:15] H
[20:30:15] e
[20:30:15] l
[20:30:15] l
[20:30:15] o
```

### FTP Dosya Adı
```
COMPUTER_NAME_USER_NAME_20250105_203015.txt
```

### Webhook Bildirimi
```json
{
  "content": "🔍 **Educational KeyLogger Alert**\n📱 **Computer:** COMPUTER_NAME\n👤 **User:** USER_NAME\n🕒 **Time:** 2025-01-05 20:30:15\n📝 **Message:** ⌨️ **Key Pressed:** [ENTER]\n⚠️ *This is for educational purposes only with proper consent*"
}
```

---

## 🛡️ Güvenlik Önerileri

### Veri Güvenliği
1. **Şifreleme**: Hassas verileri şifreleyin
2. **Güvenli Bağlantı**: SSL/TLS kullanın
3. **Düzenli Temizlik**: Eski logları silin
4. **Erişim Kontrolü**: Sadece yetkili kişiler erişsin

### Sistem Güvenliği
1. **Antivirüs**: Geçici olarak devre dışı bırakın
2. **Firewall**: Gerekli portları açın
3. **Yönetici Yetkisi**: Uygulamayı yönetici olarak çalıştırın
4. **Ağ Güvenliği**: Güvenli ağ bağlantısı kullanın

---

## 🔧 Sorun Giderme

### E-posta Gönderim Sorunları
- **SMTP ayarlarını kontrol edin**
- **2FA ve App Password kullanın**
- **Port 587'nin açık olduğundan emin olun**
- **Gmail'de "Daha az güvenli uygulama erişimi" açın**

### FTP Bağlantı Sorunları
- **FTP sunucu adresini kontrol edin**
- **Kullanıcı adı ve şifreyi doğrulayın**
- **Firewall'da port 21'i açın**
- **Pasif mod kullanmayı deneyin**

### Webhook Sorunları
- **Webhook URL'sini kontrol edin**
- **Discord/Slack kanal izinlerini kontrol edin**
- **İnternet bağlantısını test edin**
- **JSON formatını kontrol edin**

---

## 📋 Yasal Uyarılar

### Türkiye Mevzuatı
- **5237 sayılı Türk Ceza Kanunu Madde 244**: Bilişim sistemlerine izinsiz erişim
- **6698 sayılı KVKK**: Kişisel verilerin korunması
- **İzinsiz kullanım**: 2-5 yıl hapis cezası

### Etik Kurallar
- ✅ Sadece eğitim amaçlı kullanın
- ✅ Açık yazılı izin alın
- ✅ Verileri güvenli saklayın
- ❌ İzinsiz kullanım yapmayın
- ❌ Ticari amaçlı kullanmayın

---

## 🎯 Kullanım Senaryoları

### Eğitim Amaçlı
- **Siber güvenlik kursları**
- **Windows API öğrenme**
- **Programlama eğitimi**
- **Akademik araştırma**

### İzinli Kullanım
- **Çocuk aktivite takibi** (aile izni ile)
- **Çalışan aktivite takibi** (işveren izni ile)
- **Güvenlik araştırması** (kontrollü ortamda)

---

## 📞 Destek ve İletişim

### Teknik Destek
- **Hata raporları**: GitHub Issues
- **Dokümantasyon**: README.md
- **Güncellemeler**: Release Notes

### Yasal Danışmanlık
- **Bilişim hukuku uzmanına başvurun**
- **KVKK danışmanına başvurun**
- **Yerel yasalara uygunluk kontrolü yapın**

---

**⚠️ Unutmayın: Bu yazılımı kullanırken her zaman etik ve yasal kurallara uyun. Büyük güç, büyük sorumluluk getirir!**
