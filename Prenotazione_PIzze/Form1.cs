using PizzaPrenotate;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Printing;
using Prenotazione_PIzze.Database;

namespace Prenotazione_PIzze
{


    public partial class Pizzeria : Form
    {
        private class StatoElemento
        {
            public string Nome { get; set; } = "";
            public decimal Prezzo { get; set; }
        }

        private readonly CultureInfo culture = new CultureInfo("it-IT");
        private bool modalitaModifica = false;

        private readonly Dictionary<SchedaPizza, StatoElemento> statoSalvato = new();
        private readonly PrintDocument printDocument = new PrintDocument();
        private readonly PrintDocument printDocumentResoconto = new PrintDocument();
        private ResocontoGiornataDati? resocontoInStampa;
        private bool stampaResocontoCompletata = false;
        private int indiceRigaResoconto = 0;
        private readonly List<SchedaPizza> pizzeTemporanee = new List<SchedaPizza>();
        private string? codiceOrdineCorrente = null;
        private bool ordineSalvato = false;
        public Pizzeria()
        {
            InitializeComponent();
            ApplicaStileMinimal();
            DatabaseManager.InizializzaDatabase();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 1169, 827);
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDocumentResoconto.DefaultPageSettings.Landscape = false;
            printDocumentResoconto.DefaultPageSettings.PaperSize =
                new PaperSize("A4", 827, 1169);
            printDocumentResoconto.DefaultPageSettings.Margins =
                new Margins(50, 50, 50, 50);
            printDocumentResoconto.PrintPage += PrintDocumentResoconto_PrintPage;
            printDocumentResoconto.BeginPrint += (s, e) =>
            {
                indiceRigaResoconto = 0;
            };
            printDocumentResoconto.EndPrint += (s, e) =>
            {
                if (e.PrintAction != PrintAction.PrintToPreview)
                {
                    stampaResocontoCompletata = true;
                }
            };
            btnStampa.Click += btnStampa_Click;
            btnModEl.Visible = false;
            btnEliminaMod.Visible = false;
            btnAddBevande.Visible = false;
            btnAddDolci.Visible = false;
            btnAddPizze.Visible = false;
            flpPizze.WrapContents = true;
            flpPizze.AutoScroll = true;
            ConfiguraTabella();
            CaricaMenuDatabase();
            RegistraStatoSalvato();
            AggiornaTotale();
            btnModEl.Click += btnModEl_Click;
            btnEliminaMod.Click += btnEliminaMod_Click;
            btnPersonalizza.Click += btnPersonalizza_Click;
            btnAddBevande.Click += (s, e) =>
            {
                AggiungiSchedaElemento(flpBevande, "", 0.00m, 0);
                AggiornaTotale();
            };
            btnAddDolci.Click += (s, e) =>
            {
                AggiungiSchedaElemento(flpDolci, "", 0.00m, 0);
                AggiornaTotale();
            };
            btnAddPizze.Click += (s, e) =>
            {
                AggiungiSchedaElemento(flpPizze, "", 0.00m, 0);
                AggiornaTotale();
            };
            AggiornaTotale();

        }


