
using RealEstateWebsite.Models.Home;

using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;

namespace RealEstateWebsite.Repository.Home
{
    public class UpdatePasswordRepository
    {
        public interface IForgetPasswordService
        {
            void ForgetPassword(UpdatePasswordModel updatePasswordModel);   
        }

        public class ForgetPasswordService : IForgetPasswordService
        {
            public void ForgetPassword(UpdatePasswordModel updatePasswordModel)
            {
                SqlConnection Con = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
                updatePasswordModel.CheckUser = updatePasswordModel.ForgetPassword.Contains("syaser327@outlook.com");
              
                if(updatePasswordModel.CheckUser == true)
                {
                    Random res = new Random();

                    // String that contain both alphabets and numbers
                    String str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
                    int size = 8;

                    // Initializing the empty string
                    String randomstring = "";

                    for (int i = 0; i < size; i++)
                    {

                        // Selecting a index randomly
                        int x = res.Next(str.Length);

                        // Appending the character at the 
                        // index to the random alphanumeric string.
                        randomstring = randomstring + str[x];
                    }

                    SqlCommand Cmd = new SqlCommand("Update [RegistrationTable] SET [Password]='" + randomstring + "' Where Email='" + updatePasswordModel.ForgetPassword + "';", Con);
                    Con.Open();
                    Cmd.ExecuteNonQuery();
                    updatePasswordModel.Message = "Password Updated!!!";
                    updatePasswordModel.Message = "Check Mail!!!";
                    Con.Close();

                    //Sending An Email
                    try
                    {
                        SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        System.Net.NetworkCredential credential = new System.Net.NetworkCredential("syaser327@outlook.com", ",zk7ZLkTE#7f4Y^");
                        client.EnableSsl = true;
                        client.Credentials = credential;
                        MailMessage message = new MailMessage("syaser327@outlook.com", updatePasswordModel.ForgetPassword);
                        message.Subject = "Password Recovery";
                        message.Body = "<table style='border:2px solid black;'>    <tr>        <td style='border:2px solid black;text-align: center;background-color: white;font-family: Poppins, sans-serif;font-size: x-large;font-weight: bolder;'>Real<span style='color: green;'>Estate</span></td>  </tr>    <tr>       <td style='border:2px solid black;font-family: cursive;text-align: center;'>Use This Password to Login</td>   </tr>    <tr>       <td style='border:2px solid black;font-family: cursive;text-align: center;color:green'>"+ randomstring + "</td>   </tr></table>";                       
                        message.IsBodyHtml = true;
                        client.Send(message);

                        // return Content(FirstName,LastName,Phone, Email, gender, Address,DOB,Username,Password,CPassword);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message);
                    }
                }
                else
                {


                    Random res = new Random();

                    // String that contain both alphabets and numbers
                    String str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
                    int size = 8;

                    // Initializing the empty string
                    String randomstring = "";

                    for (int i = 0; i < size; i++)
                    {

                        // Selecting a index randomly
                        int x = res.Next(str.Length);

                        // Appending the character at the 
                        // index to the random alphanumeric string.
                        randomstring = randomstring + str[x];
                    }
                  

                    SqlCommand Cmd = new SqlCommand("Update [RegistrationTable] SET Password='" + randomstring + "' Where Email='" + updatePasswordModel.ForgetPassword + "';", Con);
                    Con.Open();
                    Cmd.ExecuteNonQuery();
                    updatePasswordModel.Message = "Password Updated!!!";
                    updatePasswordModel.Message = "Check Mail!!!";
                    Con.Close();

                    //Sending An Email
                    try
                    {
                        SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        System.Net.NetworkCredential credential = new System.Net.NetworkCredential("syaser327@outlook.com", ",zk7ZLkTE#7f4Y^");
                        client.EnableSsl = true;
                        client.Credentials = credential;
                        MailMessage message = new MailMessage("syaser327@outlook.com", updatePasswordModel.ForgetPassword);
                        message.Subject = "Password Recovery";
                        message.Body = randomstring;
                        message.IsBodyHtml = true;
                        client.Send(message);

                        // return Content(FirstName,LastName,Phone, Email, gender, Address,DOB,Username,Password,CPassword);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message);
                    }

                }
            }
        }
    }
}
