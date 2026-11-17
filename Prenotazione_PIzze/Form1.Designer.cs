namespace Prenotazione_PIzze
{
    partial class Pizzeria
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pizzeria));
            dgvPizze = new DataGridView();
            lblTotale = new Label();
            tlpPrincipale = new TableLayoutPanel();
            pnlPizze = new Panel();
            btnAddPizze = new Button();
            btnPersonalizza = new Button();
            lblTitoloPizze = new Label();
            flpPizze = new FlowLayoutPanel();
            pnlExtra = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlCoperto = new Panel();
            tlpCoperto = new TableLayoutPanel();
            flpCoperto = new FlowLayoutPanel();
            lblCoperto = new Label();
            pnlNomeCliente = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtNomeCliente = new TextBox();
            lblNomeCliente = new Label();
            pnlBevande = new Panel();
            btnAddBevande = new Button();
            flpBevande = new FlowLayoutPanel();
            lblBevande = new Label();
            pnlDolci = new Panel();
            btnAddDolci = new Button();
            flpDolci = new FlowLayoutPanel();
            lblDolci = new Label();
            pnlModStampa = new Panel();
            btnStampa = new Button();
            btnEliminaMod = new Button();
            btnModEl = new Button();
            menuStripPrincipale = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            resocontoFinaleToolStripMenuItem = new ToolStripMenuItem();
            tabellaOrdiniToolStripMenuItem = new ToolStripMenuItem();
            gestioneMenùToolStripMenuItem = new ToolStripMenuItem();
            modificaToolStripMenuItem = new ToolStripMenuItem();
            modificaIngredientiToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvPizze).BeginInit();
            tlpPrincipale.SuspendLayout();
            pnlPizze.SuspendLayout();
            pnlExtra.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlCoperto.SuspendLayout();
            tlpCoperto.SuspendLayout();
            pnlNomeCliente.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            pnlBevande.SuspendLayout();
            pnlDolci.SuspendLayout();
            pnlModStampa.SuspendLayout();
            menuStripPrincipale.SuspendLayout();
            SuspendLayout();
            // 
            // dgvPizze
            // 
            dgvPizze.AllowUserToAddRows = false;
            dgvPizze.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPizze.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPizze.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPizze.Location = new Point(10, 41);
            dgvPizze.Margin = new Padding(3, 2, 3, 2);
            dgvPizze.Name = "dgvPizze";
            dgvPizze.RowHeadersWidth = 51;
            dgvPizze.Size = new Size(1484, 734);
            dgvPizze.TabIndex = 0;
            // 
            // lblTotale
            // 
            lblTotale.Anchor = AnchorStyles.Left;
            lblTotale.AutoSize = true;
            lblTotale.ImageAlign = ContentAlignment.TopRight;
            lblTotale.Location = new Point(3, 28);
            lblTotale.Name = "lblTotale";
            lblTotale.Size = new Size(75, 15);
            lblTotale.TabIndex = 2;
            lblTotale.Text = "Totale: € 0,00";
            lblTotale.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tlpPrincipale
            // 
            tlpPrincipale.ColumnCount = 2;
            tlpPrincipale.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpPrincipale.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tlpPrincipale.Controls.Add(pnlPizze, 1, 0);
            tlpPrincipale.Controls.Add(pnlExtra, 0, 0);
            tlpPrincipale.Dock = DockStyle.Fill;
            tlpPrincipale.Location = new Point(0, 24);
            tlpPrincipale.Margin = new Padding(3, 2, 3, 2);
            tlpPrincipale.Name = "tlpPrincipale";
            tlpPrincipale.Padding = new Padding(18, 15, 18, 38);
            tlpPrincipale.RowCount = 1;
            tlpPrincipale.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipale.Size = new Size(1505, 785);
            tlpPrincipale.TabIndex = 3;
            tlpPrincipale.Paint += tlpPrincipale_Paint;
            // 
            // pnlPizze
            // 
            pnlPizze.Controls.Add(btnAddPizze);
            pnlPizze.Controls.Add(btnPersonalizza);
            pnlPizze.Controls.Add(lblTitoloPizze);
            pnlPizze.Controls.Add(flpPizze);
            pnlPizze.Dock = DockStyle.Fill;
            pnlPizze.Location = new Point(388, 17);
            pnlPizze.Margin = new Padding(3, 2, 3, 2);
            pnlPizze.Name = "pnlPizze";
            pnlPizze.Padding = new Padding(4);
            pnlPizze.Size = new Size(1096, 728);
            pnlPizze.TabIndex = 0;
            // 
            // btnAddPizze
            // 
            btnAddPizze.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddPizze.Location = new Point(995, 11);
            btnAddPizze.Margin = new Padding(3, 2, 3, 2);
            btnAddPizze.Name = "btnAddPizze";
            btnAddPizze.Size = new Size(83, 22);
            btnAddPizze.TabIndex = 0;
            btnAddPizze.Text = "+ Aggiungi";
            btnAddPizze.UseVisualStyleBackColor = true;
            // 
            // btnPersonalizza
            // 
            btnPersonalizza.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPersonalizza.Location = new Point(903, 11);
            btnPersonalizza.Margin = new Padding(3, 2, 3, 2);
            btnPersonalizza.Name = "btnPersonalizza";
            btnPersonalizza.Size = new Size(88, 22);
            btnPersonalizza.TabIndex = 0;
            btnPersonalizza.Text = "Personalizza";
            btnPersonalizza.UseVisualStyleBackColor = true;
            // 
            // lblTitoloPizze
            // 
            lblTitoloPizze.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitoloPizze.Location = new Point(4, 4);
            lblTitoloPizze.Name = "lblTitoloPizze";
            lblTitoloPizze.Size = new Size(735, 34);
            lblTitoloPizze.TabIndex = 1;
            lblTitoloPizze.Text = "Pizze";
            lblTitoloPizze.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpPizze
            // 
            flpPizze.AutoScroll = true;
            flpPizze.Dock = DockStyle.Fill;
            flpPizze.FlowDirection = FlowDirection.TopDown;
            flpPizze.Location = new Point(4, 4);
            flpPizze.Margin = new Padding(3, 2, 3, 2);
            flpPizze.Name = "flpPizze";
            flpPizze.Padding = new Padding(4, 34, 4, 4);
            flpPizze.Size = new Size(1088, 720);
            flpPizze.TabIndex = 0;
            // 
            // pnlExtra
            // 
            pnlExtra.Controls.Add(tableLayoutPanel1);
            pnlExtra.Dock = DockStyle.Fill;
            pnlExtra.Location = new Point(21, 17);
            pnlExtra.Margin = new Padding(3, 2, 3, 2);
            pnlExtra.Name = "pnlExtra";
            pnlExtra.Padding = new Padding(4);
            pnlExtra.Size = new Size(361, 728);
            pnlExtra.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pnlCoperto, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlBevande, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlDolci, 0, 2);
            tableLayoutPanel1.Controls.Add(pnlModStampa, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(4, 4);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel1.Size = new Size(353, 720);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlCoperto
            // 
            pnlCoperto.Controls.Add(tlpCoperto);
            pnlCoperto.Dock = DockStyle.Fill;
            pnlCoperto.Location = new Point(3, 2);
            pnlCoperto.Margin = new Padding(3, 2, 3, 2);
            pnlCoperto.Name = "pnlCoperto";
            pnlCoperto.Size = new Size(347, 176);
            pnlCoperto.TabIndex = 0;
            // 
            // tlpCoperto
            // 
            tlpCoperto.ColumnCount = 1;
            tlpCoperto.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCoperto.Controls.Add(flpCoperto, 0, 2);
            tlpCoperto.Controls.Add(lblCoperto, 0, 0);
            tlpCoperto.Controls.Add(pnlNomeCliente, 0, 1);
            tlpCoperto.Dock = DockStyle.Fill;
            tlpCoperto.Location = new Point(0, 0);
            tlpCoperto.Margin = new Padding(3, 2, 3, 2);
            tlpCoperto.Name = "tlpCoperto";
            tlpCoperto.RowCount = 3;
            tlpCoperto.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpCoperto.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlpCoperto.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlpCoperto.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpCoperto.Size = new Size(347, 176);
            tlpCoperto.TabIndex = 2;
            // 
            // flpCoperto
            // 
            flpCoperto.AutoScroll = true;
            flpCoperto.Dock = DockStyle.Fill;
            flpCoperto.FlowDirection = FlowDirection.TopDown;
            flpCoperto.Location = new Point(3, 107);
            flpCoperto.Margin = new Padding(3, 2, 3, 2);
            flpCoperto.Name = "flpCoperto";
            flpCoperto.Size = new Size(341, 67);
            flpCoperto.TabIndex = 1;
            flpCoperto.WrapContents = false;
            // 
            // lblCoperto
            // 
            lblCoperto.AutoSize = true;
            lblCoperto.Dock = DockStyle.Top;
            lblCoperto.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCoperto.Location = new Point(3, 0);
            lblCoperto.Name = "lblCoperto";
            lblCoperto.Size = new Size(341, 30);
            lblCoperto.TabIndex = 0;
            lblCoperto.Text = "Coperto";
            // 
            // pnlNomeCliente
            // 
            pnlNomeCliente.BackColor = Color.White;
            pnlNomeCliente.Controls.Add(tableLayoutPanel2);
            pnlNomeCliente.Dock = DockStyle.Fill;
            pnlNomeCliente.Location = new Point(3, 38);
            pnlNomeCliente.MaximumSize = new Size(330, 50);
            pnlNomeCliente.MinimumSize = new Size(330, 50);
            pnlNomeCliente.Name = "pnlNomeCliente";
            pnlNomeCliente.Padding = new Padding(3);
            pnlNomeCliente.Size = new Size(330, 50);
            pnlNomeCliente.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel2.Controls.Add(txtNomeCliente, 1, 0);
            tableLayoutPanel2.Controls.Add(lblNomeCliente, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(321, 45);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // txtNomeCliente
            // 
            txtNomeCliente.Anchor = AnchorStyles.None;
            txtNomeCliente.BorderStyle = BorderStyle.FixedSingle;
            txtNomeCliente.Location = new Point(71, 11);
            txtNomeCliente.Name = "txtNomeCliente";
            txtNomeCliente.Size = new Size(242, 23);
            txtNomeCliente.TabIndex = 1;
            // 
            // lblNomeCliente
            // 
            lblNomeCliente.Anchor = AnchorStyles.Left;
            lblNomeCliente.AutoSize = true;
            lblNomeCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNomeCliente.Location = new Point(3, 15);
            lblNomeCliente.Margin = new Padding(3, 2, 3, 2);
            lblNomeCliente.Name = "lblNomeCliente";
            lblNomeCliente.Size = new Size(41, 15);
            lblNomeCliente.TabIndex = 0;
            lblNomeCliente.Text = "Nome";
            lblNomeCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlBevande
            // 
            pnlBevande.Controls.Add(btnAddBevande);
            pnlBevande.Controls.Add(flpBevande);
            pnlBevande.Controls.Add(lblBevande);
            pnlBevande.Dock = DockStyle.Fill;
            pnlBevande.Location = new Point(3, 182);
            pnlBevande.Margin = new Padding(3, 2, 3, 2);
            pnlBevande.Name = "pnlBevande";
            pnlBevande.Size = new Size(347, 248);
            pnlBevande.TabIndex = 1;
            // 
            // btnAddBevande
            // 
            btnAddBevande.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddBevande.Location = new Point(256, 5);
            btnAddBevande.Margin = new Padding(3, 2, 3, 2);
            btnAddBevande.Name = "btnAddBevande";
            btnAddBevande.Size = new Size(83, 22);
            btnAddBevande.TabIndex = 0;
            btnAddBevande.Text = "+ Aggiungi";
            btnAddBevande.UseVisualStyleBackColor = true;
            // 
            // flpBevande
            // 
            flpBevande.AutoScroll = true;
            flpBevande.Dock = DockStyle.Fill;
            flpBevande.FlowDirection = FlowDirection.TopDown;
            flpBevande.Location = new Point(0, 34);
            flpBevande.Margin = new Padding(3, 2, 3, 2);
            flpBevande.Name = "flpBevande";
            flpBevande.Size = new Size(347, 214);
            flpBevande.TabIndex = 3;
            flpBevande.WrapContents = false;
            // 
            // lblBevande
            // 
            lblBevande.Dock = DockStyle.Top;
            lblBevande.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBevande.Location = new Point(0, 0);
            lblBevande.Name = "lblBevande";
            lblBevande.Size = new Size(347, 34);
            lblBevande.TabIndex = 2;
            lblBevande.Text = "Bevande";
            lblBevande.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlDolci
            // 
            pnlDolci.Controls.Add(btnAddDolci);
            pnlDolci.Controls.Add(flpDolci);
            pnlDolci.Controls.Add(lblDolci);
            pnlDolci.Dock = DockStyle.Fill;
            pnlDolci.Location = new Point(3, 434);
            pnlDolci.Margin = new Padding(3, 2, 3, 2);
            pnlDolci.Name = "pnlDolci";
            pnlDolci.Size = new Size(347, 212);
            pnlDolci.TabIndex = 2;
            // 
            // btnAddDolci
            // 
            btnAddDolci.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddDolci.Location = new Point(256, 5);
            btnAddDolci.Margin = new Padding(3, 2, 3, 2);
            btnAddDolci.Name = "btnAddDolci";
            btnAddDolci.Size = new Size(83, 22);
            btnAddDolci.TabIndex = 0;
            btnAddDolci.Text = "+ Aggiungi";
            btnAddDolci.UseVisualStyleBackColor = true;
            // 
            // flpDolci
            // 
            flpDolci.AutoScroll = true;
            flpDolci.Dock = DockStyle.Fill;
            flpDolci.FlowDirection = FlowDirection.TopDown;
            flpDolci.Location = new Point(0, 34);
            flpDolci.Margin = new Padding(3, 2, 3, 2);
            flpDolci.Name = "flpDolci";
            flpDolci.Size = new Size(347, 178);
            flpDolci.TabIndex = 3;
            flpDolci.WrapContents = false;
            // 
            // lblDolci
            // 
            lblDolci.Dock = DockStyle.Top;
            lblDolci.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDolci.Location = new Point(0, 0);
            lblDolci.Name = "lblDolci";
            lblDolci.Size = new Size(347, 34);
            lblDolci.TabIndex = 2;
            lblDolci.Text = "Dolci";
            lblDolci.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlModStampa
            // 
            pnlModStampa.Controls.Add(btnStampa);
            pnlModStampa.Controls.Add(btnEliminaMod);
            pnlModStampa.Controls.Add(btnModEl);
            pnlModStampa.Controls.Add(lblTotale);
            pnlModStampa.Dock = DockStyle.Fill;
            pnlModStampa.Location = new Point(3, 650);
            pnlModStampa.Margin = new Padding(3, 2, 3, 2);
            pnlModStampa.Name = "pnlModStampa";
            pnlModStampa.Size = new Size(347, 68);
            pnlModStampa.TabIndex = 3;
            // 
            // btnStampa
            // 
            btnStampa.Anchor = AnchorStyles.Right;
            btnStampa.Location = new Point(256, 24);
            btnStampa.Margin = new Padding(3, 2, 3, 2);
            btnStampa.Name = "btnStampa";
            btnStampa.Size = new Size(83, 22);
            btnStampa.TabIndex = 3;
            btnStampa.Text = "Stampa";
            btnStampa.UseVisualStyleBackColor = true;
            // 
            // btnEliminaMod
            // 
            btnEliminaMod.Anchor = AnchorStyles.Right;
            btnEliminaMod.ForeColor = Color.Red;
            btnEliminaMod.Location = new Point(256, 24);
            btnEliminaMod.Margin = new Padding(3, 2, 3, 2);
            btnEliminaMod.Name = "btnEliminaMod";
            btnEliminaMod.Size = new Size(81, 22);
            btnEliminaMod.TabIndex = 1;
            btnEliminaMod.Text = "Annulla";
            btnEliminaMod.UseVisualStyleBackColor = true;
            btnEliminaMod.Click += btnEliminaMod_Click;
            // 
            // btnModEl
            // 
            btnModEl.Anchor = AnchorStyles.Left;
            btnModEl.Location = new Point(3, 24);
            btnModEl.Margin = new Padding(3, 2, 3, 2);
            btnModEl.Name = "btnModEl";
            btnModEl.Size = new Size(83, 22);
            btnModEl.TabIndex = 2;
            btnModEl.Text = "Salva";
            btnModEl.UseVisualStyleBackColor = true;
            // 
            // menuStripPrincipale
            // 
            menuStripPrincipale.ImageScalingSize = new Size(20, 20);
            menuStripPrincipale.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, gestioneMenùToolStripMenuItem });
            menuStripPrincipale.Location = new Point(0, 0);
            menuStripPrincipale.Name = "menuStripPrincipale";
            menuStripPrincipale.Padding = new Padding(5, 2, 0, 2);
            menuStripPrincipale.Size = new Size(1505, 24);
            menuStripPrincipale.TabIndex = 4;
            menuStripPrincipale.Text = "menuStrip1";
            menuStripPrincipale.ItemClicked += menuStripPrincipale_ItemClicked;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resocontoFinaleToolStripMenuItem, tabellaOrdiniToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // resocontoFinaleToolStripMenuItem
            // 
            resocontoFinaleToolStripMenuItem.Name = "resocontoFinaleToolStripMenuItem";
            resocontoFinaleToolStripMenuItem.Size = new Size(164, 22);
            resocontoFinaleToolStripMenuItem.Text = "Resoconto Finale";
            resocontoFinaleToolStripMenuItem.Click += resocontoFinaleToolStripMenuItem_Click;
            // 
            // tabellaOrdiniToolStripMenuItem
            // 
            tabellaOrdiniToolStripMenuItem.Name = "tabellaOrdiniToolStripMenuItem";
            tabellaOrdiniToolStripMenuItem.Size = new Size(164, 22);
            tabellaOrdiniToolStripMenuItem.Text = "tabella ordini";
            tabellaOrdiniToolStripMenuItem.Click += tabellaOrdiniToolStripMenuItem_Click;
            // 
            // gestioneMenùToolStripMenuItem
            // 
            gestioneMenùToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { modificaToolStripMenuItem, modificaIngredientiToolStripMenuItem });
            gestioneMenùToolStripMenuItem.Name = "gestioneMenùToolStripMenuItem";
            gestioneMenùToolStripMenuItem.Size = new Size(99, 20);
            gestioneMenùToolStripMenuItem.Text = "Gestione Menù";
            // 
            // modificaToolStripMenuItem
            // 
            modificaToolStripMenuItem.Name = "modificaToolStripMenuItem";
            modificaToolStripMenuItem.Size = new Size(181, 22);
            modificaToolStripMenuItem.Text = "Modifica";
            modificaToolStripMenuItem.Click += ModificaToolStripMenuItem_Click;
            // 
            // modificaIngredientiToolStripMenuItem
            // 
            modificaIngredientiToolStripMenuItem.Name = "modificaIngredientiToolStripMenuItem";
            modificaIngredientiToolStripMenuItem.Size = new Size(181, 22);
            modificaIngredientiToolStripMenuItem.Text = "Modifica Ingredienti";
            modificaIngredientiToolStripMenuItem.Click += modificaIngredientiToolStripMenuItem_Click;
            // 
            // Pizzeria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 248, 252);
            ClientSize = new Size(1505, 809);
            Controls.Add(tlpPrincipale);
            Controls.Add(dgvPizze);
            Controls.Add(menuStripPrincipale);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(877, 495);
            Name = "Pizzeria";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pizzeria";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPizze).EndInit();
            tlpPrincipale.ResumeLayout(false);
            pnlPizze.ResumeLayout(false);
            pnlExtra.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            pnlCoperto.ResumeLayout(false);
            tlpCoperto.ResumeLayout(false);
            tlpCoperto.PerformLayout();
            pnlNomeCliente.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            pnlBevande.ResumeLayout(false);
            pnlDolci.ResumeLayout(false);
            pnlModStampa.ResumeLayout(false);
            pnlModStampa.PerformLayout();
            menuStripPrincipale.ResumeLayout(false);
            menuStripPrincipale.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPizze;
        private Label lblTotale;
        private TableLayoutPanel tlpPrincipale;
        private Panel pnlPizze;
        private Panel pnlExtra;
        private FlowLayoutPanel flpPizze;
        private Label lblTitoloPizze;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlCoperto;
        private Label lblCoperto;
        private Panel pnlBevande;
        private Panel pnlDolci;
        private FlowLayoutPanel flpCoperto;
        private FlowLayoutPanel flpBevande;
        private Label lblBevande;
        private FlowLayoutPanel flpDolci;
        private Label lblDolci;
        private MenuStrip menuStripPrincipale;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem gestioneMenùToolStripMenuItem;
        private ToolStripMenuItem modificaToolStripMenuItem;
        private Button btnAnnullaMod;
        private Button btnAddPizze;
        private Button btnEliminaMod;
        private Button btnModEl;
        private Button btnAddBevande;
        private Button btnAddDolci;
        private Button btnStampa;
        private Panel pnlModStampa;
        private TableLayoutPanel tlpCoperto;
        private Panel pnlNomeCliente;
        private Label lblNomeCliente;
        private TextBox txtNomeCliente;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Button btnPersonalizza;
        private ToolStripMenuItem modificaIngredientiToolStripMenuItem;
        private ToolStripMenuItem resocontoFinaleToolStripMenuItem;
        private ToolStripMenuItem tabellaOrdiniToolStripMenuItem;
    }
}
