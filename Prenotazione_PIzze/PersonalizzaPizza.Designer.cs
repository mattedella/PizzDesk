namespace Prenotazione_PIzze
{
    partial class PersonalizzaPizza
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
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            pnlPersonalizza = new Panel();
            txtPrezzo = new TextBox();
            lblPrezzo = new Label();
            btnFine = new Button();
            clbIngredienti = new CheckedListBox();
            lblScegliIng = new Label();
            btnNext = new Button();
            clbBase = new CheckedListBox();
            lblSegliBase = new Label();
            pnlPersonalizza.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // pnlPersonalizza
            // 
            pnlPersonalizza.BackColor = Color.White;
            pnlPersonalizza.Controls.Add(txtPrezzo);
            pnlPersonalizza.Controls.Add(lblPrezzo);
            pnlPersonalizza.Controls.Add(btnFine);
            pnlPersonalizza.Controls.Add(clbIngredienti);
            pnlPersonalizza.Controls.Add(lblScegliIng);
            pnlPersonalizza.Controls.Add(btnNext);
            pnlPersonalizza.Controls.Add(clbBase);
            pnlPersonalizza.Controls.Add(lblSegliBase);
            pnlPersonalizza.Dock = DockStyle.Fill;
            pnlPersonalizza.Location = new Point(0, 0);
            pnlPersonalizza.Margin = new Padding(3, 2, 3, 2);
            pnlPersonalizza.Name = "pnlPersonalizza";
            pnlPersonalizza.Size = new Size(584, 561);
            pnlPersonalizza.TabIndex = 1;
            // 
            // txtPrezzo
            // 
            txtPrezzo.Location = new Point(472, 488);
            txtPrezzo.Name = "txtPrezzo";
            txtPrezzo.Size = new Size(100, 23);
            txtPrezzo.TabIndex = 7;
            txtPrezzo.Visible = false;
            // 
            // lblPrezzo
            // 
            lblPrezzo.AutoSize = true;
            lblPrezzo.Location = new Point(338, 491);
            lblPrezzo.Name = "lblPrezzo";
            lblPrezzo.Size = new Size(98, 15);
            lblPrezzo.TabIndex = 6;
            lblPrezzo.Text = "Inserisci prezzo: €";
            lblPrezzo.Visible = false;
            lblPrezzo.Click += label1_Click;
            // 
            // btnFine
            // 
            btnFine.Location = new Point(490, 516);
            btnFine.Margin = new Padding(3, 2, 3, 2);
            btnFine.Name = "btnFine";
            btnFine.Size = new Size(82, 22);
            btnFine.TabIndex = 5;
            btnFine.Text = "Aggiungi";
            btnFine.UseVisualStyleBackColor = true;
            btnFine.Visible = false;
            btnFine.Click += btnFine_Click;
            // 
            // clbIngredienti
            // 
            clbIngredienti.BorderStyle = BorderStyle.None;
            clbIngredienti.FormattingEnabled = true;
            clbIngredienti.Location = new Point(33, 50);
            clbIngredienti.Margin = new Padding(3, 2, 3, 2);
            clbIngredienti.Name = "clbIngredienti";
            clbIngredienti.RightToLeft = RightToLeft.No;
            clbIngredienti.Size = new Size(527, 396);
            clbIngredienti.TabIndex = 4;
            clbIngredienti.Visible = false;
            // 
            // lblScegliIng
            // 
            lblScegliIng.AutoSize = true;
            lblScegliIng.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblScegliIng.Location = new Point(10, 9);
            lblScegliIng.Name = "lblScegliIng";
            lblScegliIng.Size = new Size(173, 21);
            lblScegliIng.TabIndex = 3;
            lblScegliIng.Text = "Seleziona Ingredienti";
            lblScegliIng.Visible = false;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(490, 516);
            btnNext.Margin = new Padding(3, 2, 3, 2);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(82, 22);
            btnNext.TabIndex = 2;
            btnNext.Text = "Avanti ->";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // clbBase
            // 
            clbBase.BorderStyle = BorderStyle.None;
            clbBase.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clbBase.FormattingEnabled = true;
            clbBase.Location = new Point(33, 50);
            clbBase.Margin = new Padding(3, 2, 3, 2);
            clbBase.Name = "clbBase";
            clbBase.Size = new Size(527, 396);
            clbBase.TabIndex = 1;
            // 
            // lblSegliBase
            // 
            lblSegliBase.AutoSize = true;
            lblSegliBase.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSegliBase.Location = new Point(10, 9);
            lblSegliBase.Name = "lblSegliBase";
            lblSegliBase.Size = new Size(87, 21);
            lblSegliBase.TabIndex = 0;
            lblSegliBase.Text = "Segli Base";
            lblSegliBase.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PersonalizzaPizza
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 561);
            Controls.Add(pnlPersonalizza);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "PersonalizzaPizza";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Personalizza Pizza";
            pnlPersonalizza.ResumeLayout(false);
            pnlPersonalizza.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ContextMenuStrip contextMenuStrip1;
        private Panel pnlPersonalizza;
        private Label lblSegliBase;
        private Button btnNext;
        private CheckedListBox clbBase;
        private CheckedListBox clbIngredienti;
        private Label lblScegliIng;
        private Button btnFine;
        private Label lblPrezzo;
        private TextBox txtPrezzo;
    }
}