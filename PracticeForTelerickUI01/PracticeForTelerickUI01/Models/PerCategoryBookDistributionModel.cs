using System.ComponentModel.DataAnnotations;

namespace PracticeForTelerickUI01.Models
{
    public class PerCategoryBookDistributionModel
    {
        [Key]
        public float Distribution { get; set; }

        public string Category { get; set; } = string.Empty;
    }
}
