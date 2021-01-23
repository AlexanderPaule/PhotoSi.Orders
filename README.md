# PhotoSi.Orders

## Scelte Tecnologiche
- Framework: **.Net Core 3.1**
- Testing: **NUnit**
- API Documentation: **Swagger**
- Database: **SqlServer**
- Interazione Database: **Entity Framework Core**

  Ho scelto l'ORM Entity Framework perch� per il progetto serve gestire pochi dati e per abbassare il tempo della realizzare l'infratruttura.

## Demo
### Prerequisiti
- Avere a disposizione un DBMS Sql Server (ex: SqlExpress).

### Setup
- Clonare il reository
- Aprire la solution utilizzando visual studio Visual Studio
- Configurare la connection string al Database nel file 'appsettings.json', sotto la sezione 'ConnectionStrings.Sales'.

  Creare manualmente un database vuoto NON � richiesto.

- Avviare il servizio premendo F5 o ctrl+F5. Questa azione comporter� le seguenti operazioni:
  - Il servizio API verr� avviato.
  - Se non � gi� presente, un Database conforme al modello definito via EF Core verr� generato nell'istanza specificata al punto precedente.
  - Il browser di default si avvier� puntanto all'indirizzo 'https://localhost:44354/swagger/index.html'.

La libreria swagger integrata nel progetto faciliter� l'iterazione con il servizio.

Il progetto mette a disposizione 3 controller **Orders**, **Products** e **Demo**.
Il controller **Demo** permette di generare un setup base per poter testare il comportamento degli ordini e dei prodotti, inoltre permette di visualizzare i dati utilizzati nel setup.

- Effettuare la chiamata alla rotta **[POST:/Demo/SetUp]**

A questo punto il servizio � in condizioni di procedere con il test della generazione e lettura degli ordini.

### Testare Il servizio
Utilizzare le rotte disponibili per verificare il comportamento dei controller **Orders** e **Products**.
- **[POST:/Orders]**
- **[GET:/Orders]**
- **[GET:/Orders/All]**
- **[POST:/Products]**
- **[GET:/Products/All]**

Per facilitare l'operazione di creazione degli ordini, trovate di seguito 2 json compatibili con i dati generati nel setup.

#### Order1
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

#### Order1
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

## Notifica Nuovo Ordine
Per notificare l'evento di ordini creati ad altri servizi in ascolto, la scelta verso cui andrei sarebbe
quella di istanziare un servizio basato sulla gestione di messaggi a code. Questa scelta permetterebbe di
avere una soluzione asincrona che consentirebbe ad altri servizi di agganciarsi alla coda della notifica
degli ordini e restare in ascolto. I vantaggi sarebbero di disaccoppiare il funzionamento dei flussi e di
rendere modulare l'infrastruttura. Modulare perch� un eventuale nuovo servizio interessato a tale evento pu�
agganciarsi in un tempo imprecisato alla coda e questo non richiederebbe modifiche al software che si occupa degli
ordini. Il disaccoppiamento � altrettanto importante perch� in caso di down di uno dei servizi interessati al
messaggio di creazione dell'ordine il software degli ordini non ne verrebbe impattato.
