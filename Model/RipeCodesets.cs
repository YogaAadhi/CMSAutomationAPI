using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSAutomationAPI.Model
{
    [Table("ripe_codesets", Schema = "public")]
    public class RipeCodesets
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("code_set_name")]
        [MaxLength(100)]
        public string? CodeSetName { get; set; }       
        
        [Column("codetype")]
        public string CodeType { get; set; }
        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }
        [Column("created_by")]
        public int? CreatedBy { get; set; }
        [Column("modified_on")]
        public DateTime? ModifiedOn { get; set; }
        [Column("modified_by")]
        public int? ModifiedBy { get; set; }
        [Column("isdelete")]
        public bool IsDelete { get; set; }

        [Column("codelogictype")]
        public string? CodeLogicType { get; set; }

        [Column("source_system")]
        public string? SourceSystem { get; set; }

        [Column("rule_attribute")]
        public string? RuleAttribute { get; set; }

        [Column("rule_value")]
        public string? RuleValue { get; set; }

        [Column("lo_age")]
        public int Lo_Age { get; set; }

        [Column("hi_age")]
        public int Hi_Age { get; set; }

        [Column("update_frequency")]
        public string? Update_Frequency { get; set; }

        public virtual ICollection<CPT_CodeSetMapping> CPTCodesetMaps { get; set; }


        //public virtual ICollection<Ripe_icd_codeset_codes_map> ICDCodesetMaps { get; set; }
        //public virtual ICollection<Ripe_mod_codeset_codes_map> MODCodesetMaps { get; set; }
        //public virtual ICollection<Ripe_pos_codeset_codes_map> POSCodesetMaps { get; set; }
    }
}
