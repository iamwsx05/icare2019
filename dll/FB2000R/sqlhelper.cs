using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using weCare.Core.Utils;

namespace FB2000R
{
    public class SqlHelper
    {
        public SqlHelper(string _conString)
        {
            conString = _conString;
        }
        string conString = string.Empty;
        #region
        public DataTable GetDataTable(string sql)
        {
            DataTable dtRecord = null;
            // 连接参数
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 3000;
            try
            {
                cmd.CommandText = sql;
                SqlDataReader sqlReader = cmd.ExecuteReader();
                dtRecord = new DataTable();
                dtRecord.Load(sqlReader);
                sqlReader.Close();
            }
            catch (System.Exception objEx)
            {
                dtRecord = null;
                throw objEx;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }

            return dtRecord;
        }
        #endregion

        public int ExecSql(string sql)
        {
            int affectedRows = -1;
            try
            {
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 3000;
                try
                {
                    cmd.CommandText = sql;
                    affectedRows = cmd.ExecuteNonQuery();
                }
                catch (System.Exception objEx)
                {
                    throw objEx;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            return affectedRows;
        }
    }
}
