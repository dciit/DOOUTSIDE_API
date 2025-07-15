using System.Data.SqlClient;
using System.Data;

namespace API.Service
{
    public class SqlConnectDB
    {
        string connStr = "";
        public SqlConnectDB(string server)
        {
            //
            // TODO: Add constructor logic here
            //

            if (server == "DBSCM")
            {
                connStr = "Data Source=192.168.226.86;Initial Catalog=dbSCM; Persist Security Info=True; TrustServerCertificate=True; User ID=sa;Password=decjapan;";
            }
            else if (server == "dbDCI")
            {
                connStr = "Data Source=COSTY;Initial Catalog=dbDCI; Persist Security Info=True; TrustServerCertificate=True; User ID=sa;Password=decjapan;";
            }

            else if (server == "DBSCM_DATPID")
            {
                connStr = "Data Source=192.168.226.86;Initial Catalog=dbSCM; Persist Security Info=True; User ID=sa;Password=decjapan;";
            }
            else if (server == "DBBCS")
            {
                connStr = "Data Source=192.168.226.86;Initial Catalog=dbBCS; Persist Security Info=True; User ID=sa;Password=decjapan;";

            }
            else if (server == "DBHRM")
            {
                connStr = "Data Source=192.168.226.86;Initial Catalog=dbHRM; Persist Security Info=True; TrustServerCertificate=True; User ID=sa;Password=decjapan;";

            }
            else if (server == "dbDCICosty")
            {
                connStr = "Data Source=192.168.226.145;Initial Catalog=dbDCI; Persist Security Info=True; User ID=sa;Password=decjapan; TrustServerCertificate=True;";
            }
            else if (server == "TEST")
            {
                connStr = "Data Source=IT-NB_009\\SQLEXPRESS;Initial Catalog=dbDCI_Test; Persist Security Info=True; User ID=sa;Password=decjapan;";
            }
        }

        private bool useDB = true;


        //Property ObjectManages As Object

        /// <summary>
        /// Query table by string and return table 
        /// </summary>
        /// <param name="commandDb">CommandDB for query</param>
        /// <returns>DataTable</returns>
        /// <remarks></remarks>
        public DataTable Query(SqlCommand commandDb)
        {
            if (useDB)
            {
                SqlConnection conn = new SqlConnection(connStr);
                commandDb.Connection = conn;
                SqlDataAdapter adapter = new SqlDataAdapter(commandDb);
                DataTable dTable = new DataTable();
                DataSet dSet = new DataSet();

                try
                {
                    adapter.Fill(dSet, "dataTable");
                    return dSet.Tables["dataTable"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return dTable;
                }
                finally { conn.Close(); }

            }
            else
            {
                return new DataTable();
            }

            //=================================================================================
        }


        /// <summary>
        /// ExecuteCommand
        /// </summary>
        /// <param name="commandDb">commanddb for execute</param>
        /// <remarks></remarks>
        public string ExecuteCommand(SqlCommand commandDb)
        {
            if (useDB)
            {
                SqlConnection conn = new SqlConnection(connStr);
                try
                {
                    commandDb.Connection = conn;
                    conn.Open();
                    commandDb.ExecuteNonQuery();
                    conn.Close();
                    return "Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "Error" + ex.ToString();
                }
                finally { conn.Close(); }
            }
            else
            {
                return "Done";
            }
        }
    }
}
