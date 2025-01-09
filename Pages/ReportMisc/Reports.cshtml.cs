using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniProiect2.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;

namespace miniProiect2.Pages.ReportMisc
{
    public class IndexModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public List<Gestion> Gestions { get; set; }
        public DataTable ReportData { get; set; }

        [BindProperty]
        public string ReportType { get; set; }

        [BindProperty]
        public DateTime DateStart { get; set; }

        [BindProperty]
        public DateTime DateEnd { get; set; }

        [BindProperty]
        public string GestionFilter { get; set; }

        [BindProperty]
        public int? GestionId { get; set; }

        [BindProperty]
        public bool IsListSelected { get; set; } = false!;

        [BindProperty]
        public string GroupMethod { get; set; }

        public IndexModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            DateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateEnd = DateTime.Now.Date;
            if (ReportType == "Intrari")
            {
                Gestions = (from e in _context.Entries
                            join g in _context.Gestions on e.GestionId equals g.Id
                            select g).ToList();
            }
            else //iesiri
            {
                Gestions = (from e in _context.Exits
                            join g in _context.Gestions on e.GestionId equals g.Id
                            select g).ToList();
            }

        }

        public async Task<IActionResult> OnPost(string action)
        {
            Gestions = _context.Gestions.ToList();

            if (action == "Accepta")
            {
                var ReportData = GenerateReport();
                await _context.Reports.AddRangeAsync(ReportData);
                await _context.SaveChangesAsync();

            }
            else if(action == "Listeaza")
            {
                IsListSelected = true;
            }
            else if(action == "Finalizare Listare")
            {
                return ListReport();
            }
            return Page();
        }

        private DataTable ToDataTable<T>(List<T> data)
        {
            var table = new DataTable();
            foreach (var prop in typeof(T).GetProperties())
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (var prop in typeof(T).GetProperties())
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private List<Models.Report> GenerateReport()
        {
            List<Models.Report> reportData;

            if (ReportType == "Intrari")
            {
                //reportData = _context.Entries
                //    .Where(e => e.Date >= DateStart && e.Date <= DateEnd)
                //    .Select(e => new Models.Report
                //    {
                //        Date = e.Date,
                //        Gestion = e.Gestion.Name,
                //        GestionId = (int)e.GestionId
                //    }).ToList();
                reportData = (from e in _context.Entries
                             join g in _context.Gestions on e.GestionId equals g.Id
                             where e.Date >= DateStart && e.Date <= DateEnd
                             select new Models.Report
                             {
                                 Date = e.Date,
                                 Gestion = g.Name,
                                 GestionId = (int)e.GestionId,
                                 StartDate = DateStart,
                                 EndDate = DateEnd
                             }).ToList();
            }
            else
            {
                //reportData = _context.Exits
                //    .Where(e => e.Date >= DateStart && e.Date <= DateEnd)
                //    .Select(e => new Models.Report
                //    {
                //        Date = e.Date,
                //        Gestion = e.Gestion.Name,
                //        GestionId = (int)e.GestionId
                //    }).ToList();
                reportData = (from e in _context.Exits
                              join g in _context.Gestions on e.GestionId equals g.Id
                              where e.Date >= DateStart && e.Date <= DateEnd
                              select new Models.Report
                              {
                                  Date = e.Date,
                                  Gestion = g.Name,
                                  GestionId = (int)e.GestionId,
                                 StartDate = DateStart,
                                  EndDate = DateEnd
                              }
                              ).ToList();
            }

            if (GestionFilter == "Una" && GestionId.HasValue)
            {
                reportData = reportData.Where(r => r.GestionId == GestionId).ToList();
            }

            return reportData;
        }

        private IActionResult ListReport()
        {
            var reportData = ToDataTable(GenerateReport()); 

            var csvContent = new System.Text.StringBuilder();

            foreach (DataColumn column in reportData.Columns)
            {
                csvContent.Append(column.ColumnName + ",");
            }
            csvContent.Length--; 
            csvContent.AppendLine();

            foreach (DataRow row in reportData.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    csvContent.Append(item?.ToString()?.Replace(",", ";") + ",");
                }
                csvContent.Length--; // Remove the trailing comma
                csvContent.AppendLine();
            }

            // Return the CSV file as a downloadable file
            var fileContent = System.Text.Encoding.UTF8.GetBytes(csvContent.ToString());
            var fileName = $"Report_{DateTime.Now:yyyyMMddHHmmss}.csv";
            return File(fileContent, "text/csv", fileName);
        }




    }
}
