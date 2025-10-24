<<<<<<< HEAD
# ByteTech Test Project - User & Promotion Management API

## Giới thiệu
Đây là một Web API mini application quản lý người dùng và chương trình khuyến mãi, được xây dựng theo kiến trúc **Clean Architecture**

---

## Công nghệ sử dụng
- **Backend:** ASP.NET Core, .NET 8  

- **Architecture:** Clean Architecture  

- **Patterns:** CQRS, MediatR, Repository  

- **Database:** MongoDB (Write Side), Elasticsearch (Read Side) 

- **Authentication:** JWT Bearer Token  

- **Container:** Docker & Docker Compose  

---

## Cấu trúc Project

```text
ByteTech/
├── ByteTech.Domain/        # Core business entities, enums
├── ByteTech.Application/   # Business logic, CQRS, Contracts, Interfaces, Mappers, Behaviors, v.v
├── ByteTech.Infrastructure/# Data access, external services
├── ByteTech.Presentation/  # Web API, Controllers, Middlewares, ...
└── docker-compose.yml      # Docker configuration
```


### 1. Clone Project

```bash

git clone https://github.com/hungdt03/bytetech-test.git

cd bytetech-test

```

### 2. Khởi chạy Services với Docker
Tại thư mục gốc của project, chạy lệnh:

```bash

docker-compose up -d

```

Lệnh này sẽ khởi chạy 2 services:

* MongoDB: localhost:27017

* Elasticsearch: localhost:9200

### 3. Chạy Application
Mở project trong Visual Studio 2022 hoặc VS Code

* Set startup project là ByteTech.Presentation

* Build solution: Ctrl + Shift + B

* Chạy application: F5 hoặc Ctrl + F5

### 4. Truy cập API
* Web API sẽ chạy tại: http://localhost:5296

* Swagger UI để xem và test API: http://localhost:5296/swagger/index.html

## Các Endpoint chính
1. Auth
    * Đăng nhập

    * Đăng ký

2. Quản lý người dùng
    * Thêm mới

    * Chỉnh sửa (Bởi Admin hoặc User)

    * Lock / Unlock

    * GetById

    * Lấy tất cả (search theo keyword)

3. Quản lý khuyến mại

    * Thêm

    * Sửa

    * Xóa

    * Lấy tất cả (search theo keyword)

    * GetActive

    * GetUseds theo user

    * Áp dụng khuyến mại

=======
ByteTech Test Project - User & Promotion Management API
📋 Giới thiệu
Đây là một Web API mini application quản lý người dùng và chương trình khuyến mãi, được xây dựng theo kiến trúc Clean Architecture với các pattern hiện đại.

🚀 Công nghệ sử dụng
Backend: ASP.NET Core, .NET 8

Architecture: Clean Architecture

Patterns: CQRS, MediatR, Repository

Database: MongoDB (Write Side), Elasticsearch (Read Side)

Authentication: JWT Bearer Token

Container: Docker & Docker Compose

📁 Project Structure

ByteTech/
├── ByteTech.Domain/          # Core business entities, enums
├── ByteTech.Application/     # Business logic, CQRS, Contracts, Interfaces, Mappers, Behaviors, v.vv
├── ByteTech.Infrastructure/  # Data access, external services
├── ByteTech.Presentation/    # Web API, Controllers, Middlewares,...
└── docker-compose.yml        # Docker configuration

⚙️ Cài đặt và Chạy Project
1. Clone Project
   git clone https://github.com/hungdt03/bytetech-test.git
   cd bytetech-test

2. Khởi chạy Services với Docker
  Tại thư mục gốc của project, chạy lệnh:
  docker-compose up -d

  Lệnh này sẽ khởi chạy 2 services:
    MongoDB: localhost:27017
    Elasticsearch: localhost:9200

3. Chạy Application
  Mở project trong Visual Studio 2022 hoặc VS Code
  Set startup project là ByteTech.Presentation
  Build solution (Ctrl + Shift + B)
  Chạy application (F5 hoặc Ctrl + F5)

4. Truy cập API
  Application sẽ chạy tại: http://localhost:5296
  Truy cập Swagger UI để xem và test API:
    http://localhost:5296/swagger/index.html
>>>>>>> 7ff5c3108a69c6d1250ed6648b81c504d64aaab5
