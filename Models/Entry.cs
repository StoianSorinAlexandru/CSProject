using System.ComponentModel.DataAnnotations.Schema;

namespace miniProiect2.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? PartnerId { get; set; }
        [ForeignKey("PartnerId")]
        public virtual Partner? Partner { get; set; }
        public int? GestionId { get; set; }
        [ForeignKey("GestionId")]
        public virtual Gestion? Gestion { get; set; }
    }
}
