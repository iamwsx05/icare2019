using System;
using System.Data;
using System.Data.OleDb;
using weCare.Core.Utils;

namespace Com.svc
{
    public class ODBCHelper
    {
        public ODBCHelper(string filePath)
        {
            conString += filePath;
        }
        string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="; //连接Access数据
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = null;
            try
            {

                OleDbConnection con = new OleDbConnection(conString);
               
                con.Open();
                OleDbCommand com = new OleDbCommand(sql, con);
                OleDbDataAdapter da = new OleDbDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);

                dt.TableName = "test";
            }
            catch(Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            return dt;
        }

        public int ExecSql(string sql)
        {
            int affectedRows  = -1;
            try
            {
                OleDbConnection con = new OleDbConnection(conString);
                con.Open();
                OleDbCommand com = new OleDbCommand(sql, con);
                affectedRows = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            return affectedRows;
        }
    }
}
