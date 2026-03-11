using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSAutomationAPI.Model
{

    [Table("icd_delta", Schema = "public")]
    public class ICD_CodesMasterDelta
    {
        [Required]
        [Column("code")]
        [MaxLength(20)]
        public string IcdCode { get; set; }       

        [Column("effectivedate")]
        public DateTime? EffDate { get; set; }

        [Column("termdate")]
        public DateTime? TermDate { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("type")]
        public string? Type { get; set; }

        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }

        [Column("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        [Column("status")]
        public string? Status { get; set; }

    }
}
