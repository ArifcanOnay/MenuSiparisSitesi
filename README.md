# 🍔 SignalR Restoran Yönetim Sistemi

Modern restoran yönetimi için geliştirilmiş, gerçek zamanlı sipariş takibi ve QR kod tabanlı menü sistemi.

## 📋 Proje Hakkında

Bu proje, restoranların dijital dönüşümünü sağlayan kapsamlı bir yönetim sistemidir. Müşteriler QR kod ile masa başından sipariş verebilir, admin panelinden tüm operasyonlar yönetilebilir ve SignalR ile gerçek zamanlı bildirimler alınabilir.

### ✨ Özellikler

- 🔐 **Kullanıcı Yönetimi**: ASP.NET Core Identity ile güvenli giriş/kayıt
- 📱 **QR Kod Sistemi**: Masa bazlı temassız sipariş
- 🍕 **Ürün & Kategori Yönetimi**: Resim yükleme, filtreleme
- 🛒 **Sepet & Sipariş**: Gerçek zamanlı sipariş takibi
- 📊 **Dashboard**: İstatistikler, grafikler, raporlar
- 🔔 **Bildirimler**: SignalR ile anlık bildirimler
- 💰 **Kasa Yönetimi**: Günlük gelir takibi
- 📧 **Mail Sistemi**: Rezervasyon onay/iptal mailleri
- 🎨 **Responsive Tasarım**: Mobil uyumlu arayüz
- 💬 **Anlık Mesajlaşma**: SignalR chat sistemi

---

## 🛠️ Kullanılan Teknolojiler

### Backend
- **ASP.NET Core 9.0** - Web API & MVC
- **Entity Framework Core 9.0** - ORM
- **SignalR** - Gerçek zamanlı iletişim
- **N-Layer Architecture** - Katmanlı mimari

### Frontend
- **Razor Pages** - Server-side rendering
- **JavaScript (Vanilla)** - Client-side interactivity
- **Bootstrap 5** - UI framework
- **jQuery** - AJAX işlemleri
- **FontAwesome 6** - İkonlar

### Veritabanı
- **SQL Server** - İlişkisel veritabanı
- **Code First Approach** - Migration tabanlı

### Kütüphaneler
- **QRCoder** - QR kod oluşturma
- **MailKit** - SMTP mail gönderimi
- **AutoMapper** - DTO mapping
- **FluentValidation** - Validasyon

---

## 🚀 Projeyi Kendi Bilgisayarınızda Çalıştırma

### 1️⃣ Gereksinimler

- ✅ **Visual Studio 2022** veya üzeri
- ✅ **.NET 9.0 SDK**
- ✅ **SQL Server** (LocalDB yeterli)
- ✅ **Git**

### 2️⃣ Projeyi Klonlama

```bash
git clone https://github.com/ArifcanOnay/MenuSiparisSitesi.git
cd MenuSiparisSitesi
```

### 3️⃣ Veritabanı Kurulumu

#### Yöntem A - Migration ile (Önerilen):

1. **Package Manager Console** açın (Tools → NuGet Package Manager → Package Manager Console)

2. Default project: **SignalR.DataAccessLayer** seçin

3. Migration'ları çalıştırın:
```powershell
Update-Database
```



### 4️⃣ Bağlantı String Ayarı

**SignalRApi/appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SignalRDb;Integrated Security=true;TrustServerCertificate=true"
  }
}
```

**SignalRWebUI/appsettings.json:** (aynı şekilde güncelleyin)

> **Not:** SQL Server instance adınız farklıysa `Server=` kısmını değiştirin.

### 5️⃣ Projeyi Başlatma

**İki projeyi birlikte başlatmak için:**

1. Solution'a sağ tık → **Properties**
2. Startup Project → **Multiple startup projects**
3. **SignalRApi** ve **SignalRWebUI** için Action: **Start** seçin
4. **F5** ile başlatın

**Veya manuel olarak:**
```bash
# Terminal 1 - API
cd SignalRApi
dotnet run

# Terminal 2 - Web UI
cd SignalRWebUI
dotnet run
```

**Tarayıcıda açılacak URL'ler:**
- 🌐 **Müşteri Arayüzü**: https://localhost:7092
- ⚙️ **Admin Panel**: https://localhost:7092/Login/Index
- 🔌 **API**: https://localhost:7186

---

## 👤 Admin Paneline Giriş

### Varsayılan Admin Hesabı

İlk kurulumda admin hesabı yoktur. Admin hesabı oluşturmak için:

#### Yöntem 1 - Kayıt Ol Sayfasından:

1. https://localhost:7092/Register/Index adresine gidin
2. Formu doldurun ve kayıt olun
3. **SQL Server Management Studio'da** veya **Azure Data Studio'da** şu komutu çalıştırın:

```sql
USE SignalRDb;

-- Kullanıcının Role'ünü Admin yap
UPDATE AspNetUsers
SET Role = 'Admin'
WHERE UserName = 'KULLANICI_ADINIZ';
```

#### Yöntem 2 - Direkt SQL ile Admin Oluşturma:

```sql
USE SignalRDb;

