using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using net.mvc.fuse8_task.Models;

namespace net.mvc.fuse8_task.Controllers
{
    public class ReportController : Controller
    {
        public Cell CreateCell(string value, string type)
        {
            DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();

            if(type == "string")
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            else if (type == "int")
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
            else if (type == "date")
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Date;

            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value);

            return cell;
        }

        private void ExportModelToExcel(IndexViewModel model, string destination)
        {
            using (
                var workbook = SpreadsheetDocument.Create(destination,
                    DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = workbook.AddWorkbookPart();

                workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
                workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();

                workbook.WorkbookPart.Workbook.CalculationProperties = new CalculationProperties
                {
                    ForceFullCalculation = true,
                    FullCalculationOnLoad = true
                };

                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                DocumentFormat.OpenXml.Spreadsheet.Sheets sheets =
                    workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                uint sheetId = 1;
                if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
                {
                    sheetId =
                        sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>()
                            .Select(s => s.SheetId.Value)
                            .Max() + 1;
                }

                DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = relationshipId,
                    SheetId = sheetId,
                    Name = "Report"
                };
                sheets.Append(sheet);

                DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                List<String> columns = new List<string>();
                int index = 0;
                foreach (var column in model.TableHead)
                {
                    columns.Add(index.ToString());
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column);
                    headerRow.AppendChild(cell);
                    index++;
                }

                // добавляем вычисляемый столбец
                columns.Add(index.ToString());
                var cel = CreateCell("Формула (цена за единицу)", "string");
                headerRow.AppendChild(cel);

                sheetData.AppendChild(headerRow);

                index = 2;
                foreach (var row in model.Products)
                {
                    DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    var cel1 = CreateCell(row.Order.ID.ToString(), "string");
                    newRow.AppendChild(cel1);

                    var cel2 = CreateCell(row.Order.OrderDate.ToString(), "string");
                    newRow.AppendChild(cel2);

                    var cel3 = CreateCell(row.Product.ID.ToString(), "string");
                    newRow.AppendChild(cel3);

                    var cel4 = CreateCell(row.Product.Name.ToString(), "string");
                    newRow.AppendChild(cel4);

                    var cel5 = CreateCell(row.OrderDetail.Quantity.ToString(), "string");
                    newRow.AppendChild(cel5);

                    var cel6 = CreateCell(row.OrderDetail.UnitPrice.ToString(), "string");
                    newRow.AppendChild(cel6);

                    // добавляем вычисляемый столбец
                    var cel7 = new Cell();
                    CellFormula cellformula = new CellFormula { Text = $"=F{index}/E{index}"};
                    cel7.Append(cellformula);
                    newRow.AppendChild(cel7);

                    sheetData.AppendChild(newRow);
                    index++;
                }
            }
        }

        // формирует excel-файл в папку App_Data
        public JsonResult ExportExcel()
        {
            try
            {
                var hc = new HomeController();
                IndexViewModel data;

                if (Session["startDateTxt"] != null && Session["endDateTxt"] != null)
                    data = hc.GetIntervalResult(Session["startDateTxt"] as string, Session["endDateTxt"] as string);
                else
                    data = hc.GetDefaultResult();

                ExportModelToExcel(data, data.ExcelFilename);
                return Json($"Отчет сохранен как {data.ExcelFilename}");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult SendEmail(string to, string from, string pass, string smtpHost)
        {
            try
            {
                // сохраняем в Excel
                var indexViewModel = new IndexViewModel();
                ExportExcel();

                // отправляем по почте
                using (MailMessage mail = new MailMessage(from, to))
                {
                    mail.Subject = "Новый отчет";
                    mail.Body = "Отчет в прикрепленном файле";
                    mail.Attachments.Add(new Attachment(indexViewModel.ExcelFilename));

                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = smtpHost,
                        Port = 587,
                        EnableSsl = true,
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential
                            (from, pass)
                    };

                    smtp.Send(mail);
                    return Json("Письмо успешно отправлено");
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
    }
}