namespace Prenotazione_PIzze
{
    partial class GestioneIngredienti
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            lblTitolo = new Label();
            flpIngredienti = new FlowLayoutPanel();
            panel1 = new Panel();
            btnChiudi = new Button();
            btnNew = new Button();
            txtNuovoIngrediente = new TextBox();
            lblPrezzoIng = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblTitolo, 0, 0);
            tableLayoutPanel1.Controls.Add(flpIngredienti, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.Size = new Size(484, 511);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // lblTitolo
            // 
            lblTitolo.AutoSize = true;
            lblTitolo.Dock = DockStyle.Fill;
            lblTitolo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitolo.Location = new Point(3, 0);
            lblTitolo.Name = "lblTitolo";
            lblTitolo.Size = new Size(478, 40);
            lblTitolo.TabIndex = 0;
            lblTitolo.Text = "Gestione degli Ingredienti";
            lblTitolo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flpIngredienti
            // 
            flpIngredienti.Dock = DockStyle.Fill;
            flpIngredienti.Location = new Point(3, 43);
            flpIngredienti.Name = "flpIngredienti";
            flpIngredienti.Size = new Size(478, 402);
            flpIngredienti.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnChiudi);
            panel1.Controls.Add(btnNew);
            panel1.Controls.Add(txtNuovoIngrediente);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 451);
            panel1.Name = "panel1";
            panel1.Size = new Size(478, 57);
            panel1.TabIndex = 2;
            // 
            // btnChiudi
            // 
            btnChiudi.Location = new Point(394, 25);
            btnChiudi.Name = "btnChiudi";
            btnChiudi.Size = new Size(75, 23);
            btnChiudi.TabIndex = 2;
            btnChiudi.Text = "Chiudi";
            btnChiudi.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(249, 25);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(75, 23);
            btnNew.TabIndex = 1;
            btnNew.Text = "Aggiungi +";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // txtNuovoIngrediente
            // 
            txtNuovoIngrediente.Location = new Point(9, 25);
            txtNuovoIngrediente.Name = "txtNuovoIngrediente";
            txtNuovoIngrediente.Size = new Size(234, 23);
            txtNuovoIngrediente.TabIndex = 0;
            // 
            // lblPrezzoIng
            // 
            lblPrezzoIng.Location = new Point(0, 0);
            lblPrezzoIng.Name = "lblPrezzoIng";
            lblPrezzoIng.Size = new Size(100, 23);
            lblPrezzoIng.TabIndex = 0;
            // 
            // GestioneIngredienti
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 511);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GestioneIngredienti";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestione Ingredienti";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ListBox listBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTitolo;
        private FlowLayoutPanel flpIngredienti;
        private Panel panel1;
        private Button btnChiudi;
        private Button btnNew;
        private TextBox txtNuovoIngrediente;
        private Label lblPrezzoIng;
        private Label lblNomeIng;
    }
}