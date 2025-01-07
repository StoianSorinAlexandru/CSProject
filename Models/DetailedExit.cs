using miniProiect2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace miniProiect2.Models
{
    public class DetailedExit
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual Exit? Exit { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        public int Quantity { get; set; }


    }
}