-- Admin kullanıcısı ekle
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, 
                         EmailConfirmed, PasswordHash, SecurityStamp, Name, Surname, Role)
VALUES (NEWID(), 'admin', 'ADMIN', 'admin@example.com', 'ADMIN@EXAMPLE.COM',
        1, 'AQAAAAIAAYagAAAAEJmqKXr...', NEWID(), 'Admin', 'User', 'Admin');
```

> **Not:** PasswordHash için register olup sonra SQL'den rol değiştirmek daha kolay.

### Admin Panel Menüleri

Admin panelde erişilebilir sayfalar:
- 📊 **Dashboard** - İstatistikler ve grafikler
- 🍴 **Kategoriler** - Kategori yönetimi
- 🍕 **Ürünler** - Ürün CRUD işlemleri
- 🪑 **Masalar** - Masa yönetimi
- 📅 **Rezervasyonlar** - Rezervasyon onay/iptal
- 📦 **Siparişler** - Sipariş takibi
- 👥 **Kullanıcılar** - Kullanıcı yönetimi
- 💰 **Kasa** - Gelir takibi
- 📧 **Mail Gönderme** - Toplu mail
- 🔔 **Bildirimler** - Sistem bildirimleri
- 📱 **QR Kod** - Masa QR kodları
- 🎨 **İndirimler** - İndirim kampanyaları
- 📞 **İletişim** - Müşteri mesajları

---

## 🔐 Rol ve Yetki Yönetimi

### Roller

Sistemde 2 rol vardır:
- **Admin** - Tüm yetkilere sahip
- **User** - Sadece müşteri arayüzüne erişim

### Yetki Verme

**Bir kullanıcıya admin yetkisi vermek için:**

1. **Admin Panel** → Kullanıcılar sayfasına gidin
2. Kullanıcının role bilgisini "Admin" olarak güncelleyin

**Veya SQL ile:**
```sql
UPDATE AspNetUsers
SET Role = 'Admin'
WHERE Email = 'kullanici@email.com';
```

---

## 📱 QR Kod Kullanımı

### Masa İçin QR Kod Oluşturma

1. **Admin Panel** → QR Kod İşlemleri
2. Masa URL'i girin:
   ```
   localhost:7092/Menu/Index/MASA_ID
   ```
   Örnek: `localhost:7092/Menu/Index/1` (Masa 1 için)

3. **QR Kod Oluştur** butonuna basın
4. QR görüntüsüne sağ tık → **Resmi Kaydet**
5. Yazdırıp masaya yerleştirin

### Müşteri Kullanımı

1. Müşteri QR'ı telefonla tarar
2. Menü sayfası açılır
3. Sepete ürün ekler
4. Sipariş verir
5. Admin panelinde sipariş görünür

---

## 📧 Mail Ayarları

Rezervasyon onay/iptal mailleri için Gmail SMTP kullanılıyor.

### Gmail SMTP Ayarı

1. Gmail hesabınızda **2-Step Verification** aktif olmalı
2. **App Passwords** oluşturun: https://myaccount.google.com/apppasswords
3. `SignalRWebUI/appsettings.json` dosyasını güncelleyin:

```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "MAIL_ADRESINIZ@gmail.com",
    "SenderPassword": "UYGULAMA_SIFRENIZ"
  }
}
```

---

## 📂 Proje Yapısı

```
MenuSiparisSitesi/
├── SignalR.BusinessLayer/        # İş mantığı katmanı
│   ├── Abstract/                  # Interface'ler
│   ├── Concrete/                  # Manager sınıfları
│   └── ValidationRules/           # FluentValidation
├── SignalR.DataAccessLayer/       # Veri erişim katmanı
│   ├── Abstract/                  # Repository interface'leri
│   ├── Concrete/                  # DbContext
│   ├── EntityFramework/           # EF Repository implementasyonları
│   └── Migrations/                # EF Migrations
├── SignalR.DtoLayer/              # DTO sınıfları
├── SignalR.EntityLayer/           # Entity sınıfları
├── SignalRApi/                    # Web API projesi
│   ├── Controllers/               # API Controller'lar
│   ├── Hubs/                      # SignalR Hub'ları
│   └── Mapping/                   # AutoMapper profilleri
└── SignalRWebUI/                  # MVC Web projesi
    ├── Controllers/               # MVC Controller'lar
    ├── Views/                     # Razor View'lar
    ├── ViewComponents/            # View Component'ler
    └── wwwroot/                   # Static dosyalar
```

---



## 👥 Geliştirici Ekibi

- **Arif Can Önay** - [@ArifcanOnay](https://github.com/ArifcanOnay)
<<<<<<< HEAD
  
=======
- **Ferhan Çıbık** - [@ferhancibik](https://github.com/ferhancibik)
>>>>>>> cef8ffe (Frontend Degisiklikleri ve Gelistirmeler)

---

## 📞 İletişim

Sorularınız için:
- 📧 Email: arifonay.853@gmail.com
- 🐙 GitHub Issues: [Sorun Bildir](https://github.com/ArifcanOnay/MenuSiparisSitesi/issues)

---


