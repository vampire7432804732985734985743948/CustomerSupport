namespace CustomerSupport.Models
{
    public class ContactSupportModel
    {
        public string UserEmailAddress { get; set; } = string.Empty;
        public string RequestTitle { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string RequestStatus { get; set; } = string.Empty;
        public DateTime CaseSubmitionTime { get; set; }
        public DateTime? CaseClosingTime { get; set; }
    }
}
