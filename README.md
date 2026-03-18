# NightTech
NightTech is a backend .NET 8 service for managing users, authentication, and email verification with secure deployment on Azure. The app demonstrates dynamic URL generation, JWT-based email verification, and runtime secret management via Azure Key Vault.

## Features
- User registration with email verification flow
- JWT-based email verification token system
- Resend email verification functionality
- Email service integrated via SMTP
- Secrets managed securely in Azure Key Vault
- Fully deployable to Azure App Service

## Tech Stack
- C# .NET 8
- MediatR (CQRS pattern)
- AutoMapper
- MSSQL / Azure SQL
- JWT Authentication
- Serilog Logging
- Fluent Validation

## Setup

1. Clone the repo: https://github.com/toxp1233/NightTech

2. Add your local `appsettings.Development.json` with safe defaults.

3. Set up Azure App Service configuration:
- `AppSettings:BaseUrl`
- `TokenSettings:Key` (can also use Key Vault)
- `EmailSettings` (via Key Vault or environment variables)

## Deployment

- Deploy via Visual Studio / VS Code using publish profile.
- Secrets are injected at runtime from Azure Key Vault.
- Environment-specific configuration handled via `appsettings.Development.json` locally and Azure App Service settings in production.

- ## Notes
- Email verification tokens expire after a configurable period.
- Resend limits prevent abuse.
- Dynamic links adjust automatically based on environment (local or production).

Future improvements:
- Add frontend link for email verification
- Logging improvements & monitoring
- CI/CD with GitHub Actions
