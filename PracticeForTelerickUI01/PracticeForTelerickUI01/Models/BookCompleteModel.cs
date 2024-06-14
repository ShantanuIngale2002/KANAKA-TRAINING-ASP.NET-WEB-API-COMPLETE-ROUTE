using System.ComponentModel.DataAnnotations;

namespace PracticeForTelerickUI01.Models
{
    public class BookCompleteDataModel
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string BookCode { get; set; } = string.Empty;

        [Required]
        public string BookName { get; set; } = string.Empty;

        [Required]
        public DateTime AddedOnDate { get; set; } = DateTime.Now;

        [Required]
        public string Genre {  get; set; } = string.Empty;

        [Required]
        public string AuthorName { get; set; } = string.Empty;

        [Required]
        public int TotalStock { get; set; } = 0;

        [Required]
        public int BorrowedCount { get; set; } = 0;
    }
}
