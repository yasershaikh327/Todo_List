using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.Repository
{
    public class RegistrationRepository
    {
        public interface IRegister
        {
            void Registration(RegistrationModel registrationModel);
            void Login(RegistrationModel registrationModel);
            
        }

        public class RegistrationService : IRegister
        {

            //Registration of User
            public void Registration(RegistrationModel registrationModel)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
                SqlCommand CheckEmailCmd = new SqlCommand("Select Count(Email) From RegistrationTable Where Email='"+registrationModel.Email+"'" ,connection);
                SqlCommand Cmd = new SqlCommand("Insert Into [RegistrationTable] Values('"+registrationModel.FirstName+"','"+registrationModel.LastName+"','"+registrationModel.Email+"','"+registrationModel.Password+"',GETDATE())",connection);
                connection.Open();
                registrationModel.Temp = Convert.ToInt32(CheckEmailCmd.ExecuteScalar());    
                connection.Close();

                if(registrationModel.Temp >= 1)
                {
                    registrationModel.Message = "This Email Already Exsists";
                    registrationModel.Color = "Red";
                    return;
                }
                else
                {
                    connection.Open();
                    Cmd.ExecuteNonQuery();
                    registrationModel.Message = "Registered Successfully";
                    registrationModel.Color = "Green";
                    connection.Close();
                }
            }


            //User Login
            public void Login(RegistrationModel registrationModel)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
                SqlCommand CheckEmailCmd = new SqlCommand("Select Count(*) From RegistrationTable Where Email='" + registrationModel.Email + "' AND Password='"+registrationModel.Password+"'", connection);
                SqlCommand FetchIDlCmd = new SqlCommand("Select ID From [RegistrationTable] Where Email='" + registrationModel.Email + "' AND Password='"+registrationModel.Password+"'", connection);
                connection.Open();
                registrationModel.Temp = Convert.ToInt32(CheckEmailCmd.ExecuteScalar());
                registrationModel.IDCookieUser = Convert.ToInt32(FetchIDlCmd.ExecuteScalar());
                connection.Close();

                if (registrationModel.Temp <= 0)
                {
                    registrationModel.Message = "Invalid Credentials";
                    registrationModel.Color = "Red";
                    return;
                }
                else
                {
                    connection.Open();
                    registrationModel.Message = "Login Successfully";
                    registrationModel.Color = "Green";
                    connection.Close();
                }
            }


        }
    }
}
