using System.Globalization;

namespace PizzaPrenotate
{
    public class SchedaPizza : UserControl
    {
        private readonly Label lblNome;
        private readonly Label lblPrezzo;
        private readonly Label lblQuantita;
        private readonly Button btnMeno;
        private readonly Button btnPiu;
        private readonly TextBox txtNome;
        private readonly TextBox txtPrezzo;
        private readonly Button btnElimina;

        public string NomePizza { get; private set; }
        public decimal PrezzoPizza { get; private set; }
        public int Quantita { get; private set; }
        public bool ModalitaModifica { get; private set; }

        private string nomeOriginale = "";
        private string prezzoOriginale = "";

        public event EventHandler? ValoriModificati;
        public event EventHandler? RichiestaEliminazione;

        public string StatoElemento => $"{NomePizza}|{PrezzoPizza}";

        public SchedaPizza(
            string nome,
            decimal prezzo,
            int quantita)
        {
            NomePizza = nome;
            PrezzoPizza = prezzo;
            Quantita = quantita;

            Height = 54;
            MinimumSize = new Size(326, 36);
            Margin = new Padding(3);
            BackColor = Color.White;
            BorderStyle = BorderStyle.None;
            Dock = DockStyle.Top;

            lblNome = new Label
            {
                Text = nome,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true,
                AutoEllipsis = true,
                Margin = new Padding(5, 3, 3, 3),
                ForeColor = Color.FromArgb(41, 41, 41)
            };

            lblPrezzo = new Label
            {
                Text = prezzo.ToString("C2"),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Margin = new Padding(3),
                ForeColor = Color.FromArgb(90, 90, 90)
            };

            txtNome = new TextBox
            {
                Text = nome,
                Dock = DockStyle.Fill,
                Visible = false,
                Margin = new Padding(3)
            };

            txtPrezzo = new TextBox
            {
                Text = prezzo.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("it-IT")),
                Dock = DockStyle.Fill,
                Visible = false,
                Margin = new Padding(3)
            };

            btnElimina = new Button
            {
                Text = "×",
                ForeColor = Color.Red,
                Dock = DockStyle.Fill,
                Visible = false,
                Margin = new Padding(2),
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };

            btnMeno = new Button
            {
                Text = "−",
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                Cursor = Cursors.Hand
            };

            lblQuantita = new Label
            {
                Text = Quantita.ToString(),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1),
                ForeColor = Color.FromArgb(168, 61, 50),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            btnPiu = new Button
            {
                Text = "+",
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                Cursor = Cursors.Hand
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1,
                Padding = new Padding(4, 3, 4, 3),
                Margin = new Padding(0),
                BackColor = Color.White
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 60F));
            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 36F));
            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 24F));
            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 36F));

            layout.Controls.Add(lblNome, 0, 0);
            layout.Controls.Add(txtNome, 0, 0);
            layout.Controls.Add(lblPrezzo, 1, 0);
            layout.Controls.Add(txtPrezzo, 1, 0);
            layout.Controls.Add(btnMeno, 2, 0);
            layout.Controls.Add(lblQuantita, 3, 0);
            layout.Controls.Add(btnPiu, 4, 0);
            layout.Controls.Add(btnElimina, 4, 0);

            Controls.Add(layout);
            // Stile dei pulsanti
            ApplicaStilePulsante(btnMeno);
            ApplicaStilePulsante(btnPiu);
            ApplicaStilePulsante(btnElimina);

            // Stile generale della scheda
            BackColor = Color.White;
            ForeColor = Color.FromArgb(41, 41, 41);

            btnMeno.Click += (s, e) =>
            {
                if (Quantita > 0)
                {
                    Quantita--;
                    AggiornaQuantita();
                }
            };

            btnPiu.Click += (s, e) =>
            {
                Quantita++;
                AggiornaQuantita();
            };

            btnElimina.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show(
                    $"Sei sicuro di voler eliminare '{NomePizza}'?",
                    "Conferma eliminazione",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    RichiestaEliminazione?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using Pen penna = new Pen(
                Color.FromArgb(220, 222, 225), 1);

            e.Graphics.DrawRectangle(
                penna,
                0,
                0,
                Width - 1,
                Height - 1);
        }

        private void ApplicaStilePulsante(Button pulsante)
        {
            pulsante.FlatStyle = FlatStyle.Flat;
            pulsante.FlatAppearance.BorderSize = 0;

            pulsante.BackColor = Color.FromArgb(238, 238, 238);
            pulsante.ForeColor = Color.FromArgb(41, 41, 41);

            pulsante.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            pulsante.Cursor = Cursors.Hand;

            pulsante.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(225, 225, 225);

            pulsante.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(215, 215, 215);

            pulsante.FlatAppearance.BorderColor = Color.FromArgb(230, 230, 230);
            pulsante.FlatAppearance.BorderSize = 1;
        }

        private void AggiornaQuantita()
        {
            lblQuantita.Text = Quantita.ToString();
            ValoriModificati?.Invoke(this, EventArgs.Empty);
        }

        public void ImpostaModalitaModifica(bool attiva)
        {
            if (ModalitaModifica == attiva)
                return;
            ModalitaModifica = attiva;

            lblNome.Visible = !attiva;
            lblPrezzo.Visible = !attiva;

            txtNome.Visible = attiva;
            txtPrezzo.Visible = attiva;
            btnElimina.Visible = attiva;

            btnMeno.Visible = !attiva;
            btnPiu.Visible = !attiva;
            lblQuantita.Visible = !attiva;

            if (attiva)
            {
                nomeOriginale = NomePizza;
                prezzoOriginale = PrezzoPizza.ToString("0.00", CultureInfo.GetCultureInfo("it-IT"));
                txtNome.Text = nomeOriginale;
                txtPrezzo.Text = prezzoOriginale;
            }
            PerformLayout();
        }

        public void ApplicaModificheInCorso()
        {
            string nome = txtNome.Text.Trim();

            decimal.TryParse(
                txtPrezzo.Text,
                NumberStyles.Currency,
                CultureInfo.GetCultureInfo("it-IT"),
                out decimal prezzo);

            ModificaElemento(nome, prezzo);
        }

        public void ModificaElemento(string nome, decimal prezzo)
        {
            NomePizza = nome;
            PrezzoPizza = prezzo;
            lblNome.Text = nome;
            lblPrezzo.Text = prezzo.ToString("C2");
            ValoriModificati?.Invoke(this, EventArgs.Empty);
        }

        public bool ValidaModifiche(out string errore)
        {
            string nome = txtNome.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                errore = "Inserisci il nome dell'elemento.";
                return false;
            }
            
            if (!decimal.TryParse(
                txtPrezzo.Text,
                NumberStyles.Currency,
                CultureInfo.GetCultureInfo("it-IT"),
                out decimal prezzo) || prezzo <= 0)
            {
                errore = "Inserisci un prezzo valido e non negativo.";
                return false;
            }

            errore = "";
            return true;
        }

        public void impostaQuantita(int quantita)
        {
            Quantita = quantita;
            AggiornaQuantita();
        }
    }
}
