namespace RealEstateWebsite.Models.Home
{
    public class UpdatePasswordModel
    {
        public String str = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        public int size = 8;
        public String randomstring = "";
        public string ForgetPassword { get; set; }
        public int x { get; set; }
        public bool CheckUser { get; set;}
        public Random res = new Random();
        public string Message { get; set; }
        public string Message2 { get; set; }
    }
}
