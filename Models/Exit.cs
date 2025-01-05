using miniProiect2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniProiect2.Models
{
    public class Exit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? GestionId { get; set; }
        [ForeignKey("GestionId")]
        public virtual Gestion? Gestion { get; set; }
    }
}
