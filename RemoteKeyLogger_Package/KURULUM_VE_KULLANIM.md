# 🔍 Uzaktan Takip KeyLogger - Kurulum ve Kullanım

## ⚠️ KRİTİK UYARI
Bu yazılım **sadece eğitim amaçlıdır** ve **açık yazılı izin** alınmış kişilerde kullanılmalıdır. İzinsiz kullanım yasalara aykırıdır ve 2-5 yıl hapis cezası ile cezalandırılabilir.

---

## 📦 Kurulum

### 1. Sistem Gereksinimleri
- **Windows 10/11**
- **.NET Framework 4.8**
- **İnternet bağlantısı** (uzaktan takip için)
- **Yönetici yetkisi** (önerilen)

### 2. Dosyaları Hazırlama
```
RemoteKeyLogger_Package klasörünü açın
Tüm dosyaların aynı klasörde olduğundan emin olun
```

### 3. Antivirüs Ayarları
- Windows Defender'ı geçici olarak kapatın
- Antivirüs programınızı geçici olarak devre dışı bırakın
- Uygulamayı güvenilir programlar listesine ekleyin

---

## 🚀 Hızlı Başlangıç

### Adım 1: Uygulamayı Başlatma
1. **GameOdev.exe** dosyasını **sağ tık** > **Yönetici olarak çalıştır**
2. Uygulama açılacak ve uzaktan takip arayüzü görünecek

### Adım 2: Konfigürasyon
1. **⚙️ Konfigürasyon** butonuna tıklayın
2. Açılan pencerede:
   - **📧 E-posta ayarlarını** yapılandırın
   - **🌐 FTP ayarlarını** yapılandırın  
   - **📱 Webhook ayarlarını** yapılandırın
3. **💾 Kaydet** butonuna tıklayın

### Adım 3: Uzaktan Takibi Başlatma
1. **🚀 Uzaktan Takibi Başlat** butonuna tıklayın
2. İzin onayı ekranında **Yes** seçin
3. Tuş vuruşları otomatik olarak uzaktan gönderilmeye başlar

---

## 📧 E-posta Konfigürasyonu (Detaylı)

### Gmail Kullanımı
```
✅ SMTP Sunucu: smtp.gmail.com
✅ Port: 587
✅ Gönderen E-posta: your-email@gmail.com
✅ Şifre: Gmail App Password (2FA gerekli)
✅ Alıcı E-posta: recipient@example.com
✅ Gönderim Aralığı: 30 dakika
```

### Gmail App Password Alma (Adım Adım)
1. **Google Hesabınıza** giriş yapın
2. **Güvenlik** bölümüne gidin
3. **2 Adımlı Doğrulama**'yı etkinleştirin
4. **Uygulama şifreleri** bölümüne gidin
5. **Uygulama seçin**: "Diğer (Özel ad)"
6. **Ad**: "KeyLogger App"
7. **Oluştur** butonuna tıklayın
8. **16 haneli şifreyi** kopyalayın
9. Bu şifreyi konfigürasyon ekranına girin

### Outlook/Hotmail Kullanımı
```
✅ SMTP Sunucu: smtp-mail.outlook.com
✅ Port: 587
✅ Gönderen E-posta: your-email@outlook.com
✅ Şifre: Normal şifreniz
✅ Alıcı E-posta: recipient@example.com
```

### Yahoo Mail Kullanımı
```
✅ SMTP Sunucu: smtp.mail.yahoo.com
✅ Port: 587
✅ Gönderen E-posta: your-email@yahoo.com
✅ Şifre: Yahoo App Password
```

---

## 🌐 FTP Konfigürasyonu (Detaylı)

### Ücretsiz FTP Sunucuları

#### 1. FileZilla Server (Kendi Sunucunuz)
```
✅ FTP Sunucu: your-server-ip
✅ Port: 21 (varsayılan)
✅ Kullanıcı: ftp_username
✅ Şifre: ftp_password
✅ Uzak Dizin: /logs/
```

#### 2. 000webhost (Ücretsiz)
```
✅ FTP Sunucu: files.000webhost.com
✅ Port: 21
✅ Kullanıcı: 000webhost_username
✅ Şifre: 000webhost_password
✅ Uzak Dizin: /public_html/logs/
```

#### 3. InfinityFree (Ücretsiz)
```
✅ FTP Sunucu: ftp.infinityfree.net
✅ Port: 21
✅ Kullanıcı: infinityfree_username
✅ Şifre: infinityfree_password
✅ Uzak Dizin: /htdocs/logs/
```

### FTP Test Etme
1. **FileZilla Client** indirin
2. FTP bilgilerinizi girin
3. Bağlantıyı test edin
4. `/logs/` klasörünü oluşturun

---

## 📱 Webhook Konfigürasyonu (Detaylı)

