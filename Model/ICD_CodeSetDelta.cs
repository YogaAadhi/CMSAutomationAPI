using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSAutomationAPI.Model
{

    [Table("icd_codeset_delta", Schema = "public")]
    public class ICD_CodeSetDelta
    {

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("codeset_id")]
        public long CodesetId { get; set; }

        [Required]
        [Column("icd_code")]
        [MaxLength(20)]
        public string IcdCode { get; set; }

        [Required]
        [Column("action")]
        [MaxLength(20)]
        public string Action { get; set; }  // ADD / UPDATE / DELETE

        [Column("eff_date")]
        public DateTime? EffDate { get; set; }

        [Column("term_date")]
        public DateTime? TermDate { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("codeset_name")]
        public string? CodeSetName { get; set; }

    }
}
