using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeForTelerickUI01.Data
{
    public class BookInformation
    {

        [Key]
        public int map_id { get; set; }

        //[Key]
        [Required]
        [Column("book_code")]
        public string BookCode { get; set; } = string.Empty;

        [Required]
        [Column("author")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [Column("total_stock")]
        public int Stock { get; set; }

        [Required]
        [Column("borrowed")]
        public int BorrowedCount { get; set; }
    }
}
