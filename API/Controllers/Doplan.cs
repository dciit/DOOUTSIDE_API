using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API.Model.ModelDoplan;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;
using static API.Model.ModelTrigger;
using static API.Model.ModelTransaction;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Doplan : ControllerBase
    {
        SqlConnectDB conDBSCM = new("DBSCM");
        OraConnectDB dbAlpha2 = new("ALPHA02");

        [HttpGet]
        [Route("GetDoPlan")]
        public IActionResult GET_DOPLAN([FromQuery] string id)
        {
            string strDT = "1901-01-01";
            DateTime DT = Convert.ToDateTime(strDT);

            // doplan now
            List<cDOPLAN> oDOPLANs = new List<cDOPLAN>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = $@"
            SELECT  [REV],[LREV],[NBR],[SET_CODE],[PRDYMD],[DO_DATE],[VENDER],[VENDER_N],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PLAN_QTY],[CONSUMTION_QTY],
                   [CAL_QTY],[CALDOD_QTY],[MARK_QTY],[DO_QTY],[STK_INHOUSE_BF],[STK_INHOUSE_AF],[STK_WH_BF],[STK_WH_AF],[PICKLIST],[RECIVE_DT],[RECIVE_QTY],[VDTOWHB_DT],[VDTOWHB_QTY],[DATA_DT],[REMARK1],[REMARK2],[REMARK3]

            FROM [dbSCM].[dbo].[DOOUT_DOPLAN] 

            WHERE [SET_CODE] = (SELECT MAX([SET_CODE]) FROM [dbSCM].[dbo].[DOOUT_DOPLAN])
            AND [LREV] = 999 AND VENDER = '{id}'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    cDOPLAN oDOPLAN = new cDOPLAN();
                    oDOPLAN.rev = (dr["REV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["REV"]);
                    oDOPLAN.lrev = (dr["LREV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LREV"]);
                    oDOPLAN.nbr = (dr["NBR"] == DBNull.Value) ? "" : dr["NBR"].ToString();
                    oDOPLAN.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    oDOPLAN.prdymd = (dr["PRDYMD"] == DBNull.Value) ? "" : dr["PRDYMD"].ToString();
                    oDOPLAN.do_date = (dr["DO_DATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DO_DATE"]);
                    oDOPLAN.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    oDOPLAN.vender_n = (dr["VENDER_N"] == DBNull.Value) ? "" : dr["VENDER_N"].ToString();
                    oDOPLAN.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                    oDOPLAN.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                    oDOPLAN.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                    oDOPLAN.whno = (dr["WHNO"] == DBNull.Value) ? "" : dr["WHNO"].ToString();
                    oDOPLAN.whcode = (dr["WHCODE"] == DBNull.Value) ? "" : dr["WHCODE"].ToString();
                    oDOPLAN.whname = (dr["WHNAME"] == DBNull.Value) ? "" : dr["WHNAME"].ToString();
                    oDOPLAN.plan_qty = (dr["PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_QTY"]);
                    oDOPLAN.consumtion_qty = (dr["CONSUMTION_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONSUMTION_QTY"]);
                    oDOPLAN.cal_qty = (dr["CAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_QTY"]);
                    oDOPLAN.caldod_qty = (dr["CALDOD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CALDOD_QTY"]);
                    oDOPLAN.mark_qty = (dr["MARK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MARK_QTY"]);
                    oDOPLAN.do_qty = (dr["DO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DO_QTY"]);
                    oDOPLAN.stk_inhouse_bf = (dr["STK_INHOUSE_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_BF"]);
                    oDOPLAN.stk_inhouse_af = (dr["STK_INHOUSE_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_AF"]);
                    oDOPLAN.stk_wh_bf = (dr["STK_WH_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_BF"]);
                    oDOPLAN.stk_wh_af = (dr["STK_WH_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_AF"]);
                    oDOPLAN.picklist = (dr["PICKLIST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PICKLIST"]);
                    oDOPLAN.recive_dt = (dr["RECIVE_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["RECIVE_DT"]);
                    oDOPLAN.recive_qty = (dr["RECIVE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RECIVE_QTY"]);
                    oDOPLAN.vdtowhb_dt = (dr["VDTOWHB_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["VDTOWHB_DT"]);
                    oDOPLAN.vdtowhb_qty = (dr["VDTOWHB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["VDTOWHB_QTY"]);
                    oDOPLAN.data_dt = (dr["DATA_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DATA_DT"]);
                    oDOPLAN.remark1 = (dr["REMARK1"] == DBNull.Value) ? "" : dr["REMARK1"].ToString();
                    oDOPLAN.remark2 = (dr["REMARK2"] == DBNull.Value) ? "" : dr["REMARK2"].ToString();
                    oDOPLAN.remark3 = (dr["REMARK3"] == DBNull.Value) ? "" : dr["REMARK3"].ToString();

                    oDOPLANs.Add(oDOPLAN);
                }

                // doplan pre plan
                SqlCommand sqlselectpredaysplan = new SqlCommand();
                sqlselectpredaysplan.CommandText = @"
                SELECT [Dict_Code],[Dict_Type],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Dict_Ref4],[Dict_Ref5],
                       [Dict_Value1],[Dict_Value2],[DIct_Value3],[Dict_Value4],[Dict_Value5],[Update_By],[Update_DT]
                 FROM  [dbSCM].[dbo].[DOOUT_Dict]
                 WHERE [Dict_Type] = 'PG_SETTING' AND [Dict_Ref1] = 'PRE_PLAN'";

                DataTable dtpredays = conDBSCM.Query(sqlselectpredaysplan);
                int predays = 0;
                if (dtpredays.Rows.Count > 0)
                {
                    predays = Convert.ToInt32(dtpredays.Rows[0]["Dict_Value1"]);
                }
                predays = -Math.Abs(predays);

                // join plan preplan and now plan
                DateTime P_OLD_ST = oDOPLANs.Min(s => s.do_date);
                DateTime P_OLD_EN = P_OLD_ST.AddDays(predays);
                List<cDOPLAN> oDOPLAN_OLDs = new List<cDOPLAN>();
                while (P_OLD_EN < P_OLD_ST)
                {
                    SqlCommand sqlselectolddata = new SqlCommand();
                    sqlselectolddata.CommandText = $@"
                SELECT *
                  FROM [dbSCM].[dbo].[DOOUT_DOPLAN]
                  WHERE [DO_DATE] = @DT AND [LREV] = 999 AND VENDER = '{id}' AND [SET_CODE] = (SELECT MAX([SET_CODE]) FROM [dbSCM].[dbo].[DOOUT_DOPLAN] WHERE [DO_DATE] = @DT)";

                    sqlselectolddata.Parameters.Add(new SqlParameter("@DT", P_OLD_EN.Date.ToString("yyyy-MM-dd")));
                    DataTable dtoldp = conDBSCM.Query(sqlselectolddata);

                    if (dtoldp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtoldp.Rows)
                        {
                            cDOPLAN oDOPLAN = new cDOPLAN();
                            oDOPLAN.rev = (dr["REV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["REV"]);
                            oDOPLAN.lrev = (dr["LREV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LREV"]);
                            oDOPLAN.nbr = (dr["NBR"] == DBNull.Value) ? "" : dr["NBR"].ToString();
                            oDOPLAN.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                            oDOPLAN.prdymd = (dr["PRDYMD"] == DBNull.Value) ? "" : dr["PRDYMD"].ToString();
                            oDOPLAN.do_date = (dr["DO_DATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DO_DATE"]);
                            oDOPLAN.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                            oDOPLAN.vender_n = (dr["VENDER_N"] == DBNull.Value) ? "" : dr["VENDER_N"].ToString();
                            oDOPLAN.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                            oDOPLAN.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                            oDOPLAN.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                            oDOPLAN.whno = (dr["WHNO"] == DBNull.Value) ? "" : dr["WHNO"].ToString();
                            oDOPLAN.whcode = (dr["WHCODE"] == DBNull.Value) ? "" : dr["WHCODE"].ToString();
                            oDOPLAN.whname = (dr["WHNAME"] == DBNull.Value) ? "" : dr["WHNAME"].ToString();
                            oDOPLAN.plan_qty = (dr["PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_QTY"]);
                            oDOPLAN.consumtion_qty = (dr["CONSUMTION_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONSUMTION_QTY"]);
                            oDOPLAN.cal_qty = (dr["CAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_QTY"]);
                            oDOPLAN.do_qty = (dr["DO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DO_QTY"]);
                            oDOPLAN.stk_inhouse_bf = (dr["STK_INHOUSE_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_BF"]);
                            oDOPLAN.stk_inhouse_af = (dr["STK_INHOUSE_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_AF"]);
                            oDOPLAN.stk_wh_bf = (dr["STK_WH_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_BF"]);
                            oDOPLAN.stk_wh_af = (dr["STK_WH_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_AF"]);
                            oDOPLAN.picklist = (dr["PICKLIST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PICKLIST"]);
                            oDOPLAN.recive_dt = (dr["RECIVE_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["RECIVE_DT"]);
                            oDOPLAN.recive_qty = (dr["RECIVE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RECIVE_QTY"]);
                            oDOPLAN.data_dt = (dr["DATA_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DATA_DT"]);
                            oDOPLAN.remark1 = (dr["REMARK1"] == DBNull.Value) ? "" : dr["REMARK1"].ToString();
                            oDOPLAN.remark2 = (dr["REMARK2"] == DBNull.Value) ? "" : dr["REMARK2"].ToString();
                            oDOPLAN.remark3 = (dr["REMARK3"] == DBNull.Value) ? "" : dr["REMARK3"].ToString();
                            oDOPLANs.Add(oDOPLAN);
                        }
                    }
                    P_OLD_EN = P_OLD_EN.AddDays(1);
                }
                oDOPLANs = oDOPLANs
                    .OrderBy(items => items.vender)
                    .ThenBy(items => items.partno)
                    .ThenBy(items => items.do_date)
                    .ToList();


                // day off 
                List<VDFIXEDDAYS> oVDFDs = GET_FIXED_DAY();
                List<DELIVERY_CYCLES> DELICYCLE = GET_DELIVERY_CYCLE();
                List<CALENDAR> oCALENDARs = GET_CALENDAR();

                oDOPLANs = oDOPLANs.Select(item =>
                {
                    item.status_fixed_date = false;
                    return item;
                }).ToList();
                foreach (var oVDFD in oVDFDs)
                {
                    oDOPLANs.Where(item => item.whcode == oVDFD.whcode
                                          && item.do_date.Date >= DateTime.Now.Date
                                          && item.do_date.Date < DateTime.Now.AddDays(oVDFD.day).Date)
                            .ToList()
                            .ForEach(item => item.status_fixed_date = true);
                }

                foreach (var oDOPLAN in oDOPLANs)
                {
                    var TYPE_DELI = DELICYCLE.Where(s => s.vender == "SG1887").FirstOrDefault();

                    if (TYPE_DELI.del_type == "Daily" || TYPE_DELI.del_type == "Weekly")
                    {
                        string Day = oDOPLAN.do_date.ToString("ddd");
                        bool sendstatus = false;
                        switch (Day)
                        {
                            case "Sun":
                                sendstatus = (TYPE_DELI.del_wk_sun);
                                break;
                            case "Mon":
                                sendstatus = (TYPE_DELI.del_wk_mon);
                                break;
                            case "Tue":
                                sendstatus = (TYPE_DELI.del_wk_tue);
                                break;
                            case "Wed":
                                sendstatus = (TYPE_DELI.del_wk_wed);
                                break;
                            case "Thu":
                                sendstatus = (TYPE_DELI.del_wk_thu);
                                break;
                            case "Fri":
                                sendstatus = (TYPE_DELI.del_wk_fri);
                                break;
                            case "Sat":
                                sendstatus = (TYPE_DELI.del_wk_sat);
                                break;
                            default:
                                sendstatus = false;
                                break;
                        }
                        if (sendstatus)
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = true);
                        }
                        else
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = false);
                        }
                    }
                    else if (TYPE_DELI.del_type == "Monthly")
                    {
                        string Day = oDOPLAN.do_date.Day.ToString();
                        bool sendstatus = false;
                        switch (Day)
                        {
                            case "1":
                                sendstatus = TYPE_DELI.del_mo_01;
                                break;
                            case "2":
                                sendstatus = TYPE_DELI.del_mo_02;
                                break;
                            case "3":
                                sendstatus = TYPE_DELI.del_mo_03;
                                break;
                            case "4":
                                sendstatus = TYPE_DELI.del_mo_04;
                                break;
                            case "5":
                                sendstatus = TYPE_DELI.del_mo_05;
                                break;
                            case "6":
                                sendstatus = TYPE_DELI.del_mo_06;
                                break;
                            case "7":
                                sendstatus = TYPE_DELI.del_mo_07;
                                break;
                            case "8":
                                sendstatus = TYPE_DELI.del_mo_08;
                                break;
                            case "9":
                                sendstatus = TYPE_DELI.del_mo_09;
                                break;
                            case "10":
                                sendstatus = TYPE_DELI.del_mo_10;
                                break;
                            case "11":
                                sendstatus = TYPE_DELI.del_mo_11;
                                break;
                            case "12":
                                sendstatus = TYPE_DELI.del_mo_13;
                                break;
                            case "13":
                                sendstatus = TYPE_DELI.del_mo_13;
                                break;
                            case "14":
                                sendstatus = TYPE_DELI.del_mo_14;
                                break;
                            case "15":
                                sendstatus = TYPE_DELI.del_mo_15;
                                break;
                            case "16":
                                sendstatus = TYPE_DELI.del_mo_16;
                                break;
                            case "17":
                                sendstatus = TYPE_DELI.del_mo_17;
                                break;
                            case "18":
                                sendstatus = TYPE_DELI.del_mo_18;
                                break;
                            case "19":
                                sendstatus = TYPE_DELI.del_mo_19;
                                break;
                            case "20":
                                sendstatus = TYPE_DELI.del_mo_20;
                                break;
                            case "21":
                                sendstatus = TYPE_DELI.del_mo_21;
                                break;
                            case "22":
                                sendstatus = TYPE_DELI.del_mo_22;
                                break;
                            case "23":
                                sendstatus = TYPE_DELI.del_mo_23;
                                break;
                            case "24":
                                sendstatus = TYPE_DELI.del_mo_24;
                                break;
                            case "25":
                                sendstatus = TYPE_DELI.del_mo_25;
                                break;
                            case "26":
                                sendstatus = TYPE_DELI.del_mo_26;
                                break;
                            case "27":
                                sendstatus = TYPE_DELI.del_mo_27;
                                break;
                            case "28":
                                sendstatus = TYPE_DELI.del_mo_28;
                                break;
                            case "29":
                                sendstatus = TYPE_DELI.del_mo_29;
                                break;
                            case "30":
                                sendstatus = TYPE_DELI.del_mo_30;
                                break;
                            case "31":
                                sendstatus = TYPE_DELI.del_mo_31;
                                break;
                        }
                        if (sendstatus)
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = true);
                        }
                        else
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = false);
                        }
                    }
                    else
                    {

                    }
                }
                List<CALENDAR> oVD_CALENDARs = oCALENDARs.Where(item => item.h_type == "VENDER_H").ToList();
                List<CALENDAR> oDCI_CALENDARs = oCALENDARs.Where(item => item.h_type == "DCI").ToList();
                foreach (var oVD_CAL in oVD_CALENDARs)
                {
                    oDOPLANs.Where(item => item.whcode == oVD_CAL.vender && item.do_date.Date == oVD_CAL.h_date.Date)
                        .ToList()
                        .ForEach(item => item.status_holiday = true);
                }
                foreach (var oDCI_CAL in oDCI_CALENDARs)
                {
                    oDOPLANs.Where(item => item.do_date.Date == oDCI_CAL.h_date.Date)
                        .ToList()
                        .ForEach(item => item.status_holiday = true);
                }

                // cut part not use
                SqlCommand sqlselectPartvd = new SqlCommand();
                sqlselectPartvd.CommandText = @"
                SELECT [PARTNO],[CM],[VENDER_C],[STATUS]
                FROM [dbSCM].[dbo].[DOOUT_PARTMSTR]
                WHERE [STATUS] = 'INACTIVE'";
                DataTable dtpartvd = conDBSCM.Query(sqlselectPartvd);

                List<PARTMSTR> partvds = new List<PARTMSTR>();
                if (dtpartvd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtpartvd.Rows)
                    {
                        PARTMSTR opartvd = new PARTMSTR();
                        opartvd.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                        opartvd.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                        opartvd.vender = (dr["VENDER_C"] == DBNull.Value) ? "" : dr["VENDER_C"].ToString();
                        opartvd.status = (dr["STATUS"] == DBNull.Value) ? "" : dr["STATUS"].ToString();
                        partvds.Add(opartvd);
                    }
                }
                oDOPLANs = oDOPLANs.Where(item => !partvds.Any(key =>
                    item.partno == key.partno &&
                    item.cm == key.cm &&
                    item.vender == key.vender)).ToList();

                // vender -> whb actual part
                OracleCommand selectVdToFtt = new OracleCommand();
                //selectVdToFtt.CommandText = @"
                //SELECT PONO, WHNO, PARTNO, CM, SUM(WQTY) AS WQTY, ACDATE, HTCODE, ACTIME 
                //FROM DST_DATAC1 
                //WHERE ACDATE = :Datenow
                //GROUP BY PONO, WHNO, PARTNO, CM, ACDATE, HTCODE, ACTIME";
                selectVdToFtt.CommandText = @"
                SELECT WHNO, PARTNO, CM, SUM(WQTY) AS WQTY, ACDATE, HTCODE, ACTIME 
                FROM DST_DATAC1 
                WHERE TRIM(ACDATE) BETWEEN :stdate AND :endate AND WHNO = 'WB'
                GROUP BY WHNO, PARTNO, CM, ACDATE, HTCODE, ACTIME";

                selectVdToFtt.Parameters.Add(new OracleParameter("stdate", DateTime.Now.AddDays(-5).ToString("yyyyMMdd")));
                selectVdToFtt.Parameters.Add(new OracleParameter("endate", DateTime.Now.AddDays(1).ToString("yyyyMMdd")));

                DataTable dt = dbAlpha2.Query(selectVdToFtt);
                List<DST_DATAC1> vd_to_ftt = new List<DST_DATAC1>();
                foreach (DataRow dr in dt.Rows)
                {
                    DST_DATAC1 item = new DST_DATAC1();
                    item.whno = dr["WHNO"]!.ToString()!.Trim();
                    item.partno = dr["PARTNO"]!.ToString()!.Trim();
                    item.cm = dr["CM"]!.ToString()!.Trim();
                    item.wqty = double.Parse(dr["WQTY"]!.ToString()!);
                    item.acdate = dr["ACDATE"]!.ToString()!;
                    item.vendor = dr["HTCODE"]!.ToString()!;
                    vd_to_ftt.Add(item);
                }
                foreach (var dp in oDOPLANs)
                {
                    var doDateString = dp.do_date.ToString("yyyyMMdd");  // <<< แปลง do_date เป็น string

                    var matchingWQTYs = vd_to_ftt
                        .Where(dst => dst.partno.Trim() == dp.partno.Trim()
                                   && dst.cm.Trim() == dp.cm.Trim()
                                   && dst.acdate == doDateString)  // <<< เทียบ string ตรงๆ
                        .Select(dst => new { dst.wqty, dst.acdate });

                    if (matchingWQTYs.Any())
                    {
                        dp.vdtowhb_qty = Convert.ToDecimal(matchingWQTYs.Sum(m => m.wqty));
                        dp.vdtowhb_dt = DateTime.ParseExact(matchingWQTYs.FirstOrDefault()?.acdate, "yyyyMMdd", null);
                    }
                }

                //whb -> wh1 actual part
                OracleCommand selectFttToDci = new OracleCommand();
                //selectFttToDci.CommandText = @"
                //SELECT   DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM, SUM(DTD.RQTY) RQTY 
                //FROM MC.DST_ISTCTL  DTC
                //LEFT JOIN  MC.DST_ISTDTL  DTD ON DTC.DELNO = DTD.DELNO
                //WHERE DTC.CDATE  BETWEEN to_date(:stdate, 'YYYYMMDD') AND to_date(:endate,'YYYYMMDD') 
                //    AND DTC.RCBIT = 'F' AND DTC.TWH IN ('W1','W2')
                //GROUP BY  DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM";

                //selectFttToDci.CommandText = @"
                //SELECT   DTC.RDATE, DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM, SUM(DTD.RQTY) RQTY 
                //FROM MC.DST_ISTCTL  DTC
                //LEFT JOIN  MC.DST_ISTDTL  DTD ON DTC.DELNO = DTD.DELNO
                //WHERE DTC.DELDATE  BETWEEN :stdate AND :endate
                //    AND DTC.RCBIT = 'F' AND DTC.FWH = 'WB' AND DTC.TWH IN ('W1','W2')
                //GROUP BY  DTC.RDATE, DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM";

                selectFttToDci.CommandText = @"
                SELECT
                    CAST(DTC.RDATE AS DATE) AS RDATE,
                    DTC.FWH,
                    DTC.TWH,
                    DTD.PARTNO,
                    DTD.CM,
                    SUM(DTD.RQTY) AS RQTY
                FROM MC.DST_ISTCTL DTC
                LEFT JOIN MC.DST_ISTDTL DTD ON DTC.DELNO = DTD.DELNO
                WHERE
                    DTC.RDATE BETWEEN TO_DATE(:stdate, 'YYYY-MM-DD') AND TO_DATE(:endate, 'YYYY-MM-DD')
                    AND DTC.RCBIT = 'F'
                    AND DTC.FWH = 'WB'
                    AND DTC.TWH IN ('W1', 'W2')
                GROUP BY
                    CAST(DTC.RDATE AS DATE),
                    DTC.FWH,
                    DTC.TWH,
                    DTD.PARTNO,
                    DTD.CM";

                selectFttToDci.Parameters.Add(new OracleParameter("stdate", DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd")));
                selectFttToDci.Parameters.Add(new OracleParameter("endate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));

                DataTable dt_r = dbAlpha2.Query(selectFttToDci);
                List<DST_DATAC1> ftt_to_dcis = new List<DST_DATAC1>();
                foreach (DataRow dr in dt_r.Rows)
                {
                    DST_DATAC1 item = new DST_DATAC1();
                    item.whno = dr["FWH"].ToString().Trim();
                    item.partno = dr["PARTNO"].ToString().Trim();
                    item.cm = dr["CM"].ToString().Trim();
                    item.wqty = double.Parse(dr["RQTY"].ToString());
                    item.acdate = Convert.ToDateTime(dr["RDATE"]).ToString("yyyyMMdd");

                    ftt_to_dcis.Add(item);
                }
                foreach (var dp in oDOPLANs)
                {
                    var doDateString = dp.do_date.ToString("yyyyMMdd");  // <<< แปลง do_date เป็น string

                    var matchingWQTYs = ftt_to_dcis
                        .Where(dst => dst.partno.Trim() == dp.partno.Trim()
                                   && dst.cm.Trim() == dp.cm.Trim()
                                   && dst.acdate == doDateString)  // <<< เทียบ string ตรงๆ
                        .Select(dst => new { dst.wqty, dst.acdate });

                    if (matchingWQTYs.Any())
                    {
                        dp.recive_qty = Convert.ToDecimal(matchingWQTYs.Sum(m => m.wqty));
                        dp.recive_dt = DateTime.ParseExact(matchingWQTYs.FirstOrDefault()?.acdate, "yyyyMMdd", null);
                    }
                }


                // set part is shipping to day
                var partNumbersWithPositiveQty = oDOPLANs
                    .Where(item => item.do_date.Date.Date == DateTime.Now.Date && item.do_qty > 0)
                    .Select(item => new { item.partno, item.cm })
                    .Distinct()
                    .ToList();
                oDOPLANs.Where(item => partNumbersWithPositiveQty
                       .Any(p => p.partno == item.partno && p.cm == item.cm))
                       .ToList()
                       .ForEach(item => item.shipping = true);

                // update production picklist part 
                List<PICKLIST> listpicklist = new List<PICKLIST>();
                SqlCommand Sqlcmdpicklist = new SqlCommand();
                Sqlcmdpicklist.CommandText = @"
                SELECT PARTNO,BRUSN,CAST(IDATE AS DATE) AS IDATE,SUM(FQTY) AS FQTY
                FROM [dbSCM].[dbo].[AL_GST_DATPID]
                WHERE PRGBIT IN ('F', 'C') AND IDATE BETWEEN @Sdate AND @Ndate
                GROUP BY PARTNO, BRUSN, IDATE";

                Sqlcmdpicklist.Parameters.Add(new SqlParameter("@Sdate", DateTime.Now.AddDays(-4).Date.ToString("yyyy-MM-dd")));
                Sqlcmdpicklist.Parameters.Add(new SqlParameter("@Ndate", DateTime.Now.Date.ToString("yyyy-MM-dd")));
                DataTable dt_picklist = conDBSCM.Query(Sqlcmdpicklist);

                if (dt_picklist.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_picklist.Rows)
                    {
                        PICKLIST item = new PICKLIST();
                        item.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                        item.cm = (dr["BRUSN"] == DBNull.Value) ? "" : dr["BRUSN"].ToString();
                        item.date = (dr["IDATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["IDATE"]);
                        item.qty = (dr["FQTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FQTY"]);
                        listpicklist.Add(item);
                    }
                }
                foreach (var item in listpicklist)
                {
                    var match = oDOPLANs.FirstOrDefault(x =>
                    x.partno == item.partno &&
                    x.cm == item.cm &&
                    x.do_date.Date == item.date.Date
                    );
                    if (match != null)
                    {
                        match.picklist = item.qty;
                    }
                }

                // update mark status 
                List<PARTMSTR> listpartmstr = GET_PARTMSTR();
                var activeKeys = listpartmstr
                    .Where(s => s.markstatus == "ACTIVE")
                    .Select(s => $"{s.partno}_{s.cm}_{s.vender}")
                    .ToHashSet();
                foreach (var plan in oDOPLANs)
                {
                    if (activeKeys.Contains($"{plan.partno}_{plan.cm}_{plan.vender}"))
                    {
                        plan.status_mark = true;
                    }
                    var partmstr = listpartmstr.Where(s => s.partno.Trim() == plan.partno.Trim() && s.cm.Trim() == plan.cm.Trim()).FirstOrDefault();
                    if (partmstr != null)
                    {
                        plan.pm_sfstk = partmstr.sfstkqty;
                        plan.pm_markqty = partmstr.markqty;
                        plan.pm_minqty = partmstr.minqty;
                        plan.pm_maxqty = partmstr.maxqty;
                        plan.pm_qtybox = partmstr.qtybox;
                        plan.pm_boxpl = partmstr.boxpl;
                        plan.pm_typecal = partmstr.typecal;
                        plan.pm_truckstack = partmstr.truckstack;
                        plan.pm_plstack = partmstr.palletstack;
                        plan.pm_pdlt = partmstr.pdlt;
                        plan.pm_preorder = partmstr.preorderday;
                    }
                }

                return Ok(oDOPLANs);

            }
            else
            {
                return Ok(oDOPLANs);
            }

        }
        private List<DELIVERY_CYCLES> GET_DELIVERY_CYCLE()
        {
            List<DELIVERY_CYCLES> oDELIVERY_CYCLESs = new List<DELIVERY_CYCLES>();

            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
              SELECT [SET_CODE],[VENDER],[DEL_TYPE],[DEL_WK_SUN],[DEL_WK_MON],[DEL_WK_TUE],[DEL_WK_WED],[DEL_WK_THU],[DEL_WK_FRI],[DEL_WK_SAT],[DEL_MO_01],[DEL_MO_02],[DEL_MO_03],
              [DEL_MO_04],[DEL_MO_05],[DEL_MO_06],[DEL_MO_07],[DEL_MO_08],[DEL_MO_09],[DEL_MO_10],[DEL_MO_11],[DEL_MO_12],[DEL_MO_13],[DEL_MO_14],[DEL_MO_15],[DEL_MO_16],[DEL_MO_17],
              [DEL_MO_18],[DEL_MO_19],[DEL_MO_20],[DEL_MO_21],[DEL_MO_22],[DEL_MO_23],[DEL_MO_24],[DEL_MO_25],[DEL_MO_26],[DEL_MO_27],[DEL_MO_28],[DEL_MO_29],[DEL_MO_30],[DEL_MO_31],[DATA_DT]
              FROM [dbSCM].[dbo].[DOOUT_1_1DELIVERY_CYCLE]
              WHERE [SET_CODE] = (SELECT MAX(SET_CODE) FROM [dbSCM].[dbo].[DOOUT_1_1DELIVERY_CYCLE]) AND [VENDER] = 'SG1887'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);

            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    DELIVERY_CYCLES oDELIVERY_CYCLES = new DELIVERY_CYCLES();
                    oDELIVERY_CYCLES.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    oDELIVERY_CYCLES.del_type = (dr["DEL_TYPE"] == DBNull.Value) ? "" : dr["DEL_TYPE"].ToString();
                    oDELIVERY_CYCLES.del_wk_sun = (dr["DEL_WK_SUN"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_SUN"]);
                    oDELIVERY_CYCLES.del_wk_mon = (dr["DEL_WK_MON"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_MON"]);
                    oDELIVERY_CYCLES.del_wk_tue = (dr["DEL_WK_TUE"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_TUE"]);
                    oDELIVERY_CYCLES.del_wk_wed = (dr["DEL_WK_WED"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_WED"]);
                    oDELIVERY_CYCLES.del_wk_thu = (dr["DEL_WK_THU"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_THU"]);
                    oDELIVERY_CYCLES.del_wk_fri = (dr["DEL_WK_FRI"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_FRI"]);
                    oDELIVERY_CYCLES.del_wk_sat = (dr["DEL_WK_SAT"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_WK_SAT"]);
                    oDELIVERY_CYCLES.del_mo_01 = (dr["DEL_MO_01"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_01"]);
                    oDELIVERY_CYCLES.del_mo_02 = (dr["DEL_MO_02"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_02"]);
                    oDELIVERY_CYCLES.del_mo_03 = (dr["DEL_MO_03"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_03"]);
                    oDELIVERY_CYCLES.del_mo_04 = (dr["DEL_MO_04"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_04"]);
                    oDELIVERY_CYCLES.del_mo_05 = (dr["DEL_MO_05"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_05"]);
                    oDELIVERY_CYCLES.del_mo_06 = (dr["DEL_MO_06"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_06"]);
                    oDELIVERY_CYCLES.del_mo_07 = (dr["DEL_MO_07"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_07"]);
                    oDELIVERY_CYCLES.del_mo_08 = (dr["DEL_MO_08"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_08"]);
                    oDELIVERY_CYCLES.del_mo_09 = (dr["DEL_MO_09"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_09"]);
                    oDELIVERY_CYCLES.del_mo_10 = (dr["DEL_MO_10"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_10"]);
                    oDELIVERY_CYCLES.del_mo_11 = (dr["DEL_MO_11"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_11"]);
                    oDELIVERY_CYCLES.del_mo_12 = (dr["DEL_MO_12"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_12"]);
                    oDELIVERY_CYCLES.del_mo_13 = (dr["DEL_MO_13"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_13"]);
                    oDELIVERY_CYCLES.del_mo_14 = (dr["DEL_MO_14"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_14"]);
                    oDELIVERY_CYCLES.del_mo_15 = (dr["DEL_MO_15"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_15"]);
                    oDELIVERY_CYCLES.del_mo_16 = (dr["DEL_MO_16"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_16"]);
                    oDELIVERY_CYCLES.del_mo_17 = (dr["DEL_MO_17"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_17"]);
                    oDELIVERY_CYCLES.del_mo_18 = (dr["DEL_MO_18"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_18"]);
                    oDELIVERY_CYCLES.del_mo_19 = (dr["DEL_MO_19"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_19"]);
                    oDELIVERY_CYCLES.del_mo_20 = (dr["DEL_MO_20"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_20"]);
                    oDELIVERY_CYCLES.del_mo_21 = (dr["DEL_MO_21"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_21"]);
                    oDELIVERY_CYCLES.del_mo_22 = (dr["DEL_MO_22"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_22"]);
                    oDELIVERY_CYCLES.del_mo_23 = (dr["DEL_MO_23"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_23"]);
                    oDELIVERY_CYCLES.del_mo_24 = (dr["DEL_MO_24"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_24"]);
                    oDELIVERY_CYCLES.del_mo_25 = (dr["DEL_MO_25"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_25"]);
                    oDELIVERY_CYCLES.del_mo_26 = (dr["DEL_MO_26"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_26"]);
                    oDELIVERY_CYCLES.del_mo_27 = (dr["DEL_MO_27"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_27"]);
                    oDELIVERY_CYCLES.del_mo_28 = (dr["DEL_MO_28"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_28"]);
                    oDELIVERY_CYCLES.del_mo_29 = (dr["DEL_MO_29"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_29"]);
                    oDELIVERY_CYCLES.del_mo_30 = (dr["DEL_MO_30"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_30"]);
                    oDELIVERY_CYCLES.del_mo_31 = (dr["DEL_MO_31"] == DBNull.Value) ? false : Convert.ToBoolean(dr["DEL_MO_31"]);

                    oDELIVERY_CYCLESs.Add(oDELIVERY_CYCLES);
                }
            }
            return oDELIVERY_CYCLESs;
        }

        private List<VDFIXEDDAYS> GET_FIXED_DAY()
        {
            List<VDFIXEDDAYS> oVDFDs = new List<VDFIXEDDAYS>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
            SELECT [SET_CODE],[WH_OUT_CODE],[WH_OUT_NAME],[LOCATION],[PRIORITY],[RATIO],[FIXED_DAYS],[STATUS],[DATA_DT]
            FROM [dbSCM].[dbo].[DOOUT_1_3WH_OUTSITE_MSTR]
            WHERE [SET_CODE] = (SELECT MAX(SET_CODE) FROM [dbSCM].[dbo].[DOOUT_1_3WH_OUTSITE_MSTR])";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);

            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    VDFIXEDDAYS oVDFD = new VDFIXEDDAYS();
                    oVDFD.nbr = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    oVDFD.whcode = (dr["WH_OUT_CODE"] == DBNull.Value) ? "" : dr["WH_OUT_CODE"].ToString();
                    oVDFD.day = (dr["FIXED_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["FIXED_DAYS"]);
                    oVDFDs.Add(oVDFD);
                }
            }
            return oVDFDs;
        }

        private List<CALENDAR> GET_CALENDAR()
        {
            string str_DT = "1901-01-01";
            DateTime DT = Convert.ToDateTime(str_DT);

            List<CALENDAR> oCALs = new List<CALENDAR>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
                SELECT [SET_CODE],[H_TYPE],[VENDER],[H_DATE],[UPDATE_BY],[DATA_DT]
                FROM [dbSCM].[dbo].[DOOUT_1_5CALENDAR]
                WHERE [SET_CODE] = (SELECT MAX([SET_CODE]) FROM [dbSCM].[dbo].[DOOUT_1_5CALENDAR])";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);

            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    CALENDAR oCAL = new CALENDAR();
                    oCAL.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    oCAL.h_type = (dr["H_TYPE"] == DBNull.Value) ? "" : dr["H_TYPE"].ToString();
                    oCAL.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    oCAL.h_date = (dr["H_DATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["H_DATE"]);
                    oCALs.Add(oCAL);
                }
            }
            return oCALs;
        }

        private List<PARTMSTR> GET_PARTMSTR()
        {
            string str_DT = "1901-01-01";
            DateTime DT = Convert.ToDateTime(str_DT);

            List<PARTMSTR> listpartmstr = new List<PARTMSTR>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
            SELECT [PARTNO],[CM],[VENDER],[SFSTK_QTY],[MARK_QTY],[MIN_QTY],[MAX_QTY],[QTY_BOX],[BOX_PL],[TYPECAL],[TRUCK_STACK],[PALLET_STACK],[PREORDER_DAYS],[MARK_STATUS]
            FROM [dbSCM].[dbo].[DOOUT_1_2PART_MSTR]
            WHERE [SET_CODE] = (SELECT MAX([SET_CODE]) FROM [dbSCM].[dbo].[DOOUT_1_2PART_MSTR])";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);

            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    PARTMSTR opartmstr = new PARTMSTR();
                    opartmstr.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                    opartmstr.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                    opartmstr.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    opartmstr.sfstkqty = (dr["SFSTK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SFSTK_QTY"]);
                    opartmstr.markqty = (dr["MARK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MARK_QTY"]);
                    opartmstr.minqty = (dr["MIN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MIN_QTY"]);
                    opartmstr.maxqty = (dr["MAX_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MAX_QTY"]);
                    opartmstr.qtybox = (dr["QTY_BOX"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_BOX"]);
                    opartmstr.boxpl = (dr["BOX_PL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BOX_PL"]);
                    opartmstr.typecal = (dr["TYPECAL"] == DBNull.Value) ? "" : Convert.ToString(dr["TYPECAL"]);
                    opartmstr.truckstack = (dr["TRUCK_STACK"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TRUCK_STACK"]);
                    opartmstr.palletstack = (dr["PALLET_STACK"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PALLET_STACK"]);
                    opartmstr.preorderday = (dr["PREORDER_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PREORDER_DAYS"]);
                    opartmstr.markstatus = (dr["MARK_STATUS"] == DBNull.Value) ? "" : dr["MARK_STATUS"].ToString();
                    listpartmstr.Add(opartmstr);
                }
            }
            return listpartmstr;
        }

        private List<SHAREVD> GET_SHAREVD()
        {
            string str_DT = "1901-01-01";
            DateTime DT = Convert.ToDateTime(str_DT);

            List<SHAREVD> listsharevd = new List<SHAREVD>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
            SELECT [Dict_Ref1],[Dict_Ref2],[Dict_Ref3]
            FROM [dbSCM].[dbo].[DOOUT_Dict]
            WHERE [Dict_Type] = 'SHARE_VENDER' and [Status] = 'ACTIVE'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);

            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    SHAREVD item = new SHAREVD();
                    item.partno = (dr["Dict_Ref1"] == DBNull.Value) ? "" : dr["Dict_Ref1"].ToString().Trim();
                    item.cm = (dr["Dict_Ref2"] == DBNull.Value) ? "" : dr["Dict_Ref2"].ToString().Trim();
                    item.grpvd = (dr["Dict_Ref3"] == DBNull.Value) ? "" : dr["Dict_Ref3"].ToString().Trim();

                    listsharevd.Add(item);
                }
            }
            return listsharevd;
        }

        [HttpGet]
        [Route("GetVDMstr")]
        public IActionResult GET_VDMSTR()
        {
            List<VDSELECTMSTR> listvdmstr = new List<VDSELECTMSTR>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
            SELECT [Dict_Ref1],[Dict_Ref2]
	        FROM [dbSCM].[dbo].[DOOUT_Dict]
            WHERE [Dict_Type] = 'VENDER_MSTR' AND [Status] = 'ACTIVE'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    VDSELECTMSTR item = new VDSELECTMSTR();
                    item.vender = (dr["Dict_Ref1"] == DBNull.Value) ? "" : (dr["Dict_Ref1"]).ToString();
                    item.vender_n = (dr["Dict_Ref2"] == DBNull.Value) ? "" : (dr["Dict_Ref2"]).ToString();
                    listvdmstr.Add(item);
                }
            }

            //List<VDSELECTMSTR> VDMSTRs = new List<VDSELECTMSTR>();
            //OraConnectDB dbAL01 = new OraConnectDB("ALPHA01");
            //OracleCommand cmd = new OracleCommand();
            //cmd.CommandText = $@"select HTCODE,HATRNK,HATNK,TELNO,ADDRK1,ADDRK2,CDATE,UDATE,POEMAIL from  gst_mstven where kaiseq = 999 order by htcode";
            //DataTable vdmstr = dbAL01.Query(cmd);

            //if (vdmstr.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in vdmstr.Rows)
            //    {
            //        VDSELECTMSTR VDMSTR = new VDSELECTMSTR();
            //        VDMSTR.vender = (dr["HTCODE"] == DBNull.Value) ? "" : dr["HTCODE"].ToString().Trim();
            //        VDMSTR.vender_n = (dr["HATNK"] == DBNull.Value) ? "" : dr["HATNK"].ToString().Trim();
            //        VDMSTRs.Add(VDMSTR);
            //    }
            //}

            return Ok(listvdmstr);
        }
        [HttpPost]
        [Route("GetHistoryPlan")]
        public IActionResult GET_HISTORYPLAN([FromBody] DOPLANKEY param)
        {
            string strDT = "1901-01-01";
            DateTime DT = Convert.ToDateTime(strDT);

            // doplan now
            List<HISTORYPLAN> oDOPLANs = new List<HISTORYPLAN>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @$"
            SELECT TOP (10) [PLAN_QTY],[DATA_DT]
            FROM [dbSCM].[dbo].[DOOUT_DOPLAN]
            WHERE [PARTNO] = '{param.partno}'
              AND [CM] = '{param.cm}'
              AND [DO_DATE] = '{param.dodate.ToString("yyyy-MM-dd")}'
            ORDER BY [DATA_DT] ASC";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    HISTORYPLAN oDOPLAN = new HISTORYPLAN();
                    oDOPLAN.plan_qty = (dr["PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_QTY"]);
                    oDOPLAN.data_dt = (dr["DATA_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DATA_DT"]);

                    oDOPLANs.Add(oDOPLAN);
                }
            }


            return Ok(oDOPLANs);
        }

        [HttpPost]
        [Route("EditDOPlan")]
        public IActionResult EDIT_DOPLAN([FromBody] DOPLANKEY param)
        {
            EditDoplan process = new EditDoplan();
            process.CalEditDOPLAN(param);


            statusAction statusResult = new statusAction
            {
                status = "success",
                msg = "แก้ไขรายการสำเร็จ"
            };

            if (statusResult.status == "success")
            {
                string Old_Value = $@"setcode={param.setcode.ToString()},partno={param.partno.ToString()},cm={param.cm.ToString()},do_qty={param.doqtyOld.ToString()},mark_qty={param.markqtyOld},dodate={param.dodate.ToString("yyyy-MM-dd")}";

                string New_Value = $@"setcode={param.setcode.ToString()},partno={param.partno.ToString()},cm={param.cm.ToString()},do_qty={param.doqtyNew.ToString()},mark_qty={param.markqtyNew},dodate={param.dodate.ToString("yyyy-MM-dd")}";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @$"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG] 
                                  ([process],[action],[old_data],[new_data],[action_by],[action_dt])
                                  VALUES
                                  ('EDIT_DOPLAN','EDIT','{Old_Value}','{New_Value}','{param.update_by}',GETDATE())";
                conDBSCM.ExecuteCommand(cmd);
            }


            return Ok(statusResult);
        }

        [HttpGet]
        [Route("GetReport")]
        public IActionResult GET_REPORT_PLAN()
        {
            // doplan now 
            string strDT = "1901-01-01";
            DateTime DT = Convert.ToDateTime(strDT);

            // doplan now
            List<cDOPLAN> oDOPLANs = new List<cDOPLAN>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
            SELECT  [REV],[LREV],[NBR],[SET_CODE],[PRDYMD],[DO_DATE],[VENDER],[VENDER_N],[PARTNO],[CM],[PARTNAME],[WHNO],[WHCODE],[WHNAME],[PLAN_QTY],[CONSUMTION_QTY],
                   [CAL_QTY],[CALDOD_QTY],[MARK_QTY],[DO_QTY],[STK_INHOUSE_BF],[STK_INHOUSE_AF],[STK_WH_BF],[STK_WH_AF],[PICKLIST],[RECIVE_DT],[RECIVE_QTY],[VDTOWHB_DT],[VDTOWHB_QTY],[DATA_DT],[REMARK1],[REMARK2],[REMARK3]
            FROM [dbSCM].[dbo].[DOOUT_DOPLAN]

            WHERE [SET_CODE] = (SELECT MAX([SET_CODE]) FROM [dbSCM].[dbo].[DOOUT_DOPLAN])
            AND [LREV] = 999 AND ([DO_QTY] > 0 OR MARK_QTY > 0)";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    cDOPLAN oDOPLAN = new cDOPLAN();
                    oDOPLAN.rev = (dr["REV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["REV"]);
                    oDOPLAN.lrev = (dr["LREV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LREV"]);
                    oDOPLAN.nbr = (dr["NBR"] == DBNull.Value) ? "" : dr["NBR"].ToString();
                    oDOPLAN.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                    oDOPLAN.prdymd = (dr["PRDYMD"] == DBNull.Value) ? "" : dr["PRDYMD"].ToString();
                    oDOPLAN.do_date = (dr["DO_DATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DO_DATE"]);
                    oDOPLAN.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                    oDOPLAN.vender_n = (dr["VENDER_N"] == DBNull.Value) ? "" : dr["VENDER_N"].ToString();
                    oDOPLAN.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                    oDOPLAN.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                    oDOPLAN.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                    oDOPLAN.whno = (dr["WHNO"] == DBNull.Value) ? "" : dr["WHNO"].ToString();
                    oDOPLAN.whcode = (dr["WHCODE"] == DBNull.Value) ? "" : dr["WHCODE"].ToString();
                    oDOPLAN.whname = (dr["WHNAME"] == DBNull.Value) ? "" : dr["WHNAME"].ToString();
                    oDOPLAN.plan_qty = (dr["PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_QTY"]);
                    oDOPLAN.consumtion_qty = (dr["CONSUMTION_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONSUMTION_QTY"]);
                    oDOPLAN.cal_qty = (dr["CAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_QTY"]);
                    oDOPLAN.caldod_qty = (dr["CALDOD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CALDOD_QTY"]);
                    oDOPLAN.mark_qty = (dr["MARK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MARK_QTY"]);
                    oDOPLAN.do_qty = (dr["DO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DO_QTY"]);
                    oDOPLAN.stk_inhouse_bf = (dr["STK_INHOUSE_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_BF"]);
                    oDOPLAN.stk_inhouse_af = (dr["STK_INHOUSE_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_AF"]);
                    oDOPLAN.stk_wh_bf = (dr["STK_WH_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_BF"]);
                    oDOPLAN.stk_wh_af = (dr["STK_WH_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_AF"]);
                    oDOPLAN.picklist = (dr["PICKLIST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PICKLIST"]);
                    oDOPLAN.recive_dt = (dr["RECIVE_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["RECIVE_DT"]);
                    oDOPLAN.recive_qty = (dr["RECIVE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RECIVE_QTY"]);
                    oDOPLAN.vdtowhb_dt = (dr["VDTOWHB_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["VDTOWHB_DT"]);
                    oDOPLAN.vdtowhb_qty = (dr["VDTOWHB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["VDTOWHB_QTY"]);
                    oDOPLAN.data_dt = (dr["DATA_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DATA_DT"]);
                    oDOPLAN.remark1 = (dr["REMARK1"] == DBNull.Value) ? "" : dr["REMARK1"].ToString();
                    oDOPLAN.remark2 = (dr["REMARK2"] == DBNull.Value) ? "" : dr["REMARK2"].ToString();
                    oDOPLAN.remark3 = (dr["REMARK3"] == DBNull.Value) ? "" : dr["REMARK3"].ToString();

                    oDOPLANs.Add(oDOPLAN);
                }

                // doplan pre plan
                SqlCommand sqlselectpredaysplan = new SqlCommand();
                sqlselectpredaysplan.CommandText = @"
                SELECT [Dict_Code],[Dict_Type],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Dict_Ref4],[Dict_Ref5],
                       [Dict_Value1],[Dict_Value2],[DIct_Value3],[Dict_Value4],[Dict_Value5],[Update_By],[Update_DT]
                 FROM  [dbSCM].[dbo].[DOOUT_Dict]
                 WHERE [Dict_Type] = 'PG_SETTING' AND [Dict_Ref1] = 'PRE_PLAN'";

                DataTable dtpredays = conDBSCM.Query(sqlselectpredaysplan);
                int predays = 0;
                if (dtpredays.Rows.Count > 0)
                {
                    predays = Convert.ToInt32(dtpredays.Rows[0]["Dict_Value1"]);
                }
                predays = -Math.Abs(predays);

                // join plan preplan and now plan
                DateTime P_OLD_ST = oDOPLANs.Min(s => s.do_date);
                DateTime P_OLD_EN = P_OLD_ST.AddDays(predays);
                List<cDOPLAN> oDOPLAN_OLDs = new List<cDOPLAN>();
                while (P_OLD_EN < P_OLD_ST)
                {
                    SqlCommand sqlselectolddata = new SqlCommand();
                    sqlselectolddata.CommandText = @"
                SELECT *
                  FROM [dbSCM].[dbo].[DOOUT_DOPLAN]
                  WHERE [DO_DATE] = @DT AND [LREV] = 999 AND ([DO_QTY] > 0 OR MARK_QTY > 0) AND [SET_CODE] = (SELECT MAX([SET_CODE]) FROM [dbSCM].[dbo].[DOOUT_DOPLAN] WHERE [DO_DATE] = @DT)";

                    sqlselectolddata.Parameters.Add(new SqlParameter("@DT", P_OLD_EN.Date.ToString("yyyy-MM-dd")));
                    DataTable dtoldp = conDBSCM.Query(sqlselectolddata);

                    if (dtoldp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtoldp.Rows)
                        {
                            cDOPLAN oDOPLAN = new cDOPLAN();
                            oDOPLAN.rev = (dr["REV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["REV"]);
                            oDOPLAN.lrev = (dr["LREV"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LREV"]);
                            oDOPLAN.nbr = (dr["NBR"] == DBNull.Value) ? "" : dr["NBR"].ToString();
                            oDOPLAN.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : dr["SET_CODE"].ToString();
                            oDOPLAN.prdymd = (dr["PRDYMD"] == DBNull.Value) ? "" : dr["PRDYMD"].ToString();
                            oDOPLAN.do_date = (dr["DO_DATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DO_DATE"]);
                            oDOPLAN.vender = (dr["VENDER"] == DBNull.Value) ? "" : dr["VENDER"].ToString();
                            oDOPLAN.vender_n = (dr["VENDER_N"] == DBNull.Value) ? "" : dr["VENDER_N"].ToString();
                            oDOPLAN.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                            oDOPLAN.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                            oDOPLAN.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                            oDOPLAN.whno = (dr["WHNO"] == DBNull.Value) ? "" : dr["WHNO"].ToString();
                            oDOPLAN.whcode = (dr["WHCODE"] == DBNull.Value) ? "" : dr["WHCODE"].ToString();
                            oDOPLAN.whname = (dr["WHNAME"] == DBNull.Value) ? "" : dr["WHNAME"].ToString();
                            oDOPLAN.plan_qty = (dr["PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_QTY"]);
                            oDOPLAN.consumtion_qty = (dr["CONSUMTION_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONSUMTION_QTY"]);
                            oDOPLAN.cal_qty = (dr["CAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_QTY"]);
                            oDOPLAN.do_qty = (dr["DO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DO_QTY"]);
                            oDOPLAN.stk_inhouse_bf = (dr["STK_INHOUSE_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_BF"]);
                            oDOPLAN.stk_inhouse_af = (dr["STK_INHOUSE_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_INHOUSE_AF"]);
                            oDOPLAN.stk_wh_bf = (dr["STK_WH_BF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_BF"]);
                            oDOPLAN.stk_wh_af = (dr["STK_WH_AF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_WH_AF"]);
                            oDOPLAN.picklist = (dr["PICKLIST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PICKLIST"]);
                            oDOPLAN.recive_dt = (dr["RECIVE_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["RECIVE_DT"]);
                            oDOPLAN.recive_qty = (dr["RECIVE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RECIVE_QTY"]);
                            oDOPLAN.data_dt = (dr["DATA_DT"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["DATA_DT"]);
                            oDOPLAN.remark1 = (dr["REMARK1"] == DBNull.Value) ? "" : dr["REMARK1"].ToString();
                            oDOPLAN.remark2 = (dr["REMARK2"] == DBNull.Value) ? "" : dr["REMARK2"].ToString();
                            oDOPLAN.remark3 = (dr["REMARK3"] == DBNull.Value) ? "" : dr["REMARK3"].ToString();
                            oDOPLANs.Add(oDOPLAN);
                        }
                    }
                    P_OLD_EN = P_OLD_EN.AddDays(1);
                }
                oDOPLANs = oDOPLANs
                    .OrderBy(items => items.vender)
                    .ThenBy(items => items.partno)
                    .ThenBy(items => items.do_date)
                    .ToList();
                // cut part not use
                SqlCommand sqlselectPartvd = new SqlCommand();
                sqlselectPartvd.CommandText = @"
                SELECT [PARTNO],[CM],[VENDER_C],[STATUS]
                FROM [dbSCM].[dbo].[DOOUT_PARTMSTR]
                WHERE [STATUS] = 'INACTIVE'";
                DataTable dtpartvd = conDBSCM.Query(sqlselectPartvd);

                List<PARTMSTR> partvds = new List<PARTMSTR>();
                if (dtpartvd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtpartvd.Rows)
                    {
                        PARTMSTR opartvd = new PARTMSTR();
                        opartvd.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                        opartvd.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                        opartvd.vender = (dr["VENDER_C"] == DBNull.Value) ? "" : dr["VENDER_C"].ToString();
                        opartvd.status = (dr["STATUS"] == DBNull.Value) ? "" : dr["STATUS"].ToString();
                        partvds.Add(opartvd);
                    }
                }
                oDOPLANs = oDOPLANs.Where(item => !partvds.Any(key =>
                    item.partno == key.partno &&
                    item.cm == key.cm && item.vender == key.vender)).ToList();

                //whb -> wh1 actual part
                OracleCommand selectFttToDci = new OracleCommand();
                //selectFttToDci.CommandText = @"
                //SELECT   DTC.RDATE, DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM, SUM(DTD.RQTY) RQTY 
                //FROM MC.DST_ISTCTL  DTC
                //LEFT JOIN  MC.DST_ISTDTL  DTD ON DTC.DELNO = DTD.DELNO
                //WHERE DTC.DELDATE  BETWEEN :stdate AND :endate
                //    AND DTC.RCBIT = 'F' AND DTC.FWH = 'WB' AND DTC.TWH IN ('W1','W2')
                //GROUP BY  DTC.RDATE, DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM";
                selectFttToDci.CommandText = $@"
                SELECT
                    CAST(DTC.RDATE AS DATE) AS RDATE,
                    --DTC.DELDATE,
                    DTC.FWH,
                    DTC.TWH,
                    DTD.PARTNO,
                    DTD.CM,
                    SUM(DTD.RQTY) AS RQTY,
                    DTC.RCBIT
                FROM MC.DST_ISTCTL DTC
                INNER JOIN MC.DST_ISTDTL DTD ON DTC.DELNO = DTD.DELNO
                WHERE
                    --DTC.DELDATE between '{DateTime.Now.AddDays(-5).ToString("yyyyMMdd").Trim()}' AND '{DateTime.Now.AddDays(1).ToString("yyyyMMdd").Trim()}'
                    DTC.RDATE BETWEEN TO_DATE(:stdate, 'YYYY-MM-DD') AND TO_DATE(:endate, 'YYYY-MM-DD')
                    AND DTC.RCBIT = 'F'
                    AND DTC.FWH = 'WB'
                    AND DTC.TWH IN('W1', 'W2', 'WE')
                GROUP BY
                    --DTC.DELDATE,
                    CAST(DTC.RDATE AS DATE),
                    DTC.FWH,
                    DTC.TWH,
                    DTD.PARTNO,
                    DTD.CM,
                    DTC.RCBIT";

                selectFttToDci.Parameters.Add(new OracleParameter("stdate", DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd")));
                selectFttToDci.Parameters.Add(new OracleParameter("endate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));

                DataTable dt_r = dbAlpha2.Query(selectFttToDci);
                List<DST_DATAC1> ftt_to_dcis = new List<DST_DATAC1>();
                foreach (DataRow dr in dt_r.Rows)
                {
                    DST_DATAC1 item = new DST_DATAC1();
                    item.whno = dr["FWH"].ToString().Trim();
                    item.partno = dr["PARTNO"].ToString().Trim();
                    item.cm = dr["CM"].ToString().Trim();
                    item.wqty = double.Parse(dr["RQTY"].ToString());
                    //item.acdate = Convert.ToDateTime(dr["DELDATE"]).ToString("yyyyMMdd");
                    item.acdate = Convert.ToDateTime(dr["RDATE"]).ToString("yyyyMMdd");
                    ftt_to_dcis.Add(item);
                }
                foreach (var dp in oDOPLANs)
                {
                    var doDateString = dp.do_date.ToString("yyyyMMdd");  // <<< แปลง do_date เป็น string

                    var matchingWQTYs = ftt_to_dcis
                        .Where(dst => dst.partno.Trim() == dp.partno.Trim()
                                   && dst.cm.Trim() == dp.cm.Trim()
                                   && dst.acdate == doDateString)  // <<< เทียบ string ตรงๆ
                        .Select(dst => new { dst.wqty, dst.acdate });

                    if (matchingWQTYs.Any())
                    {
                        dp.recive_qty = Convert.ToDecimal(matchingWQTYs.Sum(m => m.wqty));
                        dp.recive_dt = DateTime.ParseExact(matchingWQTYs.FirstOrDefault()?.acdate, "yyyyMMdd", null);
                    }
                }
                // รายการที่ส่งแล้ว รอรับ
                string str_date = "1901-01-01";
                DateTime dt = Convert.ToDateTime(str_date);

                OracleCommand SelectFttsend = new OracleCommand();
                SelectFttsend.CommandText = $@"
                SELECT
                    DTC.DELNO,
                    DTD.DELNO,
                    --CAST(DTC.RDATE AS DATE) AS RDATE,
                    CAST(DTC.SDATE AS DATE) AS SDATE,
                    --DTC.DELDATE,
                    DTC.FWH,
                    DTC.TWH,
                    DTD.PARTNO,
                    DTD.CM,
                    SUM(DTD.SQTY) AS SQTY,
                    DTC.DELBIT,
                    DTC.RCBIT
                FROM MC.DST_ISTCTL DTC
                INNER JOIN MC.DST_ISTDTL DTD ON DTC.DELNO = DTD.DELNO
                WHERE
                    TRUNC(DTC.SDATE) BETWEEN TO_DATE(:stdate, 'YYYY-MM-DD') AND TO_DATE(:endate, 'YYYY-MM-DD')
                    AND DTC.DELBIT = 'F'
                    AND DTC.RCBIT = 'U'
                    AND DTC.FWH = 'WB'
                    --AND DTC.TWH IN ('W1', 'W2')
                GROUP BY
                    DTC.DELNO,
                    DTD.DELNO,
                    CAST(DTC.SDATE AS DATE),
                    DTC.FWH,
                    DTC.TWH,
                    DTD.PARTNO,
                    DTD.CM,
                    DTC.DELBIT,
                    DTC.RCBIT";

                SelectFttsend.Parameters.Add(new OracleParameter("stdate", DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd")));
                SelectFttsend.Parameters.Add(new OracleParameter("endate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
                DataTable dt_send = dbAlpha2.Query(SelectFttsend);
                List<FTTACT> ftt_send = new List<FTTACT>();
                if (dt_send.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_send.Rows)
                    {
                        FTTACT item = new FTTACT();
                        item.delldate = Convert.ToDateTime(dr["SDATE"]).ToString("yyyyMMdd");
                        item.partno = dr["PARTNO"].ToString().Trim();
                        item.cm = dr["CM"].ToString().Trim();
                        item.sqty = Convert.ToDecimal(dr["SQTY"]);
                        //item.rqty = Convert.ToDecimal(dr["RQTY"]);

                        ftt_send.Add(item);
                    }
                }
                foreach (var dp in oDOPLANs)
                {
                    var doDateString = dp.do_date.ToString("yyyyMMdd");  // <<< แปลง do_date เป็น string

                    var matchingWQTYs = ftt_send
                        .Where(dst => dst.partno.Trim() == dp.partno.Trim()
                                   && dst.cm.Trim() == dp.cm.Trim()
                                   && dst.delldate == doDateString)  // <<< เทียบ string ตรงๆ
                        .Select(dst => new { dst.sqty });

                    if (matchingWQTYs.Any())
                    {
                        dp.send_qty = Convert.ToDecimal(matchingWQTYs.Sum(m => m.sqty));
                    }
                }
                // set part is shipping to day
                var partNumbersWithPositiveQty = oDOPLANs
                    .Where(item => item.do_date.Date.Date == DateTime.Now.Date && item.do_qty > 0)
                    .Select(item => new { item.partno, item.cm })
                    .Distinct()
                    .ToList();
                oDOPLANs.Where(item => partNumbersWithPositiveQty
                                        .Any(p => p.partno == item.partno && p.cm == item.cm))
                       .ToList()
                       .ForEach(item => item.shipping = true);

                // update production picklist part 
                List<PICKLIST> listpicklist = new List<PICKLIST>();
                SqlCommand Sqlcmdpicklist = new SqlCommand();
                Sqlcmdpicklist.CommandText = @"
            SELECT PARTNO,BRUSN,CAST(IDATE AS DATE) AS IDATE,SUM(FQTY) AS FQTY
            FROM [dbSCM].[dbo].[AL_GST_DATPID]
            WHERE PRGBIT IN ('F', 'C') AND IDATE BETWEEN @Sdate AND @Ndate
            GROUP BY PARTNO, BRUSN, IDATE";

                Sqlcmdpicklist.Parameters.Add(new SqlParameter("@Sdate", DateTime.Now.AddDays(-4).Date.ToString("yyyy-MM-dd")));
                Sqlcmdpicklist.Parameters.Add(new SqlParameter("@Ndate", DateTime.Now.Date.ToString("yyyy-MM-dd")));
                DataTable dt_picklist = conDBSCM.Query(Sqlcmdpicklist);

                if (dt_picklist.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_picklist.Rows)
                    {
                        PICKLIST item = new PICKLIST();
                        item.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                        item.cm = (dr["BRUSN"] == DBNull.Value) ? "" : dr["BRUSN"].ToString();
                        item.date = (dr["IDATE"] == DBNull.Value) ? DT : Convert.ToDateTime(dr["IDATE"]);
                        item.qty = (dr["FQTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FQTY"]);
                        listpicklist.Add(item);
                    }
                }
                foreach (var item in listpicklist)
                {
                    var match = oDOPLANs.FirstOrDefault(x =>
                    x.partno == item.partno &&
                    x.cm == item.cm &&
                    x.do_date.Date == item.date.Date
                    );
                    if (match != null)
                    {
                        match.picklist = item.qty;
                    }
                }

                List<PARTMSTR> listpartmstr = GET_PARTMSTR();
                var activeKeys = listpartmstr
                    .Where(s => s.markstatus == "ACTIVE")
                    .Select(s => $"{s.partno}_{s.cm}_{s.vender}")
                    .ToHashSet();
                foreach (var plan in oDOPLANs)
                {
                    if (activeKeys.Contains($"{plan.partno}_{plan.cm}_{plan.vender}"))
                    {
                        plan.status_mark = true;
                    }
                    var partmstr = listpartmstr.Where(s => s.partno.Trim() == plan.partno.Trim() && s.cm.Trim() == plan.cm.Trim()).FirstOrDefault();
                    if (partmstr != null)
                    {
                        plan.pm_sfstk = partmstr.sfstkqty;
                        plan.pm_markqty = partmstr.markqty;
                        plan.pm_minqty = partmstr.minqty;
                        plan.pm_maxqty = partmstr.maxqty;
                        plan.pm_qtybox = partmstr.qtybox;
                        plan.pm_boxpl = partmstr.boxpl;
                        plan.pm_typecal = partmstr.typecal;
                        plan.pm_truckstack = partmstr.truckstack;
                        plan.pm_plstack = partmstr.palletstack;
                        plan.pm_pdlt = partmstr.pdlt;
                        plan.pm_preorder = partmstr.preorderday;
                    }
                }

                oDOPLANs.ForEach(x => x.remain_act = (x.do_qty + x.mark_qty) - x.recive_qty);

                // day off 
                List<VDFIXEDDAYS> oVDFDs = GET_FIXED_DAY();
                List<DELIVERY_CYCLES> DELICYCLE = GET_DELIVERY_CYCLE();
                List<CALENDAR> oCALENDARs = GET_CALENDAR();

                oDOPLANs = oDOPLANs.Select(item =>
                {
                    item.status_fixed_date = false;
                    return item;
                }).ToList();
                foreach (var oVDFD in oVDFDs)
                {
                    oDOPLANs.Where(item => item.whcode == oVDFD.whcode
                                          && item.do_date.Date >= DateTime.Now.Date
                                          && item.do_date.Date < DateTime.Now.AddDays(oVDFD.day).Date)
                            .ToList()
                            .ForEach(item => item.status_fixed_date = true);
                }

                foreach (var oDOPLAN in oDOPLANs)
                {
                    var TYPE_DELI = DELICYCLE.Where(s => s.vender == "SG1887").FirstOrDefault();

                    if (TYPE_DELI.del_type == "Daily" || TYPE_DELI.del_type == "Weekly")
                    {
                        string Day = oDOPLAN.do_date.ToString("ddd");
                        bool sendstatus = false;
                        switch (Day)
                        {
                            case "Sun":
                                sendstatus = (TYPE_DELI.del_wk_sun);
                                break;
                            case "Mon":
                                sendstatus = (TYPE_DELI.del_wk_mon);
                                break;
                            case "Tue":
                                sendstatus = (TYPE_DELI.del_wk_tue);
                                break;
                            case "Wed":
                                sendstatus = (TYPE_DELI.del_wk_wed);
                                break;
                            case "Thu":
                                sendstatus = (TYPE_DELI.del_wk_thu);
                                break;
                            case "Fri":
                                sendstatus = (TYPE_DELI.del_wk_fri);
                                break;
                            case "Sat":
                                sendstatus = (TYPE_DELI.del_wk_sat);
                                break;
                            default:
                                sendstatus = false;
                                break;
                        }
                        if (sendstatus)
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = true);
                        }
                        else
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = false);
                        }
                    }
                    else if (TYPE_DELI.del_type == "Monthly")
                    {
                        string Day = oDOPLAN.do_date.Day.ToString();
                        bool sendstatus = false;
                        switch (Day)
                        {
                            case "1":
                                sendstatus = TYPE_DELI.del_mo_01;
                                break;
                            case "2":
                                sendstatus = TYPE_DELI.del_mo_02;
                                break;
                            case "3":
                                sendstatus = TYPE_DELI.del_mo_03;
                                break;
                            case "4":
                                sendstatus = TYPE_DELI.del_mo_04;
                                break;
                            case "5":
                                sendstatus = TYPE_DELI.del_mo_05;
                                break;
                            case "6":
                                sendstatus = TYPE_DELI.del_mo_06;
                                break;
                            case "7":
                                sendstatus = TYPE_DELI.del_mo_07;
                                break;
                            case "8":
                                sendstatus = TYPE_DELI.del_mo_08;
                                break;
                            case "9":
                                sendstatus = TYPE_DELI.del_mo_09;
                                break;
                            case "10":
                                sendstatus = TYPE_DELI.del_mo_10;
                                break;
                            case "11":
                                sendstatus = TYPE_DELI.del_mo_11;
                                break;
                            case "12":
                                sendstatus = TYPE_DELI.del_mo_13;
                                break;
                            case "13":
                                sendstatus = TYPE_DELI.del_mo_13;
                                break;
                            case "14":
                                sendstatus = TYPE_DELI.del_mo_14;
                                break;
                            case "15":
                                sendstatus = TYPE_DELI.del_mo_15;
                                break;
                            case "16":
                                sendstatus = TYPE_DELI.del_mo_16;
                                break;
                            case "17":
                                sendstatus = TYPE_DELI.del_mo_17;
                                break;
                            case "18":
                                sendstatus = TYPE_DELI.del_mo_18;
                                break;
                            case "19":
                                sendstatus = TYPE_DELI.del_mo_19;
                                break;
                            case "20":
                                sendstatus = TYPE_DELI.del_mo_20;
                                break;
                            case "21":
                                sendstatus = TYPE_DELI.del_mo_21;
                                break;
                            case "22":
                                sendstatus = TYPE_DELI.del_mo_22;
                                break;
                            case "23":
                                sendstatus = TYPE_DELI.del_mo_23;
                                break;
                            case "24":
                                sendstatus = TYPE_DELI.del_mo_24;
                                break;
                            case "25":
                                sendstatus = TYPE_DELI.del_mo_25;
                                break;
                            case "26":
                                sendstatus = TYPE_DELI.del_mo_26;
                                break;
                            case "27":
                                sendstatus = TYPE_DELI.del_mo_27;
                                break;
                            case "28":
                                sendstatus = TYPE_DELI.del_mo_28;
                                break;
                            case "29":
                                sendstatus = TYPE_DELI.del_mo_29;
                                break;
                            case "30":
                                sendstatus = TYPE_DELI.del_mo_30;
                                break;
                            case "31":
                                sendstatus = TYPE_DELI.del_mo_31;
                                break;
                        }
                        if (sendstatus)
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = true);
                        }
                        else
                        {
                            oDOPLANs.Where(item => item.vender == oDOPLAN.vender && item.do_date.Date == oDOPLAN.do_date.Date).ToList().ForEach(item => item.status_delicycle = false);
                        }
                    }
                    else
                    {

                    }
                }
                List<CALENDAR> oVD_CALENDARs = oCALENDARs.Where(item => item.h_type == "VENDER_H").ToList();
                List<CALENDAR> oDCI_CALENDARs = oCALENDARs.Where(item => item.h_type == "DCI").ToList();
                foreach (var oVD_CAL in oVD_CALENDARs)
                {
                    oDOPLANs.Where(item => item.whcode == oVD_CAL.vender && item.do_date.Date == oVD_CAL.h_date.Date)
                        .ToList()
                        .ForEach(item => item.status_holiday = true);
                }
                foreach (var oDCI_CAL in oDCI_CALENDARs)
                {
                    oDOPLANs.Where(item => item.do_date.Date == oDCI_CAL.h_date.Date)
                        .ToList()
                        .ForEach(item => item.status_holiday = true);
                }

                // update current stock whb
                List<CURRENTSTKPS> curstkwhb = PullStockPSRealtime();
                foreach (var item in curstkwhb)
                {
                    var match = oDOPLANs.FirstOrDefault(x =>
                    x.partno.Trim() == item.partno.Trim() &&
                    x.cm.Trim() == item.cm.Trim() &&
                    x.do_date.Date == DateTime.Now.Date
                    );
                    if (match != null)
                    {
                        match.stk_current = item.whb;
                    }
                }
                // update share vender 
                List<SHAREVD> listsharevd = GET_SHAREVD();

                foreach (var item in listsharevd)
                {
                    var matches = oDOPLANs
                        .Where(x =>
                            x.partno.Trim() == item.partno.Trim() &&
                            x.cm.Trim() == item.cm.Trim());

                    foreach (var m in matches)
                    {
                        m.vender_n = item.grpvd;  // อัปเดตค่า
                    }
                }

                return Ok(oDOPLANs);
            }
            else
            {
                return Ok();
            }


        }

        [HttpPost]
        [Route("Getrecdate")]
        public IActionResult GET_RECIVEDATE([FromBody] DOPLANKEY param)
        {
            string str_date = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_date);
            //whb -> wh1 actual part
            OracleCommand selectFttToDci = new OracleCommand();
            //selectFttToDci.CommandText = @"
            //SELECT   DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM, SUM(DTD.RQTY) RQTY 
            //FROM MC.DST_ISTCTL  DTC
            //LEFT JOIN  MC.DST_ISTDTL  DTD ON DTC.DELNO = DTD.DELNO
            //WHERE DTC.CDATE  BETWEEN to_date(:stdate, 'YYYYMMDD') AND to_date(:endate,'YYYYMMDD') 
            //    AND DTC.RCBIT = 'F' AND DTC.TWH IN ('W1','W2')
            //GROUP BY  DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM";

            //selectFttToDci.CommandText = @"
            //SELECT   DTC.RDATE, DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM, SUM(DTD.RQTY) RQTY 
            //FROM MC.DST_ISTCTL  DTC
            //LEFT JOIN  MC.DST_ISTDTL  DTD ON DTC.DELNO = DTD.DELNO
            //WHERE DTC.DELDATE  BETWEEN :stdate AND :endate
            //    AND DTC.RCBIT = 'F' AND DTC.FWH = 'WB' AND DTC.TWH IN ('W1','W2')
            //GROUP BY  DTC.RDATE, DTC.DELDATE, DTC.FWH, DTC.TWH, DTD.PARTNO, DTD.CM";
            string cmCondition;

            if (string.IsNullOrWhiteSpace(param.cm))
            {
                // เช็คทั้ง NULL และค่าว่างใน Oracle
                cmCondition = "(TRIM(DTD.CM) IS NULL OR TRIM(DTD.CM) = '')";
            }
            else
            {
                cmCondition = $"TRIM(DTD.CM) = '{param.cm.Trim()}'";
            }

            selectFttToDci.CommandText = $@"
            SELECT
                DTC.DELDATE,
                CAST(DTC.SDATE AS DATE) AS SDATE,
                CAST(DTC.RDATE AS DATE) AS RDATE,
                DTC.FWH,
                DTC.TWH,
                TRIM(DTD.PARTNO) AS PARTNO,
                TRIM(DTD.CM) AS CM,
                DTD.SQTY,
                DTD.RQTY
            FROM MC.DST_ISTCTL DTC
            LEFT JOIN MC.DST_ISTDTL DTD ON DTC.DELNO = DTD.DELNO
            WHERE
                TRUNC(DTC.RDATE) = TO_DATE('{param.dodate.ToString("yyyy-MM-dd")}', 'YYYY-MM-DD')
                AND DTC.RCBIT = 'F'
                AND DTC.FWH = 'WB'
                AND DTC.TWH IN ('W1','W2')
                AND TRIM(DTD.PARTNO) = '{param.partno}'
                AND {cmCondition}
            ORDER BY SDATE ASC";


            //selectFttToDci.Parameters.Add(new OracleParameter("stdate", param.dodate.ToString("yyyy-MM-dd")));
            //selectFttToDci.Parameters.Add(new OracleParameter("endate", param.dodate.ToString("yyyy-MM-dd")));

            DataTable dt_r = dbAlpha2.Query(selectFttToDci);
            List<FTTACT> ftt_to_dcis = new List<FTTACT>();
            if (dt_r.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_r.Rows)
                {
                    FTTACT item = new FTTACT();
                    item.delldate = dr["DELDATE"].ToString().Trim();
                    item.sdate = Convert.ToDateTime(dr["SDATE"]);
                    item.rdate = Convert.ToDateTime(dr["RDATE"] == DBNull.Value ? dt : dr["RDATE"]);
                    item.partno = dr["PARTNO"].ToString().Trim();
                    item.cm = dr["CM"].ToString().Trim();
                    item.sqty = Convert.ToDecimal(dr["SQTY"]);
                    item.rqty = Convert.ToDecimal(dr["RQTY"]);

                    ftt_to_dcis.Add(item);
                }
                return Ok(ftt_to_dcis);
            }
            else
            {
                return Ok();
            }

        }


        private static List<CURRENTSTKPS> PullStockPSRealtime()
        {
            OraConnectDB OraPD2 = new OraConnectDB("ALPHA02");

            DateTime dtNow = DateTime.Now;
            string _Year = dtNow.Year.ToString();
            string _Month = dtNow.Month.ToString("00");
            string date = _Year + "" + _Month;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = @"SELECT '" + dtNow.ToString("yyyyMMdd") + "',MC1.PARTNO, MC1.CM, DECODE(SB1.DSBIT,'1','OBSOLETE','2','DEAD STOCK','3',CASE WHEN TRIM(SB1.STOPDATE) IS NOT NULL AND SB1.STOPDATE <= TO_CHAR(SYSDATE,'YYYYMMDD') THEN 'NOT USE ' || SB1.STOPDATE ELSE ' ' END, ' ') PART_STATUS, MC1.LWBAL, NVL(AC1.ACQTY,0) AS ACQTY, NVL(PID.ISQTY,0) AS ISQTY, MC1.LWBAL + NVL(AC1.ACQTY,0) - NVL(PID.ISQTY,0) AS WBAL,NVL(RT3.LREJ,0) + NVL(PID.REJIN,0) - NVL(AC1.REJOUT,0) AS REJQTY, MC2.QC, MC2.WH1, MC2.WH2, MC2.WH3, MC2.WHA, MC2.WHB, MC2.WHC, MC2.WHD, MC2.WHE,ZUB.HATANI AS UNIT, EPN.KATAKAN AS DESCR, F_GET_HTCODE_RATIO(MC1.JIBU,MC1.PARTNO, '" + dtNow.ToString("yyyyMMdd") + "') AS HTCODE, F_GET_MSTVEN_VDABBR(MC1.JIBU,F_GET_HTCODE_RATIO(MC1.JIBU,MC1.PARTNO,'" + dtNow.ToString("yyyyMMdd") + "')) SUPPLIER, SB1.LOCA1, SB1.LOCA2, SB1.LOCA3, SB1.LOCA4, SB1.LOCA5, SB1.LOCA6, SB1.LOCA7, SB1.LOCA8 FROM	(SELECT	* FROM	DST_DATMC1 WHERE	TRIM(YM) = :YM  AND CM LIKE '%'";
            cmd.CommandText = cmd.CommandText + @") MC1, 
     		    (SELECT	PARTNO, CM, SUM(WQTY) AS ACQTY, SUM(CASE WHEN WQTY < 0 THEN -1 * WQTY ELSE 0 END) AS REJOUT 
     		     FROM	DST_DATAC1 
     		     WHERE	ACDATE >= :DATE_START 
     			    AND	ACDATE <= :DATE_RUN  AND TRIM(PARTNO) LIKE '%'  AND CM LIKE '%'
     		     GROUP BY PARTNO, CM 
     		    ) AC1, 
     		    (SELECT	PARTNO, BRUSN AS CM, SUM(FQTY) AS ISQTY, SUM(DECODE(REJBIT,'R',-1*FQTY,0)) AS REJIN 
     		     FROM	MASTER.GST_DATPID@ALPHA01 
     		     WHERE	IDATE >= :DATE_START 
     			    AND	IDATE <= :DATE_RUN  AND TRIM(PARTNO) LIKE '%'  AND BRUSN LIKE '%'
     		     GROUP BY PARTNO, BRUSN 
     		    ) PID, 
     		    (SELECT    PARTNO, CM, SUM(DECODE(WHNO,'QC',BALQTY)) AS QC,SUM(DECODE(WHNO,'W1',BALQTY)) AS WH1,SUM(DECODE(WHNO,'W2',BALQTY)) AS WH2,SUM(DECODE(WHNO,'W3',BALQTY)) AS WH3, 
                         SUM(DECODE(WHNO,'WA',BALQTY)) AS WHA,SUM(DECODE(WHNO,'WB',BALQTY)) AS WHB,SUM(DECODE(WHNO,'WC',BALQTY)) AS WHC,SUM(DECODE(WHNO,'WD',BALQTY)) AS WHD,SUM(DECODE(WHNO,'WE',BALQTY)) AS WHE 
                  FROM    (SELECT    MC2.PARTNO, MC2.CM, MC2.WHNO, MC2.LWBAL, NVL(AC1.ACQTY,0) AS ACQTY, NVL(PID.ISQTY,0) AS ISQTY, MC2.LWBAL + NVL(AC1.ACQTY,0) - NVL(PID.ISQTY,0) AS BALQTY 
                          FROM    (SELECT    * 
                                  FROM    DST_DATMC2 
                                  WHERE    YM = :YM  AND TRIM(PARTNO) LIKE '%' AND CM LIKE '%'
                                 ) MC2, 
                                 (SELECT    PARTNO, CM, WHNO, SUM(WQTY) AS ACQTY 
                                  FROM    DST_DATAC1 
                                  WHERE    ACDATE >= :DATE_START 
                                     AND    ACDATE <= :DATE_RUN  AND TRIM(PARTNO) LIKE '%' AND CM LIKE '%'
                                  GROUP BY PARTNO, CM, WHNO 
                                 ) AC1, 
                                 (SELECT    PARTNO, BRUSN AS CM, WHNO, SUM(FQTY) AS ISQTY 
                                  FROM    (SELECT    * 
                                           FROM    MASTER.GST_DATPID@ALPHA01 
                                           WHERE    IDATE >= :DATE_START 
                                             AND    IDATE <= :DATE_RUN  AND TRIM(PARTNO) LIKE '%' AND BRUSN LIKE '%'
                                          UNION ALL 
                                           SELECT    * 
                                           FROM    DST_DATPID3 
                                           WHERE    IDATE >= :DATE_START 
                                             AND    IDATE <= :DATE_RUN  AND TRIM(PARTNO) LIKE '%' AND BRUSN LIKE '%'
                                         ) 
                                  GROUP BY PARTNO, BRUSN, WHNO 
                                 ) PID 
                          WHERE    MC2.PARTNO    = AC1.PARTNO(+) 
                             AND    MC2.CM        = AC1.CM(+) 
                             AND    MC2.WHNO    = AC1.WHNO(+) 
                             AND    MC2.PARTNO    = PID.PARTNO(+) 
                             AND    MC2.CM        = PID.CM(+) 
                             AND    MC2.WHNO    = PID.WHNO(+) 
                         ) 
                          GROUP BY PARTNO, CM 
                         ) MC2, 
                         MASTER.ND_EPN_TBL_V1@ALPHA01 EPN, DST_MSTSB1 SB1, MASTER.ND_ZUB_TBL@ALPHA01 ZUB, DST_DATRT3 RT3 
                 WHERE    MC1.PARTNO    = AC1.PARTNO(+) 
                     AND    MC1.CM        = AC1.CM(+) 
                     AND    MC1.PARTNO    = PID.PARTNO(+) 
                     AND    MC1.CM        = PID.CM(+) 
                     AND    MC1.YM        = RT3.YM(+) 
                     AND    MC1.PARTNO    = RT3.PARTNO(+) 
                     AND    MC1.CM        = RT3.CM(+) 
                     AND    MC1.PARTNO    = EPN.PARTNO(+) 
                     AND    MC1.PARTNO    = SB1.PARTNO(+) 
                     AND    MC1.CM        = SB1.CM(+) 
                     AND    MC1.PARTNO    = MC2.PARTNO(+) 
                     AND    MC1.CM        = MC2.CM(+) 
                     AND    MC1.PARTNO    = ZUB.PARTNO(+) 
                     AND    ZUB.STRYMN(+) <= :DATE_START 
                     AND    ZUB.ENDYMN(+) >  :DATE_RUN 
                     AND    ZUB.KSNBIT(+) <> '2'";

            cmd.Parameters.Add(new OracleParameter(":YM", date));
            cmd.Parameters.Add(new OracleParameter(":DATE_START", dtNow.ToString("yyyyMM01")));
            cmd.Parameters.Add(new OracleParameter(":DATE_RUN", dtNow.ToString("yyyyMMdd")));
            DataTable dt = OraPD2.Query(cmd);
            DataRow[] drs = dt.Select("WBAL <> '0'  ");
            dt = drs.CopyToDataTable();
            List<CURRENTSTKPS> listcurstkps = new List<CURRENTSTKPS>();
            foreach (DataRow dr in dt.Rows)
            {
                CURRENTSTKPS item = new CURRENTSTKPS();
                item.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                item.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                item.whb = (dr["WHB"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["WHB"]);
                listcurstkps.Add(item);
            }
            return listcurstkps;
        }
    }
}
