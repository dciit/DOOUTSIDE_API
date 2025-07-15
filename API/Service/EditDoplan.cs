using System.Data.SqlClient;
using System.Data;
using static API.Model.ModelTransaction;
using API.Controllers;
using static API.Model.ModelDoplan;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using static API.Model.ModelMaster;
using System.Runtime.Intrinsics.Arm;
using static API.Model.modelLog;
using Microsoft.AspNetCore.Components;

namespace API.Service
{
    public class EditDoplan
    {
        SqlConnectDB conDBSCM = new("DBSCM");
        RunNumberService svrRunning = new RunNumberService();

        // callmaster
        public SETCONTROL_T GET_SETCONTROL(string SET_CODE)
        {
            string date = "1901-01-01";
            DateTime DT = Convert.ToDateTime(date);

            SETCONTROL_T oSETCON = new SETCONTROL_T();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
                  SELECT   [SET_CODE]
                          ,[SET_DATE]
                          ,[SET_BY]
                          ,[SET_STDATE]
                          ,[SET_ENDATE]
                      FROM [dbSCM].[dbo].[DOOUT_0_SETCONTROL]
                      WHERE [SET_CODE] = @SET_CODE";
            sqlSelectFindSetcontrol.Parameters.Add(new SqlParameter("@SET_CODE", SET_CODE));

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);
            if (dtgelSK.Rows.Count > 0)
            {
                oSETCON.set_code = (dtgelSK.Rows[0]["SET_CODE"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["SET_CODE"].ToString();
                oSETCON.set_date = (dtgelSK.Rows[0]["SET_DATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dtgelSK.Rows[0]["SET_DATE"]);
                oSETCON.set_by = (dtgelSK.Rows[0]["SET_BY"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["SET_BY"].ToString();
                oSETCON.set_st_dt = (dtgelSK.Rows[0]["SET_STDATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dtgelSK.Rows[0]["SET_STDATE"]);
                oSETCON.set_en_dt = (dtgelSK.Rows[0]["SET_ENDATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dtgelSK.Rows[0]["SET_ENDATE"]);
            }
            return oSETCON;
        }
        public List<DELIVERYCYCLE_T> GET_DELIVERYCYCLE(string SET_CODE)
        {
            List<DELIVERYCYCLE_T> listDeliveryCycle = new List<DELIVERYCYCLE_T>();

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @$"
            SELECT [SET_CODE],[VENDER],[DEL_TYPE],[DEL_WK_SUN],[DEL_WK_MON],[DEL_WK_TUE],[DEL_WK_WED],[DEL_WK_THU],[DEL_WK_FRI],[DEL_WK_SAT],[DEL_MO_01],[DEL_MO_02],[DEL_MO_03],[DEL_MO_04],
            [DEL_MO_05],[DEL_MO_06],[DEL_MO_07],[DEL_MO_08],[DEL_MO_09],[DEL_MO_10],[DEL_MO_11],[DEL_MO_12],[DEL_MO_13],[DEL_MO_14],[DEL_MO_15],[DEL_MO_16],[DEL_MO_17],[DEL_MO_18],[DEL_MO_19],
            [DEL_MO_20],[DEL_MO_21],[DEL_MO_22],[DEL_MO_23],[DEL_MO_24],[DEL_MO_25],[DEL_MO_26],[DEL_MO_27],[DEL_MO_28],[DEL_MO_29],[DEL_MO_30],[DEL_MO_31]
            FROM [dbSCM].[dbo].[DOOUT_1_1DELIVERY_CYCLE] WHERE [SET_CODE] = '{SET_CODE}' AND [VENDER] = 'SG1887'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    DELIVERYCYCLE_T deliveryCycle = new DELIVERYCYCLE_T();
                    deliveryCycle.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    deliveryCycle.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    deliveryCycle.del_type = (dr["DEL_TYPE"] == DBNull.Value) ? "" : dr["DEL_TYPE"].ToString();
                    deliveryCycle.del_wk_sun = (dr["DEL_WK_SUN"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_SUN"]);
                    deliveryCycle.del_wk_mon = (dr["DEL_WK_MON"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_MON"]);
                    deliveryCycle.del_wk_tue = (dr["DEL_WK_TUE"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_TUE"]);
                    deliveryCycle.del_wk_wed = (dr["DEL_WK_WED"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_WED"]);
                    deliveryCycle.del_wk_thu = (dr["DEL_WK_THU"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_THU"]);
                    deliveryCycle.del_wk_fri = (dr["DEL_WK_FRI"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_FRI"]);
                    deliveryCycle.del_wk_sat = (dr["DEL_WK_SAT"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_SAT"]);
                    deliveryCycle.del_mo_01 = (dr["DEL_MO_01"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_01"]);
                    deliveryCycle.del_mo_02 = (dr["DEL_MO_02"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_02"]);
                    deliveryCycle.del_mo_03 = (dr["DEL_MO_03"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_03"]);
                    deliveryCycle.del_mo_04 = (dr["DEL_MO_04"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_04"]);
                    deliveryCycle.del_mo_05 = (dr["DEL_MO_05"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_05"]);
                    deliveryCycle.del_mo_06 = (dr["DEL_MO_06"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_06"]);
                    deliveryCycle.del_mo_07 = (dr["DEL_MO_07"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_07"]);
                    deliveryCycle.del_mo_08 = (dr["DEL_MO_08"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_08"]);
                    deliveryCycle.del_mo_09 = (dr["DEL_MO_09"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_09"]);
                    deliveryCycle.del_mo_10 = (dr["DEL_MO_10"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_10"]);
                    deliveryCycle.del_mo_11 = (dr["DEL_MO_11"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_11"]);
                    deliveryCycle.del_mo_12 = (dr["DEL_MO_12"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_12"]);
                    deliveryCycle.del_mo_13 = (dr["DEL_MO_13"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_13"]);
                    deliveryCycle.del_mo_14 = (dr["DEL_MO_14"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_14"]);
                    deliveryCycle.del_mo_15 = (dr["DEL_MO_15"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_15"]);
                    deliveryCycle.del_mo_16 = (dr["DEL_MO_16"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_16"]);
                    deliveryCycle.del_mo_17 = (dr["DEL_MO_17"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_17"]);
                    deliveryCycle.del_mo_18 = (dr["DEL_MO_18"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_18"]);
                    deliveryCycle.del_mo_19 = (dr["DEL_MO_19"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_19"]);
                    deliveryCycle.del_mo_20 = (dr["DEL_MO_20"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_20"]);
                    deliveryCycle.del_mo_21 = (dr["DEL_MO_21"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_21"]);
                    deliveryCycle.del_mo_22 = (dr["DEL_MO_22"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_22"]);
                    deliveryCycle.del_mo_23 = (dr["DEL_MO_23"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_23"]);
                    deliveryCycle.del_mo_24 = (dr["DEL_MO_24"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_24"]);
                    deliveryCycle.del_mo_25 = (dr["DEL_MO_25"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_25"]);
                    deliveryCycle.del_mo_26 = (dr["DEL_MO_26"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_26"]);
                    deliveryCycle.del_mo_27 = (dr["DEL_MO_27"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_27"]);
                    deliveryCycle.del_mo_28 = (dr["DEL_MO_28"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_28"]);
                    deliveryCycle.del_mo_29 = (dr["DEL_MO_29"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_29"]);
                    deliveryCycle.del_mo_30 = (dr["DEL_MO_30"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_30"]);
                    deliveryCycle.del_mo_31 = (dr["DEL_MO_31"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_31"]);

                    listDeliveryCycle.Add(deliveryCycle);
                }
            }

            return listDeliveryCycle;
        }

        public List<PARTMSTR_T> GET_PARTMSTR(string SET_CODE, string PARTNO, string CM)
        {
            List<PARTMSTR_T> listPartmstr = new List<PARTMSTR_T>();

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @$"
            SELECT [SET_CODE],[PARTNO],[CM],[PARTNAME],[VENDER],[SFSTK_QTY],[WIP_QTY],[MARK_QTY],[MIN_QTY],[MAX_QTY],[QTY_BOX],[BOX_PL],[TRUCK_STACK],[PALLET_STACK],[PD_LT],
            [PREORDER_DAYS],[STORE_WH1],[STORE_WH2],[STORE_WHB],[MARK_STATUS]
            FROM [dbSCM].[dbo].[DOOUT_1_2PART_MSTR] WHERE [SET_CODE] = '{SET_CODE}' AND [PARTNO] = '{PARTNO}' AND [CM] = '{CM}'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    PARTMSTR_T partmstr = new PARTMSTR_T();
                    partmstr.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    partmstr.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                    partmstr.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                    partmstr.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                    partmstr.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    partmstr.sfstk_qty = (dr["SFSTK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SFSTK_QTY"]);
                    partmstr.wip_qty = (dr["WIP_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["WIP_QTY"]);
                    partmstr.mark_qty = (dr["MARK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MARK_QTY"]);
                    partmstr.min_qty = (dr["MIN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MIN_QTY"]);
                    partmstr.max_qty = (dr["MAX_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MAX_QTY"]);
                    partmstr.qty_box = (dr["QTY_BOX"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_BOX"]);
                    partmstr.box_pl = (dr["BOX_PL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BOX_PL"]);
                    partmstr.truck_stack = (dr["TRUCK_STACK"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TRUCK_STACK"]);
                    partmstr.pallet_stack = (dr["PALLET_STACK"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PALLET_STACK"]);
                    partmstr.pd_lt = (dr["PD_LT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PD_LT"]);
                    partmstr.preorder_days = (dr["PREORDER_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PREORDER_DAYS"]);
                    partmstr.store_wh1 = (dr["STORE_WH1"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STORE_WH1"]);
                    partmstr.store_wh2 = (dr["STORE_WH2"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STORE_WH2"]);
                    partmstr.store_whb = (dr["STORE_WHB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STORE_WHB"]);
                    partmstr.mark_status = (dr["MARK_STATUS"] == DBNull.Value) ? "" : dr["MARK_STATUS"].ToString();

                    listPartmstr.Add(partmstr);
                }
            }

            return listPartmstr;
        }

        public List<WHOUTSIDE_T> GET_WHMSTR(string SET_CODE)
        {
            List<WHOUTSIDE_T> listwhoutside = new List<WHOUTSIDE_T>();

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @$"
            SELECT [SET_CODE],[WH_OUT_CODE],[WH_OUT_NAME],[LOCATION],[PRIORITY],[RATIO],[FIXED_DAYS],[STATUS]
            FROM [dbSCM].[dbo].[DOOUT_1_3WH_OUTSITE_MSTR] WHERE [SET_CODE] = '{SET_CODE}' AND [WH_OUT_CODE] = 'SG1887'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    WHOUTSIDE_T whoutside = new WHOUTSIDE_T();
                    whoutside.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    whoutside.wh_code = (dr["WH_OUT_CODE"] == DBNull.Value) ? "" : dr["WH_OUT_CODE"].ToString();
                    whoutside.wh_name = (dr["WH_OUT_NAME"] == DBNull.Value) ? "" : dr["WH_OUT_NAME"].ToString();
                    whoutside.location = (dr["LOCATION"] == DBNull.Value) ? "" : dr["LOCATION"].ToString();
                    whoutside.priority = (dr["PRIORITY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PRIORITY"]);
                    whoutside.ratio = (dr["RATIO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RATIO"]);
                    whoutside.fixed_days = (dr["FIXED_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["FIXED_DAYS"]);
                    whoutside.status = (dr["STATUS"] == DBNull.Value) ? "" : dr["STATUS"].ToString();


                    listwhoutside.Add(whoutside);
                }
            }

            return listwhoutside;
        }

        public List<VENDERMSTR_T> GET_VDMSTR(string SET_CODE)
        {
            List<VENDERMSTR_T> listvendermstr = new List<VENDERMSTR_T>();

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @$"
            SELECT [SET_CODE],[VENDER],[VENDER_Abbre],[VENDER_N]
            FROM [dbSCM].[dbo].[DOOUT_1_4VENDER_MSTR]
            WHERE [SET_CODE] = '{SET_CODE}'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    VENDERMSTR_T vendermstr = new VENDERMSTR_T();
                    vendermstr.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    vendermstr.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    vendermstr.vender_abbre = (dr["VENDER_Abbre"] == DBNull.Value) ? "" : dr["VENDER_Abbre"].ToString();
                    vendermstr.vender_n = (dr["VENDER_N"] == DBNull.Value) ? "" : dr["VENDER_N"].ToString();


                    listvendermstr.Add(vendermstr);
                }
            }

            return listvendermstr;
        }

        public List<CALENDAR_T> GET_CALENDAR(string SET_CODE)
        {
            List<CALENDAR_T> listcalendar = new List<CALENDAR_T>();

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @$"
            SELECT [SET_CODE],[H_TYPE],[VENDER],[H_DATE]
            FROM [dbSCM].[dbo].[DOOUT_1_5CALENDAR]
            WHERE [SET_CODE] = '{SET_CODE}' AND [VENDER] = 'SG1887'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    CALENDAR_T calendar = new CALENDAR_T();
                    calendar.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    calendar.h_type = (dr["H_TYPE"] == DBNull.Value) ? "" : dr["H_TYPE"].ToString();
                    calendar.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    calendar.h_date = (dr["H_DATE"] == DBNull.Value) ? dt : Convert.ToDateTime(dr["H_DATE"]);
                    listcalendar.Add(calendar);
                }
            }

            return listcalendar;
        }

        public List<DOPLAN_T> GET_FIXED_DOPLAN(string SET_CODE, string PARTNO, string CM, string VENDER, int fixedday)
        {
            List<DOPLAN_T> listdoplan = new List<DOPLAN_T>();

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);
            DateTime stdate = DateTime.Now;
            DateTime endate = stdate.AddDays(fixedday - 1);


            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @$"
            SELECT [REV],[LREV],[NBR],[SET_CODE],[PRDYMD],[DO_DATE],[VENDER],[VENDER_N],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PLAN_QTY],[CONSUMTION_QTY],[CAL_QTY],[CALDOD_QTY],[MARK_QTY],[DO_QTY],[STK_INHOUSE_BF],[STK_INHOUSE_AF],[STK_WH_BF],[STK_WH_AF],[PICKLIST],[RECIVE_DT],[RECIVE_QTY],[VDTOWHB_DT],[VDTOWHB_QTY],[DATA_DT],[REMARK1],[REMARK2],[REMARK3]
            FROM [dbSCM].[dbo].[DOOUT_DOPLAN]
            WHERE SET_CODE = '{SET_CODE}' and LREV = 999 and PARTNO = '{PARTNO}' and CM = '{CM}' and VENDER = '{VENDER}' and DO_DATE between '{stdate.ToString("yyyy-MM-dd")}' and '{endate.ToString("yyyy-MM-dd")}'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    DOPLAN_T doplan = new DOPLAN_T();
                    doplan.rev = (dr["REV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["REV"]);
                    doplan.lrev = (dr["LREV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LREV"]);
                    doplan.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    doplan.prdymd = (dr["PRDYMD"] == DBNull.Value) ? "" : dr["PRDYMD"].ToString();
                    doplan.do_date = (dr["DO_DATE"] == DBNull.Value) ? dt : Convert.ToDateTime(dr["DO_DATE"]);
                    doplan.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    doplan.vender_n = (dr["VENDER_N"] == DBNull.Value) ? "" : dr["VENDER_N"].ToString();
                    doplan.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                    doplan.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                    doplan.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                    doplan.whno = (dr["WHNO"] == DBNull.Value) ? "" : dr["WHNO"].ToString();
                    doplan.whcode = (dr["WHCODE"] == DBNull.Value) ? "" : dr["WHCODE"].ToString();
                    doplan.whname = (dr["WHNAME"] == DBNull.Value) ? "" : dr["WHNAME"].ToString();
                    doplan.plan_qty = (dr["PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_QTY"]);
                    doplan.consumtion_qty = (dr["CONSUMTION_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONSUMTION_QTY"]);
                    doplan.cal_qty = (dr["CAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_QTY"]);
                    doplan.mark_qty = (dr["MARK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MARK_QTY"]);
                    doplan.do_qty = (dr["DO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DO_QTY"]);
                    doplan.stk_inhouse_bf = (dr["STK_INHOUSE_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_BF"]);
                    doplan.stk_inhouse_af = (dr["STK_INHOUSE_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_AF"]);
                    doplan.stk_wh_bf = (dr["STK_WH_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_BF"]);
                    doplan.stk_wh_af = (dr["STK_WH_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_AF"]);
                    doplan.picklist = (dr["PICKLIST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PICKLIST"]);
                    doplan.recive_dt = (dr["RECIVE_DT"] == DBNull.Value) ? dt : Convert.ToDateTime(dr["RECIVE_DT"]);
                    doplan.recive_qty = (dr["RECIVE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RECIVE_QTY"]);
                    doplan.remark1 = (dr["REMARK1"] == DBNull.Value) ? "" : dr["REMARK1"].ToString();
                    doplan.remark2 = (dr["REMARK2"] == DBNull.Value) ? "" : dr["REMARK2"].ToString();
                    doplan.remark3 = (dr["REMARK3"] == DBNull.Value) ? "" : dr["REMARK3"].ToString();

                    listdoplan.Add(doplan);
                }
            }

            return listdoplan;
        }

        public List<SUMBOM_T> GET_SUMBOM(string SET_CODE)
        {
            List<SUMBOM_T> listsumbom = new List<SUMBOM_T>();

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @$"
            SELECT [SET_CODE],[PRDYMD],[PARTNO],[CM],[PARTNAME],[VENDER],[VENDER_NAME],[PLAN_QTY]
            FROM [dbSCM].[dbo].[DOOUT_3_SUM_BOM]
            WHERE [SET_CODE] = '{SET_CODE}'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    SUMBOM_T sumbom = new SUMBOM_T();
                    sumbom.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    sumbom.prdymd = (dr["PRDYMD"] == DBNull.Value) ? "" : dr["PRDYMD"].ToString();
                    sumbom.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                    sumbom.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                    sumbom.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                    sumbom.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    sumbom.vender_n = (dr["VENDER_NAME"] == DBNull.Value) ? "" : dr["VENDER_NAME"].ToString();
                    sumbom.plan_qty = (dr["PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_QTY"]);

                    listsumbom.Add(sumbom);
                }
            }

            return listsumbom;
        }

        // cal doqty 
        public void CalEditDOPLAN(DOPLANKEY EditKey)
        {
            // Call Master in Transaction 
            SETCONTROL_T SET_CON_T = GET_SETCONTROL(EditKey.setcode);
            List<DELIVERYCYCLE_T> deliveryMstr = GET_DELIVERYCYCLE(EditKey.setcode);
            List<PARTMSTR_T> PART_MSTR = GET_PARTMSTR(EditKey.setcode, EditKey.partno, EditKey.cm);


            List<WHOUTSIDE_T> whmstr = GET_WHMSTR(EditKey.setcode);
            WHOUTSIDE_T MASTER_WH = whmstr.Where(s => s.location == "WHB").FirstOrDefault();
            List<VENDERMSTR_T> VENDER_MSTR = GET_VDMSTR(EditKey.setcode);
            List<CALENDAR_T> calendarmstr = GET_CALENDAR(EditKey.setcode);
            List<DOPLAN_T> FIXED_DO_PLAN = GET_FIXED_DOPLAN(EditKey.setcode, EditKey.partno, EditKey.cm, EditKey.vender, MASTER_WH.fixed_days);
            List<SUMBOM_T> SUM_BOM = GET_SUMBOM(EditKey.setcode);

            List<VDHOLIDAY_T> oVENDOR_HOLIDAYs = calendarmstr.Where(s => s.h_type == "VENDER_H").Select(s => new VDHOLIDAY_T
            {
                TYPE = s.h_type,
                VD_HOLIDAY_DATE = s.h_date,
                VENDER = s.vender
            }).ToList();

            List<DCIHOLIDAY_T> oDCI_HOLIDAYs = calendarmstr.Where(s => s.h_type == "DCI").Select(s => new DCIHOLIDAY_T
            {
                TYPE = s.h_type,
                DCI_HOLIDAY_DATE = s.h_date
            }).ToList();

            // variable intial 
            decimal PLANQTY = 0, consumption = 0;
            int rev = FIXED_DO_PLAN.Where(s => s.lrev == 999).Select(s => s.rev).FirstOrDefault();


            // ====================== cal qty ======================
            List<CALQTY_T> oDO_QTYs = new List<CALQTY_T>();

            foreach (var oVENDER_PART in PART_MSTR.Where(s => s.partno == EditKey.partno && s.cm == EditKey.cm && s.vender == EditKey.vender))
            {
                VENDERMSTR_T VENDER = VENDER_MSTR.Where(s => s.vender.Trim() == oVENDER_PART.vender.Trim()).FirstOrDefault();
                PARTMSTR_T oPARTMSTR = PART_MSTR.FirstOrDefault(s => s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim());
                decimal STINSim = oPARTMSTR.store_wh1 + oPARTMSTR.store_wh2;
                decimal STOUTSim = oVENDER_PART.store_whb;
                decimal WIPST = oPARTMSTR.wip_qty;

                // calculate do in fixed day
                List<DOPLAN_T> oFIXED_DO_VD_PARTs = FIXED_DO_PLAN.Where(s => s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim() && s.vender.Trim() == oVENDER_PART.vender.Trim()).ToList();
                if (oFIXED_DO_VD_PARTs.Count > 0)
                {
                    bool CHECKPLAN_FIXED = false;
                    foreach (var oFIXED_DO_VD_PART in oFIXED_DO_VD_PARTs)
                    {
                        PLANQTY = Math.Ceiling(SUM_BOM.Where(s => s.prdymd == oFIXED_DO_VD_PART.do_date.ToString("yyyyMMdd") && s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim()).Sum(g => g.plan_qty));
                        SUMBOM_T PLAN = SUM_BOM.Where(s => s.prdymd == oFIXED_DO_VD_PART.do_date.ToString("yyyyMMdd") && s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim()).FirstOrDefault();
                        CHECKPLAN_FIXED = SUM_BOM.Where(s => s.prdymd == oFIXED_DO_VD_PART.do_date.ToString("yyyyMMdd") && s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim()).Any();

                        PLANQTY = PLANQTY + WIPST;

                        CALQTY_T oDO_QTY = new CALQTY_T();
                        oDO_QTY.set_code = EditKey.setcode;
                        oDO_QTY.vender = oFIXED_DO_VD_PART.vender;
                        oDO_QTY.vender_n = oFIXED_DO_VD_PART.vender_n;
                        oDO_QTY.prdymd = (CHECKPLAN_FIXED) ? PLAN.prdymd : "19010101";
                        oDO_QTY.partno = oFIXED_DO_VD_PART.partno;
                        oDO_QTY.cm = oFIXED_DO_VD_PART.cm;
                        oDO_QTY.partname = oPARTMSTR.partname;

                        oDO_QTY.whno = MASTER_WH.location;
                        oDO_QTY.whcode = MASTER_WH.wh_code;
                        oDO_QTY.whname = MASTER_WH.wh_name;
                        oDO_QTY.priority = MASTER_WH.priority;
                        oDO_QTY.ratio = MASTER_WH.ratio;

                        oDO_QTY.planqty = PLANQTY;
                        oDO_QTY.boxqty = oPARTMSTR.qty_box;
                        oDO_QTY.palletqty = oPARTMSTR.box_pl;
                        oDO_QTY.safetyqty = oPARTMSTR.sfstk_qty;
                        oDO_QTY.minqty = oPARTMSTR.min_qty;
                        oDO_QTY.maxqty = oPARTMSTR.max_qty;
                        oDO_QTY.calqtyresult = (oFIXED_DO_VD_PART.do_date.ToString("yyyy-MM-dd") == EditKey.dodate.ToString("yyyy-MM-dd")) ? EditKey.doqtyNew : oFIXED_DO_VD_PART.do_qty;

                        oDO_QTY.stk_inhouse_bf = STINSim;
                        oDO_QTY.stk_inhouse_af = (oDO_QTY.stk_inhouse_bf - PLANQTY) + oDO_QTY.calqtyresult;
                        STINSim = oDO_QTY.stk_inhouse_af;

                        oDO_QTY.stk_wh_bf = STOUTSim;
                        oDO_QTY.stk_wh_af = oDO_QTY.stk_wh_bf - (oDO_QTY.calqtyresult);
                        STOUTSim = oDO_QTY.stk_wh_af;

                        oDO_QTY.calqty_date = oFIXED_DO_VD_PART.do_date;
                        oDO_QTY.data_dt = DateTime.Now;

                        oDO_QTY.remark1 = (CHECKPLAN_FIXED) ? "HAVE PLAN" : "NO PLAN";
                        oDO_QTY.remark2 = "Create By System";
                        oDO_QTY.remark3 = "New Calculate In Fixed Date";
                        WIPST = 0;

                        oDO_QTY.rev = rev + 1;
                        //string NBR = svrRunning.LoadUnique("DOOUT_CAL").ToString(true);
                        //svrRunning.NextId("DOOUT_CAL");

                        oDO_QTYs.Add(oDO_QTY);
                    }
                }

                // ------ calculate in forcast --------
                DateTime ST_DATE = SET_CON_T.set_date.AddDays(MASTER_WH.fixed_days);
                DateTime EN_DATE = SET_CON_T.set_en_dt;
                bool CHECKPLAN = false;
                while (ST_DATE.Date <= EN_DATE.Date)
                {

                    PLANQTY = Math.Ceiling(SUM_BOM.Where(s => s.prdymd == ST_DATE.Date.ToString("yyyyMMdd") && s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim()).Sum(g => g.plan_qty));
                    SUMBOM_T PLAN = SUM_BOM.Where(s => s.prdymd == ST_DATE.Date.ToString("yyyyMMdd") && s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim()).FirstOrDefault();
                    CHECKPLAN = SUM_BOM.Where(s => s.prdymd == ST_DATE.Date.ToString("yyyyMMdd") && s.partno.Trim() == oVENDER_PART.partno.Trim() && s.cm.Trim() == oVENDER_PART.cm.Trim()).Any();
                    consumption = (CHECKPLAN) ? STINSim - PLANQTY : STINSim - oPARTMSTR.sfstk_qty;

                    CALQTY_T oDO_QTY = new CALQTY_T();
                    oDO_QTY.set_code = EditKey.setcode;
                    oDO_QTY.vender = VENDER.vender;
                    oDO_QTY.vender_n = VENDER.vender_n;
                    oDO_QTY.prdymd = (CHECKPLAN) ? PLAN.prdymd : "19010101";
                    oDO_QTY.calqty_date = Convert.ToDateTime(ST_DATE.Date);
                    oDO_QTY.partno = oVENDER_PART.partno;
                    oDO_QTY.cm = oVENDER_PART.cm;
                    oDO_QTY.partname = oPARTMSTR.partname;

                    oDO_QTY.whno = MASTER_WH.location;
                    oDO_QTY.whcode = MASTER_WH.wh_code;
                    oDO_QTY.whname = MASTER_WH.wh_name;
                    oDO_QTY.priority = MASTER_WH.priority;
                    oDO_QTY.ratio = MASTER_WH.ratio;

                    oDO_QTY.planqty = PLANQTY;
                    oDO_QTY.boxqty = oPARTMSTR.qty_box;
                    oDO_QTY.palletqty = oPARTMSTR.box_pl;
                    oDO_QTY.safetyqty = oPARTMSTR.sfstk_qty;
                    oDO_QTY.minqty = oPARTMSTR.min_qty;
                    oDO_QTY.maxqty = oPARTMSTR.max_qty;

                    if (consumption < 0)
                    {
                        oDO_QTY.consumtionqty = Math.Abs(consumption);
                        oDO_QTY.calqtyresult = Math.Abs(consumption);
                        if (oDO_QTY.boxqty > 0)
                        {
                            oDO_QTY.calqtyresult = Math.Ceiling((oDO_QTY.calqtyresult / oDO_QTY.boxqty)) * oDO_QTY.boxqty;
                        }

                        if (oDO_QTY.calqtyresult < oDO_QTY.minqty)
                        {
                            oDO_QTY.calqtyresult = oDO_QTY.minqty;
                            oDO_QTY.remark3 += "ยอดความต้องการน้อยกว่าจำนวนการสั่งซื้อขั้นต่ำ";
                        }

                        if (oDO_QTY.calqtyresult > oDO_QTY.maxqty)
                        {
                            oDO_QTY.calqtyresult = oDO_QTY.maxqty;
                            oDO_QTY.remark3 += "ยอดความต้องการมากกว่าจำนวนการสั่งซื้อสูงสุด";
                        }

                        //if (oDO_QTY.calqtyresult > STOUTSim)
                        //{
                        //    if (oDO_QTY.calqtyresult == 0)
                        //    {
                        //        oDO_QTY.calqtyresult = 0;
                        //    }
                        //    else
                        //    {
                        //        oDO_QTY.calqtyresult = STOUTSim;
                        //    }
                        //    oDO_QTY.remark3 += "WH OUTSITE มีจำนวนน้อยกว่าความต้องการ";
                        //}
                        oDO_QTY.remark3 += (CHECKPLAN) ? "(ออกความต้องการมาจากแผน)" : "(ออกความต้องการจาก Safety Stock)";
                    }
                    else
                    {
                        oDO_QTY.calqtyresult = 0;
                        oDO_QTY.remark3 = "มีจำนวน Stock เพียงพอไม่ออก D/O";
                    }


                    oDO_QTY.stk_inhouse_bf = STINSim;
                    oDO_QTY.stk_inhouse_af = (oDO_QTY.stk_inhouse_bf - PLANQTY) + oDO_QTY.calqtyresult;
                    STINSim = oDO_QTY.stk_inhouse_af;

                    oDO_QTY.stk_wh_bf = STOUTSim;
                    oDO_QTY.stk_wh_af = oDO_QTY.stk_wh_bf - oDO_QTY.calqtyresult;
                    STOUTSim = oDO_QTY.stk_wh_af;

                    oDO_QTY.data_dt = DateTime.Now;
                    oDO_QTY.remark1 = (CHECKPLAN) ? "HAVE PLAN" : "NO PLAN";
                    oDO_QTY.remark2 = "Create By System";

                    //string NBR = svrRunning.LoadUnique("DOOUT_CAL").ToString(true);
                    //svrRunning.NextId("DOOUT_CAL");

                    oDO_QTY.rev = rev + 1;
                    oDO_QTYs.Add(oDO_QTY);

                    ST_DATE = ST_DATE.AddDays(1);
                }
            }
            oDO_QTYs = oDO_QTYs.OrderBy(x => x.partno)
                               .ThenBy(x => x.calqty_date)
                               .ToList();

            Console.WriteLine(oDO_QTYs);

            // ============ update to db  ==================
            UpdateRevisionCalqty(oDO_QTYs, rev, EditKey);


            // ====================== cal dodate ======================
            List<CALDODATE_T> oCALDODATEs = new List<CALDODATE_T>();
            CALDODATE_T oCALDODATE = new CALDODATE_T();
            DELIVERYCYCLE_T oDELIVERY_CYCLE = deliveryMstr.FirstOrDefault(s => s.vender == "SG1887");

            foreach (var oPARMSTR in PART_MSTR.Where(s => s.partno == EditKey.partno && s.cm == EditKey.cm && s.vender == EditKey.vender))
            {
                DateTime ST_DATE_FIXED = SET_CON_T.set_date;
                DateTime EN_DATE_FIXED = ST_DATE_FIXED.AddDays(MASTER_WH.fixed_days - 1);
                decimal temp = 0;
                List<CALQTY_T> oRESULT_DO_QTYs = oDO_QTYs.Where(s => s.partno.Trim() == oPARMSTR.partno.Trim() && s.cm.Trim() == oPARMSTR.cm.Trim() && s.vender.Trim() == oPARMSTR.vender.Trim()).ToList();

                foreach (var oRESULT_DO_QTY in oRESULT_DO_QTYs)
                {

                    // CHECK DATA IN FIXED DATE 
                    bool CHECK_IN_FIXED = (oRESULT_DO_QTY.calqty_date.Date >= Convert.ToDateTime(ST_DATE_FIXED).Date && oRESULT_DO_QTY.calqty_date.Date <= Convert.ToDateTime(EN_DATE_FIXED).Date);

                    if (CHECK_IN_FIXED)
                    {
                        oCALDODATE = new CALDODATE_T();
                        oCALDODATE.set_code = EditKey.setcode;
                        oCALDODATE.vender = oRESULT_DO_QTY.vender;
                        oCALDODATE.vender_n = oRESULT_DO_QTY.vender_n;
                        oCALDODATE.prdymd = oRESULT_DO_QTY.prdymd;
                        oCALDODATE.partno = oRESULT_DO_QTY.partno;
                        oCALDODATE.cm = oRESULT_DO_QTY.cm;
                        oCALDODATE.partname = oRESULT_DO_QTY.partname;
                        oCALDODATE.planqty = oRESULT_DO_QTY.planqty;
                        oCALDODATE.consumtionqty = oRESULT_DO_QTY.consumtionqty;
                        oCALDODATE.calqty_result = oRESULT_DO_QTY.calqtyresult;
                        oCALDODATE.calqty_date = oRESULT_DO_QTY.calqty_date;
                        oCALDODATE.whno = oRESULT_DO_QTY.whno;
                        oCALDODATE.whcode = oRESULT_DO_QTY.whcode;
                        oCALDODATE.whname = oRESULT_DO_QTY.whname;
                        oCALDODATE.caldod_date = oRESULT_DO_QTY.calqty_date;
                        oCALDODATE.caldod_result = oRESULT_DO_QTY.calqtyresult;


                        oCALDODATE.data_dt = DateTime.Now;
                        oCALDODATE.remark1 = "D/O PLAN In Fixed Date";
                        oCALDODATE.remark2 = "Create By System";
                        oCALDODATE.remark3 = "ข้อมูลอยู่ในช่วง Fixed D/O ไม่สามารถแบ่งรอบส่งได้";
                        oCALDODATE.rev = rev + 1;
                        oCALDODATEs.Add(oCALDODATE);

                    }
                    else
                    {

                        DateTime DATERATIO = oRESULT_DO_QTY.calqty_date.AddDays(-Convert.ToInt32(oPARMSTR.preorder_days));
                        if (DATERATIO < SET_CON_T.set_date)
                        {
                            DATERATIO = SET_CON_T.set_date;
                        }
                        bool STATUSDOD = false;

                        if (oRESULT_DO_QTY.calqtyresult > 0)
                        {
                            DateTime FindNativeDATERATIO = DATERATIO;
                            if (!STATUSDOD)
                            {
                                while (true)
                                {
                                    bool CHECK_RATIO_IN_FIXED_DATE = (FindNativeDATERATIO.Date >= ST_DATE_FIXED.Date && FindNativeDATERATIO.Date <= EN_DATE_FIXED.Date);

                                    bool CHECK_VDHOLIDAY = oVENDOR_HOLIDAYs.Where(s => s.VENDER == "SG1887" && s.VD_HOLIDAY_DATE.Date == FindNativeDATERATIO.Date).Any();
                                    bool CHECK_DCIHOLIDAY = oDCI_HOLIDAYs.Where(s => s.DCI_HOLIDAY_DATE.Date == Convert.ToDateTime(FindNativeDATERATIO.Date)).Any();
                                    bool CHECK_DELICYCLES = CheckDeliveryCyCle(EditKey.setcode, "SG1887", FindNativeDATERATIO);

                                    if (!CHECK_VDHOLIDAY && !CHECK_DCIHOLIDAY && CHECK_DELICYCLES && !CHECK_RATIO_IN_FIXED_DATE)
                                    {
                                        STATUSDOD = true;

                                        oCALDODATE = new CALDODATE_T();
                                        oCALDODATE.set_code = EditKey.setcode;
                                        oCALDODATE.vender = oRESULT_DO_QTY.vender;
                                        oCALDODATE.vender_n = oRESULT_DO_QTY.vender_n;
                                        oCALDODATE.prdymd = oRESULT_DO_QTY.prdymd;
                                        oCALDODATE.partno = oRESULT_DO_QTY.partno;
                                        oCALDODATE.cm = oRESULT_DO_QTY.cm;
                                        oCALDODATE.partname = oRESULT_DO_QTY.partname;
                                        oCALDODATE.planqty = oRESULT_DO_QTY.planqty;
                                        oCALDODATE.consumtionqty = oRESULT_DO_QTY.consumtionqty;
                                        oCALDODATE.calqty_result = oRESULT_DO_QTY.calqtyresult;
                                        oCALDODATE.calqty_date = oRESULT_DO_QTY.calqty_date;
                                        oCALDODATE.whno = oRESULT_DO_QTY.whno;
                                        oCALDODATE.whcode = oRESULT_DO_QTY.whcode;
                                        oCALDODATE.whname = oRESULT_DO_QTY.whname;
                                        oCALDODATE.caldod_date = FindNativeDATERATIO.Date;
                                        oCALDODATE.caldod_result = oCALDODATE.calqty_result + temp;

                                        oCALDODATE.data_dt = DateTime.Now;
                                        if (oCALDODATE.calqty_result > 0)
                                        {
                                            oCALDODATE.remark1 = (temp > 0) ? "Call D/O +" + temp : "Call D/O";
                                            oCALDODATE.remark2 = "Create By System";
                                            oCALDODATE.remark3 = (temp > 0) ? "ออก D/O บวกกับยอดที่ยกมา" + temp : "";
                                        }
                                        else
                                        {
                                            oCALDODATE.remark1 = "No Call D/O";
                                            oCALDODATE.remark2 = "Create By System";
                                            oCALDODATE.remark3 = "ไม่มีการเรียก D/O";
                                        }
                                        temp = 0;
                                        oCALDODATE.rev = rev + 1;
                                        oCALDODATEs.Add(oCALDODATE);

                                        break;
                                    }
                                    else
                                    {
                                        oCALDODATE = new CALDODATE_T();
                                        oCALDODATE.set_code = EditKey.setcode;
                                        oCALDODATE.vender = oRESULT_DO_QTY.vender;
                                        oCALDODATE.vender_n = oRESULT_DO_QTY.vender_n;
                                        oCALDODATE.prdymd = oRESULT_DO_QTY.prdymd;
                                        oCALDODATE.partno = oRESULT_DO_QTY.partno;
                                        oCALDODATE.cm = oRESULT_DO_QTY.cm;
                                        oCALDODATE.partname = oRESULT_DO_QTY.partname;
                                        oCALDODATE.planqty = oRESULT_DO_QTY.planqty;
                                        oCALDODATE.consumtionqty = oRESULT_DO_QTY.consumtionqty;
                                        oCALDODATE.calqty_result = oRESULT_DO_QTY.calqtyresult;
                                        oCALDODATE.calqty_date = oRESULT_DO_QTY.calqty_date;
                                        oCALDODATE.whno = oRESULT_DO_QTY.whno;
                                        oCALDODATE.whcode = oRESULT_DO_QTY.whcode;
                                        oCALDODATE.whname = oRESULT_DO_QTY.whname;
                                        oCALDODATE.caldod_date = FindNativeDATERATIO.Date;
                                        oCALDODATE.caldod_result = 0;

                                        oCALDODATE.data_dt = DateTime.Now;
                                        if (oCALDODATE.calqty_result > 0 || CHECK_RATIO_IN_FIXED_DATE || CHECK_DELICYCLES == false || CHECK_VDHOLIDAY || CHECK_DCIHOLIDAY)
                                        {
                                            oCALDODATE.remark1 += "Can't Call D/O Because ";
                                            oCALDODATE.remark1 += (CHECK_RATIO_IN_FIXED_DATE) ? "In Fixed D/O " : "";
                                            oCALDODATE.remark1 += (CHECK_DELICYCLES == false) ? "Out Off Delivery Cycle " : "";
                                            oCALDODATE.remark1 += (CHECK_VDHOLIDAY) ? "In Vendor Holiday " : "";
                                            oCALDODATE.remark1 += (CHECK_DCIHOLIDAY) ? "In DCI Holiday " : "";
                                        }
                                        else
                                        {
                                            oCALDODATE.remark1 = "No Call D/O";
                                        }
                                        oCALDODATE.remark2 = "Create By System";
                                        if (oCALDODATE.calqty_result > 0 || CHECK_RATIO_IN_FIXED_DATE || CHECK_DELICYCLES == false || CHECK_VDHOLIDAY || CHECK_DCIHOLIDAY)
                                        {

                                            oCALDODATE.remark3 += "ไม่สามารถออก D/O ได้เพราะว่า ";
                                            oCALDODATE.remark3 += (CHECK_RATIO_IN_FIXED_DATE) ? "อยู่ในช่วง Fixed D/O " : "";
                                            oCALDODATE.remark3 += (CHECK_DELICYCLES == false) ? "อยู่นอกช่วง Delivery Cycles " : "";
                                            oCALDODATE.remark3 += (CHECK_VDHOLIDAY) ? "เป็นวันหยุดของ Vendor " : "";
                                            oCALDODATE.remark3 += (CHECK_DCIHOLIDAY) ? "เป็นวันหยุด DCI " : "";
                                        }
                                        else
                                        {
                                            oCALDODATE.remark3 = "ไม่มีการเรียก D/O";
                                        }
                                        oCALDODATE.rev = rev + 1;
                                        oCALDODATEs.Add(oCALDODATE);

                                    }

                                    if (CHECK_RATIO_IN_FIXED_DATE)
                                    {
                                        STATUSDOD = false;
                                        break;
                                    }
                                    FindNativeDATERATIO = FindNativeDATERATIO.AddDays(-1);
                                }
                            }

                            //DateTime FindPositiveDATERATIO = DATERATIO.AddDays(1);
                            DateTime FindPositiveDATERATIO = DATERATIO;
                            if (!STATUSDOD)
                            {
                                while (true)
                                {
                                    bool CHECK_RATIO_IN_FIXED_DATE = (FindPositiveDATERATIO.Date >= ST_DATE_FIXED.Date && FindPositiveDATERATIO.Date <= EN_DATE_FIXED.Date);
                                    bool CHECK_VDHOLIDAY = oVENDOR_HOLIDAYs.Where(s => s.VENDER == "SG1887" && s.VD_HOLIDAY_DATE.Date == FindPositiveDATERATIO.Date).Any();
                                    bool CHECK_DCIHOLIDAY = oDCI_HOLIDAYs.Where(s => s.DCI_HOLIDAY_DATE.Date == FindPositiveDATERATIO.Date).Any();
                                    bool CHECK_DELICYCLES = CheckDeliveryCyCle(EditKey.setcode, "SG1887", FindPositiveDATERATIO);



                                    if (!CHECK_VDHOLIDAY && !CHECK_DCIHOLIDAY && CHECK_DELICYCLES && !CHECK_RATIO_IN_FIXED_DATE)
                                    {
                                        STATUSDOD = true;

                                        oCALDODATE = new CALDODATE_T();
                                        oCALDODATE.set_code = EditKey.setcode;
                                        oCALDODATE.vender = oRESULT_DO_QTY.vender;
                                        oCALDODATE.vender_n = oRESULT_DO_QTY.vender_n;
                                        oCALDODATE.prdymd = oRESULT_DO_QTY.prdymd;
                                        oCALDODATE.partno = oRESULT_DO_QTY.partno;
                                        oCALDODATE.cm = oRESULT_DO_QTY.cm;
                                        oCALDODATE.partname = oRESULT_DO_QTY.partname;
                                        oCALDODATE.planqty = oRESULT_DO_QTY.planqty;
                                        oCALDODATE.consumtionqty = oRESULT_DO_QTY.consumtionqty;
                                        oCALDODATE.calqty_result = oRESULT_DO_QTY.calqtyresult;
                                        oCALDODATE.calqty_date = oRESULT_DO_QTY.calqty_date;
                                        oCALDODATE.whno = oRESULT_DO_QTY.whno;
                                        oCALDODATE.whcode = oRESULT_DO_QTY.whcode;
                                        oCALDODATE.whname = oRESULT_DO_QTY.whname;
                                        oCALDODATE.caldod_date = FindPositiveDATERATIO.Date;
                                        oCALDODATE.caldod_result = oRESULT_DO_QTY.calqtyresult + temp;

                                        oCALDODATE.data_dt = DateTime.Now;
                                        if (oCALDODATE.calqty_result > 0)
                                        {
                                            oCALDODATE.remark1 = (temp > 0) ? "Call D/O +" + temp : "Call D/O";
                                            oCALDODATE.remark2 = "Create By System";
                                            oCALDODATE.remark3 = (temp > 0) ? "ออก D/O บวกกับยอดที่ยกมา" + temp : "";
                                        }
                                        else
                                        {
                                            oCALDODATE.remark1 = "No Call D/O";
                                            oCALDODATE.remark2 = "Create By System";
                                            oCALDODATE.remark3 = "ไม่มีการเรียก D/O";
                                        }
                                        temp = 0;
                                        oCALDODATE.rev = rev + 1;
                                        oCALDODATEs.Add(oCALDODATE);

                                        break;
                                    }
                                    else
                                    {
                                        oCALDODATE = new CALDODATE_T();
                                        oCALDODATE.set_code = EditKey.setcode;
                                        oCALDODATE.vender = oRESULT_DO_QTY.vender;
                                        oCALDODATE.vender_n = oRESULT_DO_QTY.vender_n;
                                        oCALDODATE.prdymd = oRESULT_DO_QTY.prdymd;
                                        oCALDODATE.partno = oRESULT_DO_QTY.partno;
                                        oCALDODATE.cm = oRESULT_DO_QTY.cm;
                                        oCALDODATE.partname = oRESULT_DO_QTY.partname;
                                        oCALDODATE.planqty = oRESULT_DO_QTY.planqty;
                                        oCALDODATE.consumtionqty = oRESULT_DO_QTY.consumtionqty;
                                        oCALDODATE.calqty_result = oRESULT_DO_QTY.calqtyresult;
                                        oCALDODATE.calqty_date = oRESULT_DO_QTY.calqty_date;
                                        oCALDODATE.whno = oRESULT_DO_QTY.whno;
                                        oCALDODATE.whcode = oRESULT_DO_QTY.whcode;
                                        oCALDODATE.whname = oRESULT_DO_QTY.whname;
                                        oCALDODATE.caldod_date = FindPositiveDATERATIO.Date;
                                        oCALDODATE.caldod_result = 0;

                                        oCALDODATE.data_dt = DateTime.Now;
                                        if (oCALDODATE.calqty_result > 0 || CHECK_RATIO_IN_FIXED_DATE || CHECK_DELICYCLES == false || CHECK_VDHOLIDAY || CHECK_DCIHOLIDAY)
                                        {
                                            oCALDODATE.remark1 += "Can't Call D/O Because ";
                                            oCALDODATE.remark1 += (CHECK_RATIO_IN_FIXED_DATE) ? "In Fixed D/O " : "";
                                            oCALDODATE.remark1 += (CHECK_DELICYCLES == false) ? "Out Off Delivery Cycle " : "";
                                            oCALDODATE.remark1 += (CHECK_VDHOLIDAY) ? "In Vendor Holiday " : "";
                                            oCALDODATE.remark1 += (CHECK_DCIHOLIDAY) ? "In DCI Holiday " : "";
                                        }
                                        else
                                        {
                                            oCALDODATE.remark1 = "No Call D/O";
                                        }
                                        oCALDODATE.remark2 = "Create By System";
                                        if (oCALDODATE.calqty_result > 0 || CHECK_RATIO_IN_FIXED_DATE || CHECK_DELICYCLES == false || CHECK_VDHOLIDAY || CHECK_DCIHOLIDAY)
                                        {

                                            oCALDODATE.remark3 += "ไม่สามารถออก D/O ได้เพราะว่า ";
                                            oCALDODATE.remark3 += (CHECK_RATIO_IN_FIXED_DATE) ? "อยู่ในช่วง Fixed D/O " : "";
                                            oCALDODATE.remark3 += (CHECK_DELICYCLES == false) ? "อยู่นอกช่วง Delivery Cycles " : "";
                                            oCALDODATE.remark3 += (CHECK_VDHOLIDAY) ? "เป็นวันหยุดของ Vendor " : "";
                                            oCALDODATE.remark3 += (CHECK_DCIHOLIDAY) ? "เป็นวันหยุด DCI " : "";
                                        }
                                        else
                                        {
                                            oCALDODATE.remark3 = "ไม่มีการเรียก D/O";
                                        }
                                        oCALDODATE.rev = rev + 1;
                                        oCALDODATEs.Add(oCALDODATE);
                                    }

                                    if (oRESULT_DO_QTY.calqty_date == FindPositiveDATERATIO.Date)
                                    {
                                        temp += Math.Ceiling(oRESULT_DO_QTY.calqtyresult);
                                        STATUSDOD = false;
                                        break;
                                    }

                                    FindPositiveDATERATIO = FindPositiveDATERATIO.AddDays(1);
                                }
                            }
                        }
                        else
                        {
                            bool CHECK_RATIO_IN_FIXED_DATE = (DATERATIO.Date >= ST_DATE_FIXED.Date && DATERATIO.Date <= EN_DATE_FIXED.Date);
                            bool CHECK_VDHOLIDAY = oVENDOR_HOLIDAYs.Where(s => s.VENDER == "SG1887" && s.VD_HOLIDAY_DATE.Date == DATERATIO.Date).Any();
                            bool CHECK_DCIHOLIDAY = oDCI_HOLIDAYs.Where(s => s.DCI_HOLIDAY_DATE.Date == DATERATIO.Date).Any();
                            bool CHECK_DELICYCLES = CheckDeliveryCyCle(EditKey.setcode, "SG1887", DATERATIO);

                            oCALDODATE = new CALDODATE_T();
                            oCALDODATE.set_code = EditKey.setcode;
                            oCALDODATE.vender = oRESULT_DO_QTY.vender;
                            oCALDODATE.vender_n = oRESULT_DO_QTY.vender_n;
                            oCALDODATE.prdymd = oRESULT_DO_QTY.prdymd;
                            oCALDODATE.partno = oRESULT_DO_QTY.partno;
                            oCALDODATE.cm = oRESULT_DO_QTY.cm;
                            oCALDODATE.partname = oRESULT_DO_QTY.partname;
                            oCALDODATE.planqty = oRESULT_DO_QTY.planqty;
                            oCALDODATE.consumtionqty = oRESULT_DO_QTY.consumtionqty;
                            oCALDODATE.calqty_result = oRESULT_DO_QTY.calqtyresult;
                            oCALDODATE.calqty_date = oRESULT_DO_QTY.calqty_date;
                            oCALDODATE.whno = oRESULT_DO_QTY.whno;
                            oCALDODATE.whcode = oRESULT_DO_QTY.whcode;
                            oCALDODATE.whname = oRESULT_DO_QTY.whname;
                            oCALDODATE.caldod_date = DATERATIO.Date;

                            if (!CHECK_VDHOLIDAY && !CHECK_DCIHOLIDAY && CHECK_DELICYCLES && !CHECK_RATIO_IN_FIXED_DATE)
                            {
                                oCALDODATE.caldod_result = oCALDODATE.calqty_result + temp;
                                oCALDODATE.remark1 = (temp > 0) ? "Carrying D/O : " + temp : "No Call D/O";
                                oCALDODATE.remark2 = "Create By System";
                                oCALDODATE.remark3 = (temp > 0) ? "ออก D/O จากยอดยกมา : " + temp : "ไม่มีการเรียก D/O";
                                temp = 0;
                            }
                            else
                            {
                                if (temp > 0)
                                {
                                    oCALDODATE.remark1 += "Can't Call D/O Because ";
                                    oCALDODATE.remark1 += (CHECK_RATIO_IN_FIXED_DATE) ? "In Fixed D/O " : "";
                                    oCALDODATE.remark1 += (CHECK_DELICYCLES == false) ? "Out Off Delivery Cycle " : "";
                                    oCALDODATE.remark1 += (CHECK_VDHOLIDAY) ? "In Vendor Holiday " : "";
                                    oCALDODATE.remark1 += (CHECK_DCIHOLIDAY) ? "In DCI Holiday " : "";

                                    oCALDODATE.remark2 = "Create By System";

                                    oCALDODATE.remark3 += "ไม่สามารถออก D/O ได้เพราะว่า ";
                                    oCALDODATE.remark3 += (CHECK_RATIO_IN_FIXED_DATE) ? "อยู่ในช่วง Fixed D/O " : "";
                                    oCALDODATE.remark3 += (CHECK_DELICYCLES == false) ? "อยู่นอกช่วง Delivery Cycles " : "";
                                    oCALDODATE.remark3 += (CHECK_VDHOLIDAY) ? "เป็นวันหยุดของ Vendor " : "";
                                    oCALDODATE.remark3 += (CHECK_DCIHOLIDAY) ? "เป็นวันหยุด DCI " : "";

                                }
                                else
                                {
                                    oCALDODATE.caldod_result = oCALDODATE.calqty_result;
                                    oCALDODATE.remark1 = "No Call D/O";
                                    oCALDODATE.remark2 = "Create By System";
                                    oCALDODATE.remark3 = "ไม่มีการเรียก D/O";
                                }

                            }

                            oCALDODATE.data_dt = DateTime.Now;
                            oCALDODATE.rev = rev + 1;
                            oCALDODATEs.Add(oCALDODATE);
                        }

                    }

                }

            }

            Console.WriteLine(oCALDODATEs);

            // ============= update to db ===================
            UpdateRevisionCaldod(oCALDODATEs, rev, EditKey);

            // ======================  actual do ======================
            List<DOPLAN_T> oACTUAL_DO_QTYs = new List<DOPLAN_T>();
            DOPLAN_T oACTUAL_DO_QTY = new DOPLAN_T();

            List<GRPPLAN> GRP_PLAN = oCALDODATEs
               .GroupBy(x => new
               {
                   x.vender,
                   x.partno,
                   x.cm,
                   x.calqty_date
               })
               .Select(g => new GRPPLAN
               {
                   vender = g.Max(x => x.vender),
                   vender_n = g.Max(x => x.vender_n),
                   prdymd = g.Max(x => x.prdymd),
                   partno = g.Max(x => x.partno),
                   cm = g.Max(x => x.cm),
                   partname = g.Max(x => x.partname),
                   whno = g.First().whno,
                   whcode = g.First().whcode,
                   whname = g.First().whname,
                   planqty = g.First().planqty,
                   consumtionqty = g.First().consumtionqty,
                   calqty_result = g.First().calqty_result,
                   calqty_date = g.Key.calqty_date
               })
               .OrderBy(x => x.calqty_date)
               .ToList();

            List<SUMDOQTY> oSUM_DO_QTYs = oCALDODATEs
               .GroupBy(g => new { Date = g.caldod_date, PartNO = g.partno.Trim(), Cm = g.cm.Trim(), Vender = g.vender })
               .Select(s => new SUMDOQTY
               {
                   setcode = EditKey.setcode,
                   vender = s.First().vender,
                   vender_n = s.First().vender_n,
                   partno = s.First().partno,
                   cm = s.First().cm,
                   partname = s.First().partname,
                   caldod_date = s.First().caldod_date,
                   caldod_result = s.Sum(k => k.caldod_result),
               })
               .OrderBy(item => item.caldod_result)
               .ToList();

            PARTMSTR_T ACCPARTMSTR = PART_MSTR.FirstOrDefault(s => s.partno.Trim() == EditKey.partno.Trim() && s.cm.Trim() == EditKey.cm.Trim() && s.vender == EditKey.vender);
            List<DOPLAN_T> oACC_DO_QTYs = new List<DOPLAN_T>();
            DOPLAN_T oACC_DO_QTY = new DOPLAN_T();
            decimal STISIM = 0, STOSIM = 0;

            STISIM = ACCPARTMSTR.store_wh1 + ACCPARTMSTR.store_wh2;
            STOSIM = ACCPARTMSTR.store_whb;
            foreach (var PLAN in GRP_PLAN)
            {
                SUMDOQTY ACC_DOQTY = oSUM_DO_QTYs
                        .Where(s => s.partno.Trim() == EditKey.partno.Trim()
                            && s.cm.Trim() == EditKey.cm.Trim()
                            && s.caldod_date.Date == PLAN.calqty_date
                            .Date)
                        .FirstOrDefault();
                oACC_DO_QTY = new DOPLAN_T();
                oACC_DO_QTY.set_code = EditKey.setcode;
                oACC_DO_QTY.vender = PLAN.vender;
                oACC_DO_QTY.vender_n = PLAN.vender_n;
                oACC_DO_QTY.prdymd = PLAN.prdymd;
                oACC_DO_QTY.partno = PLAN.partno;
                oACC_DO_QTY.cm = PLAN.cm;
                oACC_DO_QTY.partname = PLAN.partname;
                oACC_DO_QTY.plan_qty = PLAN.planqty;
                oACC_DO_QTY.consumtion_qty = PLAN.consumtionqty;
                oACC_DO_QTY.cal_qty = PLAN.calqty_result;
                oACC_DO_QTY.caldod_qty = ACC_DOQTY?.caldod_result ?? 0;
                oACC_DO_QTY.do_date = PLAN.calqty_date;
                oACC_DO_QTY.do_qty = ACC_DOQTY?.caldod_result ?? 0;
                oACC_DO_QTY.mark_status = ACCPARTMSTR.mark_status;
                oACC_DO_QTY.stk_wh_bf = STOSIM;
                oACC_DO_QTY.whno = PLAN.whno;
                oACC_DO_QTY.whcode = PLAN.whcode;
                oACC_DO_QTY.whname = PLAN.whname;

                //if (oACC_DO_QTY.do_qty > oACC_DO_QTY.stk_wh_bf)
                //{
                //    oACC_DO_QTY.do_qty = oACC_DO_QTY.stk_wh_bf;
                //    oACC_DO_QTY.stk_wh_af = 0;
                //}

                oACC_DO_QTY.stk_wh_af = STOSIM - oACC_DO_QTY.do_qty;
                oACC_DO_QTY.stk_inhouse_bf = STISIM;
                oACC_DO_QTY.stk_inhouse_af = (STISIM + oACC_DO_QTY.do_qty) - oACC_DO_QTY.plan_qty;

                oACC_DO_QTY.data_dt = DateTime.Now;
                oACC_DO_QTY.remark1 = "";
                oACC_DO_QTY.remark2 = "Update By " + EditKey.update_by;
                oACC_DO_QTY.remark3 = "Update Date " + DateTime.Now.ToString();

                STISIM = oACC_DO_QTY.stk_inhouse_af;
                STOSIM = oACC_DO_QTY.stk_wh_af;

                if (oACC_DO_QTY.mark_status == "ACTIVE")
                {
                    bool statusDeliveryCycle = CheckDeliveryCyCle(EditKey.setcode, "SG1887", oACC_DO_QTY.do_date);
                    bool statusHolidayVender = oVENDOR_HOLIDAYs.Where(s => s.VD_HOLIDAY_DATE.Date == oACC_DO_QTY.do_date.Date).Any();
                    bool statusHolidayDCI = oDCI_HOLIDAYs.Where(s => s.DCI_HOLIDAY_DATE.Date == oACC_DO_QTY.do_date.Date).Any();

                    if (statusDeliveryCycle && !statusHolidayVender && !statusHolidayDCI)
                    {
                        oACC_DO_QTY.mark_qty = (oACC_DO_QTY.do_date.ToString("yyyy-MM-dd") == EditKey.dodate.ToString("yyyy-MM-dd")) ? EditKey.markqtyNew : ACCPARTMSTR.mark_qty;
                    }
                    else
                    {
                        oACC_DO_QTY.mark_qty = 0;
                    }
                }
                else
                {
                    oACC_DO_QTY.mark_qty = 0;
                }

                //if (oACC_DO_QTY.mark_qty >= STOSIM)
                //{
                //    oACC_DO_QTY.mark_qty = STOSIM;
                //    STOSIM = 0;
                //    oACC_DO_QTY.stk_wh_af = 0;
                //}

                oACC_DO_QTY.stk_wh_af = STOSIM - oACC_DO_QTY.mark_qty;
                STOSIM = oACC_DO_QTY.stk_wh_af;


                oACC_DO_QTY.stk_inhouse_af = STISIM + oACC_DO_QTY.mark_qty;
                STISIM = oACC_DO_QTY.stk_inhouse_af;

                oACC_DO_QTY.rev = rev + 1;
                oACC_DO_QTYs.Add(oACC_DO_QTY);
            }
            oACC_DO_QTYs = oACC_DO_QTYs
                .OrderBy(x => x.rev).ThenBy(x => x.do_date)
                .ToList();

            Console.WriteLine(oACC_DO_QTYs);

            // ============ Update to DB =============
            UpdateRevisionActual(oACC_DO_QTYs, rev, EditKey);

            // ============ Update rev fixed plan date =============
            DateTime fixstdate = SET_CON_T.set_date;
            DateTime fixendate = fixstdate.AddDays(MASTER_WH.fixed_days - 1);
            List<DateTime> Fixedays = new List<DateTime>();
            while (fixstdate.Date <= fixendate.Date)
            {
                Fixedays.Add(fixstdate);
                fixstdate = fixstdate.AddDays(1);
            }

            List<DOPLAN_T> doplanfixedRev = new List<DOPLAN_T>();

            foreach (DateTime day in Fixedays)
            {
                DOPLAN_T doplan = oACC_DO_QTYs.Where(s => s.do_date.Date == day.Date).FirstOrDefault();
                doplanfixedRev.Add(doplan);
            }
            //UpdateRevisionFixedplan(doplanfixedRev, rev, EditKey);
            UpdateRevisionDoplan(oACC_DO_QTYs, rev, EditKey);
        }

        // vender code is mean wh code 
        private bool CheckDeliveryCyCle(string setcode, string vender_c, DateTime day)
        {
            List<DELIVERYCYCLE_T> deliveryMstr = GET_DELIVERYCYCLE(setcode);
            DELIVERYCYCLE_T oDELIVERY_CYCLE = deliveryMstr.FirstOrDefault(s => s.vender.Trim() == vender_c.Trim());

            bool CHECK_DELICYCLES = false;
            string DayNameInFixedDO = day.ToString("ddd");
            string DateInFixedDO = day.Day.ToString();

            switch (oDELIVERY_CYCLE.del_type)
            {
                case "Daily":
                    CHECK_DELICYCLES = true;
                    break;
                case "Weekly":
                    switch (DayNameInFixedDO)
                    {
                        case "Mon":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_wk_mon;
                            break;
                        case "Tue":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_wk_tue;
                            break;
                        case "Wed":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_wk_wed;
                            break;
                        case "Thu":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_wk_thu;
                            break;
                        case "Fri":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_wk_fri;
                            break;
                        case "Sat":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_wk_sat;
                            break;
                        case "Sun":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_wk_sun;
                            break;
                        default:
                            break;
                    }
                    break;
                case "Monthly":
                    switch (DateInFixedDO)
                    {
                        case "1":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_01; break;
                        case "2":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_02; break;
                        case "3":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_03; break;
                        case "4":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_04; break;
                        case "5":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_05; break;
                        case "6":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_06; break;
                        case "7":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_07; break;
                        case "8":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_08; break;
                        case "9":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_09; break;
                        case "10":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_10; break;
                        case "11":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_11; break;
                        case "12":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_12; break;
                        case "13":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_13; break;
                        case "14":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_14; break;
                        case "15":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_15; break;
                        case "16":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_16; break;
                        case "17":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_17; break;
                        case "18":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_18; break;
                        case "19":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_19; break;
                        case "20":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_20; break;
                        case "21":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_21; break;
                        case "22":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_22; break;
                        case "23":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_23; break;
                        case "24":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_24; break;
                        case "25":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_25; break;
                        case "26":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_26; break;
                        case "27":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_27; break;
                        case "28":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_28; break;
                        case "29":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_29; break;
                        case "30":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_30; break;
                        case "31":
                            CHECK_DELICYCLES = oDELIVERY_CYCLE.del_mo_31; break;
                    }
                    break;
            }

            return CHECK_DELICYCLES;
        }
        private void UpdateRevisionCalqty(List<CALQTY_T> oDO_QTYs, int rev, DOPLANKEY EditKey)
        {
            // ------ SET TO DB ------
            DateTime ST_DATE_I = DateTime.Now;
            DateTime EN_DATE_I = DateTime.Now;
            string PROCESS = "CAL_DO_QTY_REV";
            int Success_Qty = 0, Error_qty = 0, row_index = 1;

            // ------- REVISION OLD DATA NOT USE  ---------
            SqlCommand sqlUpdateDoqty = new SqlCommand();
            sqlUpdateDoqty.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_4_CAL_1DO_QTY]
                      SET [LREV] = '{rev}'
                      WHERE [SETCODE] = '{EditKey.setcode}' AND [PARTNO] = '{EditKey.partno}' AND [CM] = '{EditKey.cm}' AND [VENDER] = '{EditKey.vender}' AND [REV] = '{rev}'";

            conDBSCM.ExecuteCommand(sqlUpdateDoqty);

            // ------- INSERT --------
            if (oDO_QTYs.Count > 0)
            {
                DateTime RNE_ST = DateTime.Now;
                string DBResult = "";
                try
                {
                    foreach (var oDoqty in oDO_QTYs)
                    {
                        RNE_ST = DateTime.Now;

                        SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                        sqlInsertNewRevCalDoqtyData.CommandText = $@"INSERT INTO [dbSCM].[dbo].[DOOUT_4_CAL_1DO_QTY]
                        ([REV],[LREV],[SETCODE],[PRDYMD],[CALQTYDATE],[VENDER],[VENDERNAME],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PRIORITY],[RATIO],[BOXQTY],[PALLETQTY],[SAFETYQTY],[MINQTY],[MAXQTY],
                         [PLANQTY],[CONSUMTIONQTY],[CALQTYRESULT],[STKINHOUSEBF],[STKINHOUSEAF],[STKWHBF],[STKWHAF],[DATADT],[REMARK1],[REMARK2],[REMARK3]) 
                        VALUES (@REV,@LREV,@SETCODE,@PRDYMD,@CALQTYDATE,@VENDER,@VENDERNAME,@PARTNO,@CM,@PARTNAME,@WHNO,@WHCODE,@WHNAME,@PRIORITY,@RATIO,@BOXQTY,@PALLETQTY,@SAFETYQTY,@MINQTY,@MAXQTY,
                         @PLANQTY,@CONSUMTIONQTY,@CALQTYRESULT,@STKINHOUSEBF,@STKINHOUSEAF,@STKWHBF,@STKWHAF,@DATADT,@REMARK1,@REMARK2,@REMARK3)";

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REV", oDoqty.rev);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@LREV", 999);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@SETCODE", oDoqty.set_code);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PRDYMD", oDoqty.prdymd);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALQTYDATE", oDoqty.calqty_date.ToString("yyyy-MM-dd"));
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER", oDoqty.vender);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDERNAME", oDoqty.vender_n);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNO", oDoqty.partno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CM", oDoqty.cm);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNAME", oDoqty.partname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNO", oDoqty.whno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHCODE", oDoqty.whcode);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNAME", oDoqty.whname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PRIORITY", oDoqty.priority);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@RATIO", oDoqty.ratio);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@BOXQTY", oDoqty.boxqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PALLETQTY", oDoqty.palletqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@SAFETYQTY", oDoqty.safetyqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@MINQTY", oDoqty.minqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@MAXQTY", oDoqty.maxqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PLANQTY", oDoqty.planqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CONSUMTIONQTY", oDoqty.consumtionqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALQTYRESULT", oDoqty.calqtyresult);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STKINHOUSEBF", oDoqty.stk_inhouse_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STKINHOUSEAF", oDoqty.stk_inhouse_af);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STKWHBF", oDoqty.stk_wh_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STKWHAF", oDoqty.stk_wh_af);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DATADT", DateTime.Now);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK1", oDoqty.remark1);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK2", oDoqty.remark2);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK3", oDoqty.remark3);

                        DBResult = conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);

                        if (DBResult == "Success")
                        {
                            Success_Qty += 1;
                        }
                        else
                        {
                            Error_qty += 1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    DateTime RNE_EN = DateTime.Now;

                    RUNNING_ERROR LOG_ERROR = new RUNNING_ERROR();
                    LOG_ERROR.LOG_NBR = svrRunning.LoadUnique("DOOUT_ERR").ToString(true);
                    LOG_ERROR.SET_CODE = EditKey.setcode;
                    LOG_ERROR.PROCESS = PROCESS;
                    LOG_ERROR.PROCESS_ST = RNE_ST;
                    LOG_ERROR.PROCESS_EN = RNE_EN;
                    LOG_ERROR.ERROR_ROW = row_index;
                    LOG_ERROR.ERROR_TEXT = ex.Message;
                    LOG_ERROR.RUNNING_DATE = DateTime.Now;

                    SqlCommand sqlInsertLogError = new SqlCommand();
                    sqlInsertLogError.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_ERROR] 
                        ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],[ERROR_ROW],[ERROR_TEXT],[RUNNING_DATE]) 
                        VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@ERROR_ROW,@ERROR_TEXT,@RUNNING_DATE)";
                    sqlInsertLogError.Parameters.AddWithValue("@LOG_NBR", LOG_ERROR.LOG_NBR);
                    sqlInsertLogError.Parameters.AddWithValue("@SET_CODE", LOG_ERROR.SET_CODE);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS", LOG_ERROR.PROCESS);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_ST", LOG_ERROR.PROCESS_ST);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_EN", LOG_ERROR.PROCESS_EN);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_ROW", LOG_ERROR.ERROR_ROW);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_TEXT", LOG_ERROR.ERROR_TEXT);
                    sqlInsertLogError.Parameters.AddWithValue("@RUNNING_DATE", LOG_ERROR.RUNNING_DATE);

                    conDBSCM.ExecuteCommand(sqlInsertLogError);
                    svrRunning.NextId("DOOUT_ERR");
                }

                row_index++;

                EN_DATE_I = DateTime.Now;
            }
            //-----LOG------
            RUNNING_LOG Log = new RUNNING_LOG();
            Log.LOG_NBR = svrRunning.LoadUnique("DOOUT_RNL").ToString(true);
            Log.SET_CODE = EditKey.setcode;
            Log.PROCESS = PROCESS;
            Log.PROCESS_ST = ST_DATE_I;
            Log.PROCESS_EN = EN_DATE_I;
            Log.READ_QTY = oDO_QTYs.Count;
            Log.SUCCESS_QTY = Success_Qty;
            Log.ERROR_QTY = Error_qty;
            Log.RUNNING_DATE = DateTime.Now;
            svrRunning.NextId("DOOUT_RNL");

            SqlCommand sqlInsertLog = new SqlCommand();
            sqlInsertLog.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_LOG] ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],
                                        [READ_QTY],[SUCCESS_QTY],[ERROR_QTY],[RUNNING_DATE]) 
            VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@READ_QTY,@SUCCESS_QTY,@ERROR_QTY,@RUNNING_DATE)";

            sqlInsertLog.Parameters.Add(new SqlParameter("@LOG_NBR", Log.LOG_NBR));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SET_CODE", Log.SET_CODE));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS", Log.PROCESS));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_ST", Log.PROCESS_ST));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_EN", Log.PROCESS_EN));
            sqlInsertLog.Parameters.Add(new SqlParameter("@READ_QTY", Log.READ_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SUCCESS_QTY", Log.SUCCESS_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@ERROR_QTY", Log.ERROR_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@RUNNING_DATE", Log.RUNNING_DATE));

            conDBSCM.ExecuteCommand(sqlInsertLog);


        }
        private void UpdateRevisionCaldod(List<CALDODATE_T> oDO_QTYs, int rev, DOPLANKEY EditKey)
        {
            // ------ SET TO DB ------
            DateTime ST_DATE_I = DateTime.Now;
            DateTime EN_DATE_I = DateTime.Now;
            string PROCESS = "CAL_DO_QTY_REV";
            int Success_Qty = 0, Error_qty = 0, row_index = 1;

            // ------- REVISION OLD DATA NOT USE  ---------
            SqlCommand sqlUpdateDoqty = new SqlCommand();
            sqlUpdateDoqty.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_4_CAL_2DO_DATE]
                      SET [LREV] = '{rev}'
                      WHERE [SETCODE] = '{EditKey.setcode}' AND [PARTNO] = '{EditKey.partno}' AND [CM] = '{EditKey.cm}' AND [VENDER] = '{EditKey.vender}' AND [REV] = '{rev}'";

            conDBSCM.ExecuteCommand(sqlUpdateDoqty);

            // ------- INSERT --------
            if (oDO_QTYs.Count > 0)
            {
                DateTime RNE_ST = DateTime.Now;
                string DBResult = "";
                try
                {
                    foreach (var oDoqty in oDO_QTYs)
                    {
                        RNE_ST = DateTime.Now;

                        SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                        sqlInsertNewRevCalDoqtyData.CommandText = $@"INSERT INTO [dbSCM].[dbo].[DOOUT_4_CAL_2DO_DATE]
                        ([REV],[LREV],[SETCODE],[PRDYMD],[CALQTYDATE],[CALDODDATE],[VENDER],[VENDERNAME],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PLANQTY],[CONSUMTIONQTY],[CALQTYRESULT],[CALDODRESULT],[DATADT],[REMARK1],[REMARK2],[REMARK3]) 
                        VALUES (@REV,@LREV,@SETCODE,@PRDYMD,@CALQTYDATE,@CALDODDATE,@VENDER,@VENDERNAME,@PARTNO,@CM,@PARTNAME,@WHNO,@WHCODE,@WHNAME,@PLANQTY,@CONSUMTIONQTY,@CALQTYRESULT,@CALDODRESULT,@DATADT,@REMARK1,@REMARK2,@REMARK3)";

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REV", oDoqty.rev);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@LREV", 999);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@SETCODE", oDoqty.set_code);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PRDYMD", oDoqty.prdymd);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALQTYDATE", oDoqty.calqty_date.ToString("yyyy-MM-dd"));
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALDODDATE", oDoqty.caldod_date.ToString("yyyy-MM-dd"));
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER", oDoqty.vender.ToString());
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDERNAME", oDoqty.vender_n);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNO", oDoqty.partno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CM", oDoqty.cm);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNAME", oDoqty.partname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNO", oDoqty.whno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHCODE", oDoqty.whcode);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNAME", oDoqty.whname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PLANQTY", oDoqty.planqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CONSUMTIONQTY", oDoqty.consumtionqty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALQTYRESULT", oDoqty.calqty_result);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALDODRESULT", oDoqty.caldod_result);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DATADT", DateTime.Now);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK1", oDoqty.remark1);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK2", oDoqty.remark2);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK3", oDoqty.remark3);



                        DBResult = conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);

                        if (DBResult == "Success")
                        {
                            Success_Qty += 1;
                        }
                        else
                        {
                            Error_qty += 1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    DateTime RNE_EN = DateTime.Now;

                    RUNNING_ERROR LOG_ERROR = new RUNNING_ERROR();
                    LOG_ERROR.LOG_NBR = svrRunning.LoadUnique("DOOUT_ERR").ToString(true);
                    LOG_ERROR.SET_CODE = EditKey.setcode;
                    LOG_ERROR.PROCESS = PROCESS;
                    LOG_ERROR.PROCESS_ST = RNE_ST;
                    LOG_ERROR.PROCESS_EN = RNE_EN;
                    LOG_ERROR.ERROR_ROW = row_index;
                    LOG_ERROR.ERROR_TEXT = ex.Message;
                    LOG_ERROR.RUNNING_DATE = DateTime.Now;

                    SqlCommand sqlInsertLogError = new SqlCommand();
                    sqlInsertLogError.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_ERROR] 
                        ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],[ERROR_ROW],[ERROR_TEXT],[RUNNING_DATE]) 
                        VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@ERROR_ROW,@ERROR_TEXT,@RUNNING_DATE)";
                    sqlInsertLogError.Parameters.AddWithValue("@LOG_NBR", LOG_ERROR.LOG_NBR);
                    sqlInsertLogError.Parameters.AddWithValue("@SET_CODE", LOG_ERROR.SET_CODE);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS", LOG_ERROR.PROCESS);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_ST", LOG_ERROR.PROCESS_ST);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_EN", LOG_ERROR.PROCESS_EN);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_ROW", LOG_ERROR.ERROR_ROW);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_TEXT", LOG_ERROR.ERROR_TEXT);
                    sqlInsertLogError.Parameters.AddWithValue("@RUNNING_DATE", LOG_ERROR.RUNNING_DATE);

                    conDBSCM.ExecuteCommand(sqlInsertLogError);
                    svrRunning.NextId("DOOUT_ERR");
                }

                row_index++;

                EN_DATE_I = DateTime.Now;
            }
            //-----LOG------
            RUNNING_LOG Log = new RUNNING_LOG();
            Log.LOG_NBR = svrRunning.LoadUnique("DOOUT_RNL").ToString(true);
            Log.SET_CODE = EditKey.setcode;
            Log.PROCESS = PROCESS;
            Log.PROCESS_ST = ST_DATE_I;
            Log.PROCESS_EN = EN_DATE_I;
            Log.READ_QTY = oDO_QTYs.Count;
            Log.SUCCESS_QTY = Success_Qty;
            Log.ERROR_QTY = Error_qty;
            Log.RUNNING_DATE = DateTime.Now;
            svrRunning.NextId("DOOUT_RNL");

            SqlCommand sqlInsertLog = new SqlCommand();
            sqlInsertLog.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_LOG] ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],
                                        [READ_QTY],[SUCCESS_QTY],[ERROR_QTY],[RUNNING_DATE]) 
            VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@READ_QTY,@SUCCESS_QTY,@ERROR_QTY,@RUNNING_DATE)";

            sqlInsertLog.Parameters.Add(new SqlParameter("@LOG_NBR", Log.LOG_NBR));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SET_CODE", Log.SET_CODE));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS", Log.PROCESS));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_ST", Log.PROCESS_ST));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_EN", Log.PROCESS_EN));
            sqlInsertLog.Parameters.Add(new SqlParameter("@READ_QTY", Log.READ_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SUCCESS_QTY", Log.SUCCESS_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@ERROR_QTY", Log.ERROR_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@RUNNING_DATE", Log.RUNNING_DATE));

            conDBSCM.ExecuteCommand(sqlInsertLog);


        }
        private void UpdateRevisionActual(List<DOPLAN_T> oDO_QTYs, int rev, DOPLANKEY EditKey)
        {
            // ------ SET TO DB ------
            DateTime ST_DATE_I = DateTime.Now;
            DateTime EN_DATE_I = DateTime.Now;
            string PROCESS = "CAL_DO_QTY_REV";
            int Success_Qty = 0, Error_qty = 0, row_index = 1;

            // ------- REVISION OLD DATA NOT USE  ---------
            SqlCommand sqlUpdateDoqty = new SqlCommand();
            sqlUpdateDoqty.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_5_ACTUAL_DO]
                      SET [LREV] = '{rev}'
                      WHERE [SET_CODE] = '{EditKey.setcode}' AND [PARTNO] = '{EditKey.partno}' AND [CM] = '{EditKey.cm}' AND [VENDER] = '{EditKey.vender}' AND [REV] = '{rev}'";

            conDBSCM.ExecuteCommand(sqlUpdateDoqty);

            // ------- INSERT --------
            if (oDO_QTYs.Count > 0)
            {
                DateTime RNE_ST = DateTime.Now;
                string DBResult = "";
                try
                {
                    foreach (var oDoqty in oDO_QTYs)
                    {
                        RNE_ST = DateTime.Now;

                        SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                        sqlInsertNewRevCalDoqtyData.CommandText = $@"INSERT INTO [dbSCM].[dbo].[DOOUT_5_ACTUAL_DO]
                        ([REV],[LREV],[SET_CODE],[PRDYMD],[DO_DATE],[VENDER],[VENDER_N],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PLAN_QTY],[CONSUMTION_QTY],[CAL_QTY],[CALDOD_QTY],[MARK_QTY],
                            [DO_QTY],[STK_INHOUSE_BF],[STK_INHOUSE_AF],[STK_WH_BF],[STK_WH_AF],[MARK_STATUS],[DATA_DT],[REMARK1],[REMARK2],[REMARK3]) 
                        VALUES (@REV,@LREV,@SET_CODE,@PRDYMD,@DO_DATE,@VENDER,@VENDER_N,@PARTNO,@CM,@PARTNAME,@WHNO,@WHCODE,@WHNAME,@PLAN_QTY,@CONSUMTION_QTY,@CAL_QTY,@CALDOD_QTY,@MARK_QTY,
                            @DO_QTY,@STK_INHOUSE_BF,@STK_INHOUSE_AF,@STK_WH_BF,@STK_WH_AF,@MARK_STATUS,@DATA_DT,@REMARK1,@REMARK2,@REMARK3)";

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REV", oDoqty.rev);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@LREV", 999);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@SET_CODE", oDoqty.set_code);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PRDYMD", oDoqty.prdymd);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DO_DATE", oDoqty.do_date);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER", oDoqty.vender);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER_N", oDoqty.vender_n);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNO", oDoqty.partno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CM", oDoqty.cm);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNAME", oDoqty.partname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNO", oDoqty.whno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHCODE", oDoqty.whcode);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNAME", oDoqty.whname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PLAN_QTY", oDoqty.plan_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CONSUMTION_QTY", oDoqty.consumtion_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CAL_QTY", oDoqty.cal_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALDOD_QTY", oDoqty.caldod_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@MARK_QTY", oDoqty.mark_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DO_QTY", oDoqty.do_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_INHOUSE_BF", oDoqty.stk_inhouse_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_INHOUSE_AF", oDoqty.stk_inhouse_af);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_WH_BF", oDoqty.stk_wh_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_WH_AF", oDoqty.stk_wh_af);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@MARK_STATUS", oDoqty.mark_status);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DATA_DT", DateTime.Now);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK1", oDoqty.remark1);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK2", oDoqty.remark2);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK3", oDoqty.remark3);



                        DBResult = conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);

                        if (DBResult == "Success")
                        {
                            Success_Qty += 1;
                        }
                        else
                        {
                            Error_qty += 1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    DateTime RNE_EN = DateTime.Now;

                    RUNNING_ERROR LOG_ERROR = new RUNNING_ERROR();
                    LOG_ERROR.LOG_NBR = svrRunning.LoadUnique("DOOUT_ERR").ToString(true);
                    LOG_ERROR.SET_CODE = EditKey.setcode;
                    LOG_ERROR.PROCESS = PROCESS;
                    LOG_ERROR.PROCESS_ST = RNE_ST;
                    LOG_ERROR.PROCESS_EN = RNE_EN;
                    LOG_ERROR.ERROR_ROW = row_index;
                    LOG_ERROR.ERROR_TEXT = ex.Message;
                    LOG_ERROR.RUNNING_DATE = DateTime.Now;

                    SqlCommand sqlInsertLogError = new SqlCommand();
                    sqlInsertLogError.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_ERROR] 
                        ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],[ERROR_ROW],[ERROR_TEXT],[RUNNING_DATE]) 
                        VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@ERROR_ROW,@ERROR_TEXT,@RUNNING_DATE)";
                    sqlInsertLogError.Parameters.AddWithValue("@LOG_NBR", LOG_ERROR.LOG_NBR);
                    sqlInsertLogError.Parameters.AddWithValue("@SET_CODE", LOG_ERROR.SET_CODE);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS", LOG_ERROR.PROCESS);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_ST", LOG_ERROR.PROCESS_ST);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_EN", LOG_ERROR.PROCESS_EN);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_ROW", LOG_ERROR.ERROR_ROW);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_TEXT", LOG_ERROR.ERROR_TEXT);
                    sqlInsertLogError.Parameters.AddWithValue("@RUNNING_DATE", LOG_ERROR.RUNNING_DATE);

                    conDBSCM.ExecuteCommand(sqlInsertLogError);
                    svrRunning.NextId("DOOUT_ERR");
                }

                row_index++;

                EN_DATE_I = DateTime.Now;
            }

            //-----LOG------
            RUNNING_LOG Log = new RUNNING_LOG();
            Log.LOG_NBR = svrRunning.LoadUnique("DOOUT_RNL").ToString(true);
            Log.SET_CODE = EditKey.setcode;
            Log.PROCESS = PROCESS;
            Log.PROCESS_ST = ST_DATE_I;
            Log.PROCESS_EN = EN_DATE_I;
            Log.READ_QTY = oDO_QTYs.Count;
            Log.SUCCESS_QTY = Success_Qty;
            Log.ERROR_QTY = Error_qty;
            Log.RUNNING_DATE = DateTime.Now;
            svrRunning.NextId("DOOUT_RNL");

            SqlCommand sqlInsertLog = new SqlCommand();
            sqlInsertLog.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_LOG] ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],
                                        [READ_QTY],[SUCCESS_QTY],[ERROR_QTY],[RUNNING_DATE]) 
            VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@READ_QTY,@SUCCESS_QTY,@ERROR_QTY,@RUNNING_DATE)";

            sqlInsertLog.Parameters.Add(new SqlParameter("@LOG_NBR", Log.LOG_NBR));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SET_CODE", Log.SET_CODE));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS", Log.PROCESS));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_ST", Log.PROCESS_ST));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_EN", Log.PROCESS_EN));
            sqlInsertLog.Parameters.Add(new SqlParameter("@READ_QTY", Log.READ_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SUCCESS_QTY", Log.SUCCESS_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@ERROR_QTY", Log.ERROR_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@RUNNING_DATE", Log.RUNNING_DATE));

            conDBSCM.ExecuteCommand(sqlInsertLog);


        }
        private void UpdateRevisionFixedplan(List<DOPLAN_T> oDO_QTYs, int rev, DOPLANKEY EditKey)
        {
            // ------ SET TO DB ------
            DateTime ST_DATE_I = DateTime.Now;
            DateTime EN_DATE_I = DateTime.Now;
            string PROCESS = "DO_PALN_FIXED_REV";
            int Success_Qty = 0, Error_qty = 0, row_index = 1;

            // ------- REVISION OLD DATA NOT USE  ---------
            SqlCommand sqlUpdateDoqty = new SqlCommand();
            sqlUpdateDoqty.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_1_6DO_PLAN_FIXED]
                      SET [LREV] = '{rev}'
                      WHERE [SET_CODE] = '{EditKey.setcode}' AND [PARTNO] = '{EditKey.partno}' AND [CM] = '{EditKey.cm}' AND [REV] = '{rev}'";

            conDBSCM.ExecuteCommand(sqlUpdateDoqty);

            // ------- INSERT --------
            if (oDO_QTYs.Count > 0)
            {
                DateTime RNE_ST = DateTime.Now;
                string DBResult = "";
                try
                {
                    foreach (var oDoqty in oDO_QTYs)
                    {
                        RNE_ST = DateTime.Now;

                        SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                        sqlInsertNewRevCalDoqtyData.CommandText = $@"INSERT INTO [dbSCM].[dbo].[DOOUT_1_6DO_PLAN_FIXED]
                        ([REV],[LREV],[SET_CODE],[PRDYMD],[DO_DATE],[VENDER],[VENDER_N],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PLAN_QTY],[CONSUMTION_QTY],[CAL_QTY],[MARK_QTY],
                            [DO_QTY],[STK_INHOUSE_BF],[STK_INHOUSE_AF],[STK_WH_BF],[STK_WH_AF],[DATA_DT],[REMARK1],[REMARK2],[REMARK3]) 
                        VALUES (@REV,@LREV,@SET_CODE,@PRDYMD,@DO_DATE,@VENDER,@VENDER_N,@PARTNO,@CM,@PARTNAME,@WHNO,@WHCODE,@WHNAME,@PLAN_QTY,@CONSUMTION_QTY,@CAL_QTY,@MARK_QTY,
                            @DO_QTY,@STK_INHOUSE_BF,@STK_INHOUSE_AF,@STK_WH_BF,@STK_WH_AF,@DATA_DT,@REMARK1,@REMARK2,@REMARK3)";

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REV", oDoqty.rev);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@LREV", 999);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@SET_CODE", oDoqty.set_code);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PRDYMD", oDoqty.prdymd);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DO_DATE", oDoqty.do_date);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER", oDoqty.vender);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER_N", oDoqty.vender_n);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNO", oDoqty.partno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CM", oDoqty.cm);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNAME", oDoqty.partname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNO", oDoqty.whno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHCODE", oDoqty.whcode);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNAME", oDoqty.whname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PLAN_QTY", oDoqty.plan_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CONSUMTION_QTY", oDoqty.consumtion_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CAL_QTY", oDoqty.cal_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@MARK_QTY", oDoqty.mark_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DO_QTY", oDoqty.do_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_INHOUSE_BF", oDoqty.stk_inhouse_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_INHOUSE_AF", oDoqty.stk_inhouse_af);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_WH_BF", oDoqty.stk_wh_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_WH_AF", oDoqty.stk_wh_af);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DATA_DT", DateTime.Now);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK1", oDoqty.remark1);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK2", oDoqty.remark2);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK3", oDoqty.remark3);



                        DBResult = conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);

                        if (DBResult == "Success")
                        {
                            Success_Qty += 1;
                        }
                        else
                        {
                            Error_qty += 1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    DateTime RNE_EN = DateTime.Now;

                    RUNNING_ERROR LOG_ERROR = new RUNNING_ERROR();
                    LOG_ERROR.LOG_NBR = svrRunning.LoadUnique("DOOUT_ERR").ToString(true);
                    LOG_ERROR.SET_CODE = EditKey.setcode;
                    LOG_ERROR.PROCESS = PROCESS;
                    LOG_ERROR.PROCESS_ST = RNE_ST;
                    LOG_ERROR.PROCESS_EN = RNE_EN;
                    LOG_ERROR.ERROR_ROW = row_index;
                    LOG_ERROR.ERROR_TEXT = ex.Message;
                    LOG_ERROR.RUNNING_DATE = DateTime.Now;

                    SqlCommand sqlInsertLogError = new SqlCommand();
                    sqlInsertLogError.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_ERROR] 
                        ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],[ERROR_ROW],[ERROR_TEXT],[RUNNING_DATE]) 
                        VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@ERROR_ROW,@ERROR_TEXT,@RUNNING_DATE)";
                    sqlInsertLogError.Parameters.AddWithValue("@LOG_NBR", LOG_ERROR.LOG_NBR);
                    sqlInsertLogError.Parameters.AddWithValue("@SET_CODE", LOG_ERROR.SET_CODE);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS", LOG_ERROR.PROCESS);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_ST", LOG_ERROR.PROCESS_ST);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_EN", LOG_ERROR.PROCESS_EN);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_ROW", LOG_ERROR.ERROR_ROW);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_TEXT", LOG_ERROR.ERROR_TEXT);
                    sqlInsertLogError.Parameters.AddWithValue("@RUNNING_DATE", LOG_ERROR.RUNNING_DATE);

                    conDBSCM.ExecuteCommand(sqlInsertLogError);
                    svrRunning.NextId("DOOUT_ERR");
                }

                row_index++;

                EN_DATE_I = DateTime.Now;
            }

            //-----LOG------
            RUNNING_LOG Log = new RUNNING_LOG();
            Log.LOG_NBR = svrRunning.LoadUnique("DOOUT_RNL").ToString(true);
            Log.SET_CODE = EditKey.setcode;
            Log.PROCESS = PROCESS;
            Log.PROCESS_ST = ST_DATE_I;
            Log.PROCESS_EN = EN_DATE_I;
            Log.READ_QTY = oDO_QTYs.Count;
            Log.SUCCESS_QTY = Success_Qty;
            Log.ERROR_QTY = Error_qty;
            Log.RUNNING_DATE = DateTime.Now;
            svrRunning.NextId("DOOUT_RNL");

            SqlCommand sqlInsertLog = new SqlCommand();
            sqlInsertLog.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_LOG] ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],
                                        [READ_QTY],[SUCCESS_QTY],[ERROR_QTY],[RUNNING_DATE]) 
            VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@READ_QTY,@SUCCESS_QTY,@ERROR_QTY,@RUNNING_DATE)";

            sqlInsertLog.Parameters.Add(new SqlParameter("@LOG_NBR", Log.LOG_NBR));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SET_CODE", Log.SET_CODE));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS", Log.PROCESS));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_ST", Log.PROCESS_ST));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_EN", Log.PROCESS_EN));
            sqlInsertLog.Parameters.Add(new SqlParameter("@READ_QTY", Log.READ_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SUCCESS_QTY", Log.SUCCESS_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@ERROR_QTY", Log.ERROR_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@RUNNING_DATE", Log.RUNNING_DATE));

            conDBSCM.ExecuteCommand(sqlInsertLog);


        }
        private void UpdateRevisionDoplan(List<DOPLAN_T> oDO_QTYs, int rev, DOPLANKEY EditKey)
        {
            // ------ SET TO DB ------
            DateTime ST_DATE_I = DateTime.Now;
            DateTime EN_DATE_I = DateTime.Now;
            string PROCESS = "DO_PALN_REV";
            int Success_Qty = 0, Error_qty = 0, row_index = 1;

            // ------- REVISION OLD DATA NOT USE  ---------
            SqlCommand sqlUpdateDoqty = new SqlCommand();
            sqlUpdateDoqty.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_DOPLAN]
                      SET [LREV] = '{rev}'
                      WHERE [SET_CODE] = '{EditKey.setcode}' AND [PARTNO] = '{EditKey.partno}' AND [CM] = '{EditKey.cm}' AND [VENDER] = '{EditKey.vender}' AND [REV] = '{rev}'";

            conDBSCM.ExecuteCommand(sqlUpdateDoqty);

            // ------- INSERT --------
            if (oDO_QTYs.Count > 0)
            {
                DateTime RNE_ST = DateTime.Now;
                string DBResult = "";
                try
                {
                    foreach (var oDoqty in oDO_QTYs)
                    {
                        RNE_ST = DateTime.Now;

                        SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                        sqlInsertNewRevCalDoqtyData.CommandText = $@"INSERT INTO [dbSCM].[dbo].[DOOUT_DOPLAN]
                        ([REV],[LREV],[SET_CODE],[PRDYMD],[DO_DATE],[VENDER],[VENDER_N],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PLAN_QTY],[CONSUMTION_QTY],[CAL_QTY],[CALDOD_QTY],
                            [MARK_QTY],[DO_QTY],[STK_INHOUSE_BF],[STK_INHOUSE_AF],[STK_WH_BF],[STK_WH_AF],[DATA_DT],[REMARK1],[REMARK2],[REMARK3]) 
                        VALUES (@REV,@LREV,@SET_CODE,@PRDYMD,@DO_DATE,@VENDER,@VENDER_N,@PARTNO,@CM,@PARTNAME,@WHNO,@WHCODE,@WHNAME,@PLAN_QTY,@CONSUMTION_QTY,@CAL_QTY,@CALDOD_QTY,
                            @MARK_QTY,@DO_QTY,@STK_INHOUSE_BF,@STK_INHOUSE_AF,@STK_WH_BF,@STK_WH_AF,@DATA_DT,@REMARK1,@REMARK2,@REMARK3)";

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REV", oDoqty.rev);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@LREV", 999);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@SET_CODE", oDoqty.set_code);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PRDYMD", oDoqty.prdymd);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DO_DATE", oDoqty.do_date);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER", oDoqty.vender);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@VENDER_N", oDoqty.vender_n);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNO", oDoqty.partno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CM", oDoqty.cm);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PARTNAME", oDoqty.partname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNO", oDoqty.whno);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHCODE", oDoqty.whcode);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@WHNAME", oDoqty.whname);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@PLAN_QTY", oDoqty.plan_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CONSUMTION_QTY", oDoqty.consumtion_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CAL_QTY", oDoqty.cal_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@CALDOD_QTY", oDoqty.caldod_qty);

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@MARK_QTY", oDoqty.mark_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DO_QTY", oDoqty.do_qty);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_INHOUSE_BF", oDoqty.stk_inhouse_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_INHOUSE_AF", oDoqty.stk_inhouse_af);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_WH_BF", oDoqty.stk_wh_bf);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@STK_WH_AF", oDoqty.stk_wh_af);

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@DATA_DT", DateTime.Now);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK1", oDoqty.remark1);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK2", oDoqty.remark2);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@REMARK3", oDoqty.remark3);



                        DBResult = conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);

                        if (DBResult == "Success")
                        {
                            Success_Qty += 1;
                        }
                        else
                        {
                            Error_qty += 1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    DateTime RNE_EN = DateTime.Now;

                    RUNNING_ERROR LOG_ERROR = new RUNNING_ERROR();
                    LOG_ERROR.LOG_NBR = svrRunning.LoadUnique("DOOUT_ERR").ToString(true);
                    LOG_ERROR.SET_CODE = EditKey.setcode;
                    LOG_ERROR.PROCESS = PROCESS;
                    LOG_ERROR.PROCESS_ST = RNE_ST;
                    LOG_ERROR.PROCESS_EN = RNE_EN;
                    LOG_ERROR.ERROR_ROW = row_index;
                    LOG_ERROR.ERROR_TEXT = ex.Message;
                    LOG_ERROR.RUNNING_DATE = DateTime.Now;

                    SqlCommand sqlInsertLogError = new SqlCommand();
                    sqlInsertLogError.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_ERROR] 
                        ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],[ERROR_ROW],[ERROR_TEXT],[RUNNING_DATE]) 
                        VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@ERROR_ROW,@ERROR_TEXT,@RUNNING_DATE)";
                    sqlInsertLogError.Parameters.AddWithValue("@LOG_NBR", LOG_ERROR.LOG_NBR);
                    sqlInsertLogError.Parameters.AddWithValue("@SET_CODE", LOG_ERROR.SET_CODE);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS", LOG_ERROR.PROCESS);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_ST", LOG_ERROR.PROCESS_ST);
                    sqlInsertLogError.Parameters.AddWithValue("@PROCESS_EN", LOG_ERROR.PROCESS_EN);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_ROW", LOG_ERROR.ERROR_ROW);
                    sqlInsertLogError.Parameters.AddWithValue("@ERROR_TEXT", LOG_ERROR.ERROR_TEXT);
                    sqlInsertLogError.Parameters.AddWithValue("@RUNNING_DATE", LOG_ERROR.RUNNING_DATE);

                    conDBSCM.ExecuteCommand(sqlInsertLogError);
                    svrRunning.NextId("DOOUT_ERR");
                }

                row_index++;

                EN_DATE_I = DateTime.Now;
            }

            //-----LOG------
            RUNNING_LOG Log = new RUNNING_LOG();
            Log.LOG_NBR = svrRunning.LoadUnique("DOOUT_RNL").ToString(true);
            Log.SET_CODE = EditKey.setcode;
            Log.PROCESS = PROCESS;
            Log.PROCESS_ST = ST_DATE_I;
            Log.PROCESS_EN = EN_DATE_I;
            Log.READ_QTY = oDO_QTYs.Count;
            Log.SUCCESS_QTY = Success_Qty;
            Log.ERROR_QTY = Error_qty;
            Log.RUNNING_DATE = DateTime.Now;
            svrRunning.NextId("DOOUT_RNL");

            SqlCommand sqlInsertLog = new SqlCommand();
            sqlInsertLog.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_RUNNING_LOG] ([LOG_NBR],[SET_CODE],[PROCESS],[PROCESS_ST],[PROCESS_EN],
                                        [READ_QTY],[SUCCESS_QTY],[ERROR_QTY],[RUNNING_DATE]) 
            VALUES (@LOG_NBR,@SET_CODE,@PROCESS,@PROCESS_ST,@PROCESS_EN,@READ_QTY,@SUCCESS_QTY,@ERROR_QTY,@RUNNING_DATE)";

            sqlInsertLog.Parameters.Add(new SqlParameter("@LOG_NBR", Log.LOG_NBR));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SET_CODE", Log.SET_CODE));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS", Log.PROCESS));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_ST", Log.PROCESS_ST));
            sqlInsertLog.Parameters.Add(new SqlParameter("@PROCESS_EN", Log.PROCESS_EN));
            sqlInsertLog.Parameters.Add(new SqlParameter("@READ_QTY", Log.READ_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@SUCCESS_QTY", Log.SUCCESS_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@ERROR_QTY", Log.ERROR_QTY));
            sqlInsertLog.Parameters.Add(new SqlParameter("@RUNNING_DATE", Log.RUNNING_DATE));

            conDBSCM.ExecuteCommand(sqlInsertLog);


        }
    }
}
