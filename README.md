# WebSocket API

## 📈 Market Assets Price API

Це REST+WebSocket API-сервіс для отримання **історичних** та **реальних** цін ринкових активів (наприклад, `EUR/USD`, `GOOG`) з використанням платформи **Fintacharts**.

---

## 🧾 Зміст

- [Опис](#опис)
- [Запуск через Docker](#запуск-через-docker)
- [Swagger](#swagger)
- [API ендпоінти](#api-ендпоінти)
- [Приклади використання](#приклади-використання)

---

## 📌 Опис

Цей проєкт надає API для роботи з ринковими активами, використовуючи WebSocket для отримання даних у реальному часі від Fintacharts та REST для доступу до історичних даних. Основні можливості:

- 🧠 Зберігання активів та історії цін у базі даних **SQL Server** з використанням **Entity Framework Core**.
- 🌐 Інтерактивна документація API через **Swagger UI**.
- 🐳 Розгортання за допомогою **Docker** для спрощення запуску та масштабування.
- 📡 Отримання цін активів у реальному часі через WebSocket-з’єднання.

---

## ▶️ Запуск через Docker

### 1. Клонувати репозиторій

```bash
git clone https://github.com/Rostyslav-Bodnar/WebSocketAPI.git
cd WebSocket_API
```

Або встановити docker-compose.yml з репозиторія

### 2. Побудувати та запустити Docker-контейнери

```bash
docker-compose up --build -d
```

Ця команда:
- Будує образ для API (`rostyslav05/websocket-api:latest`).
- Запускає контейнер SQL Server (`mcr.microsoft.com/mssql/server:2022-latest`).
- Налаштовує мережу для взаємодії між контейнерами.

### 3. Доступ до API

API буде доступне за адресою:
🔗 [http://localhost:8080](http://localhost:8080)

## 🧪 Swagger

Інтерактивна документація API доступна через Swagger UI:
🔗 [http://localhost:8080](http://localhost:8080)

---

## 📡 API ендпоінти

| Метод   | Шлях                              | Опис                                      |
|---------|-----------------------------------|-------------------------------------------|
| `GET`   | `/api/asset/supported`           | Отримати список підтримуваних активів      |
| `GET`   | `/api/asset/prices?assetIds=1,2` | Отримати останні ціни для вказаних активів |
| `GET`   | `/api/asset/{symbol}`            | Отримати інформацію про актив за символом |
| `DELETE`| `/api/asset/symbol?symbol={symbol}` | Відписатися від активу та видалити його |
| `GET`   |  `/api/asset/symbol?symbols={symbol}&symbols={symbol}` | Отримати інформацію про активи |

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
   curl http://localhost:8080/api/asset/symbol?symbol=EUR/FUSD?providerName=alpaca
   ```

4. **Видалити актив**:
   ```bash
   curl -X DELETE http://localhost:8080/api/asset/symbol?symbol=EUR/FUSD
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

