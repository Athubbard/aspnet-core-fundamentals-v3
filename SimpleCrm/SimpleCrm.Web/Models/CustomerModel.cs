
namespace SimpleCrm.Web.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string PreferredContactMethod { get; set; }
        public string Status { get; set; }
        public int LastContactDate { get; set; }
        
    }
      
}
