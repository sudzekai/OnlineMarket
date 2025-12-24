# OnlineMarket API

Информационная система для управления маркетплейсом, разработанная на платформе **.NET 10**. Проект реализует многослойную архитектуру (N-Tier) с разделением ответственности между данными, логикой и представлением.

---

## Архитектура решения

Проект разделен на следующие логические слои:

* **API** — Слой веб-сервисов. Содержит контроллеры, конфигурацию `Scalar` и механизмы аутентификации. Использование `BuilderExtension` позволяет держать `Program.cs` чистым и модульным.
* **BLL (Business Logic Layer)** — Слой бизнес-логики. Здесь сосредоточена вся обработка данных, интерфейсы сервисов и профили маппинга (**AutoMapper**).
* **DAL (Data Access Layer)** — Слой доступа к данным. Реализован на **EF Core**. Включает паттерны **Repository** и **Unit of Work** для обеспечения целостности транзакций.
* **DTO (Data Transfer Objects)** — Объекты передачи данных. Служат для безопасного обмена информацией между клиентом и сервером.

---

## Стек

* **Runtime:** .NET 10
* **Database:** SQL Server (EF Core)
* **Mapping:** AutoMapper
* **Auth:** JWT Bearer Token
* **Documentation:** Scalar | OpenApi
* **Web-API**: ASP.NET 

---

## Быстрый старт

### 1. Настройка базы данных
Откройте файл `API/appsettings.json` и укажите вашу строку подключения в секции `ConnectionStrings`.

**Пример корректной настройки:**
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=OnlineMarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
### 2. Установка зависимостей и запуск
Выполните следующие команды в корневой папке проекта:

1. Восстановление пакетов
```
dotnet restore
```

2. Применение миграций к базе данных
```
dotnet ef database update --project DAL --startup-project API
```
3. Запуск проекта
```
dotnet run --project API
```
---

## Реализованный функционал

В проекте реализованы:
* **CRUD** для всех моделей БД

---

## Структура проекта
```
└── OnlineMarket/
    ├── API/          # Контроллеры, Extensions и Program.cs
    ├── BLL/          # Бизнес-логика, профили AutoMapper и сервисы
    ├── DAL/          # Контекст БД (FinalProjectDbContext), репозитории и UOW
    ├── DTO/          # Модели запросов, ответов и композитные DTO
    └── OnlineMarket.slnx
```
---