        private void ApplicaStileMinimal()
        {
            Color sfondo = Color.FromArgb(245, 246, 247);
            Color bianco = Color.White;
            Color testo = Color.FromArgb(41, 41, 41);
            Color bordo = Color.FromArgb(224, 224, 224);
            Color rosso = Color.FromArgb(168, 61, 50);
            Color grigio = Color.FromArgb(238, 238, 238);

            // Finestra principale
            BackColor = sfondo;
            Font = new Font("Segoe UI", 9F);

            btnStampa.Size = new Size(100, 32);

            btnPersonalizza.Size = new Size(110, 30);

            btnAddPizze.Size = new Size(105, 30);
            btnAddBevande.Size = new Size(105, 30);
            btnAddDolci.Size = new Size(105, 30);

            btnModEl.Size = new Size(100, 30);
            btnEliminaMod.Size = new Size(100, 30);

            btnStampa.BackColor = Color.FromArgb(168, 61, 50);
            btnStampa.ForeColor = Color.White;

            btnStampa.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(145, 49, 40);

            btnStampa.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(125, 42, 35);

            btnStampa.TextAlign = ContentAlignment.MiddleCenter;
            btnPersonalizza.TextAlign = ContentAlignment.MiddleCenter;
            btnAddBevande.TextAlign = ContentAlignment.MiddleCenter;
            btnAddDolci.TextAlign = ContentAlignment.MiddleCenter;
            btnAddPizze.TextAlign = ContentAlignment.MiddleCenter;
            btnModEl.TextAlign = ContentAlignment.MiddleCenter;
            btnEliminaMod.TextAlign = ContentAlignment.MiddleCenter;

            // Pannelli e contenitori
            tlpPrincipale.BackColor = sfondo;
            pnlExtra.BackColor = sfondo;
            pnlPizze.BackColor = sfondo;

            pnlCoperto.BackColor = sfondo;
            pnlBevande.BackColor = sfondo;
            pnlDolci.BackColor = sfondo;
            pnlModStampa.BackColor = sfondo;

            flpCoperto.BackColor = sfondo;
            flpBevande.BackColor = sfondo;
            flpDolci.BackColor = sfondo;
            flpPizze.BackColor = sfondo;

            tableLayoutPanel1.BackColor = sfondo;
            tlpCoperto.BackColor = sfondo;
            tableLayoutPanel2.BackColor = bianco;

            // Campo nome cliente
            pnlNomeCliente.BackColor = bianco;
            pnlNomeCliente.BorderStyle = BorderStyle.None;

            txtNomeCliente.BackColor = bianco;
            txtNomeCliente.ForeColor = testo;
            txtNomeCliente.BorderStyle = BorderStyle.FixedSingle;
            txtNomeCliente.Font = new Font("Segoe UI", 9F);

            // Titoli delle categorie
            lblCoperto.ForeColor = testo;
            lblBevande.ForeColor = testo;
            lblDolci.ForeColor = testo;
            lblTitoloPizze.ForeColor = testo;

            lblNomeCliente.ForeColor = testo;
            lblTotale.ForeColor = testo;
            lblTotale.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Pulsanti
            ApplicaStilePulsante(btnStampa, rosso, bianco);

            ApplicaStilePulsante(btnAddPizze, grigio, testo);
            ApplicaStilePulsante(btnAddBevande, grigio, testo);
            ApplicaStilePulsante(btnAddDolci, grigio, testo);
            ApplicaStilePulsante(btnPersonalizza, grigio, testo);
            ApplicaStilePulsante(btnModEl, grigio, testo);

            ApplicaStilePulsante(
                btnEliminaMod,
                Color.FromArgb(250, 235, 235),
                Color.FromArgb(170, 50, 50)
            );

            // Menu superiore
            menuStripPrincipale.BackColor = bianco;
            menuStripPrincipale.ForeColor = testo;
            menuStripPrincipale.Font = new Font("Segoe UI", 9F);

            // Tabella pizze, se utilizzata
            dgvPizze.BackgroundColor = bianco;
            dgvPizze.GridColor = bordo;
            dgvPizze.BorderStyle = BorderStyle.None;
            dgvPizze.EnableHeadersVisualStyles = false;
            dgvPizze.ColumnHeadersDefaultCellStyle.BackColor = grigio;
            dgvPizze.ColumnHeadersDefaultCellStyle.ForeColor = testo;
            dgvPizze.DefaultCellStyle.BackColor = bianco;
            dgvPizze.DefaultCellStyle.ForeColor = testo;
            dgvPizze.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(245, 225, 222);
            dgvPizze.DefaultCellStyle.SelectionForeColor = testo;
        }

        private void ApplicaStilePulsante(
            Button pulsante,
            Color sfondo,
            Color coloreTesto)
        {
            pulsante.FlatStyle = FlatStyle.Flat;
            pulsante.FlatAppearance.BorderSize = 0;
            pulsante.BackColor = sfondo;
            pulsante.ForeColor = coloreTesto;
            pulsante.Font = new Font("Segoe UI", 9F);
            pulsante.Cursor = Cursors.Hand;
        }


        private void CaricaMenuDatabase()
        {
            var prodotti = DatabaseManager.CaricaProdotto();
            string elenco = "";
            foreach (var prodotto in prodotti)
            {
                elenco += $"{prodotto.name} - {prodotto.categoria} - {prodotto.prezzo:C2}\n";
                FlowLayoutPanel contenitore = prodotto.categoria switch
                {
                    "Pizze" => flpPizze,
                    "Coperto" => flpCoperto,
                    "Bevande" => flpBevande,
                    "Dolci" => flpDolci,
                    _ => null
                };

                if (contenitore == null)
                    continue;

                AggiungiSchedaElemento(contenitore, prodotto.name, prodotto.prezzo, 0);
            }
        }

