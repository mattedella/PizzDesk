using Microsoft.Data.Sqlite;
using PizzaPrenotate;
using Prenotazione_PIzze.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Prenotazione_PIzze.Database
{
    public class RigaResoconto
    {
        public string NomeProdotto { get; set; }
        public string Categoria { get; set; }
        public int Quantita { get; set; }
    }

    public class ResocontoGiornataDati
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Totale { get; set; }
        public List<RigaResoconto> Prodotti { get; set; } = new();
    }

    public static class DatabaseManager
    {
        private static readonly string percorsoDatabase = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Database",
                "pizzeria.db");

        private static readonly string connectionString = $"Data Source={percorsoDatabase}";

        public static void InizializzaDatabase() {

            string cartella = Path.GetDirectoryName(percorsoDatabase);
            Directory.CreateDirectory(cartella);

            using (SqliteConnection connessione = new SqliteConnection(connectionString)) {
                connessione.Open();

                string query = @"
                    CREATE TABLE IF NOT EXISTS ProdottiMenu (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        Categoria TEXT NOT NULL,
                        Prezzo REAL NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Ingredienti (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL UNIQUE,
                        Prezzo TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Ordini (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CodiceOrdine TEXT NOT NULL UNIQUE,
                        NomeCliente TEXT NOT NULL,
                        DataOrdine TEXT NOT NULL,
                        Totale REAL NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS ProdottiOrdinati (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        IdOrdine INTEGER NOT NULL,
                        NomeProdotto TEXT NOT NULL,
                        Categoria TEXT NOT NULL,
                        PrezzoUnitario REAL NOT NULL,
                        Quantita INTEGER NOT NULL,
                        FOREIGN KEY (IdOrdine) REFERENCES Ordini(Id)
                    );

                    CREATE TABLE IF NOT EXISTS ResocontoGiornata (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Data TEXT NOT NULL,
                        DettagliOrdini TEXT NOT NULL,
                        Totale REAL NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS DettagliOrdine (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        IdResoconto INTEGER NOT NULL,
                        NomeProdotto TEXT NOT NULL,
                        Categoria TEXT NOT NULL,
                        Quantita INTEGER NOT NULL,
                        FOREIGN KEY (IdResoconto) REFERENCES ResocontoGiornata(Id)
                    );
                    
                    CREATE UNIQUE INDEX IF NOT EXISTS idx_ResocontoGiornata_Data ON ResocontoGiornata(Data)
                ";

                using (SqliteCommand comando = new SqliteCommand(query, connessione))
                    comando.ExecuteNonQuery();
            }
        }
        
        public static bool MenuVuoto() {

            using (SqliteConnection connessione = new SqliteConnection(connectionString)) {
                connessione.Open();

                string query = "SELECT COUNT(*) FROM ProdottiMenu";
                using (SqliteCommand comando = new SqliteCommand(query, connessione)) {
                    long count = (long)comando.ExecuteScalar();
                    return count == 0;
                }
            }
        }

        public static void AggiungiProdotto(string name, string categoria, decimal prezzo) {

            using (SqliteConnection connessione = new SqliteConnection(connectionString)) {
                connessione.Open();

                string query = @"INSERT INTO ProdottiMenu (Nome, Categoria, Prezzo) VALUES (@Name, @Categoria, @Prezzo)";
                using (SqliteCommand comando = new SqliteCommand(query, connessione)) {
                    comando.Parameters.AddWithValue("@Name", name);
                    comando.Parameters.AddWithValue("@Categoria", categoria);
                    comando.Parameters.AddWithValue("@Prezzo", prezzo);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static List<(string name, string categoria, decimal prezzo)> CaricaProdotto() {

            var prodotti = new List<(string, string, decimal)>();
            using (SqliteConnection connessione = new SqliteConnection(connectionString)) {
                connessione.Open();

                string query = @"SELECT Nome, Categoria, Prezzo FROM ProdottiMenu";
                using (SqliteCommand comando = new SqliteCommand(query, connessione)) {
                    using (SqliteDataReader reader = comando.ExecuteReader()) {
                        while (reader.Read()) {
                            string nome = reader.GetString(0);
                            string categoria = reader.GetString(1);
                            decimal prezzo = reader.GetDecimal(2);
                            prodotti.Add((nome, categoria, prezzo));
                        }
                    }
                }
            }
            return prodotti;
        }

        public static void SalvaMenu(List<(string name, string categoria, decimal prezzo)> prodotti) {

            using (SqliteConnection connessione = new SqliteConnection(connectionString)) {
                connessione.Open();

                using (SqliteTransaction transaction = connessione.BeginTransaction()) {
                    try {
                        string deleteQuery = "DELETE FROM ProdottiMenu";
                        using (SqliteCommand deleteCommand = new SqliteCommand(deleteQuery, connessione, transaction))
                            deleteCommand.ExecuteNonQuery();

                        string insertQuery = @"INSERT INTO ProdottiMenu (Nome, Categoria, Prezzo) VALUES (@Name, @Categoria, @Prezzo)";
                        foreach (var prodotto in prodotti) {
                            using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, connessione, transaction)) {
                                insertCommand.Parameters.Clear();
                                insertCommand.Parameters.AddWithValue("@Name", prodotto.name);
                                insertCommand.Parameters.AddWithValue("@Categoria", prodotto.categoria);
                                insertCommand.Parameters.AddWithValue("@Prezzo", prodotto.prezzo);
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch {

                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public static List<(string Nome, decimal Prezzo)> CaricaIngredientiConPrezzo()
        {
            List<(string Nome, decimal Prezzo)> ingredienti = new List<(string, decimal)>();

            using (SqliteConnection connessione = new SqliteConnection(connectionString))
            {
                connessione.Open();

                string query = @"
                    SELECT Nome, Prezzo
                    FROM Ingredienti
                    ORDER BY Nome;";

                using (SqliteCommand comando = new SqliteCommand(query, connessione))
                using (SqliteDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nome = reader.GetString(0);
                        string prezzoTesto = Convert.ToString(
                            reader.GetValue(1),
                            CultureInfo.InvariantCulture) ?? "0";

                        decimal prezzo;

                        // Supporta sia valori salvati con il punto decimale
                        // sia valori inseriti con la virgola italiana.
                        bool prezzoValido;

                        if (prezzoTesto.Contains(','))
                        {
                            prezzoValido = decimal.TryParse(
                                prezzoTesto,
                                NumberStyles.Number,
                                CultureInfo.GetCultureInfo("it-IT"),
                                out prezzo);
                        }
                        else
                        {
                            prezzoValido = decimal.TryParse(
                                prezzoTesto,
                                NumberStyles.Number,
                                CultureInfo.InvariantCulture,
                                out prezzo);
                        }

                        if (!prezzoValido)
                        {
                            throw new FormatException(
                                $"Il prezzo dell'ingrediente '{nome}' non è valido: '{prezzoTesto}'.");
                        }

                        ingredienti.Add((nome, prezzo));
                    }
                }
            }

            return ingredienti;
        }

        public static Dictionary<string, string> CaricaIngredienti()
        {
            Dictionary<string, string> ingredienti = new Dictionary<string, string>();

            using (SqliteConnection connessione =
                   new SqliteConnection(connectionString))
            {
                connessione.Open();

                string query = "SELECT * FROM Ingredienti ORDER BY Nome";

                using (SqliteCommand comando =
                       new SqliteCommand(query, connessione))
                using (SqliteDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ingredienti.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }

            return ingredienti;
        }

        public static void AggiungiIngrediente(string nome, string prezzo)
        {
            using (SqliteConnection connessione =
                   new SqliteConnection(connectionString))
            {
                connessione.Open();

                string query = @"
                    INSERT OR IGNORE INTO Ingredienti (Nome, Prezzo)
                    VALUES (@Nome, @Prezzo)";

                using (SqliteCommand comando =
                       new SqliteCommand(query, connessione))
                {
                    comando.Parameters.AddWithValue("@Nome", nome.Trim());
                    comando.Parameters.AddWithValue("@Prezzo", prezzo.Trim());
                    comando.ExecuteNonQuery();
                }
            }
        }
        public static void EliminaIngrediente(string nome)
        {
            using (SqliteConnection connessione =
                   new SqliteConnection(connectionString))
            {
                connessione.Open();

                string query = @"
                    DELETE FROM Ingredienti
                    WHERE Nome = @Nome";

                using (SqliteCommand comando =
                       new SqliteCommand(query, connessione))
                {
                    comando.Parameters.AddWithValue("@Nome", nome);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void ModificaIngrediente(string nomeVecchio, string nomeNuovo)
        {
            using (SqliteConnection connessione =
                   new SqliteConnection(connectionString))
            {
                connessione.Open();
                string query = @"
                    UPDATE Ingredienti
                    SET Nome = @NomeNuovo
                    WHERE Nome = @NomeVecchio";
                using (SqliteCommand comando =
                       new SqliteCommand(query, connessione))
                {
                    comando.Parameters.AddWithValue("@NomeVecchio", nomeVecchio);
                    comando.Parameters.AddWithValue("@NomeNuovo", nomeNuovo.Trim());
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void ModificaPrezzoIngrediente(string Nome, string prezzoNuovo)
        {
            using (SqliteConnection connessione =
                   new SqliteConnection(connectionString))
            {
                connessione.Open();
                string query = @"
                    UPDATE Ingredienti
                    SET Prezzo = @prezzoNuovo
                    WHERE Nome = @Nome";
                using (SqliteCommand comando =
                       new SqliteCommand(query, connessione))
                {
                    comando.Parameters.AddWithValue("@Nome", Nome);
                    comando.Parameters.AddWithValue("@prezzoNuovo", prezzoNuovo.Trim());
                    comando.ExecuteNonQuery();
                }
            }
        }


        public static bool SalvaOrdine(
            string codiceOrdine, string nomeCliente, DateTime dataOrdine,
            decimal totale, List<(string NomeProdotto, string Categoria, decimal PrezzoUnitario, int Quantita)> prodotti)
        {
            using (SqliteConnection connessione = new SqliteConnection(connectionString))
            {
                connessione.Open();

                // Attiva il controllo delle chiavi esterne.
                using (SqliteCommand pragma = new SqliteCommand(
                    "PRAGMA foreign_keys = ON;", connessione))
                {
                    pragma.ExecuteNonQuery();
                }

                using (SqliteTransaction transaction = connessione.BeginTransaction())
                {
                    try
                    {
                        // 1. Inserisce l'ordine.
                        string queryOrdine = @"
                            INSERT INTO Ordini
                            (CodiceOrdine, NomeCliente, DataOrdine, Totale)
                            VALUES
                            (@CodiceOrdine, @NomeCliente, @DataOrdine, @Totale)
                            ON CONFLICT(CodiceOrdine) DO NOTHING;";

                        using (SqliteCommand comando = new SqliteCommand(
                            queryOrdine, connessione, transaction))
                        {
                            comando.Parameters.AddWithValue("@CodiceOrdine", codiceOrdine);
                            comando.Parameters.AddWithValue("@NomeCliente", nomeCliente);
                            comando.Parameters.AddWithValue("@DataOrdine",
                                dataOrdine.ToString("yyyy-MM-dd HH:mm:ss"));
                            comando.Parameters.AddWithValue("@Totale", totale);

                            int righeInserite = comando.ExecuteNonQuery();

                            // L'ordine esiste già: non inseriamo nuovamente i prodotti.
                            if (righeInserite == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // 2. Recupera l'ID dell'ordine appena creato.
                        long idOrdine;

                        using (SqliteCommand comando = new SqliteCommand(
                            "SELECT last_insert_rowid();",
                            connessione,
                            transaction))
                        {
                            idOrdine = (long)comando.ExecuteScalar();
                        }

                        // 3. Inserisce i prodotti dell'ordine.
                        string queryProdotti = @"
                            INSERT INTO ProdottiOrdinati
                            (IdOrdine, NomeProdotto, Categoria, PrezzoUnitario, Quantita)
                            VALUES
                            (@IdOrdine, @NomeProdotto, @Categoria, @PrezzoUnitario, @Quantita);";

                        foreach (var prodotto in prodotti)
                        {
                            if (prodotto.Quantita <= 0)
                                continue;

                            using (SqliteCommand comando = new SqliteCommand(
                                queryProdotti, connessione, transaction))
                            {
                                comando.Parameters.AddWithValue("@IdOrdine", idOrdine);
                                comando.Parameters.AddWithValue("@NomeProdotto", prodotto.NomeProdotto);
                                comando.Parameters.AddWithValue("@Categoria", prodotto.Categoria);
                                comando.Parameters.AddWithValue("@PrezzoUnitario", prodotto.PrezzoUnitario);
                                comando.Parameters.AddWithValue("@Quantita", prodotto.Quantita);

                                comando.ExecuteNonQuery();
                            }
                        }

                        // 4. Conferma tutte le operazioni.
                        transaction.Commit();

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public static void SalvaDettagliOrdine(SchedaPizza scheda, string categoria)
        {
            using (SqliteConnection connessione = new SqliteConnection(connectionString))
            {
                connessione.Open();
                string query = @"
                    INSERT OR  UPDATEINTO DettagliOrdine (IdResoconto, NomeProdotto, Categoria, Quantita)
                    VALUES (@IdResoconto, @NomeProdotto, @Categoria, @Quantita)";
                using (SqliteCommand comando = new SqliteCommand(query, connessione))
                {
                    //comando.Parameters.AddWithValue("@IdResoconto", scheda.Id);
                    comando.Parameters.AddWithValue("@NomeProdotto", scheda.NomePizza);
                    comando.Parameters.AddWithValue("@Categoria", categoria);
                    comando.Parameters.AddWithValue("@Quantita", scheda.Quantita);
                    comando.ExecuteNonQuery();
                }
            }
        }


        public static bool GeneraResocontoGiornata(DateTime data)
        {
            using (SqliteConnection connessione =
                new SqliteConnection(connectionString))
            {
                connessione.Open();

                using (SqliteCommand pragma = new SqliteCommand(
                    "PRAGMA foreign_keys = ON;", connessione))
                {
                    pragma.ExecuteNonQuery();
                }

                using (SqliteTransaction transaction =
                    connessione.BeginTransaction())
                {
                    try
                    {
                        string dataResoconto = data.ToString("yyyy-MM-dd");

                        // Controlla se esiste già un resoconto per questa data.
                        string queryEsistente = @"
                            SELECT COUNT(*)
                            FROM ResocontoGiornata
                            WHERE Data = @Data;";

                        using (SqliteCommand comando = new SqliteCommand(
                            queryEsistente, connessione, transaction))
                        {
                            comando.Parameters.AddWithValue("@Data", dataResoconto);

                            long esistente = (long)comando.ExecuteScalar();

                            if (esistente > 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // Recupera il totale degli incassi della giornata.
                        string queryTotale = @"
                            SELECT COALESCE(SUM(Totale), 0)
                            FROM Ordini
                            WHERE DATE(DataOrdine) = @Data;";

                        decimal totale;

                        using (SqliteCommand comando = new SqliteCommand(
                            queryTotale, connessione, transaction))
                        {
                            comando.Parameters.AddWithValue("@Data", dataResoconto);

                            totale = Convert.ToDecimal(comando.ExecuteScalar());
                        }

                        // Recupera i prodotti raggruppati per nome e categoria.
                        string queryProdotti = @"
                            SELECT
                                NomeProdotto,
                                Categoria,
                                SUM(Quantita) AS QuantitaTotale
                            FROM ProdottiOrdinati
                            WHERE IdOrdine IN (
                                SELECT Id
                                FROM Ordini
                                WHERE DATE(DataOrdine) = @Data
                            )
                            GROUP BY NomeProdotto, Categoria
                            ORDER BY Categoria, NomeProdotto;";

                        List<(string NomeProdotto, string Categoria, int Quantita)> prodotti = new();

                        using (SqliteCommand comando = new SqliteCommand(
                            queryProdotti, connessione, transaction))
                        {
                            comando.Parameters.AddWithValue("@Data", dataResoconto);

                            using (SqliteDataReader reader = comando.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    prodotti.Add((
                                        reader.GetString(0),
                                        reader.GetString(1),
                                        Convert.ToInt32(reader.GetInt64(2))
                                    ));
                                }
                            }
                        }

                        // Non genera resoconti per giornate senza ordini.
                        if (prodotti.Count == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        // Prepara un riepilogo testuale dei prodotti.
                        List<string> righeDettagli = new();

                        foreach (var prodotto in prodotti)
                        {
                            righeDettagli.Add(
                                $"{prodotto.NomeProdotto} ({prodotto.Categoria}): {prodotto.Quantita}"
                            );
                        }

                        string dettagliTestuali = string.Join(
                            Environment.NewLine, righeDettagli);

                        // Inserisce il resoconto principale.
                        string queryResoconto = @"
                            INSERT INTO ResocontoGiornata
                            (Data, DettagliOrdini, Totale)
                            VALUES
                            (@Data, @DettagliOrdini, @Totale);";

                        using (SqliteCommand comando = new SqliteCommand(
                            queryResoconto, connessione, transaction))
                        {
                            comando.Parameters.AddWithValue("@Data", dataResoconto);
                            comando.Parameters.AddWithValue(
                                "@DettagliOrdini", dettagliTestuali);
                            comando.Parameters.AddWithValue("@Totale", totale);

                            comando.ExecuteNonQuery();
                        }

                        // Recupera l'ID del resoconto appena creato.
                        long idResoconto;

                        using (SqliteCommand comando = new SqliteCommand(
                            "SELECT last_insert_rowid();",
                            connessione, transaction))
                        {
                            idResoconto = (long)comando.ExecuteScalar();
                        }

                        // Inserisce i prodotti aggregati in DettagliOrdine.
                        string queryDettagli = @"
                            INSERT INTO DettagliOrdine
                            (IdResoconto, NomeProdotto, Categoria, Quantita)
                            VALUES
                            (@IdResoconto, @NomeProdotto, @Categoria, @Quantita);";

                        foreach (var prodotto in prodotti)
                        {
                            using (SqliteCommand comando = new SqliteCommand(
                                queryDettagli, connessione, transaction))
                            {
                                comando.Parameters.AddWithValue(
                                    "@IdResoconto", idResoconto);
                                comando.Parameters.AddWithValue(
                                    "@NomeProdotto", prodotto.NomeProdotto);
                                comando.Parameters.AddWithValue(
                                    "@Categoria", prodotto.Categoria);
                                comando.Parameters.AddWithValue(
                                    "@Quantita", prodotto.Quantita);

                                comando.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    public static ResocontoGiornataDati? CaricaResocontoGiornata(DateTime data)
    {
        using (SqliteConnection connessione = new SqliteConnection(connectionString))
        {
            connessione.Open();

            string dataResoconto = data.ToString("yyyy-MM-dd");

            // Recupera i dati principali del resoconto.
            string queryResoconto = @"
            SELECT Id, Data, Totale
            FROM ResocontoGiornata
            WHERE Data = @Data;";

            ResocontoGiornataDati? resoconto = null;

            using (SqliteCommand comando = new SqliteCommand(
                queryResoconto, connessione))
            {
                comando.Parameters.AddWithValue("@Data", dataResoconto);

                using (SqliteDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resoconto = new ResocontoGiornataDati
                        {
                            Id = reader.GetInt64(0),
                            Data = DateTime.Parse(reader.GetString(1)),
                            Totale = Convert.ToDecimal(reader.GetValue(2))
                        };
                    }
                }
            }

            // Se non esiste un resoconto, restituisce null.
            if (resoconto == null)
                return null;

            // Recupera i prodotti associati al resoconto.
            string queryProdotti = @"
            SELECT NomeProdotto, Categoria, Quantita
            FROM DettagliOrdine
            WHERE IdResoconto = @IdResoconto
            ORDER BY Categoria, NomeProdotto;";

            using (SqliteCommand comando = new SqliteCommand(
                queryProdotti, connessione))
            {
                comando.Parameters.AddWithValue("@IdResoconto", resoconto.Id);

                using (SqliteDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resoconto.Prodotti.Add(new RigaResoconto
                        {
                            NomeProdotto = reader.GetString(0),
                            Categoria = reader.GetString(1),
                            Quantita = Convert.ToInt32(reader.GetValue(2))
                        });
                    }
                }
            }

            return resoconto;
        }
    }


    public static void SalvaResocontoGiornata()
        {
            string data = DateTime.Now.ToString("dd-MM-yyyy");
            using (SqliteConnection connessione = new SqliteConnection(connectionString))
            {
                connessione.Open();
                  
                string query = @"
                    INSERT OR UPDATE INTO ResocontoGiornata (Data, DettagliOrdini, Totale)
                    VALUES (@Data, @DettagliOrdini, @Totale)";
                using (SqliteCommand comando = new SqliteCommand(query, connessione))
                {
                    comando.Parameters.AddWithValue("@Data", data);
                    //comando.Parameters.AddWithValue("@DettagliOrdini", dettagliOrdini);
                    //comando.Parameters.AddWithValue("@Totale", totale);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void ControllaOrdini()
        {
            using (SqliteConnection connessione =
                new SqliteConnection(connectionString))
            {
                connessione.Open();

                string query = "SELECT * FROM ProdottiOrdinati;";

                using (SqliteCommand comando =
                    new SqliteCommand(query, connessione))
                using (SqliteDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string risultato = "";

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            risultato += reader.GetName(i) + ": "
                                + reader[i] + "\n";
                        }

                        MessageBox.Show(risultato);
                    }
                }
            }
        }
    }

}
