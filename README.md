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

## Solução, decisões e premissas

Esta seção descreve as decisões de design, premissas e como executar a solução entregue para o desafio.

### Visão geral da solução
A aplicação é uma API HTTP em ASP.NET Core (.NET 8) que expõe o endpoint `POST /api/password/validate` que recebe um objeto JSON com a propriedade `password` e retorna um JSON com `isValid: true|false`.

A solução segue uma separação em camadas simples:
- `Controllers` — recepção de requests e orquestração.
- `Services` — camada de serviço que encapsula a validação e abstrai o validador.
- `Validators` — responsável pelas regras de validação da senha.
- `Models` — DTOs de request/response.

Foram aplicados princípios SOLID básicos: `IPasswordValidator` e `IPasswordService` para reduzir acoplamento e facilitar testes.

### Regras implementadas
A senha é considerada válida quando satisfaz todas as regras abaixo:
- Mínimo de 9 caracteres.
- Ao menos 1 dígito.
- Ao menos 1 letra minúscula.
- Ao menos 1 letra maiúscula.
- Ao menos 1 caractere especial (apenas este conjunto: `!@#$%^&*()-+`).
- Não possui espaços em branco.
- Não possui caracteres repetidos (comparação case-sensitive, ou seja, `a` e `A` são diferentes).

> Premissa: a implementação atual opera por `char` .NET e não tenta normalização Unicode (NFC/NFD). Caso precise suportar formas Unicode equivalentes, isso deve ser documentado e tratado separadamente.

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

### Pré-requisitos
- .NET 8 SDK

### Rodar a API
dotnet run --project PasswordValidatorApi

A API estará disponível no Swagger:
https://localhost:xxxx/swagger

### Executar testes (unitários + integração)
dotnet test

### Testar endpoint (exemplo usando curl)
curl -k -X POST https://localhost:7250/api/password/validate \
  -H "Content-Type: application/json" \
  -d '{"password":"AbTp9!fok"}'
Observação: Use `-k` no curl para ignorar certificado dev local se necessário.

---

## Cobertura de testes
- Testes unitários cobrindo as regras do validador (exemplos do enunciado) em `PasswordValidatorApi.Tests.Unit.Validators`.
- Testes unitários para serviço com mock (`IPasswordValidator`) em `PasswordValidatorApi.Tests.Unit.Services`.
- Testes de integração para o controller usando `WebApplicationFactory<Program>` em `PasswordValidatorApi.Tests.Integration.Controllers` cobrindo: senha válida, senha inválida, espaço em senha, caracteres repetidos e corpo vazio (esperado `400`).

---

## Decisões, racional e premissas

### Racional rápido
- Separei responsabilidades em `Controller` / `Service` / `Validator` para seguir SRP: controller trata HTTP, service orquestra e validator contém as regras, facilitando testes e manutenção.
- Usei interfaces (`IPasswordValidator`, `IPasswordService`) para desacoplamento e para permitir mocks nos testes.
- Escolhi single-pass (loop único) no validador para reduzir iterações/allocate e detectar erros cedo; isto é uma otimização prática sem alterar o contrato do enunciado.

### Premissas importantes (o que está implementado)
- Repetição é case-sensitive: `a` e `A` são considerados diferentes.
- Não há normalização Unicode: a validação opera sobre `char` do .NET. Entradas com formas combinadas (ex.: `e` + acento) podem ser tratadas como diferentes. Emojis e símbolos fora do conjunto aceito podem ser rejeitados.
- Apenas os caracteres especiais do conjunto `!@#$%^&*()-+` são considerados válidos; qualquer outro símbolo especial (ex.: `?`, emoji) torna a senha inválida.
- A validação permite apenas letras, dígitos e os especiais acima — caracteres fora desse conjunto são recusados.
- Não há logging de conteúdo sensível (não logamos a senha).

### Como executar (resumo)
# Rodar API
dotnet run --project PasswordValidatorApi

# Rodar testes (unit + integration)
dotnet test

### Cobertura de testes
- Unit: `PasswordValidator` (regras do enunciado) e `PasswordService` (mockando `IPasswordValidator`).
- Integration: testes do controller com `WebApplicationFactory<Program>` cobrindo cenários válidos/inválidos e corpo ausente (400).

### Notas e próximos passos possíveis
- Se for necessário suportar equivalência Unicode (ex.: normalizar acentos) ou tratar emojis como "1 caractere visual", é possível adicionar normalização (`NormalizationForm.FormC`) ou usar `StringInfo` para iterar text elements; isso aumenta complexidade e deve ser justificado pelo requisito.

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
- Suporte explícito a normalização Unicode quando necessário.
- Logging e métricas para produção.
- Policies de segurança e rate limiting.