using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API.Model.ModelDoplan;
using System.Data.SqlClient;
using System.Data;
using static API.Model.ModelMaster;
using API.Service;
using System.Globalization;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Master : ControllerBase
    {
        SqlConnectDB conDBSCM = new("DBSCM");

        [HttpGet]
        [Route("GetPartMstr")]
        public IActionResult GET_PARTMSTR()
        {
            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);
            List<partmstr> listPartMstr = new List<partmstr>();

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @"
            SELECT [PARTNO],[CM],[PARTNAME],[VENDER_C],vd.[VenderName],vd.[AbbreName],[SFSTK_QTY],[MARK_QTY],[MIN_QTY],[MAX_QTY],[QTY_BOX],[BOX_PL],
                   [TRUCK_STACK],[PALLET_STACK],[PD_LT],[PREORDER_DAYS],[TYPECAL],[MARK_STATUS],[STATUS],[UPDATE_BY],[UPDATE_DT]
            FROM [dbSCM].[dbo].[DOOUT_PARTMSTR] as pm
            LEFT JOIN [dbSCM].[dbo].[AL_Vendor] as vd on pm.VENDER_C = vd.Vender";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    partmstr PartMstr = new partmstr();
                    PartMstr.partno = (dr["PARTNO"] == DBNull.Value) ? "" : dr["PARTNO"].ToString();
                    PartMstr.cm = (dr["CM"] == DBNull.Value) ? "" : dr["CM"].ToString();
                    PartMstr.partname = (dr["PARTNAME"] == DBNull.Value) ? "" : dr["PARTNAME"].ToString();
                    PartMstr.vender_c = (dr["VENDER_C"] == DBNull.Value) ? "" : dr["VENDER_C"].ToString();
                    PartMstr.vender_n = (dr["VenderName"] == DBNull.Value) ? "" : dr["VenderName"].ToString();
                    PartMstr.vender_s = (dr["AbbreName"] == DBNull.Value) ? "" : dr["AbbreName"].ToString();
                    PartMstr.sfstk_qty = (dr["SFSTK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SFSTK_QTY"]);
                    PartMstr.mark_qty = (dr["MARK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MARK_QTY"]);
                    PartMstr.min_qty = (dr["MIN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MIN_QTY"]);
                    PartMstr.max_qty = (dr["MAX_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MAX_QTY"]);
                    PartMstr.qty_box = (dr["QTY_BOX"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_BOX"]);
                    PartMstr.box_pl = (dr["BOX_PL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BOX_PL"]);
                    PartMstr.truck_stack = (dr["TRUCK_STACK"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TRUCK_STACK"]);
                    PartMstr.pallet_stack = (dr["PALLET_STACK"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PALLET_STACK"]);
                    PartMstr.pd_lt = (dr["PD_LT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PD_LT"]);
                    PartMstr.preorder_days = (dr["PREORDER_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PREORDER_DAYS"]);
                    PartMstr.typecal = (dr["TYPECAL"] == DBNull.Value) ? "" : Convert.ToString(dr["TYPECAL"]);
                    PartMstr.mark_status = (dr["MARK_STATUS"] == DBNull.Value) ? "" : dr["MARK_STATUS"].ToString();
                    PartMstr.status = (dr["STATUS"] == DBNull.Value) ? "" : dr["STATUS"].ToString();
                    PartMstr.update_by = (dr["UPDATE_BY"] == DBNull.Value) ? "" : dr["UPDATE_BY"].ToString();
                    PartMstr.update_dt = (dr["UPDATE_DT"] == DBNull.Value) ? dt : Convert.ToDateTime(dr["UPDATE_DT"]);

                    listPartMstr.Add(PartMstr);
                }
            }
            return Ok(listPartMstr);

        }
        [HttpPost]
        [Route("SetPartMstr")]
        public IActionResult SET_PARTMSTR([FromBody] paramPartmstr param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                SqlCommand sqlSelectPartmstr = new SqlCommand();
                sqlSelectPartmstr.CommandText = $@"
                SELECT [PARTNO],[CM],[PARTNAME],[VENDER_C]
                FROM [dbSCM].[dbo].[DOOUT_PARTMSTR]
                WHERE [PARTNO] = '{param.new_partno}' AND [CM] = '{param.new_cm}' AND [VENDER_C] = '{param.new_vender_c}'";

                DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
                if (dtgelSK.Rows.Count > 0)
                {
                    dbresult.status = "Waring";
                    dbresult.msg = "มีข้อมูลในระบบแล้วไม่สามารถดำเนิการได้";
                    return Ok(dbresult);
                }
                else
                {
                    SqlCommand sqlEditpartmstr = new SqlCommand();
                    sqlEditpartmstr.CommandText = $@"
                      INSERT INTO [dbSCM].[dbo].[DOOUT_PARTMSTR]
                      ([PARTNO],[CM],[PARTNAME],[VENDER_C],[SFSTK_QTY],[MARK_QTY],[MIN_QTY],[MAX_QTY],[QTY_BOX],[BOX_PL],[TRUCK_STACK],[PALLET_STACK]
                      ,[PD_LT],[PREORDER_DAYS],[TYPECAL],[MARK_STATUS],[STATUS],[UPDATE_BY],[UPDATE_DT],[CREATE_DT])
                      VALUES ( @partno,@cm,@partname,@vender_c,@sfstk_qty,@mark_qty,@min_qty,@max_qty,@qty_box,@box_pl,@truck_stack,@pallet_stack
                              ,@pd_lt,@preorder_days,@typecal,@mark_status,@status,@update_by,@update_dt,@create_dt)";

                    sqlEditpartmstr.Parameters.AddWithValue("@partno", param.new_partno);
                    sqlEditpartmstr.Parameters.AddWithValue("@cm", param.new_cm);
                    sqlEditpartmstr.Parameters.AddWithValue("@partname", param.new_partname);
                    sqlEditpartmstr.Parameters.AddWithValue("@vender_c", param.new_vender_c);
                    sqlEditpartmstr.Parameters.AddWithValue("@sfstk_qty", param.new_sfstk_qty);
                    sqlEditpartmstr.Parameters.AddWithValue("@mark_qty", param.new_mark_qty);
                    sqlEditpartmstr.Parameters.AddWithValue("@min_qty", param.new_min_qty);
                    sqlEditpartmstr.Parameters.AddWithValue("@max_qty", param.new_max_qty);
                    sqlEditpartmstr.Parameters.AddWithValue("@qty_box", param.new_qty_box);
                    sqlEditpartmstr.Parameters.AddWithValue("@box_pl", param.new_box_pl);
                    sqlEditpartmstr.Parameters.AddWithValue("@truck_stack", param.new_truck_stack);
                    sqlEditpartmstr.Parameters.AddWithValue("@pallet_stack", param.new_pallet_stack);
                    sqlEditpartmstr.Parameters.AddWithValue("@pd_lt", param.new_pd_lt);
                    sqlEditpartmstr.Parameters.AddWithValue("@preorder_days", param.new_preorder_days);
                    sqlEditpartmstr.Parameters.AddWithValue("@typecal", param.new_typecal);
                    sqlEditpartmstr.Parameters.AddWithValue("@mark_status", param.new_mark_status);
                    sqlEditpartmstr.Parameters.AddWithValue("@status", param.new_status);
                    sqlEditpartmstr.Parameters.AddWithValue("@update_by", param.new_update_by);
                    sqlEditpartmstr.Parameters.AddWithValue("@update_dt", DateTime.Now);
                    sqlEditpartmstr.Parameters.AddWithValue("@create_dt", DateTime.Now);

                    dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);

                    if (dbresult.status == "Success")
                    {
                        string Old_Value = "PARTNO=" + param.old_partno + ",CM=" + param.old_cm + ",PARTNAME=" + param.old_partname + ",VENDER_C=" + param.old_vender_c + ",SFSTK=" + param.old_sfstk_qty
                            + ",MARKQTY=" + param.old_mark_qty + ",MINQTY=" + param.old_min_qty + ",MAXQTY=" + param.old_max_qty + ",QTYBOX=" + param.old_qty_box
                            + ",BOXPL=" + param.old_box_pl + ",TRUCKSTACK=" + param.old_truck_stack + ",PLSTACK=" + param.old_pallet_stack + ",PDLT=" + param.old_pd_lt
                            + ",PREORDER=" + param.old_preorder_days + ",TYPECAL=" + param.old_typecal + ",MARKSTATUS=" + param.old_mark_status + ",STATUS" + param.old_status
                            + ",UPDATEBY=" + param.old_update_by + ",UPDATEDT=" + param.old_update_dt;

                        string New_Value = "PARTNO=" + param.new_partno + ",CM=" + param.new_cm + ",PARTNAME=" + param.new_partname + ",VENDER_C=" + param.new_vender_c + ",SFSTK=" + param.new_sfstk_qty
                            + ",MARKQTY=" + param.new_mark_qty + ",MINQTY=" + param.new_min_qty + ",MAXQTY=" + param.new_max_qty + ",QTYBOX=" + param.new_qty_box
                            + ",BOXPL=" + param.new_box_pl + ",TRUCKSTACK=" + param.new_truck_stack + ",PLSTACK=" + param.new_pallet_stack + ",PDLT=" + param.new_pd_lt
                            + ",PREORDER=" + param.new_preorder_days + ",TYPECAL=" + param.new_typecal + ",MARKSTATUS=" + param.new_mark_status + ",STATUS" + param.new_status
                            + ",UPDATEBY=" + param.new_update_by + ",UPDATEDT=" + param.new_update_dt;


                        SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                        sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                        ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                        VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_PARTMASTER");
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "ADD");
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.new_update_by);
                        sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                        conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                        return Ok(dbresult);
                    }
                    else
                    {
                        dbresult.msg = dbresult.status;
                        dbresult.status = "Fail";
                        return Ok(dbresult);
                    }
                }

            }
            else
            {
                dbresult.status = "NoData";
                dbresult.msg = "";
                return Ok(dbresult);
            }
        }
        [HttpPost]
        [Route("EditPartMstr")]
        public IActionResult EDIT_PARTMSTR([FromBody] paramPartmstr param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                SqlCommand sqlEditpartmstr = new SqlCommand();
                sqlEditpartmstr.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_PARTMSTR]
                      SET  [PARTNAME] = @partname, 
                           [SFSTK_QTY] = @sfstk, 
                           [MARK_QTY] = @markqty, 
                           [MIN_QTY] = @minqty, 
                           [MAX_QTY] = @maxqty, 
                           [QTY_BOX] = @qtybox, 
                           [BOX_PL] = @boxpl, 
                           [TRUCK_STACK] = @truckstack,
                           [PALLET_STACK] = @plstack,
                           [PD_LT] = @pdlt,
                           [PREORDER_DAYS] = @preorder,
                           [TYPECAL] = @tycal,
                           [MARK_STATUS] = @markst,
                           [STATUS] = @status,
                           [UPDATE_BY] = @updateby,
                           [UPDATE_DT] = @updatedt

                      WHERE [PARTNO] = @partno AND [CM] = @cm AND [VENDER_C] = @vender_c";
                sqlEditpartmstr.Parameters.AddWithValue("@partno", param.old_partno);
                sqlEditpartmstr.Parameters.AddWithValue("@cm", param.old_cm);
                sqlEditpartmstr.Parameters.AddWithValue("@vender_c", param.old_vender_c);

                sqlEditpartmstr.Parameters.AddWithValue("@partname", param.new_partname);
                sqlEditpartmstr.Parameters.AddWithValue("@sfstk", param.new_sfstk_qty);
                sqlEditpartmstr.Parameters.AddWithValue("@markqty", param.new_mark_qty);
                sqlEditpartmstr.Parameters.AddWithValue("@minqty", param.new_min_qty);
                sqlEditpartmstr.Parameters.AddWithValue("@maxqty", param.new_max_qty);
                sqlEditpartmstr.Parameters.AddWithValue("@qtybox", param.new_qty_box);
                sqlEditpartmstr.Parameters.AddWithValue("@boxpl", param.new_box_pl);
                sqlEditpartmstr.Parameters.AddWithValue("@truckstack", param.new_truck_stack);
                sqlEditpartmstr.Parameters.AddWithValue("@plstack", param.new_pallet_stack);
                sqlEditpartmstr.Parameters.AddWithValue("@pdlt", param.new_pd_lt);
                sqlEditpartmstr.Parameters.AddWithValue("@preorder", param.new_preorder_days);
                sqlEditpartmstr.Parameters.AddWithValue("@tycal", param.new_typecal);
                sqlEditpartmstr.Parameters.AddWithValue("@markst", param.new_mark_status);
                sqlEditpartmstr.Parameters.AddWithValue("@status", param.new_status);
                sqlEditpartmstr.Parameters.AddWithValue("@updateby", param.new_update_by);
                sqlEditpartmstr.Parameters.AddWithValue("@updatedt", DateTime.Now);

                dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);

                if (dbresult.status == "Success")
                {
                    string Old_Value = "PARTNO=" + param.old_partno + ",CM=" + param.old_cm + ",PARTNAME=" + param.old_partname + ",VENDER_C=" + param.old_vender_c + ",SFSTK=" + param.old_sfstk_qty
                        + ",MARKQTY=" + param.old_mark_qty + ",MINQTY=" + param.old_min_qty + ",MAXQTY=" + param.old_max_qty + ",QTYBOX=" + param.old_qty_box
                        + ",BOXPL=" + param.old_box_pl + ",TRUCKSTACK=" + param.old_truck_stack + ",PLSTACK=" + param.old_pallet_stack + ",PDLT=" + param.old_pd_lt
                        + ",PREORDER=" + param.old_preorder_days + ",TYPECAL=" + param.old_typecal + ",MARKSTATUS=" + param.old_mark_status + ",STATUS" + param.old_status
                        + ",UPDATEBY=" + param.old_update_by + ",UPDATEDT=" + param.old_update_dt;

                    string New_Value = "PARTNO=" + param.new_partno + ",CM=" + param.new_cm + ",PARTNAME=" + param.new_partname + ",VENDER_C=" + param.new_vender_c + ",SFSTK=" + param.new_sfstk_qty
                        + ",MARKQTY=" + param.new_mark_qty + ",MINQTY=" + param.new_min_qty + ",MAXQTY=" + param.new_max_qty + ",QTYBOX=" + param.new_qty_box
                        + ",BOXPL=" + param.new_box_pl + ",TRUCKSTACK=" + param.new_truck_stack + ",PLSTACK=" + param.new_pallet_stack + ",PDLT=" + param.new_pd_lt
                        + ",PREORDER=" + param.new_preorder_days + ",TYPECAL=" + param.new_typecal + ",MARKSTATUS=" + param.new_mark_status + ",STATUS" + param.new_status
                        + ",UPDATEBY=" + param.new_update_by + ",UPDATEDT=" + param.new_update_dt;


                    SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                    sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                        ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                        VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_PARTMASTER");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "EDIT");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.new_update_by);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                    conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                    return Ok(dbresult);
                }
                else
                {
                    dbresult.msg = dbresult.status;
                    dbresult.status = "Fail";
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.status = "NoData";
                dbresult.msg = "";
                return Ok(dbresult);
            }
        }
        [HttpPost]
        [Route("DelPartMstr")]
        public IActionResult DEL_PARTMSTR([FromBody] paramPartmstr param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                SqlCommand sqlEditpartmstr = new SqlCommand();
                sqlEditpartmstr.CommandText = $@"
                      DELETE FROM [dbSCM].[dbo].[DOOUT_PARTMSTR]

                      WHERE [PARTNO] = @partno AND [CM] = @cm AND [VENDER_C] = @vender_c";
                sqlEditpartmstr.Parameters.AddWithValue("@partno", param.old_partno);
                sqlEditpartmstr.Parameters.AddWithValue("@cm", param.old_cm);
                sqlEditpartmstr.Parameters.AddWithValue("@vender_c", param.old_vender_c);


                dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);

                if (dbresult.status == "Success")
                {
                    string Old_Value = "PARTNO=" + param.old_partno + ",CM=" + param.old_cm + ",PARTNAME=" + param.old_partname + ",VENDER_C=" + param.old_vender_c + ",SFSTK=" + param.old_sfstk_qty
                         + ",MARKQTY=" + param.old_mark_qty + ",MINQTY=" + param.old_min_qty + ",MAXQTY=" + param.old_max_qty + ",QTYBOX=" + param.old_qty_box
                         + ",BOXPL=" + param.old_box_pl + ",TRUCKSTACK=" + param.old_truck_stack + ",PLSTACK=" + param.old_pallet_stack + ",PDLT=" + param.old_pd_lt
                         + ",PREORDER=" + param.old_preorder_days + ",TYPECAL=" + param.old_typecal + ",MARKSTATUS=" + param.old_mark_status + ",STATUS" + param.old_status
                         + ",UPDATEBY=" + param.old_update_by + ",UPDATEDT=" + param.old_update_dt;

                    string New_Value = "PARTNO=" + param.new_partno + ",CM=" + param.new_cm + ",PARTNAME=" + param.new_partname + ",VENDER_C=" + param.new_vender_c + ",SFSTK=" + param.new_sfstk_qty
                        + ",MARKQTY=" + param.new_mark_qty + ",MINQTY=" + param.new_min_qty + ",MAXQTY=" + param.new_max_qty + ",QTYBOX=" + param.new_qty_box
                        + ",BOXPL=" + param.new_box_pl + ",TRUCKSTACK=" + param.new_truck_stack + ",PLSTACK=" + param.new_pallet_stack + ",PDLT=" + param.new_pd_lt
                        + ",PREORDER=" + param.new_preorder_days + ",TYPECAL=" + param.new_typecal + ",MARKSTATUS=" + param.new_mark_status + ",STATUS" + param.new_status
                        + ",UPDATEBY=" + param.new_update_by + ",UPDATEDT=" + param.new_update_dt;


                    SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                    sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                        ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                        VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_PARTMASTER");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "DELETE");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.new_update_by);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                    conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                    return Ok(dbresult);
                }
                else
                {
                    dbresult.msg = dbresult.status;
                    dbresult.status = "Fail";
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.status = "NoData";
                dbresult.msg = "";
                return Ok(dbresult);
            }
        }


        [HttpGet]
        [Route("GetWhmstr")]
        public IActionResult GET_WHMSTR()
        {
            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);
            List<whoutsite> listwhmstr = new List<whoutsite>();

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @"
            SELECT [Dict_Code],[Dict_Type],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Dict_Value3],[Update_By],[Update_DT]
            FROM [dbSCM].[dbo].[DOOUT_Dict]
            WHERE [Dict_Type] = 'WH_OUTSITE_MSTR' AND [Dict_Ref1] = 'SG1887'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    whoutsite whmstr = new whoutsite();
                    whmstr.id = (dr["Dict_Code"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Dict_Code"]);
                    whmstr.vender_c = (dr["Dict_Ref1"] == DBNull.Value) ? "" : dr["Dict_Ref1"].ToString();
                    whmstr.wh_n = (dr["Dict_Ref2"] == DBNull.Value) ? "" : dr["Dict_Ref2"].ToString();
                    whmstr.whno = (dr["Dict_Ref3"] == DBNull.Value) ? "" : dr["Dict_Ref3"].ToString();
                    whmstr.fixeddays = (dr["Dict_Value3"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Dict_Value3"]);
                    whmstr.update_by = (dr["Update_By"] == DBNull.Value) ? "" : dr["Update_By"].ToString();
                    whmstr.update_dt = (dr["Update_DT"] == DBNull.Value) ? dt : Convert.ToDateTime(dr["Update_DT"]);
                    listwhmstr.Add(whmstr);
                }
            }
            return Ok(listwhmstr);
        }
        [HttpPost]
        [Route("EditWhmstr")]
        public IActionResult EDIT_WHMSTR([FromBody] paramWhoutsite param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                SqlCommand sqlEditpartmstr = new SqlCommand();
                sqlEditpartmstr.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_Dict]
                      SET  [Dict_Value3] = @dict_value3, [Update_By] = @empcode, [Update_DT] = @dt
            
                        WHERE [Dict_Type] = 'WH_OUTSITE_MSTR' AND [Dict_Ref1] = 'SG1887'";

                sqlEditpartmstr.Parameters.AddWithValue("@dict_value3", param.new_fixeddays);
                sqlEditpartmstr.Parameters.AddWithValue("@empcode", param.new_update_by);
                sqlEditpartmstr.Parameters.AddWithValue("@dt", DateTime.Now);

                dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);

                if (dbresult.status == "Success")
                {
                    string Old_Value = "WarehouseCode=" + param.old_vender_c + ",WarehouseName=" + param.old_wh_n + ",Local=" + param.old_whno + ",FixedDays=" + param.old_fixeddays;

                    string New_Value = "WarehouseCode=" + param.new_vender_c + ",WarehouseName=" + param.new_wh_n + ",Local=" + param.new_whno + ",FixedDays=" + param.new_fixeddays;


                    SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                    sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                        ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                        VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_WAREHOUSE OUTSIDE");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "EDIT");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.new_update_by);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                    conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                    return Ok(dbresult);
                }
                else
                {
                    dbresult.msg = dbresult.status;
                    dbresult.status = "Fail";
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.status = "NoData";
                dbresult.msg = "";
                return Ok(dbresult);
            }
        }

        [HttpGet]
        [Route("GetDelCycle")]
        public IActionResult GET_DELIVERYCYCLE()
        {
            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);
            List<delcycle> listdelcycle = new List<delcycle>();

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @"
            SELECT [VENDER],[DEL_TYPE],[DEL_WK_SUN],[DEL_WK_MON],[DEL_WK_TUE],[DEL_WK_WED],[DEL_WK_THU],[DEL_WK_FRI],[DEL_WK_SAT],
                   [DEL_MO_01],[DEL_MO_02],[DEL_MO_03],[DEL_MO_04],[DEL_MO_05],[DEL_MO_06],[DEL_MO_07],[DEL_MO_08],[DEL_MO_09],
                   [DEL_MO_10],[DEL_MO_11],[DEL_MO_12],[DEL_MO_13],[DEL_MO_14],[DEL_MO_15],[DEL_MO_16],[DEL_MO_17],[DEL_MO_18],
                   [DEL_MO_19],[DEL_MO_20],[DEL_MO_21],[DEL_MO_22],[DEL_MO_23],[DEL_MO_24],[DEL_MO_25],[DEL_MO_26],[DEL_MO_27],
                   [DEL_MO_28],[DEL_MO_29],[DEL_MO_30],[DEL_MO_31],[STATUS],[DATA_DT],[UPDATE_BY],[UPDATE_DT]
            FROM [dbSCM].[dbo].[DOOUT_DELIVERY_CYCLE]
            WHERE [VENDER] = 'SG1887'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    delcycle item = new delcycle();

                    // String fields
                    item.vender = dr["VENDER"]?.ToString();
                    item.deltype = dr["DEL_TYPE"]?.ToString();
                    item.update_by = dr["UPDATE_BY"]?.ToString();

                    item.update_date = dr["UPDATE_DT"] == DBNull.Value ? dt : Convert.ToDateTime(dr["UPDATE_DT"]);

                    // Weekly fields
                    item.wk_sun = dr["DEL_WK_SUN"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_WK_SUN"]);
                    item.wk_mon = dr["DEL_WK_MON"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_WK_MON"]);
                    item.wk_tue = dr["DEL_WK_TUE"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_WK_TUE"]);
                    item.wk_wed = dr["DEL_WK_WED"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_WK_WED"]);
                    item.wk_thu = dr["DEL_WK_THU"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_WK_THU"]);
                    item.wk_fri = dr["DEL_WK_FRI"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_WK_FRI"]);
                    item.wk_sat = dr["DEL_WK_SAT"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_WK_SAT"]);

                    // Monthly delivery (DEL_MO_01 - DEL_MO_31)
                    item.mo_01 = dr["DEL_MO_01"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_01"]);
                    item.mo_02 = dr["DEL_MO_02"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_02"]);
                    item.mo_03 = dr["DEL_MO_03"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_03"]);
                    item.mo_04 = dr["DEL_MO_04"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_04"]);
                    item.mo_05 = dr["DEL_MO_05"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_05"]);
                    item.mo_06 = dr["DEL_MO_06"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_06"]);
                    item.mo_07 = dr["DEL_MO_07"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_07"]);
                    item.mo_08 = dr["DEL_MO_08"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_08"]);
                    item.mo_09 = dr["DEL_MO_09"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_09"]);
                    item.mo_10 = dr["DEL_MO_10"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_10"]);
                    item.mo_11 = dr["DEL_MO_11"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_11"]);
                    item.mo_12 = dr["DEL_MO_12"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_12"]);
                    item.mo_13 = dr["DEL_MO_13"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_13"]);
                    item.mo_14 = dr["DEL_MO_14"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_14"]);
                    item.mo_15 = dr["DEL_MO_15"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_15"]);
                    item.mo_16 = dr["DEL_MO_16"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_16"]);
                    item.mo_17 = dr["DEL_MO_17"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_17"]);
                    item.mo_18 = dr["DEL_MO_18"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_18"]);
                    item.mo_19 = dr["DEL_MO_19"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_19"]);
                    item.mo_20 = dr["DEL_MO_20"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_20"]);
                    item.mo_21 = dr["DEL_MO_21"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_21"]);
                    item.mo_22 = dr["DEL_MO_22"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_22"]);
                    item.mo_23 = dr["DEL_MO_23"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_23"]);
                    item.mo_24 = dr["DEL_MO_24"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_24"]);
                    item.mo_25 = dr["DEL_MO_25"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_25"]);
                    item.mo_26 = dr["DEL_MO_26"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_26"]);
                    item.mo_27 = dr["DEL_MO_27"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_27"]);
                    item.mo_28 = dr["DEL_MO_28"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_28"]);
                    item.mo_29 = dr["DEL_MO_29"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_29"]);
                    item.mo_30 = dr["DEL_MO_30"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_30"]);
                    item.mo_31 = dr["DEL_MO_31"] == DBNull.Value ? false : Convert.ToBoolean(dr["DEL_MO_31"]);

                    listdelcycle.Add(item);
                }
            }

            return Ok(listdelcycle);
        }
        [HttpPost]
        [Route("EditDelCycle")]
        public IActionResult EDIT_DELIVERYCYCLE([FromBody] paramDelcycle param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                SqlCommand sqlEditpartmstr = new SqlCommand();
                sqlEditpartmstr.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_DELIVERY_CYCLE]
                      SET  [DEL_WK_SUN] = @sun
                          ,[DEL_WK_MON] = @mon
                          ,[DEL_WK_TUE] = @tue
                          ,[DEL_WK_WED] = @wed
                          ,[DEL_WK_THU] = @thu
                          ,[DEL_WK_FRI] = @fri
                          ,[DEL_WK_SAT] = @sat
                          ,[UPDATE_BY] = @by
                          ,[UPDATE_DT] = @dt
                      WHERE [VENDER] = 'SG1887'";

                sqlEditpartmstr.Parameters.AddWithValue("@sun", param.new_delsun);
                sqlEditpartmstr.Parameters.AddWithValue("@mon", param.new_delmon);
                sqlEditpartmstr.Parameters.AddWithValue("@tue", param.new_deltue);
                sqlEditpartmstr.Parameters.AddWithValue("@wed", param.new_delwed);
                sqlEditpartmstr.Parameters.AddWithValue("@thu", param.new_delthu);
                sqlEditpartmstr.Parameters.AddWithValue("@fri", param.new_delfri);
                sqlEditpartmstr.Parameters.AddWithValue("@sat", param.new_delsat);

                sqlEditpartmstr.Parameters.AddWithValue("@by", param.new_updateby);
                sqlEditpartmstr.Parameters.AddWithValue("@dt", Convert.ToDateTime(param.new_updatedt));

                dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);

                if (dbresult.status == "Success")
                {
                    string Old_Value = "sun=" + param.old_delsun + "mon=" + param.old_delmon + "tue=" + param.old_deltue + "wed=" + param.old_delwed + "thu=" + param.old_delthu + "fri=" + param.old_delfri + "sat=" + param.old_delsat;

                    string New_Value = "sun=" + param.new_delsun + "mon=" + param.new_delmon + "tue=" + param.new_deltue + "wed=" + param.new_delwed + "thu=" + param.new_delthu + "fri=" + param.new_delfri + "sat=" + param.new_delsat;


                    SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                    sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                        ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                        VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_DELIVERY CYCLE");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "EDIT");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.new_updateby);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                    conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                    return Ok(dbresult);
                }
                else
                {
                    dbresult.msg = dbresult.status;
                    dbresult.status = "Fail";
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.status = "NoData";
                dbresult.msg = "";
                return Ok(dbresult);
            }
        }

        [HttpGet]
        [Route("GetCalFTT")]
        public IActionResult GET_CALENDAR_FTT()
        {
            List<calendarmstr> listCalendar = new List<calendarmstr>();

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @"
            SELECT [Dict_Code],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Dict_Value1],[Dict_Value2],[Update_By],[Update_DT]
            FROM [dbSCM].[dbo].[DOOUT_Dict]
            WHERE [Dict_Type] = 'CALENDAR' AND [Dict_Ref1] = 'VENDER_H' AND [Dict_Ref2] = 'SG1887'
            ORDER BY [Dict_Value1] ASC";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    calendarmstr calendar = new calendarmstr();
                    calendar.id = (dr["Dict_Code"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Dict_Code"]);
                    calendar.type = (dr["Dict_Ref1"] == DBNull.Value) ? "" : dr["Dict_Ref1"].ToString();
                    calendar.code = (dr["Dict_Ref2"] == DBNull.Value) ? "" : dr["Dict_Ref2"].ToString();
                    calendar.name = (dr["Dict_Ref3"] == DBNull.Value) ? "" : dr["Dict_Ref3"].ToString();
                    calendar.dateoff = (dr["Dict_Value1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Dict_Value1"]);
                    calendar.desc = (dr["Dict_Value2"] == DBNull.Value) ? "" : Convert.ToString(dr["Dict_Value2"]);
                    calendar.update_by = (dr["Update_By"] == DBNull.Value) ? "" : dr["Update_By"].ToString();
                    calendar.update_dt = (dr["Update_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Update_DT"]);


                    listCalendar.Add(calendar);
                }
            }
            return Ok(listCalendar);
        }

        [HttpPost]
        [Route("SetCalFTT")]
        public IActionResult SET_CALENDAR_FTT([FromBody] paramCalftt param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                DateTime start = DateTime.ParseExact(param.start, "dd-MM-yyyy", CultureInfo.CurrentCulture);
                DateTime end = DateTime.ParseExact(param.end, "dd-MM-yyyy", CultureInfo.CurrentCulture);
                //int day = Convert.ToInt32(param.start.Substring(0, 2));
                //int month = Convert.ToInt32(param.start.Substring(3, 2));
                //int year = Convert.ToInt32(param.start.Substring(6,4));
                //DateTime start = new DateTime(Convert.ToInt32(param.start.Substring(6,4)), Convert.ToInt32(param.start.Substring(3, 2)), Convert.ToInt32(param.start.Substring(0,2)));
                //DateTime end = new DateTime(Convert.ToInt32(param.end.Substring(6,4)), Convert.ToInt32(param.end.Substring(3, 2)), Convert.ToInt32(param.end.Substring(0,2)));
                string condition = "";

                DateTime sDate = start;
                DateTime eDate = end;
                while (sDate.Date <= eDate.Date)
                {
                    condition += "'" + sDate.Date.ToString("yyyy-MM-dd") + "'" + ",";
                    sDate = sDate.AddDays(1);
                }
                condition = condition.TrimEnd(',');

                SqlCommand sqlCheckCalendarFTT = new SqlCommand();
                sqlCheckCalendarFTT.CommandText = $@"
                SELECT [Dict_Code]
                      ,[Dict_Type]
                      ,[Dict_Ref1]
                      ,[Dict_Value1]
     
                FROM [dbSCM].[dbo].[DOOUT_Dict]
                WHERE [Dict_Type] = 'CALENDAR' AND [Dict_Ref1] = 'VENDER_H' AND [Dict_Value1] IN ( {condition} ) AND Dict_Ref2 = 'SG1887' ";

                DataTable dtgelSK = conDBSCM.Query(sqlCheckCalendarFTT);
                if (dtgelSK.Rows.Count > 0)
                {
                    dbresult.status = "Waring";
                    dbresult.msg = "มีข้อมูลในระบบแล้วไม่สามารถดำเนิการได้";
                    return Ok(dbresult);
                }
                else
                {
                    while (start.Date <= end.Date)
                    {
                        SqlCommand sqlEditpartmstr = new SqlCommand();
                        sqlEditpartmstr.CommandText = $@"
                      INSERT INTO [dbSCM].[dbo].[DOOUT_Dict]
                      ([Dict_Type],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Dict_Ref4],[Dict_Ref5],[Dict_Value1],[Dict_Value2],[Dict_Value3],[Dict_Value4],[Dict_Value5],[Update_By],[Update_DT])
                      VALUES (@Dict_Type,@Dict_Ref1,@Dict_Ref2,@Dict_Ref3,@Dict_Ref4,@Dict_Ref5,@Dict_Value1,@Dict_Value2,@Dict_Value3,@Dict_Value4,@Dict_Value5,@Update_By,@Update_DT)";

                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Type", "CALENDAR");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref1", "VENDER_H");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref2", "SG1887");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref3", "FUJITRANS (THAILAND) CO.,LTD");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref4", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref5", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value1", start.ToString("yyyy-MM-dd"));
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value2", param.desc.ToString());
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value3", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value4", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value5", "");

                        sqlEditpartmstr.Parameters.AddWithValue("@Update_By", param.empcode);
                        sqlEditpartmstr.Parameters.AddWithValue("@Update_DT", DateTime.Now);


                        dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);
                        if (dbresult.status == "Success")
                        {
                            string Old_Value = "";

                            string New_Value = "DATEOFF=" + param.start + ",DESC=" + param.desc;


                            SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                            sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                            ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                            VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_FTT_CALENDAR");
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "ADD");
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.empcode);
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                            conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                        }
                        else
                        {
                            dbresult.msg = dbresult.status;
                            dbresult.status = "Fail";
                        }
                        start = start.AddDays(1);
                    }
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.msg = dbresult.status;
                dbresult.status = "Fail";
                return Ok(dbresult);
            }
        }

        [HttpPost]
        [Route("DelCalFTT")]
        public IActionResult DEL_CALENDAR_FTT([FromBody] paramCalftt param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                SqlCommand sqlEditpartmstr = new SqlCommand();
                sqlEditpartmstr.CommandText = $@"
                      DELETE [dbSCM].[dbo].[DOOUT_Dict]
                      WHERE [Dict_Type] = @Dict_Type AND [Dict_Ref1] = @Dict_Ref1 AND [Dict_Ref2] = @Dict_Ref2 AND [Dict_Value1] = @Dict_Value1";

                sqlEditpartmstr.Parameters.AddWithValue("@Dict_Type", "CALENDAR");
                sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref1", "VENDER_H");
                sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref2", "SG1887");
                sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value1", param.start);

                dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);
                if (dbresult.status == "Success")
                {
                    string Old_Value = "DATEOFF=" + param.start + ",DESC=" + param.desc;

                    string New_Value = "";


                    SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                    sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                            ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                            VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_FTT_CALENDAR");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "DELETE");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.empcode);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                    conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                    return Ok(dbresult);
                }
                else
                {
                    dbresult.msg = dbresult.status;
                    dbresult.status = "Fail";
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.msg = dbresult.status;
                dbresult.status = "Fail";
                return Ok(dbresult);
            }
        }


        [HttpGet]
        [Route("GetCalDCI")]
        public IActionResult GET_CALENDAR_DCI()
        {
            List<calendarmstr> listCalendar = new List<calendarmstr>();

            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @"
            SELECT [Dict_Code],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Dict_Value1],[Dict_Value2],[Update_By],[Update_DT]
            FROM [dbSCM].[dbo].[DOOUT_Dict]
            WHERE [Dict_Type] = 'CALENDAR' AND [Dict_Ref1] = 'DCI'
            ORDER BY [Dict_Value1] ASC";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    calendarmstr calendar = new calendarmstr();
                    calendar.id = (dr["Dict_Code"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Dict_Code"]);
                    calendar.type = (dr["Dict_Ref1"] == DBNull.Value) ? "" : dr["Dict_Ref1"].ToString();
                    calendar.code = (dr["Dict_Ref2"] == DBNull.Value) ? "" : dr["Dict_Ref2"].ToString();
                    calendar.name = (dr["Dict_Ref3"] == DBNull.Value) ? "" : dr["Dict_Ref3"].ToString();
                    calendar.dateoff = (dr["Dict_Value1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Dict_Value1"]);
                    calendar.desc = (dr["Dict_Value2"] == DBNull.Value) ? "" : Convert.ToString(dr["Dict_Value2"]);
                    calendar.update_by = (dr["Update_By"] == DBNull.Value) ? "" : dr["Update_By"].ToString();
                    calendar.update_dt = (dr["Update_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Update_DT"]);


                    listCalendar.Add(calendar);
                }
            }
            return Ok(listCalendar);
        }

        [HttpPost]
        [Route("SetCalDCI")]
        public IActionResult SET_CALENDAR_DCI([FromBody] paramCalftt param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                DateTime start = DateTime.ParseExact(param.start, "dd-MM-yyyy", CultureInfo.CurrentCulture);
                DateTime end = DateTime.ParseExact(param.end, "dd-MM-yyyy", CultureInfo.CurrentCulture);
                //int day = Convert.ToInt32(param.start.Substring(0, 2));
                //int month = Convert.ToInt32(param.start.Substring(3, 2));
                //int year = Convert.ToInt32(param.start.Substring(6,4));
                //DateTime start = new DateTime(Convert.ToInt32(param.start.Substring(6,4)), Convert.ToInt32(param.start.Substring(3, 2)), Convert.ToInt32(param.start.Substring(0,2)));
                //DateTime end = new DateTime(Convert.ToInt32(param.end.Substring(6,4)), Convert.ToInt32(param.end.Substring(3, 2)), Convert.ToInt32(param.end.Substring(0,2)));
                string condition = "";

                DateTime sDate = start;
                DateTime eDate = end;
                while (sDate.Date <= eDate.Date)
                {
                    condition += "'" + sDate.Date.ToString("yyyy-MM-dd") + "'" + ",";
                    sDate = sDate.AddDays(1);
                }
                condition = condition.TrimEnd(',');

                SqlCommand sqlCheckCalendarFTT = new SqlCommand();
                sqlCheckCalendarFTT.CommandText = $@"
                SELECT [Dict_Code]
                      ,[Dict_Type]
                      ,[Dict_Ref1]
                      ,[Dict_Value1]
     
                FROM [dbSCM].[dbo].[DOOUT_Dict]
                WHERE [Dict_Type] = 'CALENDAR' AND [Dict_Ref1] = 'DCI' AND [Dict_Value1] IN ( {condition} )";

                DataTable dtgelSK = conDBSCM.Query(sqlCheckCalendarFTT);
                if (dtgelSK.Rows.Count > 0)
                {
                    dbresult.status = "Waring";
                    dbresult.msg = "มีข้อมูลในระบบแล้วไม่สามารถดำเนิการได้";
                    return Ok(dbresult);
                }
                else
                {

                    while (start.Date <= end.Date)
                    {
                        SqlCommand sqlEditpartmstr = new SqlCommand();
                        sqlEditpartmstr.CommandText = $@"
                      INSERT INTO [dbSCM].[dbo].[DOOUT_Dict]
                      ([Dict_Type],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Dict_Ref4],[Dict_Ref5],[Dict_Value1],[Dict_Value2],[Dict_Value3],[Dict_Value4],[Dict_Value5],[Update_By],[Update_DT])
                      VALUES (@Dict_Type,@Dict_Ref1,@Dict_Ref2,@Dict_Ref3,@Dict_Ref4,@Dict_Ref5,@Dict_Value1,@Dict_Value2,@Dict_Value3,@Dict_Value4,@Dict_Value5,@Update_By,@Update_DT)";

                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Type", "CALENDAR");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref1", "DCI");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref2", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref3", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref4", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref5", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value1", start.ToString("yyyy-MM-dd"));
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value2", param.desc.ToString());
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value3", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value4", "");
                        sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value5", "");

                        sqlEditpartmstr.Parameters.AddWithValue("@Update_By", param.empcode);
                        sqlEditpartmstr.Parameters.AddWithValue("@Update_DT", DateTime.Now);


                        dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);
                        if (dbresult.status == "Success")
                        {
                            string Old_Value = "";

                            string New_Value = "DATEOFF=" + param.start + ",DESC=" + param.desc;


                            SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                            sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                            ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                            VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_FTT_CALENDAR");
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "ADD");
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.empcode);
                            sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                            conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                        }
                        else
                        {
                            dbresult.msg = dbresult.status;
                            dbresult.status = "Fail";
                        }
                        start = start.AddDays(1);
                    }
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.msg = dbresult.status;
                dbresult.status = "Fail";
                return Ok(dbresult);
            }
        }

        [HttpPost]
        [Route("DelCalDCI")]
        public IActionResult DEL_CALENDAR_DCI([FromBody] paramCalftt param)
        {
            dbResult dbresult = new dbResult();
            if (param != null)
            {
                SqlCommand sqlEditpartmstr = new SqlCommand();
                sqlEditpartmstr.CommandText = $@"
                      DELETE [dbSCM].[dbo].[DOOUT_Dict]
                      WHERE [Dict_Type] = @Dict_Type AND [Dict_Ref1] = @Dict_Ref1 AND [Dict_Value1] = @Dict_Value1";

                sqlEditpartmstr.Parameters.AddWithValue("@Dict_Type", "CALENDAR");
                sqlEditpartmstr.Parameters.AddWithValue("@Dict_Ref1", "DCI");
                sqlEditpartmstr.Parameters.AddWithValue("@Dict_Value1", param.start);

                dbresult.status = conDBSCM.ExecuteCommand(sqlEditpartmstr);
                if (dbresult.status == "Success")
                {
                    string Old_Value = "DATEOFF=" + param.start + ",DESC=" + param.desc;

                    string New_Value = "";


                    SqlCommand sqlInsertNewRevCalDoqtyData = new SqlCommand();
                    sqlInsertNewRevCalDoqtyData.CommandText = @"INSERT INTO [dbSCM].[dbo].[DOOUT_ACTION_LOG]
                            ([process],[action],[old_data],[new_data],[action_by],[action_dt]) 
                            VALUES (@process,@action,@old_data,@new_data,@action_by,@action_dt)";

                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@process", "SETTING_DCI_CALENDAR");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action", "DELETE");
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@old_data", Old_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@new_data", New_Value);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_by", param.empcode);
                    sqlInsertNewRevCalDoqtyData.Parameters.AddWithValue("@action_dt", DateTime.Now);

                    conDBSCM.ExecuteCommand(sqlInsertNewRevCalDoqtyData);
                    return Ok(dbresult);
                }
                else
                {
                    dbresult.msg = dbresult.status;
                    dbresult.status = "Fail";
                    return Ok(dbresult);
                }
            }
            else
            {
                dbresult.msg = dbresult.status;
                dbresult.status = "Fail";
                return Ok(dbresult);
            }
        }

        [HttpGet]
        [Route("GetRangeDate")]
        public IActionResult GET_RANGE_DATE()
        {

            List<dict> dict = new List<dict>();
            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = @"
            SELECT [Dict_Type]
                  ,[Dict_Ref1]
                  ,[Dict_Value1]
     
            FROM [dbSCM].[dbo].[DOOUT_Dict]
            WHERE [Dict_Type] = 'PG_SETTING'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    dict odict = new dict();
                    odict.Dict_Type = (dr["Dict_Type"] == DBNull.Value) ? "" : Convert.ToString(dr["Dict_Type"]);
                    odict.Dict_Ref1 = (dr["Dict_Ref1"] == DBNull.Value) ? "" : Convert.ToString(dr["Dict_Ref1"]);
                    odict.Dict_Value1 = (dr["Dict_Value1"] == DBNull.Value) ? "" : Convert.ToString(dr["Dict_Value1"]);

                    dict.Add(odict);
                }
            }

            rangecal caldate = new rangecal();

            caldate.preday = dict.Where(s => s.Dict_Ref1 == "PRE_PLAN").Select(s => Convert.ToInt32(s.Dict_Value1)).FirstOrDefault();
            caldate.fcday = dict.Where(s => s.Dict_Ref1 == "FORCAST_DAY").Select(s => Convert.ToInt32(s.Dict_Value1)).FirstOrDefault();

            return Ok(caldate);
        }

        [HttpGet]
        [Route("GetSharevd")]
        public IActionResult GET_SHARE_VENDER()
        {
            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);
            List<dict> listsharevd = new List<dict>();
            SqlCommand sqlSelectFindSetcontrol = new SqlCommand();
            sqlSelectFindSetcontrol.CommandText = @"
            SELECT [Dict_Code],[Dict_Type],[Dict_Ref1],[Dict_Ref2],[Dict_Ref3],[Status],[Update_By],[Update_DT]
            FROM [dbSCM].[dbo].[DOOUT_Dict]
            WHERE [Dict_Type] = 'SHARE_VENDER' and [Status] = 'ACTIVE'";

            DataTable dtgelSK = conDBSCM.Query(sqlSelectFindSetcontrol);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    dict item = new dict();
                    item.Dict_Code = (dr["Dict_Code"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Dict_Code"]);
                    item.Dict_Type = (dr["Dict_Type"] == DBNull.Value) ? "" : (dr["Dict_Type"]).ToString().Trim();
                    item.Dict_Ref1 = (dr["Dict_Ref1"] == DBNull.Value) ? "" : (dr["Dict_Ref1"]).ToString().Trim();
                    item.Dict_Ref2 = (dr["Dict_Ref2"] == DBNull.Value) ? "" : (dr["Dict_Ref2"]).ToString().Trim();
                    item.Dict_Ref3 = (dr["Dict_Ref3"] == DBNull.Value) ? "" : (dr["Dict_Ref3"]).ToString().Trim();
                    item.Status = (dr["Status"] == DBNull.Value) ? "" : (dr["Status"]).ToString().Trim();
                    item.Update_By = (dr["Update_By"] == DBNull.Value) ? "" : (dr["Update_By"]).ToString().Trim();
                    item.Update_DT = (dr["Update_DT"] == DBNull.Value) ? dt : Convert.ToDateTime(dr["Update_DT"]);

                    listsharevd.Add(item);
                }
            }
            return Ok(listsharevd);
        }
    }
}
