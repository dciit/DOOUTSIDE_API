using System.Runtime.InteropServices;

namespace API.Model
{
    public class ModelMaster
    {
        public class dbResult
        {
            public string status { get; set; }
            public string msg { get; set; }
        }
        public class partmstr
        {
            public string partno { get; set; }
            public string cm { get; set; }
            public string partname { get; set; }
            public string vender_c { get; set; }
            public string vender_n { get; set; }
            public string vender_s { get; set; }
            public decimal sfstk_qty { get; set; }
            public decimal mark_qty { get; set; }
            public decimal min_qty { get; set; }
            public decimal max_qty { get; set; }
            public decimal qty_box { get; set; }
            public decimal box_pl { get; set; }
            public int truck_stack { get; set; }
            public int pallet_stack { get; set; }
            public int pd_lt { get; set; }
            public int preorder_days { get; set; }
            public string typecal { get; set; }
            public string mark_status { get; set; }
            public string status { get; set; }
            public string update_by { get; set; }
            public DateTime update_dt { get; set; }
        }
        public class paramPartmstr
        {
            public string old_partno { get; set; }
            public string old_cm { get; set; }
            public string old_partname { get; set; }
            public string old_vender_c { get; set; }
            public int old_sfstk_qty { get; set; }
            public int old_mark_qty { get; set; }
            public int old_min_qty { get; set; }
            public int old_max_qty { get; set; }
            public int old_qty_box { get; set; }
            public int old_box_pl { get; set; }
            public int old_truck_stack { get; set; }
            public int old_pallet_stack { get; set; }
            public int old_pd_lt { get; set; }
            public int old_preorder_days { get; set; }
            public string old_typecal { get; set; }
            public string old_mark_status { get; set; }
            public string old_status { get; set; }
            public string old_update_by { get; set; }
            public string old_update_dt { get; set; }

            public string new_partno { get; set; }
            public string new_cm { get; set; }
            public string new_partname { get; set; }
            public string new_vender_c { get; set; }
            public int new_sfstk_qty { get; set; }
            public int new_mark_qty { get; set; }
            public int new_min_qty { get; set; }
            public int new_max_qty { get; set; }
            public int new_qty_box { get; set; }
            public int new_box_pl { get; set; }
            public int new_truck_stack { get; set; }
            public int new_pallet_stack { get; set; }
            public int new_pd_lt { get; set; }
            public int new_preorder_days { get; set; }
            public string new_typecal { get; set; }
            public string new_mark_status { get; set; }
            public string new_status { get; set; }
            public string new_update_by { get; set; }
            public string new_update_dt { get; set; }
        }
        public class calendarmstr
        {
            public int id { get; set; }
            public string type { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public DateTime dateoff { get; set; }
            public string desc { get; set; }
            public string update_by { get; set; }
            public DateTime update_dt { get; set; }

        }
        public class paramCalftt
        {
            public string start { get; set; }
            public string end { get; set; }
            public string desc { get; set; }
            public string empcode { get; set; }
        }
        public class whoutsite
        {
            public int id { get; set; }
            public string vender_c { get; set; }
            public string wh_n { get; set; }
            public string whno { get; set; }
            public int fixeddays { get; set; }
            public string update_by { get; set; }
            public DateTime update_dt { get; set; }
        }
        public class paramWhoutsite
        {
            public int old_id { get; set; }
            public string old_vender_c { get; set; }
            public string old_wh_n { get; set; }
            public string old_whno { get; set; }
            public int old_fixeddays { get; set; }
            public string old_update_by { get; set; }
            public string old_update_dt { get; set; }

            public int new_id { get; set; }
            public string new_vender_c { get; set; }
            public string new_wh_n { get; set; }
            public string new_whno { get; set; }
            public int new_fixeddays { get; set; }
            public string new_update_by { get; set; }
            public string new_update_dt { get; set; }
        }
        public class delcycle
        {
            public string vender { get; set; }
            public string deltype { get; set; }
            public Boolean wk_sun { get; set; }
            public Boolean wk_mon { get; set; }
            public Boolean wk_tue { get; set; }
            public Boolean wk_wed { get; set; }
            public Boolean wk_thu { get; set; }
            public Boolean wk_fri { get; set; }
            public Boolean wk_sat { get; set; }
            public Boolean mo_01 { get; set; }
            public Boolean mo_02 { get; set; }
            public Boolean mo_03 { get; set; }
            public Boolean mo_04 { get; set; }
            public Boolean mo_05 { get; set; }
            public Boolean mo_06 { get; set; }
            public Boolean mo_07 { get; set; }
            public Boolean mo_08 { get; set; }
            public Boolean mo_09 { get; set; }
            public Boolean mo_10 { get; set; }
            public Boolean mo_11 { get; set; }
            public Boolean mo_12 { get; set; }
            public Boolean mo_13 { get; set; }
            public Boolean mo_14 { get; set; }
            public Boolean mo_15 { get; set; }
            public Boolean mo_16 { get; set; }
            public Boolean mo_17 { get; set; }
            public Boolean mo_18 { get; set; }
            public Boolean mo_19 { get; set; }
            public Boolean mo_20 { get; set; }
            public Boolean mo_21 { get; set; }
            public Boolean mo_22 { get; set; }
            public Boolean mo_23 { get; set; }
            public Boolean mo_24 { get; set; }
            public Boolean mo_25 { get; set; }
            public Boolean mo_26 { get; set; }
            public Boolean mo_27 { get; set; }
            public Boolean mo_28 { get; set; }
            public Boolean mo_29 { get; set; }
            public Boolean mo_30 { get; set; }
            public Boolean mo_31 { get; set; }
            public string update_by { get; set; }
            public DateTime update_date { get; set; }
        }
        public class paramDelcycle
        {
            public string old_venderc { get; set; }
            public string old_deltype { get; set; }
            public bool old_delsun { get; set; }
            public bool old_delmon { get; set; }
            public bool old_deltue { get; set; }
            public bool old_delwed { get; set; }
            public bool old_delthu { get; set; }
            public bool old_delfri { get; set; }
            public bool old_delsat { get; set; }
            public string old_updateby { get; set; }
            public string old_updatedt { get; set; }

            public string new_venderc { get; set; }
            public string new_deltype { get; set; }
            public bool new_delsun { get; set; }
            public bool new_delmon { get; set; }
            public bool new_deltue { get; set; }
            public bool new_delwed { get; set; }
            public bool new_delthu { get; set; }
            public bool new_delfri { get; set; }
            public bool new_delsat { get; set; }
            public string new_updateby { get; set; }
            public string new_updatedt { get; set; }
        }
        public class dict
        {
            public int Dict_Code { get; set; }
            public string Dict_Type { get; set; }
            public string Dict_Ref1 { get; set; }
            public string Dict_Ref2 { get; set; }
            public string Dict_Ref3 { get; set; }
            public string Dict_Ref4 { get; set; }
            public string Dict_Ref5 { get; set; }
            public string Dict_Value1 { get; set; }
            public string Dict_Value2 { get; set; }
            public string Dict_Value3 { get; set; }
            public string Dict_Value4 { get; set; }
            public string Dict_Value5 { get; set; }
            public string Status { get; set; }
            public string Update_By { get; set; }
            public DateTime Update_DT { get; set; }
        }
        public class rangecal
        {
            public int preday { get; set; }
            public int fcday { get; set; }
        }
    }
}
