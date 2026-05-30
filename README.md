# SatMonitor API

API REST para monitoramento de satélites e sensores orbitais, desenvolvida em .NET 10 com Entity Framework Core e banco de dados Oracle.

> Global Solution 2026/1 — FIAP | Advanced Business Development with .NET

---

## 👥 Integrantes

| Nome | RM |
|------|-----|
| Leonardo José Pereira | RM563065 |
| Pedro Henrique de Oliveira | RM562312 |
| Fabricio Henrique Pereira | RM563237 |
| Henrique Sinkevicius Maran | RM562977 |
| Miguel Henrique Oliveira Dias | RM565492 |

---

## 🏗️ Arquitetura

```
SatMonitor/
├── src/
│   ├── SatMonitor.Domain/         # Entidades e Enums
│   ├── SatMonitor.Application/    # DTOs e Interfaces
│   ├── SatMonitor.Infrastructure/ # DbContext, Migrations e Services
│   └── SatMonitor.Api/            # Controllers, Middlewares e configuração
```

### Diagrama de Relacionamentos

```
Missao (1) ──── (N) Satelite (1) ──── (N) Sensor (1) ──── (N) LeituraSensor
```

---

## 🚀 Como Executar

### Pré-requisitos
- .NET 10 SDK
- Acesso ao Oracle Database (FIAP)

### Configuração

1. Clone o repositório:
```bash
git clone https://github.com/leojp04/SatMonitor.git
cd SatMonitor
```

2. Crie o arquivo `src/SatMonitor.Api/appsettings.json` baseado no `appsettings.example.json`:
```json
{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User Id=SEU_RM;Password=SUA_SENHA;"
  }
}
```

3. Execute as migrations:
```bash
dotnet ef database update --project src/SatMonitor.Infrastructure --startup-project src/SatMonitor.Api
```

4. Rode a API:
```bash
dotnet run --project src/SatMonitor.Api
```

5. Acesse o Swagger:
```
http://localhost:5291/swagger
```

---

## 📡 Endpoints

### Missoes
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/Missoes | Lista todas as missões |
| GET | /api/Missoes/{id} | Busca missão por ID |
| POST | /api/Missoes | Cria nova missão |
| PUT | /api/Missoes/{id} | Atualiza missão |
| DELETE | /api/Missoes/{id} | Remove missão |

### Satelites
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/Satelites | Lista todos os satélites |
| GET | /api/Satelites/{id} | Busca satélite por ID |
| GET | /api/Satelites/missao/{missaoId} | Lista satélites de uma missão |
| POST | /api/Satelites | Cria novo satélite |
| PUT | /api/Satelites/{id} | Atualiza satélite |
| DELETE | /api/Satelites/{id} | Remove satélite |

### Sensores
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/Sensores | Lista todos os sensores |
| GET | /api/Sensores/{id} | Busca sensor por ID |
| GET | /api/Sensores/satelite/{sateliteId} | Lista sensores de um satélite |
| POST | /api/Sensores | Cria novo sensor |
| PUT | /api/Sensores/{id} | Atualiza sensor |
| DELETE | /api/Sensores/{id} | Remove sensor |

### Leituras
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/Leituras | Lista todas as leituras |
| GET | /api/Leituras/{id} | Busca leitura por ID |
| GET | /api/Leituras/sensor/{sensorId} | Lista leituras de um sensor |
| POST | /api/Leituras | Registra nova leitura |
| DELETE | /api/Leituras/{id} | Remove leitura |

---

## 🧪 Exemplos de Teste

### 1. Criar Missão
```http
POST /api/Missoes
Content-Type: application/json

{
  "nome": "Missão Sentinel-1",
  "descricao": "Monitoramento climático orbital",
  "dataLancamento": "2024-03-15T00:00:00",
  "status": "Ativa"
}
```

### 2. Criar Satélite
```http
POST /api/Satelites
Content-Type: application/json

{
  "nome": "SAT-BR-01",
  "altitude": 550.5,
  "inclinacao": 97.6,
  "dataLancamento": "2024-06-01T00:00:00",
  "missaoId": 1
}
```

### 3. Criar Sensor
```http
POST /api/Sensores
Content-Type: application/json

{
  "nome": "Sensor Térmico Principal",
  "tipo": "Temperatura",
  "unidade": "°C",
  "sateliteId": 1
}
```

### 4. Registrar Leitura
```http
POST /api/Leituras
Content-Type: application/json

{
  "valor": -45.7,
  "dataHoraLeitura": "2024-06-15T14:30:00",
  "sensorId": 1
}
```

---

## 🛠️ Tecnologias

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- Oracle Database
- Swagger / OpenAPI
```

