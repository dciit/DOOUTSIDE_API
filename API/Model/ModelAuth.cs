namespace API.Model
{
    public class ModelAuth
    {
        public class vduser
        {
            public string code { get; set; }
            public string name { get; set; }
            public string fullname { get; set; }
            public string pren { get; set; }
            public string surn { get; set; }
            public string status { get; set; }
        }
        public class paramLogin
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
