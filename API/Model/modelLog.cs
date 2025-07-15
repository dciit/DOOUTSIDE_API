namespace API.Model
{
    public class modelLog
    {
        public class RUNNING_LOG
        {
            public string LOG_NBR { get; set; }
            public string SET_CODE { get; set; }
            public string PROCESS { get; set; }
            public DateTime PROCESS_ST { get; set; }
            public DateTime PROCESS_EN { get; set; }
            public int READ_QTY { get; set; }
            public int SUCCESS_QTY { get; set; }
            public int ERROR_QTY { get; set; }
            public DateTime RUNNING_DATE { get; set; }
        }
        public class RUNNING_ERROR
        {
            public string LOG_NBR { get; set; }
            public string SET_CODE { get; set; }
            public string PROCESS { get; set; }
            public DateTime PROCESS_ST { get; set; }
            public DateTime PROCESS_EN { get; set; }
            public int ERROR_ROW { get; set; }
            public string ERROR_TEXT { get; set; }
            public DateTime RUNNING_DATE { get; set; }
        }
    }
}
