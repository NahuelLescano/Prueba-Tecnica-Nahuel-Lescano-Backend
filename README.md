# Unilink Roulette API

Este proyecto es una API REST para simular apuestas de ruleta, desarrollada en C# 12 y .NET 8. Utiliza Entity Framework Core con SQLite para persistencia de datos y sigue una arquitectura sencilla basada en controladores.

## Características principales

- **Simulación de tiradas de ruleta:**  
  Endpoint para obtener un número aleatorio (0-36) y color ("rojo" o "negro").

- **Gestión de usuarios y saldos:**  
  Permite consultar y actualizar el saldo de cada usuario, sumando o restando montos según las apuestas.

- **Cálculo de premios:**  
  Soporta distintos tipos de apuestas:
  - Por color
  - Por paridad y color
  - Por número y color  
  El cálculo del premio se realiza según el tipo de apuesta y el resultado de la tirada.

- **Persistencia con SQLite:**  
  Los saldos de los usuarios se almacenan en una base de datos SQLite, fácilmente configurable.

- **Swagger UI:**  
  Documentación y pruebas interactivas disponibles en `/swagger`.

## Endpoints principales

- `GET /api/roulette/spin`  
  Devuelve un resultado de ruleta (número y color).

- `POST /api/roulette/payout`  
  Calcula el premio de una apuesta según el resultado.

- `GET /api/users/{name}`  
  Consulta el saldo de un usuario.

- `POST /api/users/save`  
  Suma o resta un monto al saldo de un usuario.

## Estructura del proyecto

- **Controllers:**  
  - `RouletteController`: Lógica de la ruleta y cálculo de premios.
  - `UsersController`: Gestión de usuarios y saldos.

- **Models:**  
  - `SpinResult`, `BetRequest`, `PayoutResponse`, `UserBalance`, etc.

- **Data:**  
  - `AppDbContext`: Contexto de EF Core para acceso a datos.

## Requisitos

- .NET 8 SDK
- SQLite (por defecto, configurable)

---

Desarrollado como prueba técnica para Unilink.
