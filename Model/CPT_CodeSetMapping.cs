using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSAutomationAPI.Model
{

    [Table("ripe_cpt_codeset_codes_map", Schema = "public")]
    public class CPT_CodeSetMapping
    {
        public CPT_CodeSetMapping()
        {

        }

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long codeset_id { get; set; }
        public string cpt_code { get; set; }
        public DateTime? eff_date { get; set; }
        public DateTime? term_date { get; set; }
        public bool is_delete { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public virtual RipeCodesets RipeCodeset { get; set; }
    }
}
