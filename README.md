# PizzDesk

**PizzDesk** è un'applicazione desktop per la gestione degli ordini di
una pizzeria. Il progetto offre un'interfaccia per comporre gli ordini,
personalizzare le pizze e gestire il menu e gli ingredienti, con
salvataggio dei dati tramite SQLite.

> Il nome PizzDesk è provvisorio: puoi sostituirlo con il nome
> definitivo che sceglierai.

## Funzionalità

-   **Gestione degli ordini** -- composizione degli ordini con prodotti
    suddivisi per categoria, quantità e prezzo.
-   **Gestione del menu** -- caricamento e modifica dei prodotti
    disponibili e dei relativi prezzi.
-   **Personalizzazione delle pizze** -- scelta della base e degli
    ingredienti, con aggiornamento del prezzo.
-   **Gestione degli ingredienti** -- aggiunta, modifica ed eliminazione
    degli ingredienti e dei relativi prezzi.
-   **Salvataggio dei dati** -- memorizzazione di menu, ordini e
    informazioni correlate in un database SQLite.
-   **Stampa** -- stampa dello scontrino dell'ordine e del resoconto
    giornaliero.
-   **Resoconto giornaliero** -- generazione e consultazione del
    riepilogo degli ordini della giornata.

## Tecnologie

-   C#
-   .NET 10
-   Windows Forms
-   SQLite
-   `Microsoft.Data.Sqlite`

## Requisiti

Per compilare ed eseguire il progetto sono necessari:

-   Windows
-   .NET 10 SDK
-   Visual Studio con il carico di lavoro **Sviluppo desktop .NET**,
    oppure un ambiente compatibile con .NET 10 e Windows Forms.

## Avvio del progetto

1.  Clona il repository:

    ``` bash
    git clone https://github.com/USERNAME/REPOSITORY.git
    ```

2.  Apri la soluzione o il file `Prenotazione_PIzze.csproj` con Visual
    Studio.

3.  Ripristina i pacchetti NuGet e compila il progetto.

4.  Avvia l'applicazione da Visual Studio oppure esegui il progetto da
    terminale su Windows:

    ``` bash
    dotnet restore
    dotnet build
    dotnet run
    ```

## Database

Il progetto utilizza SQLite. Il database `pizzeria.db` viene creato e
inizializzato automaticamente all'avvio, nella cartella `Database`
accanto all'eseguibile.

La cartella e il database vengono utilizzati per conservare i dati
dell'applicazione. Se distribuisci l'applicazione su un altro computer,
assicurati che l'utente abbia i permessi di scrittura nella directory in
cui viene eseguita.

## Struttura del progetto

``` text
Prenotazione_PIzze/
└── Prenotazione_PIzze/
    ├── Database/
    │   └── DatabaseManager.cs
    ├── Form1.cs
    ├── Form1.Designer.cs
    ├── GestioneIngredienti.cs
    ├── GestioneIngredienti.Designer.cs
    ├── PersonalizzaPizza.cs
    ├── PersonalizzaPizza.Designer.cs
    ├── SchedaPizza.cs
    ├── Program.cs
    └── Prenotazione_PIzze.csproj
```

-   `Form1.cs` -- finestra principale e gestione del flusso degli
    ordini.
-   `Database/DatabaseManager.cs` -- accesso e operazioni sul database
    SQLite.
-   `GestioneIngredienti.cs` -- schermata per la gestione degli
    ingredienti.
-   `PersonalizzaPizza.cs` -- schermata per la personalizzazione delle
    pizze.
-   `SchedaPizza.cs` -- componente grafico e logica delle schede
    prodotto.

## Stato del progetto

Il progetto è in sviluppo. Funzionalità, interfaccia e struttura possono
evolvere nel tempo.

## Contributi

Suggerimenti, segnalazioni di bug e proposte di miglioramento sono
benvenuti. Per modifiche importanti, apri prima una issue così da
discutere l'intervento.

