namespace CMSAutomationAPI.ViewModel
{
    public class ICDCodeSetDelta_ViewModel
    {
        public long CodesetId { get; set; }
        public string CodesetName { get; set; }
        public string IcdCode { get; set; }
        public string Action { get; set; }
        public DateTime? EffDate { get; set; }
        public DateTime? TermDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