        private void ConfiguraTabella()
        {
            dgvPizze.Columns.Clear();
            dgvPizze.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNome",
                HeaderText = "Nome Pizza",
                Width = 150
            });
            dgvPizze.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrezzo",
                HeaderText = "(€)",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            dgvPizze.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantita",
                HeaderText = "Quantità",
                Width = 80
            });
            dgvPizze.AllowUserToAddRows = false;
            dgvPizze.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPizze.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        private void SalvaMenuDatabase()
        {

            var prodotti = new List<(string name, string categoria, decimal prezzo)>();
            AggiungiProdottialSalvataggio(flpPizze, "Pizze", prodotti, true);
            AggiungiProdottialSalvataggio(flpCoperto, "Coperto", prodotti);
            AggiungiProdottialSalvataggio(flpBevande, "Bevande", prodotti);
            AggiungiProdottialSalvataggio(flpDolci, "Dolci", prodotti);
            DatabaseManager.SalvaMenu(prodotti);
        }

        private void AggiungiProdottialSalvataggio(FlowLayoutPanel contenitore, string categoria, List<(string name, string categoria, decimal prezzo)> prodotti, bool escludiTemporanee = false)
        {

            foreach (Control controllo in contenitore.Controls)
            {
                if (controllo is SchedaPizza scheda)
                {
                    if (escludiTemporanee && pizzeTemporanee.Contains(scheda))
                        continue;
                    prodotti.Add((scheda.NomePizza, categoria, scheda.PrezzoPizza));
                }
            }
        }

        private SchedaPizza AggiungiSchedaElemento(
        FlowLayoutPanel contenitore, string nome, decimal prezzo, int quantita)
        {

            SchedaPizza scheda = new SchedaPizza(
                nome, prezzo, quantita);

            scheda.RichiestaEliminazione += (s, e) =>
            {
                contenitore.Controls.Remove(scheda);
                scheda.Dispose();
                AggiornaTotale();
            };

            scheda.ValoriModificati +=
                (s, e) => AggiornaTotale();

            contenitore.Controls.Add(scheda);
            if (modalitaModifica)
            {
                scheda.ImpostaModalitaModifica(true);
            }
            return scheda;
        }

        private void AggiornaTotale()
        {
            decimal totale = 0.00m;
            FlowLayoutPanel[] panels = { flpPizze, flpCoperto, flpBevande, flpDolci };
            foreach (FlowLayoutPanel panel in panels)
            {
                foreach (SchedaPizza scheda in panel.Controls)
                {
                    totale += scheda.PrezzoPizza * scheda.Quantita;
                }
            }

            lblTotale.Text = $"Totale: {totale.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("it-IT"))}";
        }

        private bool CiSonoModificheNonSalvate()
        {
            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };

            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    if (!statoSalvato.TryGetValue(
                        scheda, out StatoElemento? stato))
                    {
                        return true;
                    }

                    if (scheda.NomePizza != stato.Nome ||
                        scheda.PrezzoPizza != stato.Prezzo)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private void RipristinaStatoSalvato()
        {
            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };

            // 1. Rimuove le schede aggiunte dopo l'ultimo salvataggio.
            foreach (FlowLayoutPanel pannello in pannelli)
            {
                for (int i = pannello.Controls.Count - 1; i >= 0; i--)
                {
                    if (pannello.Controls[i] is SchedaPizza scheda &&
                        !statoSalvato.ContainsKey(scheda))
                    {
                        pannello.Controls.Remove(scheda);
                        scheda.Dispose();
                    }
                }
            }

            // 2. Ripristina nome e prezzo delle schede ancora presenti.
            foreach (KeyValuePair<SchedaPizza, StatoElemento> elemento
                     in statoSalvato)
            {
                SchedaPizza scheda = elemento.Key;
                StatoElemento stato = elemento.Value;

                if (!scheda.IsDisposed && scheda.Parent != null)
                {
                    scheda.ModificaElemento(stato.Nome, stato.Prezzo);
                }
            }

            AggiornaTotale();
        }


        private void btnModEl_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };

            // Prima controlliamo tutte le schede.
            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    if (!scheda.ValidaModifiche(out string errore))
                    {
                        MessageBox.Show(
                            errore,
                            "Dati non validi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }
                }
            }

            // Se tutti i campi sono validi, applichiamo le modifiche.
            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    scheda.ApplicaModificheInCorso();
                }
            }

            // Aggiorniamo il riferimento delle modifiche salvate.
            try
            {
                SalvaMenuDatabase();

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Errore durante il salvataggio del menu:\n" + ex.Message,
                    "Errore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            RegistraStatoSalvato();
            AggiornaTotale();
            ChiudiModalitaModifica();

            MessageBox.Show(
                "Modifiche al menu salvate.",
                "Salvataggio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnEliminaMod_Click(object sender, EventArgs e)
        {
            if (CiSonoModificheNonSalvate())
            {
                DialogResult risultato = MessageBox.Show(
                    "Hai modifiche non salvate. Vuoi uscire senza salvarle?",
                    "Conferma uscita",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (risultato != DialogResult.Yes)
                    return;

                RipristinaStatoSalvato();
            }

            modalitaModifica = false;

            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };

            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    scheda.ImpostaModalitaModifica(false);
                }
            }

            btnModEl.Visible = false;
            btnEliminaMod.Visible = false;
            btnAddBevande.Visible = false;
            btnAddDolci.Visible = false;
            btnAddPizze.Visible = false;
            lblTotale.Visible = true;
            btnStampa.Visible = true;
            btnPersonalizza.Visible = true;
        }

        private void RegistraStatoSalvato()
        {
            statoSalvato.Clear();

            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };

            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    statoSalvato[scheda] = new StatoElemento
                    {
                        Nome = scheda.NomePizza,
                        Prezzo = scheda.PrezzoPizza
                    };
                }
            }
        }

        private void ChiudiModalitaModifica()
        {
            modalitaModifica = false;

            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };

            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    scheda.ImpostaModalitaModifica(false);
                }
            }

            btnModEl.Visible = false;
            btnEliminaMod.Visible = false;
            btnAddBevande.Visible = false;
            btnAddDolci.Visible = false;
            btnAddPizze.Visible = false;
            lblTotale.Visible = true;
            btnStampa.Visible = true;
            btnPersonalizza.Visible = true;
        }

        private void tlpPrincipale_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStripPrincipale_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void ModificaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Se siamo già in modalità modifica, non fare nulla.
            if (modalitaModifica)
                return;

            modalitaModifica = true;

            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };

            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    scheda.ImpostaModalitaModifica(true);
                }
            }

            btnModEl.Visible = true;
            btnEliminaMod.Visible = true;
            btnAddBevande.Visible = true;
            btnAddDolci.Visible = true;
            btnAddPizze.Visible = true;
            lblTotale.Visible = false;
            btnStampa.Visible = false;
            btnPersonalizza.Visible = false;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblCoperto_Click(object sender, EventArgs e)
        {

        }

        private bool SalvaOrdineCorrente()
        {
            List<(string NomeProdotto, string Categoria, decimal PrezzoUnitario, int Quantita)> prodotti = new();

            FlowLayoutPanel[] pannelli =
            {
                flpCoperto,
                flpBevande,
                flpPizze,
                flpDolci
            };

            string[] categorie =
            {
                "Coperto",
                "Bevande",
                "Pizze",
                "Dolci"
            };

            decimal totale = 0;

            for (int i = 0; i < pannelli.Length; i++)
            {
                foreach (SchedaPizza scheda in pannelli[i].Controls)
                {
                    if (scheda.Quantita <= 0)
                        continue;

                    prodotti.Add((
                        scheda.NomePizza,
                        categorie[i],
                        scheda.PrezzoPizza,
                        scheda.Quantita
                    ));

                    totale += scheda.PrezzoPizza * scheda.Quantita;
                }
            }

            if (prodotti.Count == 0)
            {
                MessageBox.Show("Non ci sono prodotti nell'ordine.");
                return false;
            }

            if (codiceOrdineCorrente == null)
                codiceOrdineCorrente = Guid.NewGuid().ToString();

            try
            {
                bool salvato = DatabaseManager.SalvaOrdine(
                    codiceOrdineCorrente,
                    txtNomeCliente.Text.Trim(),
                    DateTime.Now,
                    totale,
                    prodotti
                );

                if (!salvato && !ordineSalvato)
                {
                    MessageBox.Show("Questo ordine risulta già registrato.");
                    return false;
                }

                ordineSalvato = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Errore durante il salvataggio dell'ordine:\n" + ex.Message,
                    "Errore database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics grafica = e.Graphics;

            using Font fontTitolo = new Font("Arial", 12, FontStyle.Bold);
            using Font fontNormale = new Font("Arial", 7.5F);
            using Font fontGrassetto = new Font("Arial", 8, FontStyle.Bold);

            float spazio = 20;
            float larghezzaColonna =
                (e.MarginBounds.Width - spazio) / 2;

            RectangleF colonnaSinistra = new RectangleF(
                e.MarginBounds.Left,
                e.MarginBounds.Top,
                larghezzaColonna,
                e.MarginBounds.Height);

            RectangleF colonnaDestra = new RectangleF(
                e.MarginBounds.Left + larghezzaColonna + spazio,
                e.MarginBounds.Top,
                larghezzaColonna,
                e.MarginBounds.Height);

            // Disegna la linea di separazione.
            float xSeparatore =
                e.MarginBounds.Left + larghezzaColonna + spazio / 2;

            grafica.DrawLine(
                Pens.Gray,
                xSeparatore,
                e.MarginBounds.Top,
                xSeparatore,
                e.MarginBounds.Bottom);

            // Prima copia.
            DisegnaScontrino(
                grafica,
                colonnaSinistra,
                fontTitolo,
                fontNormale,
                fontGrassetto);

            // Seconda copia identica.
            DisegnaScontrino(
                grafica,
                colonnaDestra,
                fontTitolo,
                fontNormale,
                fontGrassetto);

            e.HasMorePages = false;
        }


        private void PrintDocumentResoconto_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (resocontoInStampa == null)
            {
                e.HasMorePages = false;
                return;
            }

            Graphics grafica = e.Graphics;

            using Font fontTitolo = new Font("Arial", 20, FontStyle.Bold);
            using Font fontSottotitolo = new Font("Arial", 12, FontStyle.Bold);
            using Font fontNormale = new Font("Arial", 10);
            using Font fontGrassetto = new Font("Arial", 10, FontStyle.Bold);
            using Pen penna = new Pen(Color.Black, 1);

            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float larghezza = e.MarginBounds.Width;

            // INTESTAZIONE
            grafica.DrawString(
                "RESOCONTO GIORNALIERO",
                fontTitolo,
                Brushes.Black,
                x,
                y
            );

            y += 40;

            grafica.DrawString(
                $"Data: {resocontoInStampa.Data:dd/MM/yyyy}",
                fontNormale,
                Brushes.Black,
                x,
                y
            );

            y += 25;

            grafica.DrawString(
                $"Totale incassi: {resocontoInStampa.Totale:C2}",
                fontSottotitolo,
                Brushes.Black,
                x,
                y
            );

            y += 40;

            // INTESTAZIONE TABELLA
            grafica.DrawLine(penna, x, y, x + larghezza, y);

            y += 8;

            grafica.DrawString(
                "Prodotto",
                fontGrassetto,
                Brushes.Black,
                x,
                y
            );

            grafica.DrawString(
                "Categoria",
                fontGrassetto,
                Brushes.Black,
                x + larghezza * 0.55f,
                y
            );

            grafica.DrawString(
                "Quantità",
                fontGrassetto,
                Brushes.Black,
                x + larghezza * 0.85f,
                y
            );

            y += 25;

            grafica.DrawLine(penna, x, y, x + larghezza, y);

            y += 10;

            // ELENCO PRODOTTI
            while (indiceRigaResoconto < resocontoInStampa.Prodotti.Count)
            {
                RigaResoconto prodotto =
                    resocontoInStampa.Prodotti[indiceRigaResoconto];

                // Controlla lo spazio disponibile nella pagina.
                if (y + 25 > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                grafica.DrawString(
                    prodotto.NomeProdotto,
                    fontNormale,
                    Brushes.Black,
                    x,
                    y
                );

                grafica.DrawString(
                    prodotto.Categoria,
                    fontNormale,
                    Brushes.Black,
                    x + larghezza * 0.55f,
                    y
                );

                grafica.DrawString(
                    prodotto.Quantita.ToString(),
                    fontNormale,
                    Brushes.Black,
                    x + larghezza * 0.85f,
                    y
                );

                y += 25;

                indiceRigaResoconto++;
            }

            // CHIUSURA TABELLA
            grafica.DrawLine(
                penna,
                x,
                y + 5,
                x + larghezza,
                y + 5
            );

            e.HasMorePages = false;
        }


        private void DisegnaScontrino(
            Graphics grafica,
            RectangleF area,
            Font fontTitolo,
            Font fontNormale,
            Font fontGrassetto)
        {
            float x = area.X + 5;
            float y = area.Y + 5;
            float larghezza = area.Width - 10;

            using StringFormat centro = new StringFormat
            {
                Alignment = StringAlignment.Center
            };

            using StringFormat destra = new StringFormat
            {
                Alignment = StringAlignment.Far
            };

            using Pen linea = new Pen(Color.Black, 0.7F);

            // NOME CLIENTE
            string cliente = "Cliente: " + txtNomeCliente.Text.Trim();

            float altezzaCliente = grafica.MeasureString(
                cliente,
                fontGrassetto,
                (int)larghezza
            ).Height;

            grafica.DrawString(
                cliente,
                fontGrassetto,
                Brushes.Black,
                new RectangleF(x, y, larghezza, altezzaCliente)
            );

            y += altezzaCliente + 5;

            grafica.DrawLine(
                linea,
                x,
                y,
                x + larghezza,
                y
            );

            y += 5;

            decimal totale = 0;

            FlowLayoutPanel[] pannelli =
            {
        flpCoperto,
        flpBevande,
        flpPizze,
        flpDolci
    };

            string[] categorie =
            {
        "COPERTO",
        "BEVANDE",
        "PIZZE",
        "DOLCI"
    };

            float larghezzaPrezzo = 60;
            float larghezzaNome = larghezza - larghezzaPrezzo - 8;

            // PRODOTTI
            for (int i = 0; i < pannelli.Length; i++)
            {
                bool categoriaStampata = false;

                foreach (SchedaPizza scheda in pannelli[i].Controls)
                {
                    if (scheda.Quantita <= 0)
                        continue;

                    // Titolo categoria
                    if (!categoriaStampata)
                    {
                        grafica.DrawString(
                            categorie[i],
                            fontGrassetto,
                            Brushes.Black,
                            x,
                            y
                        );

                        y += 14;
                        categoriaStampata = true;
                    }

                    decimal subtotale =
                        scheda.PrezzoPizza * scheda.Quantita;

                    totale += subtotale;

                    string nomeProdotto =
                        $"{scheda.NomePizza} x {scheda.Quantita}";

                    string prezzo =
                        subtotale.ToString("C2", culture);

                    // Calcola l'altezza necessaria per il nome.
                    float altezzaNome = grafica.MeasureString(
                        nomeProdotto,
                        fontNormale,
                        (int)larghezzaNome
                    ).Height;

                    float altezzaRiga = Math.Max(12, altezzaNome);

                    // Nome e quantità a sinistra.
                    grafica.DrawString(
                        nomeProdotto,
                        fontNormale,
                        Brushes.Black,
                        new RectangleF(
                            x,
                            y,
                            larghezzaNome,
                            altezzaRiga
                        )
                    );

                    // Prezzo allineato a destra.
                    grafica.DrawString(
                        prezzo,
                        fontNormale,
                        Brushes.Black,
                        new RectangleF(
                            x + larghezzaNome + 8,
                            y,
                            larghezzaPrezzo,
                            altezzaRiga
                        ),
                        destra
                    );

                    y += altezzaRiga + 2;
                }

                // Piccolo spazio tra le categorie.
                if (categoriaStampata)
                    y += 3;
            }

            // TOTALE
            y += 3;

            grafica.DrawLine(
                linea,
                x,
                y,
                x + larghezza,
                y
            );

            y += 6;

            grafica.DrawString(
                "TOTALE",
                fontTitolo,
                Brushes.Black,
                new RectangleF(x, y, larghezza / 2, 20)
            );

            grafica.DrawString(
                totale.ToString("C2", culture),
                fontTitolo,
                Brushes.Black,
                new RectangleF(
                    x + larghezza / 2,
                    y,
                    larghezza / 2,
                    20
                ),
                destra
            );
        }


        private void btnStampa_Click(object sender, EventArgs e)
        {
            string nomeCliente = txtNomeCliente.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomeCliente))
            {
                MessageBox.Show(
                    "Inserisci il nome del cliente prima di stampare.",
                    "Nome cliente mancante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNomeCliente.Focus();
                return;
            }

            // Salva l'ordine prima di procedere con la stampa.
            if (!SalvaOrdineCorrente())
                return;

            using (PrintDialog dialog = new PrintDialog())
            {
                dialog.Document = printDocument;
                dialog.UseEXDialog = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    printDocument.Print();

                    RimuoviPizzeTemporanee();
                    resetQuantita();

                    txtNomeCliente.Clear();

                    // Prepara un nuovo ordine.
                    codiceOrdineCorrente = null;
                    ordineSalvato = false;

                    MessageBox.Show(
                        "Stampa inviata correttamente.",
                        "Stampa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Errore durante la stampa:\n" + ex.Message +
                        "\n\nL'ordine è comunque stato salvato nel database.",
                        "Errore stampa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }


        private void RimuoviPizzeTemporanee()
        {
            foreach (SchedaPizza scheda in pizzeTemporanee)
            {
                if (!scheda.IsDisposed)
                {
                    scheda.Parent?.Controls.Remove(scheda);
                    scheda.Dispose();
                }
            }
            pizzeTemporanee.Clear();
            AggiornaTotale();
        }

        private void resetQuantita()
        {
            FlowLayoutPanel[] pannelli =
            {
                flpPizze,
                flpCoperto,
                flpBevande,
                flpDolci
            };
            foreach (FlowLayoutPanel pannello in pannelli)
            {
                foreach (SchedaPizza scheda in pannello.Controls)
                {
                    scheda.impostaQuantita(0);
                }
            }
            AggiornaTotale();
        }
        private void btnPersonalizza_Click(object sender, EventArgs e)
        {
            using (PersonalizzaPizza form = new PersonalizzaPizza())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SchedaPizza temp = AggiungiSchedaElemento(
                        flpPizze,
                        form.NomePizza,
                        form.PrezzoPizza,
                        1
                    );
                    pizzeTemporanee.Add(temp);
                    AggiornaTotale();
                }
            }
        }

        private void modificaIngredientiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (GestioneIngredienti finestra = new GestioneIngredienti())
            {
                finestra.ShowDialog(this);
            }
        }

        private void tabellaOrdiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseManager.ControllaOrdini();
        }


        private void resocontoFinaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime oggi = DateTime.Today;

                // 1. Controlla se esiste già il resoconto.
                ResocontoGiornataDati? resoconto =
                    DatabaseManager.CaricaResocontoGiornata(oggi);

                // 2. Se non esiste, prova a generarlo.
                if (resoconto == null)
                {
                    bool creato = DatabaseManager.GeneraResocontoGiornata(oggi);

                    // 3. Recupera il resoconto appena creato.
                    resoconto = DatabaseManager.CaricaResocontoGiornata(oggi);

                    // Se non è stato possibile recuperarlo, non ci sono ordini
                    // oppure si è verificato un problema nella generazione.
                    if (resoconto == null)
                    {
                        MessageBox.Show(
                            "Non è stato possibile creare il resoconto.\n" +
                            "Potrebbe non esserci ancora nessun ordine registrato.",
                            "Resoconto giornaliero",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        return;
                    }
                }

                // 4. Prepara i dati per la stampa.
                resocontoInStampa = resoconto;
                indiceRigaResoconto = 0;

                // 5. Imposta il nome del documento.
                printDocumentResoconto.DocumentName =
                    $"Resoconto giornaliero {oggi:dd-MM-yyyy}";

                // 6. Apre l'anteprima di stampa.
                stampaResocontoCompletata = false;

                using (PrintPreviewDialog anteprima = new PrintPreviewDialog())
                {
                    anteprima.Document = printDocumentResoconto;
                    anteprima.StartPosition = FormStartPosition.CenterScreen;
                    anteprima.Width = 1000;
                    anteprima.Height = 800;

                    printDocumentResoconto.EndPrint += (s, e) =>
                    {
                        if (e.PrintAction != PrintAction.PrintToPreview &&
                            stampaResocontoCompletata)
                        {
                            anteprima.BeginInvoke(new Action(() =>
                            {
                                anteprima.Close();
                            }));
                        }
                    };

                    anteprima.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Errore durante la preparazione del resoconto:\n" + ex.Message,
                    "Errore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

    }
}
