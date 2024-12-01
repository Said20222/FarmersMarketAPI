# FarmersMarketAPI

.NET 8 Web API server for the Farmer Market System project for the course lab CSCI 361

**Do not forget to create appsettings.json file in the root folder**
with the following content:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ConnStr": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StoreSystemDB;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
    "AzureDbConnection": "<paste Azure SQL Connection String>"
  },
  "JWT": {
    "ValidAudience": "http://localhost:4200",
    "ValidIssuer": "http://localhost:5000",
    "Secret": "<any generated secret - must be more than 80 characters>"
  }
}
```
