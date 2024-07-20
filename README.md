# Elers

Online distance learning platform. Bachelor's thesis

### Connect with me:

[![LinkedIn Badge](https://cdn.icon-icons.com/icons2/99/PNG/32/linkedin_socialnetwork_17441.png)](https://www.linkedin.com/in/oleksandr-tsvik/)
[![Email Badge](https://cdn.icon-icons.com/icons2/72/PNG/32/email_14410.png)](mailto:oleksandr.zwick@gmail.com)

## [Demo Website](https://elers.onrender.com/)

### Student credentials

email: student@gmail.com\
password: 123456

### Student credentials that can create new courses

email: demo@gmail.com\
password: 123456

### Teacher credentials

email: teacher@gmail.com\
password: 123456

## Docker Setup

### Step 1: Create Database Migration

First, you need to create a database migration. Make sure to fill in the `appsettings.json` file in the `Api` folder.

Run the following command to update the database in the Production environment:

```sh
dotnet ef database update -s Api -p Persistence -c ApplicationDbContext -- --environment Production
```

### Step 2: Build Docker Image

To create a Docker image, execute the following command in the root of your project:

```sh
docker build . -t elers
```

> Alternatively, you can use the pre-built Docker image available on Docker Hub: https://hub.docker.com/r/oleksandrtsvik/elers

### Step 3: Run Docker Container

Before running the container, make sure you have a `.env` file with the necessary production environment variables.

To run the Docker container, use the following command:

```sh
docker run \
  -p 5000:8080 \
  --name elers-app \
  --env-file ./.env \
  -it \
  --rm \
  elers
```

## References

- [Policy-based authorization in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-8.0)
- [Custom Authorization Policy Providers using IAuthorizationPolicyProvider in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/iauthorizationpolicyprovider?view=aspnetcore-8.0)
- [ASP.NET Core: Custom Authorization Policies with Multiple Requirements and Multiple Handlers](https://medium.com/@kadir.kilicoglu_67563/asp-net-core-custom-authorization-policies-with-multiple-requirements-and-multiple-handlers-487f920ae13e)
- [Routing in ASP.NET Core. Route constraints](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-constraints)
- [EF Core. Inheritance. Entity type hierarchy mapping](https://learn.microsoft.com/uk-ua/ef/core/modeling/inheritance#entity-type-hierarchy-mapping)
- [MongoDB. GUIDs](https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/serialization/guid-serialization/)
- [MongoDB. C# GUID Style dont work](https://www.mongodb.com/community/forums/t/c-guid-style-dont-work/126901)
- [MongoDB. Polymorphic Objects](https://www.mongodb.com/docs/drivers/csharp/upcoming/fundamentals/serialization/polymorphic-objects/)
- [MongoDB. Mapping Classes](https://mongodb.github.io/mongo-csharp-driver/2.0/reference/bson/mapping/)
- [Updating Arrays in MongoDB with C#](https://kevsoft.net/2020/03/23/updating-arrays-in-mongodb-with-csharp.html)
- [Configure portable object localization in ASP.NET Core](https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/fundamentals/portable-object-localization.md)
- [Localization (OrchardCore.Localization)](https://docs.orchardcore.net/en/main/reference/modules/Localize/)
- [Docker images for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-8.0)
- [Naming of environment variables](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0#naming-of-environment-variables)
- [The .NET Weekly Newsletter | Milan Jovanović](https://www.milanjovanovic.tech/blog)
- [Enforcing Software Architecture With Architecture Tests](https://www.milanjovanovic.tech/blog/enforcing-software-architecture-with-architecture-tests)
- [GitHub Markup Reference](https://gist.github.com/ChrisTollefson/a3af6d902a74a0afd1c2d79aadc9bb3f#file-1_markup-md)
