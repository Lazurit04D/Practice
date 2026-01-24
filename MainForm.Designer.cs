namespace Practice
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoadExcel = new System.Windows.Forms.Button();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.lvResults = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnToggleTheme = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.BackColor = System.Drawing.Color.White;
            this.btnLoadExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadExcel.ForeColor = System.Drawing.Color.Black;
            this.btnLoadExcel.Location = new System.Drawing.Point(10, 12);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Size = new System.Drawing.Size(430, 61);
            this.btnLoadExcel.TabIndex = 0;
            this.btnLoadExcel.Text = "Загрузить .xlsx и создать отчёт";
            this.btnLoadExcel.UseVisualStyleBackColor = false;
            this.btnLoadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
            // 
            // cbReportType
            // 
            this.cbReportType.BackColor = System.Drawing.Color.White;
            this.cbReportType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.ForeColor = System.Drawing.Color.Black;
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "1 — Отчёт по расписанию",
            "2 — Отчёт по темам занятий",
            "3 — Отчёт по студентам",
            "4 — Отчёт по посещаемости",
            "5 — Отчёт по проверенным ДЗ",
            "6 — Отчёт по сданным ДЗ"});
            this.cbReportType.Location = new System.Drawing.Point(446, 18);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(420, 37);
            this.cbReportType.TabIndex = 2;
            // 
            // lvResults
            // 
            this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvResults.BackColor = System.Drawing.Color.White;
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvResults.ForeColor = System.Drawing.Color.Black;
            this.lvResults.FullRowSelect = true;
            this.lvResults.GridLines = true;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(12, 79);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(918, 617);
            this.lvResults.TabIndex = 3;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Результат";
            this.columnHeader1.Width = 1000;
            // 
            // btnToggleTheme
            // 
            this.btnToggleTheme.BackColor = System.Drawing.Color.White;
            this.btnToggleTheme.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleTheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnToggleTheme.ForeColor = System.Drawing.Color.Black;
            this.btnToggleTheme.Location = new System.Drawing.Point(874, 12);
            this.btnToggleTheme.Name = "btnToggleTheme";
            this.btnToggleTheme.Size = new System.Drawing.Size(61, 61);
            this.btnToggleTheme.TabIndex = 4;
            this.btnToggleTheme.Text = "🌙";
            this.btnToggleTheme.UseVisualStyleBackColor = false;
            this.btnToggleTheme.Click += new System.EventHandler(this.btnToggleTheme_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(936, 671);
            this.Controls.Add(this.btnToggleTheme);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.cbReportType);
            this.Controls.Add(this.btnLoadExcel);
            this.ForeColor = System.Drawing.Color.Black;
            this.MinimumSize = new System.Drawing.Size(964, 750);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Практика - отчёты";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadExcel;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnToggleTheme;
    }
}

