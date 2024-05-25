# Elers/API

## Running Locally

To run the project locally, first, you need to install dependencies:

```sh
dotnet restore
dotnet build
```

Fill in the `appsettings.json` file with the following required fields:

```json
{
  "DatabaseSettings": {
    "ApplicationDb": {
      "ConnectionString": ""
    },
    "MongoDb": {
      "ConnectionString": "",
      "DatabaseName": ""
    }
  },
  "Jwt": {
    "Issuer": "",
    "Audience": "",
    "AccessTokenExpirationInMinutes": 0,
    "RefreshTokenExpirationInDays": 0,
    "SecretKey": ""
  },
  "Supabase": {
    "Url": "",
    "Key": "",
    "BucketName": ""
  },
  "FileSettings": {
    "SizeLimit": 0
  }
}
```

> **NOTE:** You must have a [Supabase](https://supabase.com/) account.

Then, create EF Migrations and database:

```sh
dotnet ef migrations add InitialCreate -s API -p Persistence -c ApplicationDbContext -o Migrations/ApplicationDb

dotnet ef database update -s API -p Persistence -c ApplicationDbContext
```

Run api:

```sh
cd API
dotnet watch --no-hot-reload
```

## Setting up Supabase

1. Create a project in the [Supabase](https://supabase.com/) **dashboard**.
2. Create a **Bucket** in **Storage**.
3. Add `Url`, `Key` and `BucketName` _(elers-files)_ to `appsettings.json`.
4. Configure `anon policy` to allow all operations on the project to be performed without an authorized user.
