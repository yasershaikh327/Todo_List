using System.Data.SqlClient;

namespace RealEstateWebsite.Models.Admin
{
    public class AdminUpdatePasswordModel
    {
        public string Msg { get; set; }
        public string color { get; set; }
        public string AdminOldPassword { get; set; }
        public string AdminNewPassword { get; set; }
        public string AdminCNewPassword { get; set; } 
        public string ID { get; set; }


    }
}