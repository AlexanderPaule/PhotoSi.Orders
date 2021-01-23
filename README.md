# PhotoSi.Orders

## Prerequisiti
Avere a disposizione un DBMS Sql Server (ex: SqlExpress).
Configurare la connection string al Database nel file 'appsettings.json', sotto la sezione 'ConnectionStrings.Sales'.
Creare manualmente un database vuoto NON è richiesto.

## Scelte Tecnologiche
- Testing: *NUnit*
- API Documentation: *Swagger*
- Database: SqlServer
- Interazione Database: *Entity Framework Core*
  Ho scelto l'ORM Entity Framework perché per il progetto serve gestire pochi dati e per abbassare il tempo della realizzare l'infratruttura.

## Start
TODO

## Test

### Order1
```json
{
  "id": "44485f64-5717-4562-b3fc-2c963f66a444",
  "createdOn": "2021-01-22T23:42:54.573Z",
  "category": {
      "id": "222174e4-37b7-44ee-a8a2-ee920c6fab9d"
    },
  "products": [
    {
      "id": "211174e4-37b7-44ee-a8a2-ee920c6fab9d",
      "customOptions": [
        {
          "id": "311174e4-37b7-44ee-a8a2-ee920c6fab9d",
          "name": "Dimensioni",
          "content": "21cm x 21cm"
        }
      ]
    }
  ]
}
```

### Order1
```json
{
  "id": "55585f64-5717-4562-b3fc-2c963f66a555",
  "createdOn": "2021-01-22T23:42:54.573Z",
  "category": {
      "id": "222174e4-37b7-44ee-a8a2-ee920c6fab9d"
    },
  "products": [
    {
      "id": "211174e4-37b7-44ee-a8a2-ee920c6fab9d",
      "customOptions": []
    }
  ]
}
```