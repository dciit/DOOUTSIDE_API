using Microsoft.AspNetCore.Components.Server;

namespace API.Model
{
    public class ModelTransaction
    {
        public class SETCONTROL_T
        {
            public string set_code { get; set; }
            public DateTime set_date { get; set; }
            public string set_by { get; set; }
            public DateTime set_st_dt { get; set; }
            public DateTime set_en_dt { get; set; }
            public DateTime process_begin { get; set; }
            public DateTime process_finish { get; set; }
            public string status_distribute { get; set; }
            public DateTime distribute_dt { get; set; }
        }
        public class DELIVERYCYCLE_T
        {
            public string set_code { get; set; }
            public string vender { get; set; }
            public string del_type { get; set; }
            public Boolean del_wk_sun { get; set; }
            public Boolean del_wk_mon { get; set; }
            public Boolean del_wk_tue { get; set; }
            public Boolean del_wk_wed { get; set; }
            public Boolean del_wk_thu { get; set; }
            public Boolean del_wk_fri { get; set; }
            public Boolean del_wk_sat { get; set; }
            public Boolean del_mo_01 { get; set; }
            public Boolean del_mo_02 { get; set; }
            public Boolean del_mo_03 { get; set; }
            public Boolean del_mo_04 { get; set; }
            public Boolean del_mo_05 { get; set; }
            public Boolean del_mo_06 { get; set; }
            public Boolean del_mo_07 { get; set; }
            public Boolean del_mo_08 { get; set; }
            public Boolean del_mo_09 { get; set; }
            public Boolean del_mo_10 { get; set; }
            public Boolean del_mo_11 { get; set; }
            public Boolean del_mo_12 { get; set; }
            public Boolean del_mo_13 { get; set; }
            public Boolean del_mo_14 { get; set; }
            public Boolean del_mo_15 { get; set; }
            public Boolean del_mo_16 { get; set; }
            public Boolean del_mo_17 { get; set; }
            public Boolean del_mo_18 { get; set; }
            public Boolean del_mo_19 { get; set; }
            public Boolean del_mo_20 { get; set; }
            public Boolean del_mo_21 { get; set; }
            public Boolean del_mo_22 { get; set; }
            public Boolean del_mo_23 { get; set; }
            public Boolean del_mo_24 { get; set; }
            public Boolean del_mo_25 { get; set; }
            public Boolean del_mo_26 { get; set; }
            public Boolean del_mo_27 { get; set; }
            public Boolean del_mo_28 { get; set; }
            public Boolean del_mo_29 { get; set; }
            public Boolean del_mo_30 { get; set; }
            public Boolean del_mo_31 { get; set; }
        }
        public class PARTMSTR_T
        {
            public string set_code { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string vender { get; set; }
            public decimal sfstk_qty { get; set; }
            public decimal wip_qty { get; set; }
            public decimal mark_qty { get; set; }
            public decimal min_qty { get; set; }
            public decimal max_qty { get; set; }
            public decimal qty_box { get; set; }
            public decimal box_pl { get; set; }
            public int truck_stack { get; set; }
            public int pallet_stack { get; set; }
            public int pd_lt { get; set; }
            public int preorder_days { get; set; }
            public decimal store_wh1 { get; set; }
            public decimal store_wh2 { get; set; }
            public decimal store_whb { get; set; }
            public string mark_status { get; set; }
        }
        public class WHOUTSIDE_T
        {
            public string set_code { get; set; }
            public string wh_code { get; set; }
            public string wh_name { get; set; }
            public string location { get; set; }
            public int priority { get; set; }
            public decimal ratio { get; set; }
            public int fixed_days { get; set; }
            public string status { get; set; }
            public DateTime data_dt { get; set; }
        }
        public class VENDERMSTR_T
        {
            public string set_code { get; set; }
            public string vender { get; set; }
            public string vender_abbre { get; set; }
            public string vender_n { get; set; }
        }
        public class CALENDAR_T
        {
            public string set_code { get; set; }
            public string h_type { get; set; }
            public string vender { get; set; }
            public DateTime h_date { get; set; }
        }
        public class PLAN_T
        {
            public string set_code { get; set; }
            public string wcno { get; set; }
            public string prdymd { get; set; }
            public string model { get; set; }
            public decimal fgqty { get; set; }
        }
        public class BOM_T
        {
            public string set_code { get; set; }
            public string wcno { get; set; }
            public string model { get; set; }
            public string prdymd { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string vender { get; set; }
            public string vender_n { get; set; }
            public decimal plan_qty { get; set; }
            public decimal useage_qty { get; set; }
            public decimal req_qty { get; set; }

        }
        public class SUMBOM_T
        {
            public string set_code { get; set; }
            public string prdymd { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string vender { get; set; }
            public string vender_n { get; set; }
            public decimal plan_qty { get; set; }
        }
        public class CALQTY_T
        {
            public int rev { get; set; }
            public int lrev { get; set; }
            public string set_code { get; set; }
            public string prdymd { get; set; }
            public DateTime calqty_date { get; set; }
            public string vender { get; set; }
            public string vender_n { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string whno { get; set; }
            public string whcode { get; set; }
            public string whname { get; set; }
            public int priority { get; set; }
            public decimal ratio { get; set; }
            public decimal boxqty { get; set; }
            public decimal palletqty { get; set; }
            public decimal safetyqty { get; set; }
            public decimal minqty { get; set; }
            public decimal maxqty { get; set; }
            public decimal planqty { get; set; }
            public decimal consumtionqty { get; set; }
            public decimal calqtyresult { get; set; }
            public decimal stk_inhouse_bf { get; set; }
            public decimal stk_inhouse_af { get; set; }
            public decimal stk_wh_bf { get; set; }
            public decimal stk_wh_af { get; set; }
            public DateTime data_dt { get; set; }
            public string remark1 { get; set; }
            public string remark2 { get; set; }
            public string remark3 { get; set; }
        }
        public class CALDODATE_T
        {
            public int rev { get; set; }
            public int lrev { get; set; }
            public string set_code { get; set; }
            public string prdymd { get; set; }
            public DateTime calqty_date { get; set; }
            public DateTime caldod_date { get; set; }
            public string vender { get; set; }
            public string vender_n { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string whno { get; set; }
            public string whcode { get; set; }
            public string whname { get; set; }
            public decimal planqty { get; set; }
            public decimal consumtionqty { get; set; }
            public decimal calqty_result { get; set; }
            public decimal caldod_result { get; set; }
            public DateTime data_dt { get; set; }
            public string remark1 { get; set; }
            public string remark2 { get; set; }
            public string remark3 { get; set; }

        }
        // fixed plan, actual plan , doplan
        public class DOPLAN_T
        {
            public int rev { get; set; }
            public int lrev { get; set; }
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
            public decimal picklist { get; set; }
            public DateTime recive_dt { get; set; }
            public decimal recive_qty { get; set; }
            public string mark_status { get; set; }
            public DateTime data_dt { get; set; }
            public string remark1 { get; set; }
            public string remark2 { get; set; }
            public string remark3 { get; set; }

        }
        public class GRPPLAN
        {
            public string prdymd { get; set; }
            public DateTime calqty_date { get; set; }
            public string vender { get; set; }
            public string vender_n { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string whno { get; set; }
            public string whcode { get; set; }
            public string whname { get; set; }
            public decimal planqty { get; set; }
            public decimal consumtionqty { get; set; }
            public decimal calqty_result { get; set; }
        }
        public class SUMDOQTY
        {
            public string setcode { get; set; }
            public string vender { get; set; }
            public string vender_n { get; set; }
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public DateTime caldod_date { get; set; }
            public decimal caldod_result { get; set; }
        }
        public class DCIHOLIDAY_T
        {
            public string TYPE { get; set; }
            public DateTime DCI_HOLIDAY_DATE { get; set; }
        }
        public class VDHOLIDAY_T
        {
            public string TYPE { get; set; }
            public DateTime VD_HOLIDAY_DATE { get; set; }
            public string VENDER { get; set; }
        }
    }
}
