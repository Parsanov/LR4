using ClosedXML.Excel;

namespace LR4.Application.Services
{
    /// <summary>
    /// Сервіс збереження даних у файл Excel.
    /// Відповідає за Single Responsibility — лише запис у книгу Excel.
    /// </summary>
    public class ExcelExportService
    {
        /// <summary>
        /// Формує книгу Excel і повертає байти файлу.
        /// </summary>
        /// <param name="title">Заголовок звіту (рядки розділяти \n)</param>
        /// <param name="header">Заголовки стовпців таблиці</param>
        /// <param name="data">Рядки таблиці — кожен рядок є списком рядкових значень</param>
        /// <param name="footer">Підпис звіту</param>
        /// <returns>Байти .xlsx файлу</returns>
        public byte[] SaveToExcel(
            string title,
            List<string> header,
            List<List<string>> data,
            string footer)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Звіт");

            int colCount = header.Count;
            int currentRow = 1;

            // --- Заголовок звіту ---
            var titleCell = ws.Cell(currentRow, 1);
            titleCell.Value = title;
            titleCell.Style.Font.Bold = true;
            titleCell.Style.Font.FontSize = 14;
            titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            titleCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            titleCell.Style.Alignment.WrapText = true;
            ws.Range(currentRow, 1, currentRow, colCount).Merge();
            ws.Row(currentRow).Height = 30;
            currentRow++;

            // --- Заголовки таблиці ---
            for (int c = 0; c < colCount; c++)
            {
                var cell = ws.Cell(currentRow, c + 1);
                cell.Value = header[c];
                cell.Style.Font.Bold = true;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Fill.BackgroundColor = XLColor.LightBlue;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            currentRow++;

            // --- Дані таблиці ---
            foreach (var row in data)
            {
                for (int c = 0; c < row.Count; c++)
                {
                    var cell = ws.Cell(currentRow, c + 1);
                    var raw = row[c];

                    // Визначаємо тип і записуємо відповідно
                    if (DateTime.TryParseExact(raw, "dd.MM.yyyy HH:mm",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out var dt))
                    {
                        cell.Value = dt;
                        cell.Style.DateFormat.Format = "dd.MM.yyyy HH:mm";
                    }
                    else if (DateOnly.TryParseExact(raw, "dd.MM.yyyy",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out var d))
                    {
                        cell.Value = d.ToDateTime(TimeOnly.MinValue);
                        cell.Style.DateFormat.Format = "dd.MM.yyyy";
                    }
                    else if (double.TryParse(raw,
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out var num))
                    {
                        cell.Value = num;
                    }
                    else
                    {
                        cell.Value = raw;
                    }

                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }
                currentRow++;
            }

            // --- Підпис ---
            var footerCell = ws.Cell(currentRow, 1);
            footerCell.Value = footer;
            footerCell.Style.Alignment.WrapText = true;
            ws.Range(currentRow, 1, currentRow, colCount).Merge();

            // --- Авторозмір стовпців ---
            ws.Columns().AdjustToContents();

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms.ToArray();
        }
    }
}
