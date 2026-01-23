using System;
using System.IO;
using System.Data;
using System.Linq;
using NPOI.SS.UserModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Practice
{
    public partial class MainForm : Form
    {
        private DataSet excelData;

        public MainForm()
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
                        case 2:
                            ShowLessonTopicsReport();
                            break;
                        case 3:
                            ShowStudentsReport();
                            break;
                        case 4:
                            ShowAttendanceReport();
                            break;
                        case 5:
                            ShowHomeworkCheckedReport();
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
            Cursor = Cursors.WaitCursor;

            var ds = new DataSet();
            IWorkbook workbook;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = WorkbookFactory.Create(fs);

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

            Cursor = Cursors.Default;

            return ds;
        }

        private void ShowScheduleReport()
        {
            Cursor = Cursors.WaitCursor;

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

            if (counts.Keys.Count == 0)
            {
                MessageBox.Show("В файле отсутствуют необходимые ключевые слова (Предмет:).");
            }
            else
            {
                foreach (var kv in counts.OrderByDescending(k => k.Value))
                {
                    lvResults.Items.Add(new ListViewItem($"{kv.Key} - {kv.Value} пар"));
                }
            }

            Cursor = Cursors.Default;
        }

        private void ShowLessonTopicsReport()
        {
            Cursor = Cursors.WaitCursor;

            var dt = excelData.Tables[0];

            int themeColumnIndex = dt.Columns.IndexOf("Тема урока");

            if (themeColumnIndex == -1)
            {
                MessageBox.Show("В файле отсутствует необходимый столбец (Тема урока).");
                Cursor = Cursors.Default;
                return;
            }

            lvResults.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string cell = row[themeColumnIndex]?.ToString()?.Trim();
                if (string.IsNullOrEmpty(cell))
                {
                    continue;
                }

                if (!IsValidLessonTheme(cell))
                {
                    lvResults.Items.Add(new ListViewItem(cell));
                }
            }

            if (lvResults.Items.Count == 0)
            {
                lvResults.Items.Add(new ListViewItem("Все темы соответствуют формату."));
            }

            Cursor = Cursors.Default;
        }

        private bool IsValidLessonTheme(string text)
        {
            if (!text.StartsWith("Урок №"))
            {
                return false;
            }

            int dotIndex = text.IndexOf('.');
            if (dotIndex < 0)
            {
                return false;
            }

            string afterDot = text.Substring(dotIndex + 1).Trim();
            if (!afterDot.StartsWith("Тема:"))
            {
                return false;
            }

            return true;
        }

        private void ShowStudentsReport()
        {
            Cursor = Cursors.WaitCursor;

            var dt = excelData.Tables[0];

            int fioCol = dt.Columns.IndexOf("FIO");
            int homeworkCol = dt.Columns.IndexOf("Homework");
            int classroomCol = dt.Columns.IndexOf("Classroom");

            if (fioCol == -1 || homeworkCol == -1 || classroomCol == -1)
            {
                MessageBox.Show("В файле отсутствуют необходимые столбцы (FIO, Homework, Classroom).");
                Cursor = Cursors.Default;
                return;
            }

            lvResults.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fio = row[fioCol]?.ToString()?.Trim();

                if (!double.TryParse(row[homeworkCol]?.ToString(), out double homework))
                {
                    continue;
                }

                if (!double.TryParse(row[classroomCol]?.ToString(), out double classroom))
                {
                    continue;
                }

                if (homework < 1 && classroom < 3)
                {
                    lvResults.Items.Add(new ListViewItem(fio));
                }
            }

            if (lvResults.Items.Count == 0)
            {
                lvResults.Items.Add(new ListViewItem("Подходящих студентов не найдено."));
            }

            Cursor = Cursors.Default;
        }

        private void ShowAttendanceReport()
        {
            Cursor = Cursors.WaitCursor;

            var dt = excelData.Tables[0];

            int fioCol = dt.Columns.IndexOf("ФИО преподавателя");
            int attendanceCol = dt.Columns.IndexOf("Средняя посещаемость");

            if (fioCol == -1 || attendanceCol == -1)
            {
                MessageBox.Show("В файле отсутствуют необходимые столбцы (ФИО преподавателя, Средняя посещаемость).");
                Cursor = Cursors.Default;
                return;
            }

            lvResults.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string teacher = row[fioCol]?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(teacher))
                {
                    continue;
                }

                string raw = row[attendanceCol]?.ToString()?.Trim();
                if (string.IsNullOrEmpty(raw))
                {
                    continue;
                }

                string norm = raw.Replace("%", "").Replace(",", ".").Trim();
                if (!double.TryParse(norm, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val))
                {
                    continue;
                }

                if (val <= 1)
                {
                    val *= 100;
                }

                if (val < 40)
                {
                    lvResults.Items.Add(new ListViewItem($"{teacher} - {val:0.##}%"));
                }
            }

            if (lvResults.Items.Count == 0)
            {
                lvResults.Items.Add(new ListViewItem("Подходящих преподавателей не найдено."));
            }

            Cursor = Cursors.Default;
        }

        private void ShowHomeworkCheckedReport()
        {
            Cursor = Cursors.WaitCursor;

            var dt = excelData.Tables[0];

            if (dt.Rows.Count < 2)
            {
                MessageBox.Show("Неподходящая структура файла (ожидаются две строки шапки).");
                Cursor = Cursors.Default;
                return;
            }

            int cols = dt.Columns.Count;
            string[] topHeader = new string[cols];
            string lastTop = "";

            for (int i = 0; i < cols; i++)
            {
                string t = dt.Columns[i].ColumnName?.Trim() ?? "";
                if (!string.IsNullOrEmpty(t) && !t.StartsWith("Column", StringComparison.OrdinalIgnoreCase))
                {
                    lastTop = t;
                }
                topHeader[i] = lastTop;
            }

            DataRow lowerHeaderRow = dt.Rows[0];

            int teacherCol = -1;
            for (int i = 0; i < cols; i++)
            {
                string lower = (lowerHeaderRow[i]?.ToString() ?? "").Trim();
                string top = (topHeader[i] ?? "").Trim();
                if (string.Equals(lower, "ФИО преподавателя", StringComparison.OrdinalIgnoreCase) || string.Equals(dt.Columns[i].ColumnName?.Trim(), "ФИО преподавателя", StringComparison.OrdinalIgnoreCase) || top.Equals("ФИО преподавателя", StringComparison.OrdinalIgnoreCase))
                {
                    teacherCol = i;
                    break;
                }
            }

            int receivedCol = -1;
            int checkedCol = -1;

            for (int i = 0; i < cols; i++)
            {
                string top = (topHeader[i] ?? "").Trim();
                string lower = (lowerHeaderRow[i]?.ToString() ?? "").Trim();

                if (top.Equals("Месяц", StringComparison.OrdinalIgnoreCase))
                {
                    if (lower.StartsWith("Получено", StringComparison.OrdinalIgnoreCase))
                    {
                        receivedCol = i;
                    }

                    if (lower.StartsWith("Проверено", StringComparison.OrdinalIgnoreCase))
                    {
                        checkedCol = i;
                    }
                }
            }

            if (receivedCol == -1 || checkedCol == -1)
            {
                MessageBox.Show("В файле отсутствуют необходимые столбцы (Получено, Проверено).");
                Cursor = Cursors.Default;
                return;
            }

            lvResults.Items.Clear();

            for (int r = 1; r < dt.Rows.Count; r++)
            {
                var row = dt.Rows[r];
                string teacher = row[teacherCol]?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(teacher))
                {
                    continue;
                }

                string rawReceived = row[receivedCol]?.ToString()?.Trim();
                string rawChecked = row[checkedCol]?.ToString()?.Trim();
                if (string.IsNullOrEmpty(rawReceived) || string.IsNullOrEmpty(rawChecked))
                {
                    continue;
                }

                string normReceived = rawReceived.Replace("%", "").Replace(",", ".").Trim();
                string normChecked = rawChecked.Replace("%", "").Replace(",", ".").Trim();
                if (!double.TryParse(normReceived, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double receivedHw))
                {
                    continue;
                }

                if (!double.TryParse(normChecked, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double checkedHw))
                {
                    continue;

                }

                if (receivedHw <= 0)
                {
                    continue;
                }

                double percent = checkedHw / receivedHw * 100.0;

                if (percent < 70.0)
                {
                    lvResults.Items.Add(new ListViewItem($"{teacher} - {percent:0.##}%"));
                }
            }

            if (lvResults.Items.Count == 0)
            {
                lvResults.Items.Add(new ListViewItem("Подходящих преподавателей не найдено."));
            }

            Cursor = Cursors.Default;
        }
    }
}