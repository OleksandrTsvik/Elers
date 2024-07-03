# Elers/API

## Running Locally

To run the project locally, first, you need to install dependencies:

```sh
dotnet restore
dotnet build
```

Be sure to fill in the `appsettings.json` file!

> **NOTES:**
>
> For MongoDB, you can use [MongoDB Atlas](https://www.mongodb.com/docs/atlas).
>
> You must have a [Supabase](https://supabase.com/) account.
>
> You must have a [Cloudinary](https://cloudinary.com/) account.

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
