What is TEP - Training Evaluation Platform
=====================
TEP is a open-source project written in .NET Core for BackEnding VR Trainning Aplications.

Besides providing services for storage and edition tools; score processing; and alternative apps, such as Quizzes, this project aims to be used as self-learning PBL project by the authors. :mortar_board: 

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/91e984d9fad349de82d2888efce791ca)](https://app.codacy.com/manual/rmcs87/TrainingEvaluationPlatform?utm_source=github.com&utm_medium=referral&utm_content=rmcs87/TrainingEvaluationPlatform&utm_campaign=Badge_Grade_Dashboard)[![License](https://img.shields.io/github/license/rmcs87/TrainingEvaluationPlatform)](LICENSE)

## How to use:
- You will need the latest Visual Studio 2019 and the latest .NET Core SDK.
- The latest SDK and tools can be downloaded from https://dot.net/core.

## Technologies being Studied:
- ASP.NET Core 3.1 (with .NET Core 3.1)
- ASP.NET MVC Core 
- ASP.NET WebApi Core with JWT Bearer Authentication
- Entity Framework Core 3.1
- .NET Core Native DI
- AutoMapper
- FluentValidation
- MediatR
- MSTests
- Coverage
- Swagger

## Architecture Principles being Studied:
- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design
- Domain Events
- Domain Notification
- Domain Validations
- CQRS
- Event Sourcing
- Unit of Work
- Repository

## Study References:

#### Architecture
* Common web application architectures: https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
* Fundamentos de Arquitetura de Software: https://www.youtube.com/watch?v=jUH5lKfpWE0
* Modelo C4 de documentação: https://www.infoq.com/br/articles/C4-architecture-model/
* Services: https://pt.stackoverflow.com/questions/365350/domain-driven-design-qual-a-diferen%C3%A7a-entre-domain-services-infrastructure-ser
* Clean Architecture with ASP.NET Core 3.0: https://www.youtube.com/watch?v=dK4Yb6-LxAk
* A Brief Intro to Clean Architecture, Clean DDD, and CQRS: https://medium.com/software-alchemy/a-brief-intro-to-clean-architecture-clean-ddd-and-cqrs-23243c3f31b3

#### AutoMapper:
* AutoMapper: https://docs.automapper.org/en/latest/Getting-started.html
* AutoMapper in C#: https://dotnettutorials.net/lesson/automapper-in-c-sharp/
* 5 AutoMapper tips and tricks: https://www.codeproject.com/articles/814869/automapper-tips-and-tricks

#### Await/Async
* C# Async / Await: https://www.youtube.com/watch?v=2moh18sh5p4
* C# Advanced Async: https://www.youtube.com/watch?v=ZTKGRJy5P2M

#### Caching:
* Caching in ASP.NET Core: https://docs.microsoft.com/pt-br/aspnet/core/performance/caching/response?view=aspnetcore-3.1

#### Compression:
* Compression in ASP.NET Core: https://docs.microsoft.com/pt-br/aspnet/core/performance/response-compression?view=aspnetcore-3.1

#### DDD:
* Arquitetura em camadas com DDD: https://medium.com/@ericandrade_24404/parte-01-criando-arquitetura-em-camadas-com-ddd-inje%C3%A7%C3%A3o-de-dep-ef-60b851c88461
* Create Data Transfer Objects (DTOs): https://docs.microsoft.com/pt-br/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
* DDD ASP.NET https://www.devmedia.com.br/ddd-asp-net-criando-um-repositorio-de-dados-parte-1/31733
* DDD: Repository Implementation Patterns: https://lostechies.com/jimmybogard/2009/09/03/ddd-repository-implementation-patterns/
* Domain-Driven Design: Atacando as Complexidades no Coração do Software: https://www.amazon.com.br/Domain-Driven-Design-Eric-Evans/dp/8550800651
* Services in Domain-Driven Design (DDD): http://gorodinski.com/blog/2012/04/14/services-in-domain-driven-design-ddd/

#### EFC:
* Entity Framework Core: https://docs.microsoft.com/pt-br/ef/core/
* Criando uma API com ASP.NET Core 3 e EF Core 3 em menos de 15 minutos: https://www.youtube.com/watch?v=but7jqjopKM
* Entity Framework Core Guide: https://www.learnentityframeworkcore.com/
* Migrations: https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx

#### Docker: (future steps)
* Azure na Prática Gratuito #2 - Docker: https://www.youtube.com/watch?v=AAp1N3gBWOU
* Docker - Guia de Referência Gratuito:
[Docker Guide](https://medium.com/@renato.groffe/docker-guia-de-refer%C3%AAncia-gratuito-70c14cfd8132 "Docker Guide")

#### Fluent Validation
* Fluent validation: https://fluentvalidation.net/

#### Good Practices
* Documentando o código no Visual Studio: https://www.devmedia.com.br/documentando-o-codigo-no-visual-studio/28927
* Clean Code: https://www.pearson.ch/Informatik/PrenticeHall/EAN/9780132350884/Clean-Code

#### MediatR
* Mediator Pattern com MediatR no ASP.NET Core: https://www.treinaweb.com.br/blog/mediator-pattern-com-mediatr-no-asp-net-core/
* Clean ASP.NET Core API using MediatR and CQRS: https://www.youtube.com/watch?v=YzOBrVlthMk
* Validation using MediatR's Pipeline Behaviors and FluentValidation: https://www.youtube.com/watch?v=2JzQuIvxIqk

#### Logging
* Logging in .NET Core 3.0 and Beyond: https://www.youtube.com/watch?v=oXNslgIXIbQ

#### REST
* SOAP vs REST vs JSON: https://raygun.com/blog/soap-vs-rest-vs-json/
* What is REST: https://restfulapi.net/
* Architectural Styles and the Design of Network-based Software Architectures: https://www.ics.uci.edu/~fielding/pubs/dissertation/fielding_dissertation.pdf
* JSON:API: https://jsonapi.org/

#### Security
* Autenticação e Autorização com Bearer e JWT: https://balta.io/blog/aspnetcore-3-autenticacao-autorizacao-bearer-jwt?rdst_srcid=2132416
* JWT com chave simétrica: https://www.brunobrito.net.br/jwt-com-chave-assimetrica/?fbclid=IwAR3B2xknd8cH3XULDsYYCAMhRq6SBQs6ON3shemPIUoXPDbUZpPXyduQld0
* Policy based role checks: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1

#### Tests:
* Unit test your code: https://docs.microsoft.com/pt-br/visualstudio/test/unit-test-your-code?view=vs-2019
* Mocking em testes unitários com o framework Moq: https://www.devmedia.com.br/mocking-em-testes-unitarios-com-o-framework-moq/36724
* Moq QuickStart:  https://github.com/Moq/moq4/wiki/Quickstart
* Como Testar o ASP.NET Core Web API: https://www.infoq.com/br/articles/testing-aspnet-core-web-api/
* MSTest Cheat Sheet: https://www.automatetheplanet.com/wp-content/uploads/2018/05/mstest-cheat-sheet-automatetheplanet.pdf || https://www.automatetheplanet.com/mstest-cheat-sheet/

#### Others:
* Efficient post calls with HttpClient and JSON.NET: https://johnthiriet.com/efficient-post-calls/
* Upload files: https://docs.microsoft.com/pt-br/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1
* multipart requests with JSON and file: https://thomaslevesque.com/2018/09/04/handling-multipart-requests-with-json-and-file-uploads-in-asp-net-core/
* Dependency injection: https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1

