using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.middletier.DataExchangeSystem_Svc
{
    #region Database

    /// <summary>
    /// 类，用于数据访问的类。
    /// </summary>
    public class clsDatabaseSVC : IDisposable
    {
        /// <summary>
        /// 保护变量，数据库连接。
        /// </summary>
        protected SqlConnection Connection;

        /// <summary>
        /// 保护变量，数据库连接串。
        /// </summary>
        protected String ConnectionString;

        #region API导入
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string p_strSection, string p_strKey, string p_strValue, string p_strFilePath);
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string p_strSection, string p_strKey, string p_strDef, StringBuilder p_sbValue, int p_intSize, string p_strFilePath);
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDatabaseSVC()
        {

            try
            {
                string strConn = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
                string m_strIniFilePath = Application.StartupPath + "\\DataExchangeSetting.ini";
                if (System.IO.File.Exists(m_strIniFilePath))
                {
                    StringBuilder sb1 = new StringBuilder(128);
                    StringBuilder sb2 = new StringBuilder(128);
                    StringBuilder sb3 = new StringBuilder(128);
                    StringBuilder sb4 = new StringBuilder(128);
                    GetPrivateProfileString("DSN", "dbserver", "", sb1, 128, m_strIniFilePath);
                    GetPrivateProfileString("DSN", "dbname", "", sb2, 128, m_strIniFilePath);
                    GetPrivateProfileString("DSN", "loginid", "sa", sb3, 128, m_strIniFilePath);
                    GetPrivateProfileString("DSN", "password", "icare", sb4, 128, m_strIniFilePath);
                    strConn = string.Format(strConn, sb1.ToString().Trim(), sb2.ToString().Trim(), sb3.ToString().Trim(), sb4.ToString().Trim());
                }
                this.ConnectionString = strConn;
            }
            catch (Exception e)
            {
                bool flag = new clsLogText().LogError("数据库连接配置错误：" + e.Message);
            }
        }

        /// <summary>
        /// 析构函数，释放非托管资源
        /// </summary>
        ~clsDatabaseSVC()
        {
            try
            {
                if (Connection != null)

                    Connection.Close();


            }
            catch { }
            try
            {
                Dispose();
            }
            catch { }
        }

        /// <summary>
        /// 保护方法，打开数据库连接。
        /// </summary>
        public void Open()
        {
            if (Connection == null)
            {
                try
                {
                    Connection = new SqlConnection(ConnectionString);
                }
                catch (Exception e)
                {
                    bool flag = new clsLogText().LogError(e.Message);
                }
            }
            if (Connection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    Connection.Open();
                }
                catch (Exception e)
                {
                    bool flag = new clsLogText().LogError(e.Message);
                }
            }
        }

        /// <summary>
        /// 公有方法，关闭数据库连接。
        /// </summary>
        public void Close()
        {
            try
            {
                if (Connection != null)
                    Connection.Close();
            }
            catch (Exception e)
            {
                bool flag = new clsLogText().LogError(e.Message);
            }
        }

        /// <summary>
        /// 公有方法，释放资源。
        /// </summary>
        public void Dispose()
        {
            // 确保连接被关闭
            try
            {
                if (Connection != null)
                {
                    Connection.Dispose();
                    Connection = null;
                }
            }
            catch { }
        }

        /// <summary>
        /// 公有方法，获取数据，返回一个SqlDataReader （调用后主意调用SqlDataReader.Close()）。
        /// </summary>
        /// <param name="SqlString">Sql语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader GetDataReader(String SqlString)
        {
            Open();
            try
            {
                SqlCommand cmd = new SqlCommand(SqlString, Connection);
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                bool flag = new clsLogText().LogError("数据查询（GetDataReader）失败，SqlString=" + SqlString + ",系统异常信息：" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 公有方法，获取数据，返回一个DataSet。
        /// </summary>
        /// <param name="SqlString">Sql语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(String SqlString)
        {
            DataSet dataset = new DataSet();
            Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(SqlString, Connection);
                adapter.Fill(dataset);
            }
            catch (Exception e)
            {
                bool flag = new clsLogText().LogError("数据查询（GetDataSet）失败，SqlString=" + SqlString + ",系统异常信息：" + e.Message);
            }
            return dataset;
        }

        /// <summary>
        /// 公有方法，获取数据，返回一个DataTable。
        /// </summary>
        /// <param name="SqlString">Sql语句</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(String SqlString)
        {
            DataSet dataset = GetDataSet(SqlString);
            dataset.CaseSensitive = false;
            return dataset.Tables[0];
        }

        /// <summary>
        /// 公有方法，获取数据，返回一个DataRow。
        /// </summary>
        /// <param name="SqlString">Sql语句</param>
        /// <returns>DataRow</returns>
        public DataRow GetDataRow(String SqlString)
        {
            DataSet dataset = GetDataSet(SqlString);
            dataset.CaseSensitive = false;
            if (dataset.Tables[0].Rows.Count > 0)
            {
                return dataset.Tables[0].Rows[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 公有方法，执行Sql语句。
        /// </summary>
        /// <param name="SqlString">Sql语句</param>
        public int ExecuteScalar(string sql)
        {
            int count;
            Open();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, Connection);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
            catch (Exception e)
            {
                bool flag = new clsLogText().LogError("数据查询（GetDataReader）失败，SqlString=" + sql + ",系统异常信息：" + e.Message);
                return 0;
            }
        }

        /// <summary>
        /// 公有方法，执行Sql语句。
        /// </summary>
        /// <param name="SqlString">Sql语句</param>
        /// <returns>对Update、Insert、Delete为影响到的行数，其他情况为-1</returns>
        public long ExecuteSQL(String SqlString, SqlParameter[] objParmArr)
        {
            long count = -1;
            Open();
            try
            {
                SqlCommand cmd = new SqlCommand(SqlString, Connection);
                cmd.CommandTimeout = 30000000;
                cmd.Parameters.AddRange(objParmArr);
                string strSQLLOG = SqlString + "\n \n [" + DateTime.Now.ToShortDateString() + "] Values:";
                for (int i = 0; i < objParmArr.Length; i++)
                {
                    strSQLLOG += i.ToString() + ": =" + objParmArr[i].Value.ToString() + " ; ";
                }
                Log2File(m_strGetFilePath + "\\" + m_strGetFileName(), false, strSQLLOG, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
                count = 1;
            }
            catch (Exception e)
            {
                count = -1;
                string s = string.Empty;
                for (int i = 0; i < objParmArr.Length; i++)
                {
                    s += i.ToString() + ": =" + objParmArr[i].Value.ToString() + " ; ";
                }
                bool flag = new clsLogText().LogError("数据更新失败，SqlString=" + SqlString + "\n Values:" + s + ",系统异常信息：" + e.Message);
            }
            return count;
        }

        #region log相关函数

        /// <summary>
        /// 写log
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="blnAllWaysNew"></param>
        /// <param name="strText"></param>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public bool Log2File(string strFileName, bool blnAllWaysNew, string strText, string strTime)
        {
            StreamWriter writer = null;
            try
            {
                FileInfo info = new FileInfo(strFileName);
                if (info.Exists)
                {
                    if (info.Length >= 2000000)
                    {
                        writer = info.CreateText();
                    }
                    else if (blnAllWaysNew)
                    {
                        writer = info.CreateText();
                    }
                    else
                    {
                        writer = info.AppendText();
                    }
                }
                else
                {
                    if (!Directory.Exists(info.DirectoryName))
                    {
                        Directory.CreateDirectory(info.DirectoryName);
                    }
                    writer = info.CreateText();
                }
                writer.WriteLine("[" + strTime + "] " + strText);
                writer.WriteLine();
                info = null;
            }
            catch (IOException exception)
            {
                this.logthis(exception);
                return false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    writer = null;
                }
            }
            return true;
        }

        private void logthis(IOException e)
        {
            if (!Directory.Exists(this.m_strGetFilePath))
            {
                Directory.CreateDirectory(this.m_strGetFilePath);
            }
            StreamWriter writer = File.CreateText(this.m_strGetFilePath + @"\logfile.txt");
            writer.WriteLine(e.Message);
            writer.Close();
            writer.Dispose();
            writer = null;
        }

        /// <summary>
        /// 获取目录
        /// </summary>
        public string m_strGetFilePath
        {
            get
            {
                string s = Application.StartupPath.ToString() + "\\code";
                if (Directory.Exists(@s))
                {
                    return @s;
                }
                return (@"D:\code");
            }
        }

        public string m_strGetFileName()
        {
            string filename = string.Empty;
            try
            {
                string m_strIniFilePath = Application.StartupPath + "\\DataExchangeSetting.ini";
                if (System.IO.File.Exists(m_strIniFilePath))
                {
                    StringBuilder sb1 = new StringBuilder(128);

                    GetPrivateProfileString("LogInfo", "FileName", "log.txt", sb1, 128, m_strIniFilePath);

                    filename = sb1.ToString().Trim();
                }

            }
            catch (Exception e)
            {
                bool flag = new clsLogText().LogError("数据库连接配置错误：" + e.Message);
            }
            return filename;
        }
        #endregion
    }
    #endregion Database

    #region GetSafeData
    /// <summary>
    /// 从数据库中安全获取数据，即当数据库中的数据为NULL时，保证读取不发生异常。
    /// </summary>
    public class GetSafeData
    {
        #region DataRow

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为字符串类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.String.Empty</returns>
        public static string ValidateDataRow_S(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return row[colname].ToString();
            else
                return System.String.Empty;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为整数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static int ValidateDataRow_N(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return Convert.ToInt32(row[colname]);
            else
                return System.Int32.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为浮点数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Double.MinValue</returns>
        public static double ValidateDataRow_F(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return Convert.ToDouble(row[colname]);
            else
                return System.Double.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为时间类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.DateTime.MinValue;</returns>
        public static DateTime ValidateDataRow_T(DataRow row, string colname)
        {
            if (row[colname] != DBNull.Value)
                return Convert.ToDateTime(row[colname]);
            else
                return System.DateTime.MinValue;
        }
        #endregion DataRow

        #region DataReader

        /// <summary>
        /// 从SqlDataReader中安全获取数据
        /// </summary>
        /// <param name="reader">数据读取器SqlDataReader</param>
        /// <param name="colname">列名</param>
        /// <returns>列中的字符串数据，如果为空，则返回System.String.Empty</returns>
        public static string ValidateDataReader_S(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetString(reader.GetOrdinal(colname));
            else
                return System.String.Empty;
        }

        public static int ValidateDataReader_N(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetInt32(reader.GetOrdinal(colname));
            else
                return System.Int32.MinValue;
        }

        public static double ValidateDataReader_F(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetDouble(reader.GetOrdinal(colname));
            else
                return System.Double.MinValue;
        }

        public static DateTime ValidateDataReader_T(SqlDataReader reader, string colname)
        {
            if (reader.GetValue(reader.GetOrdinal(colname)) != DBNull.Value)
                return reader.GetDateTime(reader.GetOrdinal(colname));
            else
                return System.DateTime.MinValue;
        }

        #endregion DataReader
    }
    #endregion GetSafeData

    #region SqlStringConstructor
    /// <summary>
    /// SQLString 的摘要说明。
    /// </summary>
    public class SqlStringConstructor
    {
        /// <summary>
        /// 公有静态方法，将文本转换成适合在Sql语句里使用的字符串。
        /// </summary>
        /// <returns>转换后文本</returns>	
        public static String GetQuotedString(String pStr)
        {
            pStr = pStr.Trim();
            return ("'" + pStr.Replace("'", "''") + "'");
        }
    }

    #endregion SqlStringConstructor
}