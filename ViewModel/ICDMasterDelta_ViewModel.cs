namespace CMSAutomationAPI.ViewModel
{
    public class ICDMasterDelta_ViewModel
    {
        public string ICDCode { get; set; }
        public string? Description { get; set; }        
        public string? Type { get; set; }
        public DateTime? EffDate { get; set; }
        public DateTime? TermDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
