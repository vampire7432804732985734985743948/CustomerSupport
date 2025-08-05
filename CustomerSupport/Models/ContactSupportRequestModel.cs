using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerSupport.Models
{
    [Table("ContactSupportCases")]
    public class ContactSupportRequestModel : ContactSupportModel
    {
        public int Id { get; set; }
        public string RequestDescription { get; set; } = string.Empty;
    }
}
