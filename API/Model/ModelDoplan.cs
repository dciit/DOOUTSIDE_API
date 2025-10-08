namespace API.Model
{
    public class ModelDoplan
    {
        public class cDOPLAN
        {
            public int rev { get; set; }
            public int lrev { get; set; }
            public string nbr { get; set; }
            public string set_code { get; set; }
            public string prdymd { get; set; }
            public DateTime do_date { get; set; }
            public string vender { get; set; }
            public string vender_n { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string whno { get; set; }
            public string whcode { get; set; }
            public string whname { get; set; }
            public decimal plan_qty { get; set; }
            public decimal consumtion_qty { get; set; }
            public decimal cal_qty { get; set; }
            public decimal caldod_qty { get; set; }
            public decimal mark_qty { get; set; }
            public decimal do_qty { get; set; }
            public decimal stk_inhouse_bf { get; set; }
            public decimal stk_inhouse_af { get; set; }
            public decimal stk_wh_bf { get; set; }
            public decimal stk_wh_af { get; set; }
            public decimal stk_current { get; set; }
            public decimal picklist { get; set; }
            public DateTime recive_dt { get; set; }
            public decimal recive_qty { get; set; }
            public decimal send_qty { get; set; }
            public DateTime vdtowhb_dt { get; set; }
            public decimal vdtowhb_qty { get; set; }
            public DateTime data_dt { get; set; }
            public string remark1 { get; set; }
            public string remark2 { get; set; }
            public string remark3 { get; set; }
            public bool status_mark { get; set; }
            public bool status_fixed_date { get; set; }
            public bool status_delicycle { get; set; }
            public bool status_holiday { get; set; }
            public bool shipping { get; set; }
            public decimal remain_act { get; set; }

            public decimal pm_sfstk { get; set; }
            public decimal pm_markqty { get; set; }
            public decimal pm_minqty { get; set; }
            public decimal pm_maxqty { get; set; }
            public decimal pm_qtybox { get; set; }
            public decimal pm_boxpl { get; set; }
            public string pm_typecal { get; set; }
            public int pm_truckstack { get; set; }
            public int pm_plstack { get; set; }
            public int pm_pdlt { get; set; }
            public int pm_preorder { get; set; }
        }

        public class VDHOLIDAY
        {
            public string vender { get; set; }
            public string sdate { get; set; }
            public string ndate { get; set; }
            public string description { get; set; }
            public string empcode { get; set; }
        }

        public class DCIHOLIDAY
        {
            public int nbr { get; set; }
            public DateTime date { get; set; }
            public string description { get; set; }
        }

        public class VDFIXEDDAYS
        {
            public string nbr { get; set; }
            public string whcode { get; set; }
            public int day { get; set; }
        }

        public class CALENDAR
        {
            public string set_code { get; set; }
            public string h_type { get; set; }
            public string vender { get; set; }
            public DateTime h_date { get; set; }
        }

        public class DELIVERY_CYCLES
        {
            public int nbr { get; set; }
            public string vender { get; set; }
            public string del_type { get; set; }
            public bool del_wk_sun { get; set; }
            public bool del_wk_mon { get; set; }
            public bool del_wk_tue { get; set; }
            public bool del_wk_wed { get; set; }
            public bool del_wk_thu { get; set; }
            public bool del_wk_fri { get; set; }
            public bool del_wk_sat { get; set; }
            public bool del_mo_01 { get; set; }
            public bool del_mo_02 { get; set; }
            public bool del_mo_03 { get; set; }
            public bool del_mo_04 { get; set; }
            public bool del_mo_05 { get; set; }
            public bool del_mo_06 { get; set; }
            public bool del_mo_07 { get; set; }
            public bool del_mo_08 { get; set; }
            public bool del_mo_09 { get; set; }
            public bool del_mo_10 { get; set; }
            public bool del_mo_11 { get; set; }
            public bool del_mo_12 { get; set; }
            public bool del_mo_13 { get; set; }
            public bool del_mo_14 { get; set; }
            public bool del_mo_15 { get; set; }
            public bool del_mo_16 { get; set; }
            public bool del_mo_17 { get; set; }
            public bool del_mo_18 { get; set; }
            public bool del_mo_19 { get; set; }
            public bool del_mo_20 { get; set; }
            public bool del_mo_21 { get; set; }
            public bool del_mo_22 { get; set; }
            public bool del_mo_23 { get; set; }
            public bool del_mo_24 { get; set; }
            public bool del_mo_25 { get; set; }
            public bool del_mo_26 { get; set; }
            public bool del_mo_27 { get; set; }
            public bool del_mo_28 { get; set; }
            public bool del_mo_29 { get; set; }
            public bool del_mo_30 { get; set; }
            public bool del_mo_31 { get; set; }
            public string empcode { get; set; }
        }

        public class PARTMSTR
        {
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string vender { get; set; }
            public decimal sfstkqty { get; set; }
            public decimal wipqty { get; set; }
            public decimal markqty { get; set; }
            public decimal minqty { get; set; }
            public decimal maxqty { get; set; }
            public decimal qtybox { get; set; }
            public decimal boxpl { get; set; }
            public string typecal { get; set; }
            public int truckstack { get; set; }
            public int palletstack { get; set; }
            public int pdlt { get; set; }
            public int preorderday { get; set; }
            public decimal stkwh1 { get; set; }
            public decimal stkwh2 { get; set; }
            public decimal stkwhb { get; set; }
            public string markstatus { get; set; }
            public string status { get; set; }
        }

        public class DST_DATAC1
        {
            public string pono { get; set; }
            public string whno { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public double wqty { get; set; }
            public string acdate { get; set; }
            public string vendor { get; set; }
            public string actime { get; set; }
            public string bit { get; set; }
        }
        public class FTTACT
        {
            public string delldate { get; set; }
            public DateTime sdate { get; set; }
            public DateTime rdate { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public decimal sqty { get; set; }
            public decimal rqty { get; set; }
        }
        public class PICKLIST
        {
            public string partno { get; set; }
            public string cm { get; set; }
            public DateTime date { get; set; }
            public decimal qty { get; set; }
        }
        public class DOPLANKEY
        {
            public string nbr { get; set; }
            public string setcode { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string vender { get; set; }
            public int doqtyOld { get; set; }
            public int doqtyNew { get; set; }
            public int markqtyOld { get; set; }
            public int markqtyNew { get; set; }
            public DateTime dodate { get; set; }
            public string update_by { get; set; }
        }
        public class HISTORYPLAN
        {
            public DateTime data_dt { get; set; }
            public int plan_qty { get; set; }
        }

        public class CURRENTSTKPS
        {
            public string partno { get; set; }
            public string cm { get; set; }
            public decimal whb { get; set; }
        }

        public class VDSELECTMSTR
        {
            public string vender { get; set; }
            public string vender_n { get; set; }
        }
        public class SHAREVD
        {
            public string partno { get; set; }
            public string cm { get; set; }
            public string grpvd { get; set; }
        }

    }
}
