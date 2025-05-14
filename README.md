# Restaurantopia 🍽️

نظام إدارة مطعم متكامل مبني باستخدام ASP.NET Core MVC. يوفر النظام واجهة لإدارة المنتجات، الطلبات، والمستخدمين مع دعم كامل للمصادقة والتفويض.

## المتطلبات الأساسية 📋

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (موصى به) أو [VS Code](https://code.visualstudio.com/)

## التثبيت والإعداد 🚀

### 1. تجهيز قاعدة البيانات

```sql
-- إنشاء قاعدة البيانات
CREATE DATABASE Restaurantopia;
GO

-- إنشاء مستخدم
CREATE LOGIN NewUser WITH PASSWORD = '123';
GO

USE Restaurantopia;
GO

CREATE USER NewUser FOR LOGIN NewUser;
GO

-- منح الصلاحيات
ALTER ROLE db_owner ADD MEMBER NewUser;
GO
```

### 2. تشغيل المشروع محلياً

```bash
# استنساخ المشروع
git clone [رابط المشروع]
cd Restaurantopia

# استعادة الحزم
dotnet restore

# تشغيل الهجرات
dotnet ef database update

# تشغيل المشروع
dotnet run
```

### 3. تشغيل المشروع باستخدام Docker

```bash
# بناء وتشغيل الحاويات
docker-compose up -d --build

# عرض السجلات
docker-compose logs -f

# إيقاف الحاويات
docker-compose down
```

## هيكل المشروع 📁

```
Restaurantopia/
├── Controllers/         # وحدات التحكم
├── Models/             # نماذج البيانات
├── Views/              # واجهات المستخدم
├── wwwroot/           # الملفات الثابتة (CSS, JS, Images)
├── Interfaces/        # الواجهات
├── Repositories/      # مستودعات البيانات
└── Program.cs         # نقطة البداية
```

## المميزات الرئيسية ✨

- 🔐 نظام مصادقة وتفويض متكامل
- 👥 إدارة المستخدمين والأدوار
- 🍕 إدارة المنتجات والفئات
- 🛒 نظام طلبات متكامل
- 📱 واجهة مستخدم متجاوبة
- 🖼️ دعم تحميل وعرض الصور
- 🔍 البحث والتصفية

## الأدوار المتاحة 👥

1. **المدير (Admin)**
   - إدارة المنتجات والفئات
   - إدارة المستخدمين والأدوار
   - عرض وإدارة الطلبات

2. **العميل (Customer)**
   - تصفح المنتجات
   - إنشاء وتتبع الطلبات
   - إضافة تقييمات

## الإعدادات 🔧

### Connection String

```json
{
  "ConnectionStrings": {
    "Default": "Server=host.docker.internal;Database=Restaurantopia;User Id=NewUser;Password=123;TrustServerCertificate=true;MultipleActiveResultSets=true;"
  }
}
```

### المنافذ

- التطبيق: `http://localhost:8080`
- قاعدة البيانات: `1433`

## Docker 🐳

### الملفات الرئيسية

- `Dockerfile`: إعدادات بناء الصورة
- `docker-compose.yml`: تكوين الخدمات
- `.dockerignore`: تجاهل الملفات غير الضرورية

### الأوامر المفيدة

```bash
# بناء الصورة
docker build -t restaurantopia .

# تشغيل الحاوية
docker run -d -p 8080:80 restaurantopia

# عرض السجلات
docker logs restaurantopia-app

# الدخول إلى الحاوية
docker exec -it restaurantopia-app sh
```

## حل المشاكل الشائعة 🔍

1. **مشكلة الاتصال بقاعدة البيانات**
   - تأكد من تشغيل SQL Server
   - تحقق من صحة Connection String
   - تأكد من صلاحيات المستخدم

2. **مشكلة عرض الصور**
   - تأكد من وجود الصور في مجلد `/wwwroot/Images`
   - تحقق من صلاحيات المجلد
   - تأكد من صحة المسارات في الكود

3. **مشاكل Docker**
   - تأكد من تشغيل Docker Desktop
   - جرب إعادة تشغيل Docker
   - تحقق من السجلات للأخطاء

## المساهمة 🤝

1. Fork المشروع
2. إنشاء فرع للميزة الجديدة
3. Commit التغييرات
4. Push إلى الفرع
5. إنشاء Pull Request

## الترخيص 📄

هذا المشروع مرخص تحت [MIT License](LICENSE).

## الدعم 💬

للمساعدة أو الاستفسارات:
- فتح issue في GitHub
- التواصل عبر [البريد الإلكتروني]

## شكر خاص 🙏

شكر خاص لكل المساهمين في هذا المشروع.