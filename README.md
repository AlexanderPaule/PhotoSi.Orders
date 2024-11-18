# PhotoSi.Orders

## Scelte Tecnologiche
- Framework: **.Net Core 9**
- Testing: **NUnit**
- API Documentation: **Swagger**
- Database: **SqlServer**
- Interazione Database: **Entity Framework Core**

## Demo
### Prerequisiti
- Avere a disposizione un DBMS Sql Server (ex: SqlExpress).

### Setup
- Clonare il repository
- Aprire la solution utilizzando Visual Studio
- Settare PhotoSi.API come progetto di avvio
- Configurare le connection string ai 4 Database nel file 'appsettings.json', nella sezione 'ConnectionStrings' del progetto PhotoSi.API.

- Avviare il progetto premendo F5 o ctrl+F5. Questa azione comporterà le seguenti operazioni:
  - Il servizio API verrà avviato.
  - Se non è già presente, 4 Database conformi ai modelli definiti via EF Core verranno generati nell'istanza specificata al punto precedente.
  - Il browser di default si avvierà puntando all'indirizzo 'https://localhost:44354/swagger/index.html'.

La libreria swagger integrata nel progetto faciliterà l'iterazione con il servizio.

Il progetto mette a disposizione 5 controller **Orders**, **Products**, **Users**, **Addresses** e **Demo**.
Il controller **Demo** permette di generare un setup base per poter testare il comportamento degli ordini e dei prodotti.

- Effettuare la chiamata alla rotta **[POST:/Demo/SetUp]**

A questo punto il servizio è in condizioni di procedere con il test della generazione e lettura degli ordini.

### Testare Il servizio
Utilizzare le rotte disponibili per verificare il comportamento dei controller.


Per facilitare l'operazione di creazione degli ordini, trovate di seguito 1 json compatibile con i dati generati nel setup.

#### Order1
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "addressId": "222274e4-37b7-44ee-a8a2-ee920c6fab9d",
  "userId": "311174e4-37b7-44ee-a8a2-ee920c6fab9d",
  "productsIds": [
    "121174e4-37b7-44ee-a8a2-ee920c6fab9d",
    "112174e4-37b7-44ee-a8a2-ee920c6fab9d",
    "211174e4-37b7-44ee-a8a2-ee920c6fab9d"
  ],
  "createdOn": "2024-11-17T20:18:19.466Z"
}
```
