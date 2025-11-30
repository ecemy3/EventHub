# EventHub 🎉

ASP.NET MVC ile geliştirilmiş modern bir etkinlik yönetim platformu.

## 📋 Özellikler

- **Kullanıcı Yönetimi**: Kayıt, giriş ve profil yönetimi
- **Etkinlik Oluşturma**: Kullanıcılar kendi etkinliklerini oluşturabilir
- **Etkinlik Kategorileri**: Farklı kategorilerde etkinlik düzenleme
- **Etkinlik Katılımı**: Etkinliklere katılma ve üye yönetimi
- **Mesajlaşma Sistemi**: Kullanıcılar arası mesajlaşma
- **Harita Entegrasyonu**: Etkinlik lokasyonlarını haritada görüntüleme
- **Admin Paneli**: Yönetici kontrol paneli
- **Puan Sistemi**: Kullanıcı puanlama sistemi

## 🛠️ Teknolojiler

- **Framework**: ASP.NET MVC 5
- **ORM**: Entity Framework 6.5
- **Frontend**: HTML5, CSS3, JavaScript
- **Veritaşbanı**: SQL Server
- **Paket Yönetimi**: NuGet

## 📦 Kurulum

### Gereksinimler

- Visual Studio 2017 veya üzeri
- .NET Framework 4.7.2+
- SQL Server 2014 veya üzeri
- IIS Express (Visual Studio ile birlikte gelir)

### Adımlar

1. Projeyi klonlayın:
```bash
git clone https://github.com/ecemy3/EventHub.git
cd EventHub
```

2. Visual Studio ile `eventhub.sln` dosyasını açın

3. NuGet paketlerini geri yükleyin:
   - Solution Explorer'da solution'a sağ tıklayın
   - "Restore NuGet Packages" seçeneğini seçin

4. Veritabanı bağlantı ayarlarını yapılandırın:
   - `Web.config` dosyasında connection string'i düzenleyin
   - SQL Server bağlantı bilgilerinizi girin

5. Package Manager Console'dan migration'ları çalıştırın:
```
Update-Database
```

6. Projeyi çalıştırın (F5)

## 📁 Proje Yapısı

```
eventhub/
├── Controllers/        # MVC Controller'ları
├── Models/            # Veritabanı modelleri
├── Views/             # Razor görünümleri
├── assets/            # CSS, JS, resimler
├── Migrations/        # Entity Framework migration'ları
└── App_Start/         # Uygulama başlangıç yapılandırması
```

## 🔑 Temel Controller'lar

- **HomeController**: Ana sayfa ve genel sayfalar
- **AuthenticationController**: Kullanıcı girişi ve kaydı
- **EventController**: Etkinlik işlemleri
- **ProfileController**: Kullanıcı profili yönetimi
- **MessageController**: Mesajlaşma sistemi
- **AdminController**: Yönetici paneli
- **MapsController**: Harita işlemleri

## 💾 Veritabanı Modelleri

- **User**: Kullanıcı bilgileri
- **Event**: Etkinlik bilgileri
- **EventCategory**: Etkinlik kategorileri
- **EventMember**: Etkinlik katılımcıları
- **Message**: Mesaj sistemi
- **MessageDetail**: Mesaj detayları
- **Score**: Puan sistemi

## 🚀 Kullanım

1. Uygulamayı başlatın
2. Ana sayfadan kayıt olun veya giriş yapın
3. Etkinliklere göz atın veya yeni etkinlik oluşturun
4. Etkinliklere katılın ve diğer kullanıcılarla mesajlaşın

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/yeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeniOzellik`)
5. Pull Request oluşturun

## 📝 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

---
⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
