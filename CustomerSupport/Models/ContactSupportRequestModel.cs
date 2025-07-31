using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerSupport.Models
{
    [Table("ContactSupportCases")]
    public class ContactSupportRequestModel
    {
        public int Id { get; set; }
        public string UserEmailAddress { get; set; } = string.Empty;
        public string RequestTitle { get; set; } = string.Empty;
        public string RequestDescription{ get; set; } = string.Empty;
        public string Category{ get; set; } = string.Empty;
    }
}
