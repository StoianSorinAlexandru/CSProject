using System.ComponentModel.DataAnnotations.Schema;

namespace miniProiect2.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CUI { get; set; }
        public string Address { get; set; }   
    }
}
