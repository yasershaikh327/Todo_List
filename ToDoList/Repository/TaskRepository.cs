using Quartz;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using ToDoList.Models;

namespace ToDoList.Repository
{
    public class TaskRepository
    {
        public interface ITask
        {
            void TaskFunction(TaskModel tasksss);
            void FetchTasks(TaskModel taskModel);
            void DeleteTaskFunction(TaskModel tasksss);
            void UpdateTaskFunction(TaskModel tasksss);
            void FetchBasedOnTime();
        }

        public class TaskService : ITask, IJob
        {
            //Cron Job Services
            public Task Execute(IJobExecutionContext context)
            {
                //Write your custom code here
                /*UpdateEmptyRooms();
                GetData();
                UpdateEmptyRooms();*/
                FetchBasedOnTime();
                return Task.FromResult(true);
            }



            //Add Notifcations
            public void TaskFunction(TaskModel tasksss)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
                SqlCommand Cmd = new SqlCommand("Insert Into [TaskTable] Values('" + tasksss.TaskData + "',GETDATE(),'" + tasksss.TaskTime + "','" + tasksss.UserID + "')", connection);
                connection.Open();
                Cmd.ExecuteNonQuery();
                tasksss.Message = "Task Added Successfully";
                connection.Close();
            }



            //Fetch Notifications
            public void FetchTasks(TaskModel taskModel)
            {
                string constr = @"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //string query = "Select R.Rent_Type,R.Location,R.Price,R.ID From RoomForm R;";
                    //string query1 = "R.ID From RoomForm R;";
                    string query = "    SELECT  *  FROM [ToDoList].[dbo].[TaskTable] Where UserID='" + taskModel.UserID + "';  ";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                taskModel.list1.Add(new TaskModel
                                {
                                    taskshow = sdr["Task"].ToString(),
                                    taskId = Convert.ToInt32(sdr["ID"]),
                                    TaskTDime = sdr["Reminder"].ToString(),
                                });
                            }

                        }

                        con.Close();
                    }
                    taskModel.list2 = taskModel.list1;
                    return;
                }
            }

            //Delete Task
            public void DeleteTaskFunction(TaskModel tasksss)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
                SqlCommand Cmd = new SqlCommand("Delete From [TaskTable] Where ID='" + tasksss.deletetask + "'", connection);
                connection.Open();
                Cmd.ExecuteNonQuery();
                tasksss.Message = "Task Deleted Successfully";
                connection.Close();
            }


            //Delete Task
            public void UpdateTaskFunction(TaskModel tasksss)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
                SqlCommand Cmd = new SqlCommand("Update [TaskTable] Set [Task] ='" + tasksss.Updatetask + "',[Reminder] = '" + tasksss.UpdatetaskTime + "' Where ID='" + tasksss.tasdID + "'", connection);
                connection.Open();
                Cmd.ExecuteNonQuery();
                tasksss.Message = "Task Updated Successfully";
                connection.Close();
            }

            //Send Mail based on Time
            public void FetchBasedOnTime()
            {
                string formatedTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
                SqlConnection connection = new SqlConnection(@"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False");
               
                string constr = @"Data Source=ZCBLRLP0261\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = " Select R.Email,TT.Task From TaskTable TT, RegistrationTable R Where(R.ID= TT.UserID) AND(Reminder = '" + formatedTime.ToString() + "');  ";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                               string Email = sdr["Email"].ToString();
                               string Task = sdr["Task"].ToString();
                                
                                //Sending An Email
                                try
                                {

                                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                                    client.Port = 587;
                                   // client.Timeout = 10;
                                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    client.UseDefaultCredentials = false;
                                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("syaser327@outlook.com", ",zk7ZLkTE#7f4Y^");
                                    client.EnableSsl = true;
                                    client.Credentials = credential;
                                    MailMessage message = new MailMessage("syaser327@outlook.com", Email);
                                    message.Subject = "Reminder";
                                    message.Body = "<table style=\"border:2px solid black;\">\r\n    <tr>\r\n        <td style=\"border:2px solid black;text-align: center;background-color: white;font-family: Poppins, sans-serif;font-size: x-large;font-weight: bolder;\">ToDo<span style=\"color: green;\">List</span></td>\r\n    </tr>\r\n    <tr>\r\n        <td style=\"border:2px solid black;font-family: cursive;text-align: center;\">Your Reminder</td>\r\n    </tr>\r\n    <tr>\r\n        <td style=\"border:2px solid black;font-family: cursive;text-align: center;\">'" + Task + "'</td>\r\n    </tr>\r\n</table>";
                                    message.IsBodyHtml = true;
                                    client.Send(message);
                                   
                                }
                                catch (Exception E)
                                {
                                    Console.WriteLine(E.Message);
                                }

                            }

                        }
                      
                        con.Close();
                    }
                   
                    return;
                }

            }
        }
    }
}