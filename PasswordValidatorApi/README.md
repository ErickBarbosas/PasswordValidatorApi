# Password Validator API

API desenvolvida em .NET 8 para validação de senhas seguindo regras específicas de segurança.

O projeto foi desenvolvido seguindo princípios de Clean Code, SOLID, baixo acoplamento, alta coesão e testes automatizados.

---

## Arquitetura da Solução

A aplicação foi organizada em camadas para separar responsabilidades:

PasswordValidatorApi
 ├── Controllers
 ├── Services
 ├── Validators
 ├── Models
 └── Program.cs

PasswordValidatorApi.Tests
 ├── Unit
 └── Integration

### Responsabilidades das camadas

- Controller: Receber requisições HTTP
- Service: Regras de negócio
- Validator: Validação da senha
- Models: Request/Response
- Tests: Testes unitários e integração

---

## Regras de validação da senha

Uma senha válida deve:

- Possuir pelo menos 9 caracteres
- Conter pelo menos 1 dígito
- Conter pelo menos 1 letra minúscula
- Conter pelo menos 1 letra maiúscula
- Conter pelo menos 1 caractere especial (!@#$%^&*()-+)
- Não possuir espaços
- Não possuir caracteres repetidos

---

## Como executar o projeto

### Rodar a API
dotnet run --project PasswordValidatorApi

A API estará disponível no Swagger:
https://localhost:xxxx/swagger

---

## Testes

O projeto possui testes unitários e testes de integração.

### Rodar os testes
dotnet test

---

## Decisões técnicas

- Uso de Interface para reduzir acoplamento
- Camada de Service para regras de negócio
- Uso de POST para enviar a senha com segurança no body
- Testes unitários e de integração
- Arquitetura em camadas

---

## Endpoint

POST /api/password/validate

### Request
{
  "password": "AbTp9!fok"
}

### Response
{
  "isValid": true
}

---

## Tecnologias utilizadas

- .NET 8
- ASP.NET Core
- xUnit
- Moq
- Microsoft.AspNetCore.Mvc.Testing
- Swagger

---

## Possíveis melhorias futuras

- Implementar logging
- Implementar autenticação
- Versionamento de API
- Docker
- CI/CD pipeline