### Discord Webhook (Adım Adım)
1. **Discord sunucunuzu** açın
2. **Bir kanal seçin** (örn: #keylogger-logs)
3. **Kanal ayarları** > **Entegrasyonlar** > **Webhooklar**
4. **"Yeni Webhook"** butonuna tıklayın
5. **Webhook adı**: "KeyLogger Bot"
6. **Webhook URL'sini** kopyalayın
7. Konfigürasyon ekranına yapıştırın

### Slack Webhook (Adım Adım)
1. **Slack workspace'inizi** açın
2. **Bir kanal seçin** (örn: #keylogger-alerts)
3. **Kanal ayarları** > **Entegrasyonlar** > **Gelen Webhooklar**
4. **"Webhook Ekle"** butonuna tıklayın
5. **Webhook adı**: "KeyLogger Integration"
6. **Webhook URL'sini** kopyalayın
7. Konfigürasyon ekranına yapıştırın

### Webhook Test Mesajı
```json
{
  "content": "🔍 **Educational KeyLogger Test**\n📱 **Computer:** TEST_COMPUTER\n👤 **User:** TEST_USER\n🕒 **Time:** 2025-01-05 20:30:15\n📝 **Message:** Test mesajı başarılı!\n⚠️ *This is for educational purposes only*"
}
```

---

## 🔧 Gelişmiş Özellikler

### Otomatik Gönderim Sistemi
- **E-posta**: 30 dakikada bir otomatik gönderim
- **FTP**: 15 dakikada bir otomatik yükleme
- **Webhook**: Önemli tuşlar için anlık bildirim

### Queue (Kuyruk) Sistemi
- **Maksimum 1000 tuş vuruşu** bellekte tutulur
- **Bellek dolduğunda** eski veriler silinir
- **Uygulama kapanırken** kalan veriler gönderilir

### Önemli Tuşlar (Webhook)
Webhook bildirimleri şu tuşlar için gönderilir:
- `[ENTER]` - Enter tuşu
- `[TAB]` - Tab tuşu
- `[ESC]` - Escape tuşu
- `[DELETE]` - Delete tuşu
- `[CTRL]` - Ctrl tuşu
- `[ALT]` - Alt tuşu
- `[WIN]` - Windows tuşu

### Veri Formatları

#### E-posta Konu Satırı
```
🔍 KeyLogger Log - COMPUTER_NAME - 2025-01-05 20:30
```

#### FTP Dosya Adı
```
COMPUTER_NAME_USER_NAME_20250105_203015.txt
```

#### Webhook JSON
```json
{
  "content": "🔍 **Educational KeyLogger Alert**\n📱 **Computer:** COMPUTER_NAME\n👤 **User:** USER_NAME\n🕒 **Time:** 2025-01-05 20:30:15\n📝 **Message:** ⌨️ **Key Pressed:** [ENTER]\n⚠️ *This is for educational purposes only*"
}
```

---

## 🛡️ Güvenlik ve Gizlilik

### Veri Güvenliği
1. **Şifreleme**: Hassas verileri şifreleyin
2. **Güvenli Bağlantı**: SSL/TLS kullanın
3. **Düzenli Temizlik**: Eski logları silin
4. **Erişim Kontrolü**: Sadece yetkili kişiler erişsin

### Sistem Güvenliği
1. **Antivirüs**: Geçici olarak devre dışı bırakın
2. **Firewall**: Gerekli portları açın (587, 21, 443)
3. **Yönetici Yetkisi**: Uygulamayı yönetici olarak çalıştırın
4. **Ağ Güvenliği**: Güvenli ağ bağlantısı kullanın

### Gizlilik Ayarları
1. **Kişisel Veriler**: Hassas bilgileri filtreleyin
2. **Şifreler**: Şifre alanlarını boş bırakın
3. **Kredi Kartı**: Finansal bilgileri yakalamayın
4. **Özel Mesajlar**: Kişisel mesajları filtreleyin

---

## 🔧 Sorun Giderme

### E-posta Gönderim Sorunları

#### Problem: "SMTP Authentication Failed"
**Çözüm:**
1. Gmail App Password kullanın (normal şifre değil)
2. 2FA'yı etkinleştirin
3. "Daha az güvenli uygulama erişimi"ni açın

#### Problem: "Connection Timeout"
**Çözüm:**
1. Port 587'nin açık olduğundan emin olun
2. Firewall ayarlarını kontrol edin
3. İnternet bağlantısını test edin

#### Problem: "Invalid Recipient"
**Çözüm:**
1. E-posta adresini doğrulayın
2. @ sembolü ve domain'i kontrol edin
3. Alıcı e-posta adresinin geçerli olduğundan emin olun

### FTP Bağlantı Sorunları

#### Problem: "Connection Refused"
**Çözüm:**
1. FTP sunucu adresini kontrol edin
2. Port 21'in açık olduğundan emin olun
3. Kullanıcı adı ve şifreyi doğrulayın

#### Problem: "Login Failed"
**Çözüm:**
1. Kullanıcı adı ve şifreyi kontrol edin
2. Büyük/küçük harf duyarlılığına dikkat edin
3. Özel karakterleri kontrol edin

#### Problem: "Directory Not Found"
**Çözüm:**
1. Uzak dizin yolunu kontrol edin
2. `/logs/` klasörünü manuel olarak oluşturun
3. Dizin izinlerini kontrol edin

### Webhook Sorunları

#### Problem: "Webhook URL Invalid"
**Çözüm:**
1. Webhook URL'sini kontrol edin
2. Discord/Slack kanal izinlerini kontrol edin
3. Webhook'u yeniden oluşturun

#### Problem: "Rate Limited"
**Çözüm:**
1. Gönderim sıklığını azaltın
2. Sadece önemli tuşlar için bildirim gönderin
3. Webhook ayarlarını optimize edin

### Genel Sorunlar

#### Problem: "Application Won't Start"
**Çözüm:**
1. .NET Framework 4.8 yükleyin
2. Yönetici olarak çalıştırın
3. Antivirüs programını geçici olarak kapatın

#### Problem: "No Keystrokes Captured"
**Çözüm:**
1. Uygulamayı yönetici olarak çalıştırın
2. Windows Defender'ı geçici olarak kapatın
3. Hook ayarlarını kontrol edin

---

## 📋 Yasal Uyarılar ve Etik Kurallar

### Türkiye Mevzuatı
- **5237 sayılı Türk Ceza Kanunu Madde 244**: Bilişim sistemlerine izinsiz erişim
- **6698 sayılı KVKK**: Kişisel verilerin korunması
- **İzinsiz kullanım**: 2-5 yıl hapis cezası
- **Ağır para cezası**: 50.000 TL'ye kadar

### Etik Kurallar
- ✅ **Sadece eğitim amaçlı** kullanın
- ✅ **Açık yazılı izin** alın
- ✅ **Verileri güvenli** saklayın
- ✅ **Düzenli temizlik** yapın
- ❌ **İzinsiz kullanım** yapmayın
- ❌ **Ticari amaçlı** kullanmayın
- ❌ **Kişisel veri** toplamayın

### İzin Belgesi
Her kullanım öncesi mutlaka:
1. **Yazılı izin belgesi** doldurun
2. **Her iki taraf da** imzalayın
3. **İzin süresini** belirleyin
4. **Kullanım amacını** açıklayın

---

## 🎯 Kullanım Senaryoları

### Eğitim Amaçlı
- **Siber güvenlik kursları**
- **Windows API öğrenme**
- **Programlama eğitimi**
- **Akademik araştırma**
- **Etik hacking eğitimi**

### İzinli Kullanım
- **Çocuk aktivite takibi** (aile izni ile)
- **Çalışan aktivite takibi** (işveren izni ile)
- **Güvenlik araştırması** (kontrollü ortamda)
- **Akademik çalışma** (üniversite projesi)

---

## 📞 Destek ve İletişim

### Teknik Destek
- **Hata raporları**: GitHub Issues
- **Dokümantasyon**: README.md ve rehberler
- **Güncellemeler**: Release Notes

### Yasal Danışmanlık
- **Bilişim hukuku uzmanına** başvurun
- **KVKK danışmanına** başvurun
- **Yerel yasalara uygunluk** kontrolü yapın

### Acil Durum
- **Yasal sorun**: Derhal kullanımı durdurun
- **Veri sızıntısı**: Tüm verileri silin
- **Güvenlik ihlali**: Yetkili makamlara başvurun

---

## 🔄 Güncelleme ve Bakım

### Düzenli Bakım
1. **Log dosyalarını** düzenli olarak temizleyin
2. **Konfigürasyonu** periyodik olarak kontrol edin
3. **Güvenlik ayarlarını** güncelleyin
4. **Yasal uygunluk** kontrolü yapın

### Güncelleme
1. **Yeni sürümleri** takip edin
2. **Güvenlik yamalarını** uygulayın
3. **Özellik güncellemelerini** kontrol edin
4. **Uyumluluk testleri** yapın

---

**⚠️ SON UYARI: Bu yazılımı kullanırken her zaman etik ve yasal kurallara uyun. Büyük güç, büyük sorumluluk getirir. İzinsiz kullanımın sonuçları tamamen kullanıcıya aittir.**

**📚 Eğitim amaçlı kullanım için hazırlanmıştır. Güvenli ve etik kullanım için tüm rehberleri dikkatlice okuyun.**
