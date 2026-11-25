# TradeFlow

ERP-style backend built as independent microservices: users (customers, suppliers, employees), products and brands, warehouse inventory, and sales orders, with a separate read side for sales order product queries.

## Architecture

Five ASP.NET Core services behind a YARP reverse-proxy API Gateway:

- **Gateway**: routes `/api/*` requests to the right service (YARP ReverseProxy, config in `Gateway/appsettings.json`).
- **UserService**: users, customers, suppliers, employees and addresses.
- **ProductSercice**: products, brands and categories. Publishes product events over RabbitMQ.
- **InventoryService**: warehouses and stock (reserve, release, increase, decrease, confirm reservation).
- **SalesService**: sales orders and order items. Publishes sales events over RabbitMQ.
- **SalesRead**: read-side service (CQRS read model) that consumes product and sales events from RabbitMQ and serves a flattened read model.
- **Shared**: shared kernel (common primitives, Result pattern).

Each service follows Clean Architecture (`Api` / `Application` / `Domain` / `InfraStrucure`) with its own SQL Server database and EF Core migrations.

## Patterns

- CQRS with MediatR-style command/query handlers and FluentValidation validators
- Separate read-model service fed by RabbitMQ consumers (`RabbitMqConsumerService`, event processors)
- Domain events and per-entity domain errors with the Result pattern
- EF Core audit interceptor, global exception handling in each API

## Tech

.NET 8, ASP.NET Core, Entity Framework Core, SQL Server, RabbitMQ, YARP, FluentValidation

## Running locally

1. Prerequisites: .NET 8 SDK, SQL Server, RabbitMQ.
2. Update the connection string in each service's `appsettings.json` (keep secrets out of source control; use `dotnet user-secrets`).
3. Apply migrations per service: `dotnet ef database update`.
4. Start the services, then the Gateway. Sample requests are in the included `.http` files.
