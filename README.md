# WebSocket API

## 📈 Market Assets Price API

Це REST+WebSocket API-сервіс для отримання **історичних** та **реальних** цін ринкових активів (наприклад, `EUR/USD`, `GOOG`) з використанням платформи **Fintacharts**.

---

## 🧾 Зміст

- [Опис](#опис)
- [Передумови](#передумови)
- [Запуск через Docker](#запуск-через-docker)
- [Swagger](#swagger)
- [API ендпоінти](#api-ендпоінти)
- [Конфігурація](#конфігурація)
- [Приклади використання](#приклади-використання)
- [Зупинка та очищення](#зупинка-та-очищення)

---

## 📌 Опис

Цей проєкт надає API для роботи з ринковими активами, використовуючи WebSocket для отримання даних у реальному часі від Fintacharts та REST для доступу до історичних даних. Основні можливості:

- 🧠 Зберігання активів та історії цін у базі даних **SQL Server** з використанням **Entity Framework Core**.
- 🌐 Інтерактивна документація API через **Swagger UI**.
- 🐳 Розгортання за допомогою **Docker** для спрощення запуску та масштабування.
- 📡 Отримання цін активів у реальному часі через WebSocket-з’єднання.

---

## 🛠 Передумови

Перед запуском переконайтеся, що у вас встановлено:

- [Docker](https://www.docker.com/get-started) (включаючи Docker Compose)
- [Git](https://git-scm.com/downloads) (для клонування репозиторію)
- Доступ до Fintacharts API (токен автентифікації)

---

## ▶️ Запуск через Docker

### 1. Клонувати репозиторій

```bash
git clone https://github.com/your-repo-url.git
cd WebSocket_API
```

### 2. Налаштувати конфігурацію

Переконайтеся, що файл `appsettings.json` містить коректний рядок підключення до бази даних та налаштування для Fintacharts API. Наприклад:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db,1433;Database=FinancialInfo;User Id=sa;Password=MyStrength@ssword123;TrustServerCertificate=true"
  },
  "Fintacharts": {
    "ApiUrl": "https://platform.fintacharts.com",
    "WebSocketUrl": "wss://platform.fintacharts.com/api/streaming/ws/v1/realtime",
    "Token": "your-fintacharts-token"
  }
}
```

### 3. Побудувати та запустити Docker-контейнери

```bash
docker-compose up --build -d
```

Ця команда:
- Будує образ для API (`rostyslav05/websocket-api:latest`).
- Запускає контейнер SQL Server (`mcr.microsoft.com/mssql/server:2022-latest`).
- Налаштовує мережу для взаємодії між контейнерами.

### 4. Доступ до API

API буде доступне за адресою:
🔗 [http://localhost:8080](http://localhost:8080)

### 5. Перевірка стану

Перегляньте логи для діагностики:
```bash
docker logs websocket_api-api-1
docker logs websocket_api-db-1
```

---

## 🧪 Swagger

Інтерактивна документація API доступна через Swagger UI:
🔗 [http://localhost:8080/swagger](http://localhost:8080/swagger)

Swagger дозволяє тестувати ендпоінти, переглядати їх параметри та відповіді.

---

## 📡 API ендпоінти

| Метод   | Шлях                              | Опис                                      |
|---------|-----------------------------------|-------------------------------------------|
| `GET`   | `/api/asset/supported`           | Отримати список підтримуваних активів      |
| `GET`   | `/api/asset/prices?assetIds=1,2` | Отримати останні ціни для вказаних активів |
| `GET`   | `/api/asset/{symbol}`            | Отримати інформацію про актив за символом |
| `DELETE`| `/api/asset/symbol?symbol={symbol}` | Відписатися від активу та видалити його |

### Приклади запитів

1. **Отримати підтримуваних активи**:
   ```bash
   curl http://localhost:8080/api/asset/supported
   ```

2. **Отримати ціни для активів**:
   ```bash
   curl "http://localhost:8080/api/asset/prices?assetIds=1&assetIds=2"
   ```

3. **Отримати інформацію про актив**:
   ```bash
   curl http://localhost:8080/api/asset/EUR%2FUSD?providerName=alpaca
   ```

4. **Видалити актив**:
   ```bash
   curl -X DELETE http://localhost:8080/api/asset/symbol?symbol=EUR%2FUSD
   ```

---

## ⚙️ Конфігурація

- **База даних**: Використовується SQL Server у контейнері Docker. Рядок підключення задається у `appsettings.json` або через змінну середовища `ConnectionStrings__DefaultConnection`.
- **Fintacharts API**: Налаштуйте токен і URL у `appsettings.json` для доступу до WebSocket та REST API Fintacharts.
- **Логування**: Увімкнено детальне логування для діагностики (вимкніть `EnableSensitiveDataLogging` у продакшені).

### Налаштування `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db,1433;Database=FinancialInfo;User Id=sa;Password=MyStrength@ssword123;TrustServerCertificate=true"
  },
  "Fintacharts": {
    "ApiUrl": "https://platform.fintacharts.com",
    "WebSocketUrl": "wss://platform.fintacharts.com/api/streaming/ws/v1/realtime",
    "Token": "your-fintacharts-token"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

---

## 📝 Приклади використання

### 1. Отримання активу за символом

```bash
curl -X GET "http://localhost:8080/api/asset/AAPL?providerName=alpaca"
```

**Відповідь**:
```json
[
  {
    "instrumentId": "914f3cf3-0d91-4240-b27e-6479d88df25c",
    "provider": "alpaca",
    "symbol": "AAPL",
    "price": 150.25,
    "updatedAt": "2025-07-05T18:30:00Z",
    "change": 1.25
  }
]
```

### 2. Видалення активу

```bash
curl -X DELETE "http://localhost:8080/api/asset/symbol?symbol=AAPL"
```

**Відповідь**:
```json
{
  "instrumentId": "914f3cf3-0d91-4240-b27e-6479d88df25c",
  "provider": "alpaca",
  "symbol": "AAPL",
  "price": 150.25,
  "updatedAt": "2025-07-05T18:30:00Z",
  "change": 1.25
}
```

### 3. WebSocket підписка

Після створення активу через `GET /api/asset/{symbol}`, сервіс автоматично підписується на оновлення цін через WebSocket. Перевірте логи для підтвердження:

```bash
docker logs websocket_api-api-1
```

Очікуваний лог:
```
info: WebSocket_API.Services.WebSocket.WebSocketClientService[0]
      WebSocket connected to wss://platform.fintacharts.com/...
info: WebSocket_API.Services.WebSocket.WebSocketSubscriptionService[0]
      Sent subscription request for InstrumentId: 914f3cf3-0d91-4240-b27e-6479d88df25c, Provider: alpaca
```

---

## 🛑 Зупинка та очищення

1. Зупинити контейнери:
   ```bash
   docker-compose down
   ```

2. Видалити контейнери, образи та томи (для повного очищення):
   ```bash
   docker-compose down -v --rmi all
   ```

**Примітка**: Видалення томів (`-v`) знищить дані в базі SQL Server. Використовуйте з обережністю.

---

## 📚 Залежності

- **.NET 9.0** – Основа для API
- **Entity Framework Core** – Для роботи з SQL Server
- **Microsoft SQL Server** – База даних
- **Fintacharts API** – Джерело даних про ціни
- **Swagger** – Документація API
- **Docker** – Контейнеризація

---

## 🛠 Вирішення проблем

- **Помилка підключення до бази даних**:
  - Перевірте логи SQL Server:
    ```bash
    docker logs websocket_api-db-1
    ```
  - Переконайтеся, що пароль `sa` відповідає вимогам SQL Server (мін. 8 символів, включаючи великі/малі літери, цифри, спеціальні символи).
  - Дочекайтеся ініціалізації SQL Server (може зайняти до 30 секунд).

- **Помилка WebSocket**:
  - Перевірте токен Fintacharts у `appsettings.json`.
  - Переконайтеся, що `InstrumentId` і `Provider` коректні (використовуйте документацію Fintacharts).

- **Дані не оновлюються**:
  - Перевірте логи WebSocket:
    ```bash
    docker logs websocket_api-api-1
    ```
  - Переконайтеся, що WebSocket-з’єднання активне та надходять повідомлення типу `l1-update`.

---
