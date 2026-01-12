using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Practice
{
    public partial class Form1 : Form
    {
        private DataSet excelData;

        public Form1()
        {
            InitializeComponent();
            cbReportType.SelectedIndex = 0;
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel файлы|*.xls;*.xlsx";

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    excelData = ReadExcelFile(ofd.FileName);
                    MessageBox.Show("Файл загружен. Листов: " + excelData.Tables.Count);

                    if (excelData == null || excelData.Tables.Count == 0)
                    {
                        MessageBox.Show("Файл пустой или повреждён.");
                        return;
                    }

                    switch (cbReportType.SelectedIndex + 1)
                    {
                        case 1:
                            ShowScheduleReport();
                            break;
                        default:
                            MessageBox.Show("Отчёт пока не реализован.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при чтении Excel: " + ex.Message);
                }
            }
        }

        private DataSet ReadExcelFile(string path)
        {
            var ds = new DataSet();
            IWorkbook workbook;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(path).ToLower() == ".xls")
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    workbook = new XSSFWorkbook(fs);
                }

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    var sheet = workbook.GetSheetAt(i);
                    var dt = new DataTable(sheet.SheetName);

                    var headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;

                    for (int j = 0; j < cellCount; j++)
                    {
                        string colName = headerRow.GetCell(j)?.ToString() ?? $"Column{j}";
                        int dup = 1;
                        string baseName = colName;

                        while (dt.Columns.Contains(colName))
                        {
                            colName = $"{baseName}_{dup}";
                            dup++;
                        }

                        dt.Columns.Add(colName);
                    }

                    for (int r = 1; r <= sheet.LastRowNum; r++)
                    {
                        var row = sheet.GetRow(r);
                        if (row == null)
                        {
                            continue;
                        }

                        var dr = dt.NewRow();
                        for (int c = 0; c < cellCount; c++)
                        {
                            dr[c] = row.GetCell(c)?.ToString() ?? "";
                        }

                        dt.Rows.Add(dr);
                    }

                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        private void ShowScheduleReport()
        {
            var dt = excelData.Tables[0];

            var counts = new Dictionary<string, int>();

            foreach (DataRow row in dt.Rows)
            {
                for (int c = 2; c < dt.Columns.Count; c++)
                {
                    string cell = row[c]?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(cell))
                    {
                        continue;
                    }

                    string prefix = "Предмет:";
                    int idx = cell.IndexOf(prefix);
                    if (idx < 0)
                    {
                        continue;
                    }

                    string discipline = cell.Substring(idx + prefix.Length).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                    if (string.IsNullOrEmpty(discipline))
                    {
                        continue;
                    }

                    if (!counts.ContainsKey(discipline))
                    {
                        counts[discipline] = 0;
                    }
                    counts[discipline]++;
                }
            }

            lvResults.Items.Clear();
            foreach (var kv in counts.OrderByDescending(k => k.Value))
            {
                lvResults.Items.Add(new ListViewItem($"{kv.Key} - {kv.Value} пар"));
            }
        }
    }
}