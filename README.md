# 💼 Financial CRM Enterprise

C# ve Windows Forms teknolojileri kullanılarak, **N-Tier (Çok Katmanlı) Mimari** ve **OOP (Nesne Yönelimli Programlama)** prensipleriyle sıfırdan geliştirilmekte olan Kurumsal Finans ve Müşteri İlişkileri (CRM) Yönetim Sistemi.

Bu proje, küçük ve orta ölçekli işletmelerin (KOBİ) gelir-gider takiplerini, fatura yönetimlerini ve banka hareketlerini güvenli bir altyapıda yönetebilmeleri amacıyla tasarlanmaktadır.

## 🏗️ Proje Mimarisi ve Teknoloji Yığını

Proje, spagetti kod oluşumunu engellemek ve sürdürülebilirliği sağlamak adına 4 ana katmana (N-Tier) bölünmüştür:

*   **FinancialCrm.Entity:** Veritabanı nesnelerinin (POCO) ve modellerin bulunduğu katman.
*   **FinancialCrm.DataAccess:** Entity Framework ve LINQ kullanılarak CRUD operasyonlarının (Repository Pattern) yürütüldüğü veri erişim katmanı.
*   **FinancialCrm.Business:** Veri erişim katmanından gelen verilerin iş mantığına (Business Logic) ve yetki kontrollerine sokulduğu katman.
*   **FinancialCrm.UI:** Windows Forms ve Flat Design prensipleriyle tasarlanan kullanıcı arayüzü katmanı.

**Kullanılan Teknolojiler:**
*   C# & .NET Framework
*   MS SQL Server
*   Entity Framework & LINQ
*   Git & GitHub (Sürüm Kontrolü)

## 🚀 Geliştirme Yol Haritası (Roadmap)

Proje şu an aktif geliştirme aşamasındadır. Planlanan temel modüller şunlardır:

- [x] N-Tier mimari iskeletinin kurulması.
- [x] Temel UI (Kullanıcı Arayüzü) tasarımlarının oluşturulması.
- [ ] Veritabanı ilişkilerinin (Foreign Key) kurulması ve Entity katmanına bağlanması.
- [ ] SHA-256 Kriptografik şifreleme ile güvenli Login altyapısı.
- [ ] RBAC (Role-Based Access Control) ile Yönetici/Personel yetkilendirmeleri.
- [ ] LINQ sorguları ile desteklenmiş dinamik Chart (Grafik) Dashboard'u.
- [ ] Kategori, Banka ve Fatura/Gider modüllerinin (Tam kapsamlı CRUD) entegrasyonu.# FinancialCrmEnterprise


* **Not: Bu repo, geliştirme süreci boyunca düzenli commit'ler ile güncellenecektir.**
