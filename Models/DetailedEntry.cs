using miniProiect2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniProiect2.Models
{
    public class DetailedEntry
    {
        public int Id { get; set; }
        public int EntryId { get; set; }   
        [ForeignKey("EntryId")]
        public virtual Entry? Entry { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
        public float Value => Quantity * (Product?.Price ?? 0);
    }
}
