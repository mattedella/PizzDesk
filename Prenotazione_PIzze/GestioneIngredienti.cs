using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Prenotazione_PIzze.Database;

namespace Prenotazione_PIzze
{
    public partial class GestioneIngredienti : Form
    {
        private static readonly CultureInfo CulturaItaliana = CultureInfo.GetCultureInfo("it-IT");

        public GestioneIngredienti()
        {
            InitializeComponent();
            btnChiudi.Click += btnChiudi_Click;
            ApplicaStileMinimal();
            CaricaListaIngredienti();
        }

        private void ApplicaStileMinimal()
        {
            BackColor = Color.FromArgb(245, 246, 247);
            Font = new Font("Segoe UI", 9F);
            flpIngredienti.BackColor = Color.FromArgb(245, 246, 247);

            txtNuovoIngrediente.BackColor = Color.White;
            txtNuovoIngrediente.ForeColor = Color.FromArgb(41, 41, 41);
            txtNuovoIngrediente.BorderStyle = BorderStyle.FixedSingle;
            txtNuovoIngrediente.Font = new Font("Segoe UI", 10F);

            ApplicaStilePulsante(btnNew, Color.FromArgb(168, 61, 50), Color.White);
            ApplicaStilePulsante(btnChiudi, Color.FromArgb(238, 238, 238), Color.FromArgb(41, 41, 41));
        }

        private void ApplicaStilePulsante(Button btn, Color sfondo, Color testo)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = sfondo;
            btn.ForeColor = testo;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 225, 225);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(215, 215, 215);
        }

        private void CaricaListaIngredienti()
        {
            flpIngredienti.Controls.Clear();

            List<(string Nome, decimal Prezzo)> ingredienti = DatabaseManager.CaricaIngredientiConPrezzo();

            foreach (var ingrediente in ingredienti)
            {
                TableLayoutPanel riga = new TableLayoutPanel
                {
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(3),
                    Padding = new Padding(3),
                    ColumnCount = 4,
                    Height = 45,
                    Width = Math.Max(300, flpIngredienti.ClientSize.Width - 25)
                };

                riga.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
                riga.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
                riga.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
                riga.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));

                Label lblIngrediente = new Label
                {
                    ForeColor = Color.FromArgb(41, 41, 41),
                    Font = new Font("Segoe UI", 10F),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = ingrediente.Nome,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5)
                };

                Label lblPrezzo = new Label
                {
                    ForeColor = Color.FromArgb(41, 41, 41),
                    Font = new Font("Segoe UI", 9F),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = ingrediente.Prezzo.ToString("C2", CulturaItaliana),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(2)
                };

                Button btnModifica = new Button
                {
                    Text = "✎",
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(238, 238, 238),
                    ForeColor = Color.FromArgb(41, 41, 41),
                    Font = new Font("Segoe UI Symbol", 12F),
                    Cursor = Cursors.Hand,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(2)
                };
                btnModifica.FlatAppearance.BorderSize = 0;
                btnModifica.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 225, 225);
                btnModifica.Click += (sender, e) => ModificaIngrediente(ingrediente.Nome, ingrediente.Prezzo);

                Button btnElimina = new Button
                {
                    Text = "×",
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(238, 238, 238),
                    ForeColor = Color.FromArgb(168, 61, 50),
                    Font = new Font("Segoe UI", 14F),
                    Cursor = Cursors.Hand,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(2)
                };
                btnElimina.FlatAppearance.BorderSize = 0;
                btnElimina.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 225, 223);
                btnElimina.Click += (sender, e) => EliminaIngrediente(ingrediente.Nome);

                riga.Controls.Add(lblIngrediente, 0, 0);
                riga.Controls.Add(lblPrezzo, 1, 0);
                riga.Controls.Add(btnModifica, 2, 0);
                riga.Controls.Add(btnElimina, 3, 0);

                flpIngredienti.Controls.Add(riga);
            }
        }

        private void ModificaIngrediente(string nomeAttuale, decimal prezzoAttuale)
        {
            string nuovoNome = Microsoft.VisualBasic.Interaction.InputBox(
                "Modifica il nome dell'ingrediente:",
                "Modifica Ingrediente",
                nomeAttuale);

            if (string.IsNullOrWhiteSpace(nuovoNome))
                return;

            string nuovoPrezzoTesto = Microsoft.VisualBasic.Interaction.InputBox(
                "Modifica il prezzo dell'ingrediente:",
                "Modifica Prezzo",
                prezzoAttuale.ToString("0.00", CulturaItaliana));

            if (string.IsNullOrWhiteSpace(nuovoPrezzoTesto))
                return;

            if (!ProvaLeggerePrezzo(nuovoPrezzoTesto, out decimal nuovoPrezzo))
            {
                MessageBox.Show("Inserisci un prezzo valido (es. 1,50).", "Prezzo non valido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DatabaseManager.ModificaIngrediente(nomeAttuale, nuovoNome.Trim());
                DatabaseManager.ModificaPrezzoIngrediente(nuovoNome.Trim(), nuovoPrezzo.ToString("F2", CultureInfo.GetCultureInfo("it-IT")));
                CaricaListaIngredienti();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la modifica dell'ingrediente:\n" + ex.Message);
            }
        }

        private bool ProvaLeggerePrezzo(string testo, out decimal prezzo)
        {
            return decimal.TryParse(testo, NumberStyles.Currency, CulturaItaliana, out prezzo)
                && prezzo >= 0;
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string nomeIngrediente = txtNuovoIngrediente.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomeIngrediente))
            {
                MessageBox.Show("Inserisci il nome dell'ingrediente.");
                return;
            }

            string prezzoTesto = Microsoft.VisualBasic.Interaction.InputBox(
                "Inserisci il prezzo dell'ingrediente:",
                "Nuovo Ingrediente",
                "0,00");

            if (string.IsNullOrWhiteSpace(prezzoTesto))
                return;

            if (!ProvaLeggerePrezzo(prezzoTesto, out decimal prezzo))
            {
                MessageBox.Show("Inserisci un prezzo valido (es. 1,50).", "Prezzo non valido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DatabaseManager.AggiungiIngrediente(nomeIngrediente, prezzo.ToString("F2", CultureInfo.GetCultureInfo("it-IT")));
                CaricaListaIngredienti();
                txtNuovoIngrediente.Clear();
                txtNuovoIngrediente.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'aggiunta dell'ingrediente:\n" + ex.Message);
            }
        }

        private void EliminaIngrediente(string nomeIngrediente)
        {
            DialogResult result = MessageBox.Show(
                $"Sei sicuro di voler eliminare l'ingrediente '{nomeIngrediente}'?",
                "Conferma eliminazione",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            try
            {
                DatabaseManager.EliminaIngrediente(nomeIngrediente);
                CaricaListaIngredienti();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'eliminazione dell'ingrediente:\n" + ex.Message);
            }
        }

        private void btnChiudi_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
