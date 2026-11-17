using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Prenotazione_PIzze.Database;

namespace Prenotazione_PIzze
{
    public partial class PersonalizzaPizza : Form
    {
        private string basePizzaSelezionata = "";
        private decimal prezzoBaseSelezionata = 0m;

        private static readonly CultureInfo cultura = CultureInfo.GetCultureInfo("it-IT");

        public string NomePizza { get; private set; } = "";
        public decimal PrezzoPizza { get; private set; }
        public int QuantitaPizza { get; private set; } = 1;

        private class ElementoPrezzo
        {
            public string Nome { get; }
            public decimal Prezzo { get; }

            public ElementoPrezzo(string nome, decimal prezzo)
            {
                Nome = nome;
                Prezzo = prezzo;
            }

            public override string ToString()
            {
                return $"{Nome} - {Prezzo.ToString("C2", cultura)}";
            }
        }

        public PersonalizzaPizza()
        {
            InitializeComponent();
            ApplicaStileMinimal();

            clbBase.MouseDown += clbBase_MouseDown;
            clbBase.ItemCheck += clbBase_ItemCheck;
            clbIngredienti.MouseDown += clbIngredienti_MouseDown;
            clbIngredienti.ItemCheck += clbIngredienti_ItemCheck;

            txtPrezzo.ReadOnly = true;
            txtPrezzo.Text = 0m.ToString("C2", cultura);

            CaricaBasi();
            CaricaIngredienti();
        }

        private void ApplicaStileMinimal()
        {
            Color sfondo = Color.FromArgb(245, 246, 247);
            Color bianco = Color.White;
            Color testo = Color.FromArgb(41, 41, 41);
            Color rosso = Color.FromArgb(168, 61, 50);

            BackColor = sfondo;
            Font = new Font("Segoe UI", 9F);

            clbBase.BackColor = bianco;
            clbBase.ForeColor = testo;
            clbBase.BorderStyle = BorderStyle.None;
            clbBase.Font = new Font("Segoe UI", 12F);

            clbIngredienti.BackColor = bianco;
            clbIngredienti.ForeColor = testo;
            clbIngredienti.BorderStyle = BorderStyle.None;
            clbIngredienti.Font = new Font("Segoe UI", 11F);

            lblSegliBase.ForeColor = testo;
            lblSegliBase.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            lblScegliIng.ForeColor = testo;
            lblScegliIng.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            lblPrezzo.ForeColor = testo;
            lblPrezzo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            txtPrezzo.BackColor = bianco;
            txtPrezzo.ForeColor = testo;
            txtPrezzo.BorderStyle = BorderStyle.FixedSingle;
            txtPrezzo.Font = new Font("Segoe UI", 10F);
            txtPrezzo.ReadOnly = true;

            ApplicaStilePulsante(btnNext, rosso, bianco);
            ApplicaStilePulsante(btnFine, rosso, bianco);
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
            pulsante.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pulsante.Cursor = Cursors.Hand;
            pulsante.FlatAppearance.MouseOverBackColor = Color.FromArgb(145, 49, 40);
            pulsante.FlatAppearance.MouseDownBackColor = Color.FromArgb(125, 42, 35);
            pulsante.Height = 36;
        }

