using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miniProiect2.Models;
using System.Data;

namespace miniProiect2.Pages.Report
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

        public IndexModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Gestions = _context.Gestions.ToList();
            DateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateEnd = DateTime.Now.Date;
        }

        public IActionResult OnPost(string action)
        {
            Gestions = _context.Gestions.ToList();

            if (action == "Accepta")
            {
                ReportData = GenerateReport();
            }
            else if(action == "Listeaza")
            {

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

        private DataTable GenerateReport()
        {
            List<Models.Report> reportData;

            if (ReportType == "Intrari")
            {
                reportData = _context.Entries
                    .Where(e => e.Date >= DateStart && e.Date <= DateEnd)
                    .Select(e => new Models.Report
                    {
                        Date = e.Date,
                        Gestion = e.Gestion.Name,
                        GestionId = (int)e.GestionId
                    }).ToList();
            }
            else // ReportType == "Iesiri"
            {
                reportData = _context.Exits
                    .Where(e => e.Date >= DateStart && e.Date <= DateEnd)
                    .Select(e => new Models.Report
                    {
                        Date = e.Date,
                        Gestion = e.Gestion.Name,
                        GestionId = (int)e.GestionId
                    }).ToList();
            }

            if (GestionFilter == "Una" && GestionId.HasValue)
            {
                reportData = reportData.Where(r => r.GestionId == GestionId).ToList();
            }

            return ToDataTable(reportData);
        }


    }
}
