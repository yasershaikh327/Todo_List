//using Microsoft.Data.SqlClient;
using RealEstateWebsite.Models.Admin;
using System.Data.SqlClient;
using System.Drawing;

namespace RealEstateWebsite.Repository.Admin
{
    public class AdminUpdatePasswordRespository
    {
        public interface IAdminUpdatePasswordService
        {
            void Update(AdminUpdatePasswordModel adminUpdatePasswordModel);
        }

        public class UpdatePassWordAdminService : IAdminUpdatePasswordService
        {
            //Update Password
            public void Update(AdminUpdatePasswordModel adminUpdatePasswordModel)
            {
                SqlConnection Con = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
                SqlCommand Check = new SqlCommand("Select Count(*) From [RegistrationTable] Where Password='" + adminUpdatePasswordModel.AdminOldPassword + "' AND ID='" + adminUpdatePasswordModel.ID + "';", Con);
                SqlCommand Cmd = new SqlCommand("Update [RegistrationTable] SET Password='" + adminUpdatePasswordModel.AdminNewPassword + "' Where ID='" + adminUpdatePasswordModel.ID + "';", Con);
                Con.Open();
                int temp1 = Convert.ToInt32(Check.ExecuteScalar());
                Con.Close();


                if (temp1 == 0)
                {
                    adminUpdatePasswordModel.Msg = "Invalid Old Password";
                    adminUpdatePasswordModel.color = "red";
                }
                else if (adminUpdatePasswordModel.AdminNewPassword != adminUpdatePasswordModel.AdminCNewPassword)
                {
                    adminUpdatePasswordModel.Msg = "New Password and Confirm Password Doesnt Match";
                }
                else
                {
                    Con.Open();
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    adminUpdatePasswordModel.color = "green";
                    adminUpdatePasswordModel.Msg = "Password Updated!!";
                }



            }
        }
    }
}