        private void CaricaBasi()
        {
            clbBase.Items.Clear();

            // Le basi sono le pizze già salvate nel menu.
            // La categoria usata da ProdottiMenu è "Pizze".
            var prodotti = DatabaseManager.CaricaProdotto();

            foreach (var prodotto in prodotti)
            {
                if (string.Equals(
                    prodotto.categoria,
                    "Pizze",
                    StringComparison.OrdinalIgnoreCase))
                {
                    clbBase.Items.Add(
                        new ElementoPrezzo(prodotto.name, prodotto.prezzo));
                }
            }

            if (clbBase.Items.Count == 0)
            {
                MessageBox.Show(
                    "Non ci sono pizze salvate nel menu da usare come base.",
                    "Nessuna base disponibile",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                btnNext.Enabled = false;
            }
        }

        private void CaricaIngredienti()
        {
            clbIngredienti.Items.Clear();

            var ingredienti = DatabaseManager.CaricaIngredientiConPrezzo();

            foreach (var ingrediente in ingredienti)
            {
                clbIngredienti.Items.Add(
                    new ElementoPrezzo(ingrediente.Nome, ingrediente.Prezzo));
            }
        }

        private void clbBase_MouseDown(object sender, MouseEventArgs e)
        {
            int indice = clbBase.IndexFromPoint(e.Location);

            if (indice != ListBox.NoMatches)
            {
                clbBase.SetItemChecked(
                    indice,
                    !clbBase.GetItemChecked(indice));
            }
        }

        private void clbBase_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < clbBase.Items.Count; i++)
                {
                    if (i != e.Index)
                        clbBase.SetItemChecked(i, false);
                }
            }

            BeginInvoke(new Action(() =>
            {
                if (IsDisposed)
                    return;

                AggiornaPrezzo();
            }));
        }

        private void clbIngredienti_MouseDown(object sender, MouseEventArgs e)
        {
            int indice = clbIngredienti.IndexFromPoint(e.Location);

            if (indice != ListBox.NoMatches)
            {
                clbIngredienti.SetItemChecked(
                    indice,
                    !clbIngredienti.GetItemChecked(indice));
            }
        }

        private void clbIngredienti_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed)
                    AggiornaPrezzo();
            }));
        }

        private ElementoPrezzo? OttieniBaseSelezionata()
        {
            if (clbBase.CheckedItems.Count == 0)
                return null;

            return clbBase.CheckedItems[0] as ElementoPrezzo;
        }

        private List<ElementoPrezzo> OttieniIngredientiSelezionati()
        {
            List<ElementoPrezzo> ingredienti = new List<ElementoPrezzo>();

            foreach (var ingrediente in clbIngredienti.CheckedItems)
            {
                if (ingrediente is ElementoPrezzo elemento)
                    ingredienti.Add(elemento);
            }

            return ingredienti;
        }

        private decimal CalcolaPrezzoTotale()
        {
            decimal totale = prezzoBaseSelezionata;

            foreach (var ingrediente in OttieniIngredientiSelezionati())
                totale += ingrediente.Prezzo;

            return totale;
        }

        private void AggiornaPrezzo()
        {
            decimal totale = CalcolaPrezzoTotale();
            txtPrezzo.Text = totale.ToString("C2", cultura);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ElementoPrezzo? baseSelezionata = OttieniBaseSelezionata();

            if (baseSelezionata == null)
            {
                MessageBox.Show("Seleziona una base prima di continuare.");
                return;
            }

            basePizzaSelezionata = baseSelezionata.Nome;
            prezzoBaseSelezionata = baseSelezionata.Prezzo;

            // Nasconde la schermata della base.
            clbBase.Visible = false;
            lblSegliBase.Visible = false;
            btnNext.Visible = false;

            // Mostra la schermata degli ingredienti.
            clbIngredienti.Visible = true;
            lblScegliIng.Visible = true;
            lblPrezzo.Visible = true;
            btnFine.Visible = true;
            txtPrezzo.Visible = true;

            AggiornaPrezzo();
        }

        private void btnFine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(basePizzaSelezionata))
            {
                MessageBox.Show("Seleziona una base prima di continuare.");
                return;
            }

            List<ElementoPrezzo> ingredienti = OttieniIngredientiSelezionati();

            string nomePizza = basePizzaSelezionata;

            if (ingredienti.Count > 0)
            {
                List<string> nomiIngredienti = new List<string>();

                foreach (var ingrediente in ingredienti)
                    nomiIngredienti.Add(ingrediente.Nome);

                nomePizza += ", " + string.Join(", ", nomiIngredienti);
            }

            PrezzoPizza = CalcolaPrezzoTotale();

            if (PrezzoPizza <= 0)
            {
                MessageBox.Show("Il prezzo della pizza deve essere maggiore di zero.");
                return;
            }

            NomePizza = nomePizza;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
