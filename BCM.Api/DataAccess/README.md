# Database Migrations

## How to add Db migration
- Install tools (https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- Don't forget to specify in memory db correctly (**"DataSource=file::memory:?cache=shared"**) 

- Create a new migration
```shell
dotnet ef migrations add Initial --output-dir .\DataAccess\Migrations\
```

- Update the database (https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created)

- Update the database manually (not in memory)
```shell
dotnet ef database update
```