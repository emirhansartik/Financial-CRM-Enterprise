# 💼 Financial CRM Enterprise

C# ve Windows Forms teknolojileri kullanılarak, **N-Tier (Çok Katmanlı) Mimari**, **ASP.NET Web API** ve **OOP (Nesne Yönelimli Programlama)** prensipleriyle geliştirilen, dağıtık (Client-Server) altyapıya sahip Kurumsal Finans ve Müşteri İlişkileri (CRM) Yönetim Sistemi.

Bu proje, küçük ve orta ölçekli işletmelerin (KOBİ) gelir-gider takiplerini, fatura yönetimlerini ve banka hareketlerini güvenli bir altyapıda yönetebilmeleri amacıyla tasarlanmaktadır. Veritabanı işlemleri arayüzden tamamen izole edilerek arka planda çalışan bir API sunucusuna devredilmiştir.

## 🏗️ Proje Mimarisi ve Teknoloji Yığını

Proje, spagetti kod oluşumunu engellemek, sürdürülebilirliği sağlamak ve farklı platformlara (Web/Mobil) entegrasyona hazır olmak adına **5 ana katmana** bölünmüştür:

*   **FinancialCrm.WebApi (Sunucu):** İstemciden (UI) gelen HTTP isteklerini karşılayan, veritabanı işlemlerini yöneten ve sonuçları JSON formatında dışa aktaran API santrali.
*   **FinancialCrm.Entity:** Veritabanı nesnelerinin (POCO) ve modellerin bulunduğu katman.
*   **FinancialCrm.DataAccess:** Entity Framework ve LINQ kullanılarak CRUD operasyonlarının (Repository Pattern) yürütüldüğü veri erişim katmanı.
*   **FinancialCrm.Business:** Veri erişim katmanından gelen verilerin iş mantığına (Business Logic) ve yetki kontrollerine sokulduğu katman.
*   **FinancialCrm.UI (İstemci):** Windows Forms ve Flat Design prensipleriyle tasarlanan, veritabanından tamamen bağımsız, API ile HTTP (HttpClient & DTO) üzerinden haberleşen kullanıcı arayüzü katmanı.

**Kullanılan Teknolojiler:**
*   C# & .NET Core / Web API
*   MS SQL Server
*   Entity Framework & LINQ
*   Newtonsoft.Json & DTO (Data Transfer Object) Mimarisi
*   Git & GitHub (Sürüm Kontrolü)

## 🚀 Geliştirme Yol Haritası (Roadmap)

Proje şu an aktif geliştirme aşamasındadır. Planlanan ve tamamlanan temel modüller şunlardır:

- [x] N-Tier mimari iskeletinin kurulması.
- [x] Temel UI (Kullanıcı Arayüzü) tasarımlarının oluşturulması.
- [x] **Dağıtık Mimariye Geçiş:** İstemci (WinForms) ve Sunucu (Web API) ayrımının yapılması.
- [x] API tabanlı güvenli Login altyapısının kurulması.
- [x] LINQ sorguları ile desteklenmiş dinamik Chart (Grafik) Dashboard'unun API üzerinden beslenmesi.
- [x] Kategori modülünün (Ekle/Sil/Güncelle/Listele) API destekli entegrasyonu.
- [ ] Bankalar ve Fatura/Gider modüllerinin (Tam kapsamlı CRUD) API'ye bağlanması.
- [ ] SHA-256 Kriptografik şifreleme ve RBAC (Yönetici/Personel) yetkilendirmeleri.

**Not: Bu repo, geliştirme süreci boyunca düzenli commit'ler ile güncellenecektir.**
