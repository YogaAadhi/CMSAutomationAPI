using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSAutomationAPI.Model
{
    [Table("oce_delta", Schema = "public")]
    public class CPT_CodesMasterDelta
    {
        [Required]
        [Column("code")]
        [MaxLength(20)]
        public string CptCode { get; set; }

        [Column("long_desc")]
        public string? Long_Desc { get; set; }

        [Column("medium_desc")]
        public string? Medium_Desc { get; set; }

        [Column("short_desc")]
        public string? Short_Desc { get; set; }

        [Column("code_type")]
        public string? Code_Type { get; set; }

        [Column("process_type")]
        public string? Process_Type { get; set; }

        [Column("eff_date")]
        public DateTime? EffDate { get; set; }

        [Column("term_date")]
        public DateTime? TermDate { get; set; }

        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }

        [Column("status")]
        public string? status { get; set; }

        [Column("updated_on")]
        public DateTime? UpdatedOn { get; set; }

    }
}
