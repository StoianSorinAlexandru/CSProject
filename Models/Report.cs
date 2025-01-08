using System.ComponentModel.DataAnnotations.Schema;

namespace miniProiect2.Models
{
    public class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Date { get; set; }
        public int GestionId { get; set; }
        public string Gestion { get; set; }
    }
}
