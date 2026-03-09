namespace CMSAutomationAPI.ViewModel
{
    public class CPTCodeSetDelta_ViewModel
    {
        public long CodesetId { get; set; }
        public string? CodesetName { get; set; }
        public string CptCode { get; set; }
        public string Action { get; set; }
        public DateTime? EffDate { get; set; }
        public DateTime? TermDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }


    public class WorkBenchQueryParams
    {
        public int? PageSize { get; set; } = 50;
        public int? PageNumber { get; set; } = 1;
        public string[] HcpcCode { get; set; }        
        public string[] IcdCode { get; set; }        
        public string? Status { get; set; }
        public long? AssingId { get; set; }               
        public DateTime? Effective_date { get; set; }
        public DateTime? Term_date { get; set; }
    }

    public enum CodesetDeltaStatus
    {
        Pending,       
        Approved,
        Rejected
    }


}
