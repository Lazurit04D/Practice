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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            resources.ApplyResources(this.btnLoadExcel, "btnLoadExcel");
            this.btnLoadExcel.Name = "btnLoadExcel";
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
            resources.ApplyResources(this.cbReportType, "cbReportType");
            this.cbReportType.Items.AddRange(new object[] {
            resources.GetString("cbReportType.Items"),
            resources.GetString("cbReportType.Items1"),
            resources.GetString("cbReportType.Items2"),
            resources.GetString("cbReportType.Items3"),
            resources.GetString("cbReportType.Items4"),
            resources.GetString("cbReportType.Items5")});
            this.cbReportType.Name = "cbReportType";
            // 
            // lvResults
            // 
            resources.ApplyResources(this.lvResults, "lvResults");
            this.lvResults.BackColor = System.Drawing.Color.White;
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvResults.ForeColor = System.Drawing.Color.Black;
            this.lvResults.FullRowSelect = true;
            this.lvResults.GridLines = true;
            this.lvResults.HideSelection = false;
            this.lvResults.Name = "lvResults";
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // btnToggleTheme
            // 
            this.btnToggleTheme.BackColor = System.Drawing.Color.White;
            this.btnToggleTheme.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnToggleTheme, "btnToggleTheme");
            this.btnToggleTheme.ForeColor = System.Drawing.Color.Black;
            this.btnToggleTheme.Name = "btnToggleTheme";
            this.btnToggleTheme.UseVisualStyleBackColor = false;
            this.btnToggleTheme.Click += new System.EventHandler(this.btnToggleTheme_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnToggleTheme);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.cbReportType);
            this.Controls.Add(this.btnLoadExcel);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "MainForm";
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

