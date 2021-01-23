# PhotoSi.Orders

## Scelte Tecnologiche
- Testing: **NUnit**
- API Documentation: **Swagger**
- Database: **SqlServer**
- Interazione Database: **Entity Framework Core**

  Ho scelto l'ORM Entity Framework perché per il progetto serve gestire pochi dati e per abbassare il tempo della realizzare l'infratruttura.

## Demo
### Prerequisiti
- Avere a disposizione un DBMS Sql Server (ex: SqlExpress).

### Setup
- Clonare il reository
- Aprire la solution utilizzando visual studio Visual Studio
- Configurare la connection string al Database nel file 'appsettings.json', sotto la sezione 'ConnectionStrings.Sales'.

  Creare manualmente un database vuoto NON è richiesto.

- Avviare il servizio premendo F5 o ctrl+F5. Questa azione comporterà le seguenti operazioni:
-- Il servizio API verrà avviato.
-- Un Database conforme al modello definito via EF Core verrà generato nell'istanza specificata al punto precedente se non è già presente.
-- Il browser di default si avvierà puntanto all'indirizzo 'https://localhost:44354/swagger/index.html'

   La libreria swagger integrata nel progetto faciliterà l'iterazione con il servizio

Il progetto mette a disposizione 2 controller, **Demo** e **Orders**.
Il controller **Demo** permette di generare un setup base per poter testare il comportamento degli ordini, inoltre permette di visualizzare i dati utilizzati nel setup.

- Effettuare la chiamata alla rotta **[POST:/Demo/SetUp]**

A questo punto il servizio è in condizioni di procedere con il test della generazione e lettura degli ordini.

## Creazione Ordini
Per facilitare l'operazione di creazione degli ordini, trovate di seguito 2 json compatibili con i dati generati nel setup.

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