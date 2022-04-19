
Solution is using so called clean architecture and DDD approach:
  
 - DDD domain entities in the centre (911Medical.Domain)
 - Application layer around it (911Medical.Application)
 - 911Medical.Persistance in infrastructure layer
 - and 911Medical.WebApp in presentation.

 You can read about it at https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture.

 Some of the patterns used in solution:
  - Mediator pattern using Mediatr library (https://www.programmingwithwolfgang.com/mediator-pattern-in-asp-net-core-3-1/)
  - Specification pattern using Ardalis.Specification library (https://ardalis.github.io/Specification/)
  - CQRS pattern with help of Mediatr library (https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
  - ...

