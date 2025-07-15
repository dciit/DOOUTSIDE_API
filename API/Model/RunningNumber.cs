using System.Globalization;
using System.Text;

namespace API.Model
{
    public class RunningNumber
    {
        private int m_FormatId;
        private string m_Key;
        private string m_Prefix;
        private int m_YearNbrPrefix = 0;
        private int m_MonthNbrPrefix = 0;
        private int m_DayNbrPrefix = 0;
        private int m_LengthNbr = 0;
        private int m_Id;
        private string m_Remark;
        private string m_ResetOption;
        private DateTime m_ActiveDate = new DateTime();
        private DateTime m_Date = DateTime.Now;

        public RunningNumber()
        {

        }

        public int FormatId
        {
            get { return this.m_FormatId; }
            set { this.m_FormatId = value; }
        }

        public string Key
        {
            get { return this.m_Key; }
            set { this.m_Key = value; }
        }

        public string Prefix
        {
            get { return this.m_Prefix; }
            set { this.m_Prefix = value; }
        }

        public int LenYearPrefix
        {
            get { return this.m_YearNbrPrefix; }
            set { this.m_YearNbrPrefix = value; }
        }

        public int LenMonthPrefix
        {
            get { return this.m_MonthNbrPrefix; }
            set { this.m_MonthNbrPrefix = value; }
        }

        public int LenDayPrefix
        {
            get { return this.m_DayNbrPrefix; }
            set { this.m_DayNbrPrefix = value; }
        }

        public int LenRunId
        {
            get { return this.m_LengthNbr; }
            set { this.m_LengthNbr = value; }
        }

        public DateTime ActiveDate
        {
            get { return this.m_ActiveDate; }
            set { this.m_ActiveDate = value; }
        }

        public DateTime Date
        {
            get { return this.m_Date; }
            set { this.m_Date = value; }
        }

        public int NextId
        {
            get
            {
                if (MustReset)
                {
                    m_Id = 0;
                }
                return this.m_Id + 1;
            }
            set { this.m_Id = value; }
        }

        public string Remark
        {
            get { return this.m_Remark; }
            set { this.m_Remark = value; }
        }

        public string ResetOption
        {
            get { return this.m_ResetOption; }
            set { this.m_ResetOption = value; }
        }

        public override string ToString()
        {
            return this.ToString(false);
        }

        public string ToString(bool isIncludePrefixCode)
        {
            StringBuilder id = new StringBuilder();

            int year = Convert.ToInt32(this.Format(this.Date, "yy"));
            int month = Convert.ToInt32(this.Format(this.Date, "MM"));
            int day = Convert.ToInt32(this.Format(this.Date, "dd"));

            if (isIncludePrefixCode) id.Append(this.Prefix);

            id.Append(this.AddLength(year.ToString(), this.LenYearPrefix));
            id.Append(this.AddLength(month.ToString(), this.LenMonthPrefix));
            id.Append(this.AddLength(day.ToString(), this.LenDayPrefix));
            id.Append(this.AddLength(this.NextId.ToString(), this.LenRunId));

            return id.ToString();
        }

        private string Format(DateTime dt, string format)
        {
            return dt.ToString(format, new CultureInfo("en-US"));
        }

        private string AddLength(string text, int len)
        {
            if (len > 0)
            {
                if (text.Length < len)
                {
                    while (text.Length < len)
                    {
                        text = "0" + text;
                    }
                }
                else
                {
                    text = text.Substring(0, len);
                }
            }
            else
            {
                text = "";
            }
            return text;
        }

        public bool MustReset
        {
            get
            {
                bool isReset = false;

                int actY = Convert.ToInt32(this.ActiveDate.ToString("yy"));
                int curY = Convert.ToInt32(this.Date.ToString("yy"));

                int actM = Convert.ToInt32(this.ActiveDate.ToString("MM"));
                int curM = Convert.ToInt32(this.Date.ToString("MM"));

                int actD = Convert.ToInt32(this.ActiveDate.ToString("dd"));
                int curD = Convert.ToInt32(this.Date.ToString("dd"));

                if (this.ResetOption.Equals("Y"))
                {
                    if (curY > actY)
                    {
                        isReset = true;
                    }
                }
                else if (this.ResetOption.Equals("M"))
                {
                    if (curM > actM)
                    {
                        isReset = true;
                    }
                }
                else
                {
                    if (curD > actD)
                    {
                        isReset = true;
                    }
                }

                return isReset;
            }
        }
    }
}
