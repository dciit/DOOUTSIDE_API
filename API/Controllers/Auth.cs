using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using static API.Model.ModelAuth;
using static API.Model.ModelMaster;
using System.Text;
using System.Security.Cryptography;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        SqlConnectDB conDBSCM = new("DBSCM");

        [HttpPost]
        [Route("chk_login")]
        public IActionResult CHK_Login([FromBody] paramLogin param)
        {
            if (param != null)
            {
                string str_dt = "1901-01-01";
                DateTime dt = Convert.ToDateTime(str_dt);

                vduser item = new vduser();
                SqlCommand sqlChecklogin = new SqlCommand();
                sqlChecklogin.CommandText = @"
                SELECT [code]
                      ,[name]
                      ,[fullname]
                      ,[pren]
                      ,[surn]
                  FROM [dbSCM].[dbo].[DOOUT_SUP_USER]
                  WHERE [username] = @username AND [password] = @password";

                sqlChecklogin.Parameters.AddWithValue("@username", param.username.Trim());
                sqlChecklogin.Parameters.AddWithValue("@password", param.password.Trim());

                DataTable dtgelSK = conDBSCM.Query(sqlChecklogin);
                if (dtgelSK.Rows.Count > 0)
                {
                    item.code = (dtgelSK.Rows[0]["code"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["code"].ToString();
                    item.name = (dtgelSK.Rows[0]["name"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["name"].ToString();
                    item.fullname = (dtgelSK.Rows[0]["fullname"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["fullname"].ToString();
                    item.pren = (dtgelSK.Rows[0]["pren"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["pren"].ToString();
                    item.surn = (dtgelSK.Rows[0]["surn"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["surn"].ToString();

                    SqlCommand sqlLoginDate = new SqlCommand();
                    sqlLoginDate.CommandText = $@"
                      UPDATE [dbSCM].[dbo].[DOOUT_SUP_USER]
                      SET  [Last_Login] = @lastlogin
                      WHERE [username] = @username";

                    sqlLoginDate.Parameters.AddWithValue("@lastlogin", DateTime.Now);

                    sqlLoginDate.Parameters.AddWithValue("@username", param.username);

                    conDBSCM.ExecuteCommand(sqlLoginDate);

                }
                return Ok(item);
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost]
        [Route("chk_login_emp")]
        public IActionResult CHK_Emp_Login([FromBody] paramLogin param)
        {
            if (param != null)
            {
                string str_dt = "1901-01-01";
                DateTime dt = Convert.ToDateTime(str_dt);

                vduser item = new vduser();
                SqlCommand sqlChecklogin = new SqlCommand();
                sqlChecklogin.CommandText = @"
                SELECT [CODE]
                      ,[PREN]
                      ,[NAME]
                      ,[SURN]     
                      ,SUBSTRING(MAIL, 1, CHARINDEX('@', MAIL) - 1) AS username
                  FROM [dbHRM].[dbo].[Employee]
                  WHERE [username] = @username AND [password] = @password";

                sqlChecklogin.Parameters.AddWithValue("@username", param.username.Trim());
                sqlChecklogin.Parameters.AddWithValue("@password", param.password.Trim());

                DataTable dtgelSK = conDBSCM.Query(sqlChecklogin);
                if (dtgelSK.Rows.Count > 0)
                {
                    item.code = (dtgelSK.Rows[0]["code"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["code"].ToString();
                    item.name = (dtgelSK.Rows[0]["name"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["name"].ToString();
                    item.fullname = (dtgelSK.Rows[0]["fullname"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["fullname"].ToString();
                    item.pren = (dtgelSK.Rows[0]["pren"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["pren"].ToString();
                    item.surn = (dtgelSK.Rows[0]["surn"] == DBNull.Value) ? "" : dtgelSK.Rows[0]["surn"].ToString();

                }
                return Ok(item);
            }
            else
            {
                return Ok();
            }
        }
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // แปลง string เป็น byte array แล้ว hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // แปลง byte array เป็น hex string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }
}
