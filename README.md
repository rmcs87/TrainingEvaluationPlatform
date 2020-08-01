What is TEP - Training Evaluation Platform
=====================
TEP is a open-source project written in .NET Core for BackEnding VR Trainning Aplications.

Besides providing services for storage and edition tools; score processing; and alternative apps, such as Quizzes, this project aims to be used as self-learning PBL project by the authors. :mortar_board: 

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/91e984d9fad349de82d2888efce791ca)](https://app.codacy.com/manual/rmcs87/TrainingEvaluationPlatform?utm_source=github.com&utm_medium=referral&utm_content=rmcs87/TrainingEvaluationPlatform&utm_campaign=Badge_Grade_Dashboard)[![License](https://img.shields.io/github/license/rmcs87/TrainingEvaluationPlatform)](LICENSE)![.NET Core](https://github.com/rmcs87/TrainingEvaluationPlatform/workflows/.NET%20Core/badge.svg?branch=master)

## How to use:
- You will need the latest Visual Studio 2019 and the latest .NET Core SDK.
- The latest SDK and tools can be downloaded from https://dot.net/core.

## Technologies being Studied:
- ASP.NET Core 3.1 (with .NET Core 3.1)
- ASP.NET Core with JWT Bearer Authentication
- Entity Framework Core 3.1
- AutoMapper
- FluentValidation
- MediatR
- MSTests
- Coverage*
- Swagger*
- GitHub Actions

## Architecture Principles being Studied:
- Full architecture with responsibility separation concerns, SOLID and Clean Code*
- Domain Driven Design
- Domain Events*
- Domain Notification*
- Domain Validations*
- CQRS
- Event Sourcing*
- Unit of Work*
- Repository*

## Study References:

#### Architecture
*  Common web application architectures: [link](https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
*  Fundamentos de Arquitetura de Software: [link](https://www.youtube.com/watch?v=jUH5lKfpWE0)
*  Modelo C4 de documentação: [link](https://www.infoq.com/br/articles/C4-architecture-model/)
*  Services: [link](https://pt.stackoverflow.com/questions/365350/domain-driven-design-qual-a-diferen%C3%A7a-entre-domain-services-infrastructure-ser)
*  Clean Architecture with ASP.NET Core 3.0: [link](https://www.youtube.com/watch?v=dK4Yb6-LxAk)
*  A Brief Intro to Clean Architecture, Clean DDD, and CQRS: [link](https://medium.com/software-alchemy/a-brief-intro-to-clean-architecture-clean-ddd-and-cqrs-23243c3f31b3)

#### ASP.NET Core
*  IActionResult and ActionResult: [link](https://exceptionnotfound.net/asp-net-core-demystified-action-results/)
*  Create web APIs with ASP.NET Core: [link](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.0#apicontroller-attribute)
*  Model Binding in ASP.NET Core: [link](https://docs.microsoft.com/pt-br/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1)

#### AutoMapper:
*  AutoMapper: [link](https://docs.automapper.org/en/latest/Getting-started.html)
*  AutoMapper in C#: [link](https://dotnettutorials.net/lesson/automapper-in-c-sharp/)
*  5 AutoMapper tips and tricks: [link](https://www.codeproject.com/articles/814869/automapper-tips-and-tricks)

#### Await/Async
*  C# Async / Await: [link](https://www.youtube.com/watch?v=2moh18sh5p4)
*  C# Advanced Async: [link](https://www.youtube.com/watch?v=ZTKGRJy5P2M)

#### Azure
*  Deploy a WebApp: [link](https://docs.microsoft.com/en-us/azure/developer/python/tutorial-deploy-app-service-on-linux-04)

#### Caching:
*  Caching in ASP.NET Core: [link](https://docs.microsoft.com/pt-br/aspnet/core/performance/caching/response?view=aspnetcore-3.1)

#### CI/CD
*  Integração Contínua: [link](https://www.youtube.com/watch?v=nI3IjYcBGiU)
*  Integração e entrega contínuas: pipeline CI/CD: [link](https://www.redhat.com/pt-br/topics/devops/what-is-ci-cd)
*  GitHub Actions: [link](https://docs.github.com/en/actions)

#### Compression:
*  Compression in ASP.NET Core: [link](https://docs.microsoft.com/pt-br/aspnet/core/performance/response-compression?view=aspnetcore-3.1)

#### DDD:
*  Arquitetura em camadas com DDD: [link](https://medium.com/@ericandrade_24404/)parte-01-criando-arquitetura-em-camadas-com-ddd-inje%C3%A7%C3%A3o-de-dep-ef-60b851c88461)
*  Create Data Transfer Objects (DTOs): [link](https://docs.microsoft.com/pt-br/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5)
*  DDD ASP.NET: [link](https://www.devmedia.com.br/ddd-asp-net-criando-um-repositorio-de-dados-parte-1/31733)
*  DDD: Repository Implementation Patterns: [link](https://lostechies.com/jimmybogard/2009/09/03/ddd-repository-implementation-patterns/)
*  Domain-Driven Design: Atacando as Complexidades no Coração do Software: [link](https://www.amazon.com.br/Domain-Driven-Design-Eric-Evans/dp/8550800651)
*  Services in Domain-Driven Design (DDD): [link](http://gorodinski.com/blog/2012/04/14/services-in-domain-driven-design-ddd/)

#### Docker: (future steps)
*  Step by step - Run and Connect to SQL Server in Docker: [link](https://www.youtube.com/watch?v=SJAl3vOX05M)
*  Docker - Guia de Referência Gratuito:
[Docker Guide](https://medium.com/@renato.groffe/docker-guia-de-refer%C3%AAncia-gratuito-70c14cfd8132 "Docker Guide")

#### EFC:
*  Entity Framework Core: [link](https://docs.microsoft.com/pt-br/ef/core/)
*  Criando uma API com ASP.NET Core 3 e EF Core 3 em menos de 15 minutos: [link](https://www.youtube.com/watch?v=but7jqjopKM)
*  Entity Framework Core Guide: [link](https://www.learnentityframeworkcore.com/)
*  Migrations: [link](https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx)

#### Fluent Validation
*  Fluent validation: [link](https://fluentvalidation.net/)

#### Good Practices
*  Documentando o código no Visual Studio: [link](https://www.devmedia.com.br/documentando-o-codigo-no-visual-studio/28927)
*  Clean Code: [link](https://www.pearson.ch/Informatik/PrenticeHall/EAN/9780132350884/Clean-Code)

#### MediatR
*  Mediator Pattern com MediatR no ASP.NET Core: [link](https://www.treinaweb.com.br/blog/mediator-pattern-com-mediatr-no-asp-net-core/)
*  Clean ASP.NET Core API using MediatR and CQRS: [link](https://www.youtube.com/watch?v=YzOBrVlthMk)
*  Validation using MediatR's Pipeline Behaviors and FluentValidation: [link](https://www.youtube.com/watch?v=2JzQuIvxIqk)

#### Microservices
*  Monolith Decomposition Patterns: [link](https://www.infoq.com/presentations/microservices-principles-patterns/)

#### Logging
*  Logging in .NET Core 3.0 and Beyond: [link](https://www.youtube.com/watch?v=oXNslgIXIbQ)

#### REST
*  SOAP vs REST vs JSON: [link](https://raygun.com/blog/soap-vs-rest-vs-json/)
*  What is REST: [link](https://restfulapi.net/)
*  Architectural Styles and the Design of Network-based Software Architectures: [link](https://www.ics.uci.edu/~fielding/pubs/dissertation/)fielding_dissertation.pdf)
*  JSON:API: [link](https://jsonapi.org/)

#### Security
*  Autenticação e Autorização com Bearer e JWT: [link](https://balta.io/blog/aspnetcore-3-autenticacao-autorizacao-bearer-jwt?rdst_srcid=2132416)
*  JWT com chave simétrica: [link](https://www.brunobrito.net.br/jwt-com-chave-assimetrica/?fbclid=IwAR3B2xknd8cH3XULDsYYCAMhRq6SBQs6ON3shemPIUoXPDbUZpPXyduQld0)
*  Policy based role checks: [link](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1)
*  Safe storage of app secrets in development in ASP.NET Core: [link](https://docs.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows)

#### Tests:
*  Test-Driven Development: [link](https://tdd.caelum.com.br/)
*  Unit test your code: [link](https://docs.microsoft.com/pt-br/visualstudio/test/unit-test-your-code?view=vs-2019)
*  Mocking em testes unitários com o framework Moq: [link](https://www.devmedia.com.br/mocking-em-testes-unitarios-com-o-framework-moq/36724)
*  Moq QuickStart: [link](https://github.com/Moq/moq4/wiki/Quickstart)
*  Como Testar o ASP.NET Core Web API: [link](https://www.infoq.com/br/articles/testing-aspnet-core-web-api/)
*  MSTest Cheat Sheet: [link](https://www.automatetheplanet.com/wp-content/uploads/2018/05/mstest-cheat-sheet-automatetheplanet.pdf || https://www.automatetheplanet.com/mstest-cheat-sheet/)

#### Others:
*  Efficient post calls with HttpClient and JSON.NET: [link](https://johnthiriet.com/efficient-post-calls/)
*  Upload files: [link](https://docs.microsoft.com/pt-br/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1)
*  multipart requests with JSON and file: [link](https://thomaslevesque.com/2018/09/04/handling-multipart-requests-with-json-and-file-uploads-in-asp-net-core/)
*  Dependency injection: [link](https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1)
*  Generating realistic fake data in .NET using Bogus: [link](https://www.youtube.com/watch?v=T9pwE1GAr_U)
*  Background tasks with hosted services in ASP.NET Core: [link](https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio)
*  No need for repositories and unit of work with Entity Framework Core: [link](https://gunnarpeipman.com/ef-core-repository-unit-of-work/)
