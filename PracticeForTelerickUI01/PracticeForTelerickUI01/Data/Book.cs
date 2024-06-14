using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeForTelerickUI01.Data
{
    public class Book
    {
        [Key]
        [Required]
        [Column("book_id")]
        public int BookId { get; set; }

        [Required]
        [Column("book_code")]
        public string BookCode { get; set; } = "B" + DateTime.Now.ToString();

        [Column("book_name")]
        [Required]
        public string BookName { get; set; } = string.Empty;

        [Column("added_on")]
        [Required]
        public string AddedDate { get; set; } = string.Empty;

        [Column("book_type")]
        [Required]
        public string BookType { get; set; } = string.Empty;
    }
}
