namespace ToDoList.Models
{
    public class RegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPassword { get; set; }
        public string Message { get; set; }
        public string Color { get; set; }
        public int Temp { get; set; }
        public int IDCookieUser { get; set; }

        public bool FirstNameCheck { get; set; }
        public bool LastNameCheck { get; set; }
        public bool EmailCheck { get; set; }
        public bool PasswordCheck { get; set; }
        public bool CPasswordCheck { get; set; }
       
    }
}
