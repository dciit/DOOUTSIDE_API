using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API.Model.ModelMaster;
using System.Data.SqlClient;
using System.Data;
using API.Service;
using static API.Model.ModelTransaction;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Transaction : ControllerBase
    {
        SqlConnectDB conDBSCM = new("DBSCM");

        private List<SETCONTROL_T> GetSetControlData()
        {
            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            List<SETCONTROL_T> setcontrol = new List<SETCONTROL_T>();
            SqlCommand sqlselectRangeData = new SqlCommand();
            sqlselectRangeData.CommandText = @"
                SELECT [SET_CODE],[SET_DATE],[SET_BY],[SET_STDATE],[SET_ENDATE],
                       [PROCESS_BEGIN],[PROCESS_FINISH],[STATUS_DISTRIBUTE],[DISTRIBUTE_DT]
                FROM [dbSCM].[dbo].[DOOUT_0_SETCONTROL]
                WHERE [SET_DATE] BETWEEN @start AND @end
                ORDER BY [SET_DATE] DESC";

            sqlselectRangeData.Parameters.AddWithValue("@start", DateTime.Now.AddDays(-10).Date);
            sqlselectRangeData.Parameters.AddWithValue("@end", DateTime.Now.AddDays(1).Date);

            DataTable dtsc = conDBSCM.Query(sqlselectRangeData);
            if (dtsc.Rows.Count > 0)
            {
                foreach (DataRow dr in dtsc.Rows)
                {
                    SETCONTROL_T item = new SETCONTROL_T
                    {
                        set_code = dr["SET_CODE"]?.ToString() ?? "",
                        set_date = dr["SET_DATE"] == DBNull.Value ? dt : Convert.ToDateTime(dr["SET_DATE"]),
                        set_by = dr["SET_BY"]?.ToString() ?? "",
                        set_st_dt = dr["SET_STDATE"] == DBNull.Value ? dt : Convert.ToDateTime(dr["SET_STDATE"]),
                        set_en_dt = dr["SET_ENDATE"] == DBNull.Value ? dt : Convert.ToDateTime(dr["SET_ENDATE"]),
                        process_begin = dr["PROCESS_BEGIN"] == DBNull.Value ? dt : Convert.ToDateTime(dr["PROCESS_BEGIN"]),
                        process_finish = dr["PROCESS_FINISH"] == DBNull.Value ? dt : Convert.ToDateTime(dr["PROCESS_FINISH"]),
                        status_distribute = dr["STATUS_DISTRIBUTE"]?.ToString() ?? "",
                        distribute_dt = dr["DISTRIBUTE_DT"] == DBNull.Value ? dt : Convert.ToDateTime(dr["DISTRIBUTE_DT"])
                    };
                    setcontrol.Add(item);
                }
            }
            return setcontrol;
        }

        [HttpGet]
        [Route("GetSetcontrol")]
        public IActionResult GETSETCONTROL()
        {
            List<SETCONTROL_T> setcontrol = GetSetControlData();
            return Ok(setcontrol);
        }

        [HttpGet]
        [Route("Getwhmstr")]
        public IActionResult GET_WHMSTR()
        {
            List<SETCONTROL_T> setcontrol = GetSetControlData();
            string setcode = "";
            foreach (var scontrol in setcontrol)
            {
                setcode += "'" + scontrol.set_code + "'" + ",";
            }
            setcode = setcode.TrimEnd(',');

            string str_dt = "1901-01-01";
            DateTime dt = Convert.ToDateTime(str_dt);

            List<WHOUTSIDE_T> listwhmstr = new List<WHOUTSIDE_T>();
            SqlCommand sqlSelectPartmstr = new SqlCommand();
            sqlSelectPartmstr.CommandText = $@"
            SELECT [SET_CODE],[WH_OUT_CODE],[WH_OUT_NAME],[LOCATION],[PRIORITY],[RATIO],[FIXED_DAYS],[STATUS],[DATA_DT]
            FROM [dbSCM].[dbo].[DOOUT_1_3WH_OUTSITE_MSTR]
            WHERE [SET_CODE] IN ({setcode})
            ORDER BY [SET_CODE] DESC";


            DataTable dtgelSK = conDBSCM.Query(sqlSelectPartmstr);
            if (dtgelSK.Rows.Count > 0)
            {
                foreach (DataRow dr in dtgelSK.Rows)
                {
                    WHOUTSIDE_T whmstr = new WHOUTSIDE_T();
                    whmstr.set_code = (dr["SET_CODE"] == DBNull.Value) ? "" : Convert.ToString(dr["SET_CODE"]);
                    whmstr.wh_code = (dr["WH_OUT_CODE"] == DBNull.Value) ? "" : Convert.ToString(dr["WH_OUT_CODE"]);
                    whmstr.wh_name = (dr["WH_OUT_NAME"] == DBNull.Value) ? "" : Convert.ToString(dr["WH_OUT_NAME"]);
                    whmstr.location = (dr["LOCATION"] == DBNull.Value) ? "" : Convert.ToString(dr["LOCATION"]);
                    whmstr.priority = (dr["PRIORITY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PRIORITY"]);
                    whmstr.ratio = (dr["RATIO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RATIO"]);
                    whmstr.fixed_days = (dr["FIXED_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["FIXED_DAYS"]);
                    whmstr.status = (dr["STATUS"] == DBNull.Value) ? "" : Convert.ToString(dr["STATUS"]);
                    whmstr.data_dt = (dr["DATA_DT"] == DBNull.Value) ? dt : Convert.ToDateTime(dr["DATA_DT"]);


                    listwhmstr.Add(whmstr);
                }
            }
            return Ok(listwhmstr);
        }


    }
}
