# NotificationsAPI

Microsserviço responsável por simular o envio de notificações por e-mail na plataforma FiapCloudGames.

## Responsabilidades
- Consumir `UserCreatedEvent` → simular envio de e-mail de boas-vindas (log no console)
- Consumir `PaymentProcessedEvent` → simular envio de confirmação de compra (log no console)

## Eventos consumidos
| Evento | Ação |
|--------|------|
| `UserCreatedEvent` | Loga e-mail de boas-vindas no console |
| `PaymentProcessedEvent` | Se Approved: loga confirmação de compra no console |

## Variáveis de Ambiente

| Variável | Descrição |
|----------|-----------|
| `RabbitMQ__Host` | Host do RabbitMQ |
| `RabbitMQ__Username` | Usuário RabbitMQ |
| `RabbitMQ__Password` | Senha RabbitMQ |

## Health Check

```
GET /health
```
